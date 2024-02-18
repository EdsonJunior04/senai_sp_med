using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using senai.sp_med_group.webApi.Domains;

#nullable disable

namespace senai.sp_med_group.webApi.Context
{
    public partial class SpMedContext : DbContext
    {
        public SpMedContext()
        {
        }

        public SpMedContext(DbContextOptions<SpMedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<Especializacao> Especializacaos { get; set; }
        public virtual DbSet<Imagemusuario> Imagemusuarios { get; set; }
        public virtual DbSet<Instituicao> Instituicaos { get; set; }
        public virtual DbSet<Medico> Medicos { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<Situacao> Situacaos { get; set; }
        public virtual DbSet<Tipousuario> Tipousuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //Senai
                //optionsBuilder.UseSqlServer("Data Source=NOTE0113E4\\SQLEXPRESS; Initial Catalog=SP_MED_GROUP; user id=sa; pwd=Senai@132;");

                //Casa
               optionsBuilder.UseSqlServer("Data Source=DESKTOP-R5M5A4B\\SQLEXPRESS; Initial Catalog=SP_MED_GROUP; user id=sa; pwd=Senai@134;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(e => e.IdConsulta)
                    .HasName("PK__CONSULTA__CA9C61F52D0FF1A0");

                entity.ToTable("CONSULTA");

                entity.Property(e => e.IdConsulta).HasColumnName("idConsulta");

                entity.Property(e => e.DataConsulta)
                    .HasColumnType("datetime")
                    .HasColumnName("dataConsulta");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.IdMedico).HasColumnName("idMedico");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.IdSituacao)
                    .HasColumnName("idSituacao")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdMedico)
                    .HasConstraintName("FK__CONSULTA__idMedi__59FA5E80");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("FK__CONSULTA__idPaci__5CD6CB2B");

                entity.HasOne(d => d.IdSituacaoNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdSituacao)
                    .HasConstraintName("FK__CONSULTA__idSitu__5AEE82B9");
            });

            modelBuilder.Entity<Especializacao>(entity =>
            {
                entity.HasKey(e => e.IdEspecializacao)
                    .HasName("PK__ESPECIAL__FC35476C224B94A5");

                entity.ToTable("ESPECIALIZACAO");

                entity.HasIndex(e => e.TituloEspecializacao, "UQ__ESPECIAL__BC93263E9A524D56")
                    .IsUnique();

                entity.Property(e => e.IdEspecializacao).HasColumnName("idEspecializacao");

                entity.Property(e => e.TituloEspecializacao)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("tituloEspecializacao");
            });

            modelBuilder.Entity<Imagemusuario>(entity =>
            {
                entity.ToTable("IMAGEMUSUARIO");

                entity.HasIndex(e => e.IdUsuario, "UQ__IMAGEMUS__645723A7CF1DA4B1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Binario)
                    .IsRequired()
                    .HasColumnName("binario");

                entity.Property(e => e.DataInclusao)
                    .HasColumnType("datetime")
                    .HasColumnName("data_inclusao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("mimeType");

                entity.Property(e => e.NomeArquivo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nomeArquivo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithOne(p => p.Imagemusuario)
                    .HasForeignKey<Imagemusuario>(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IMAGEMUSU__idUsu__70DDC3D8");
            });

            modelBuilder.Entity<Instituicao>(entity =>
            {
                entity.HasKey(e => e.IdInstituicao)
                    .HasName("PK__INSTITUI__8EA7AB002D74133D");

                entity.ToTable("INSTITUICAO");

                entity.HasIndex(e => e.Endereco, "UQ__INSTITUI__9456D4062153BC58")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial, "UQ__INSTITUI__9BF93A304B233B99")
                    .IsUnique();

                entity.HasIndex(e => e.Cnpj, "UQ__INSTITUI__AA57D6B45A72A220")
                    .IsUnique();

                entity.HasIndex(e => e.NomeFantasia, "UQ__INSTITUI__E7ADFC7046F080BC")
                    .IsUnique();

                entity.Property(e => e.IdInstituicao).HasColumnName("idInstituicao");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("CNPJ");

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("endereco");

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomeFantasia");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("razaoSocial");
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.IdMedico)
                    .HasName("PK__MEDICO__4E03DEBA9A3040B2");

                entity.ToTable("MEDICO");

                entity.HasIndex(e => e.Crm, "UQ__MEDICO__C1F887FFA9AF17EF")
                    .IsUnique();

                entity.Property(e => e.IdMedico).HasColumnName("idMedico");

                entity.Property(e => e.Crm)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CRM");

                entity.Property(e => e.IdEspecializacao).HasColumnName("idEspecializacao");

                entity.Property(e => e.IdInstituicao).HasColumnName("idInstituicao");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdEspecializacaoNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdEspecializacao)
                    .HasConstraintName("FK__MEDICO__idEspeci__49C3F6B7");

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdInstituicao)
                    .HasConstraintName("FK__MEDICO__idInstit__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__MEDICO__idUsuari__4BAC3F29");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("PK__PACIENTE__F48A08F2FC1A1D5D");

                entity.ToTable("PACIENTE");

                entity.HasIndex(e => e.Rg, "UQ__PACIENTE__321537C8A8E08414")
                    .IsUnique();

                entity.HasIndex(e => e.Endereco, "UQ__PACIENTE__9456D40648DCDE4F")
                    .IsUnique();

                entity.HasIndex(e => e.Cpf, "UQ__PACIENTE__C1F897312467ED6C")
                    .IsUnique();

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CPF")
                    .IsFixedLength(true);

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date")
                    .HasColumnName("dataNascimento");

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("endereco");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("RG")
                    .IsFixedLength(true);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("telefone");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__PACIENTE__idUsua__5165187F");
            });

            modelBuilder.Entity<Situacao>(entity =>
            {
                entity.HasKey(e => e.IdSituacao)
                    .HasName("PK__SITUACAO__12AFD1970BF4B62A");

                entity.ToTable("SITUACAO");

                entity.HasIndex(e => e.Descricao, "UQ__SITUACAO__91D38C2864DF004A")
                    .IsUnique();

                entity.Property(e => e.IdSituacao)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idSituacao");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("descricao");
            });

            modelBuilder.Entity<Tipousuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TIPOUSUA__03006BFF90B5831A");

                entity.ToTable("TIPOUSUARIO");

                entity.HasIndex(e => e.Tipo, "UQ__TIPOUSUA__E7F956495DA8A8B0")
                    .IsUnique();

                entity.Property(e => e.IdTipoUsuario)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idTipoUsuario");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__645723A6E151BCD9");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdTipoUsuario).HasColumnName("idTipoUsuario");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__USUARIO__idTipoU__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
