USE SP_MED_GROUP;

--listar tudo de cada um
SELECT * FROM PACIENTE;
SELECT * FROM USUARIO;
SELECT * FROM CONSULTA;

SELECT * FROM MEDICO;
SELECT * FROM SITUACAO;
SELECT * FROM TIPOUSUARIO;
SELECT * FROM INSTITUICAO;
SELECT * FROM ESPECIALIZACAO;
SELECT * FROM IMAGEMUSUARIO;

--listar todos dados relacionados à consulta
SELECT UP.nome Paciente, 
	   E.tituloEspecializacao Especialização,
	   CONVERT(VARCHAR(25),C.dataConsulta,103) [Data da Consulta],
	   S.descricao Situação,
	   C.descricao 
  FROM CONSULTA C
 INNER JOIN SITUACAO S
    ON C.idSituacao = S.idSituacao
 INNER JOIN PACIENTE P
    ON C.idPaciente = P.idPaciente
 INNER JOIN MEDICO M
    ON C.idMedico = M.idMedico 
 INNER JOIN ESPECIALIZACAO E
    ON M.idEspecializacao = E.idEspecializacao
 INNER JOIN USUARIO UP
    ON P.idUsuario = UP.idUsuario
 INNER JOIN USUARIO UM
    ON M.idUsuario = UM.idUsuario
GO

--Criou uma função para retornar a quantidade de médicos de uma determinada especialidade
 CREATE FUNCTION MED_ESPECIALIZACAO(@tituloEspec VARCHAR(90))
RETURNS TABLE
     AS
 RETURN (
          SELECT @tituloEspec AS especializacao, COUNT(idEspecializacao) [Número de Médicos]
		    FROM ESPECIALIZACAO
		   WHERE tituloEspecializacao LIKE '%' + @tituloEspec + '%'
        )
GO
--DROP FUNCTION MED_ESPECIALIZACAO
SELECT * FROM MED_ESPECIALIZACAO('Psiquiatria')
GO

--Criou uma função para que retorne a idade do usuário a partir de uma determinada stored procedure
CREATE PROCEDURE  IdadePaciente
 @nome VARCHAR(20)
    AS
 BEGIN
SELECT nome, DATEDIFF(YEAR,dataNascimento,GETDATE())
    AS idade
  FROM USUARIO U
 INNER JOIN PACIENTE P
    ON U.idUsuario = P.idUsuario
 WHERE nome = @nome
   END
GO

EXEC IdadePaciente 'Ligia'

--Mostrar a quantidade de usuário após realizar a importação do banco de dados
SELECT COUNT(idUsuario) [Quantidade de Usuarios] FROM USUARIO
GO