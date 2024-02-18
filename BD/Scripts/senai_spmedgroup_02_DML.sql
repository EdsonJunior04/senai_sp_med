USE SP_MED_GROUP;
GO

--TIPO USUARIO
INSERT INTO TIPOUSUARIO(tipo)
VALUES ('Medico'),('Paciente'),('Administrador');
GO

--SITUACAO
INSERT INTO SITUACAO(descricao)
VALUES ('Agendada'),('Cancelada'),('Realizada')
GO

--ESPECIALIZACAO
INSERT INTO ESPECIALIZACAO(tituloEspecializacao)
VALUES ('Acupuntura'),('Anestesiologia'),('Angiologia'),('Cardiologia'),
       ('Cirurgia Cardiovascular'),('Cirurgia da M�o'),('Cirurgia do Aparelho Digestivo'),('Cirurgia Geral'),('Cirurgia Pedi�trica'),
	   ('Cirurgia Pl�stica'),('Cirurgia Tor�cica'),('Cirurgia Vascular'),('Dermatologia'),('Radioterapia'),('Urologia'),('Pediatria'),('Psiquiatria')
GO

--INSTITUICAO
INSERT INTO INSTITUICAO(nomeFantasia, razaoSocial, endereco, CNPJ)
VALUES ('Clinica Possarle','SP Medical Group','Av. Bar�o Limeira, 532, S�o Paulo, SP','86.400.902/0001-30')
GO

--USUARIO
INSERT INTO USUARIO(idTipoUsuario,nome,email,senha)
VALUES ('3', 'Administrador', 'adm@adm.com', 'adm12345'),
	   ('2','Ligia','ligia@gmail.com','ligia123'),
	   ('2','Alexandre','alexandre@gmail.com','alexandre123'),
	   ('2','Fernando','fernando@gmail.com','fernando123'),
	   ('2','Henrique','henrique@gmail.com','henrique123'),
	   ('2','Joao','joao@hotmail.com','joao1234'),
	   ('2','Bruno','bruno@gmail.com','bruno123'),
	   ('2','Mariana','mariana@outlook.com','mariana123'),
	   ('1','Ricardo Lemos','ricardo.lemos@spmedicalgroup.com.br','ricardo123'),
	   ('1','Roberto Possarle','roberto.possarle@spmedicalgroup.com.br','roberto123'),
	   ('1','Helena Strada','helena.souza@spmedicalgroup.com.br','helena123')
GO

--MEDICO
INSERT INTO MEDICO(idEspecializacao,idInstituicao,idUsuario,CRM)
VALUES ('2','1','9','54356'),
	   ('17','1','10','53452'),
	   ('16','1','11','65463')
GO


--PACIENTE
INSERT INTO PACIENTE(idUsuario,dataNascimento,CPF,RG,telefone,endereco)
VALUES ('2','13/10/1983','94839859000','435225435','11 34567654','Rua Estado de Israel 240,�S�o Paulo, Estado de S�o Paulo, 04022-000'),
	   ('3','23/7/2001','73556944057','326543457','11 987656543','Av. Paulista, 1578 - Bela Vista, S�o Paulo - SP, 01310-200'),
	   ('4','10/10/1978','16839338002','546365253','11 972084453','Av. Ibirapuera - Indian�polis, 2927,  S�o Paulo - SP, 04029-200'),
	   ('5','13/10/1985','14332654765','543663625','11 34566543','R. Vit�ria, 120 - Vila Sao Jorge, Barueri - SP, 06402-030'),
	   ('6','27/08/1975','91305348010','532544441','11 76566377','R. Ver. Geraldo de Camargo, 66 - Santa Luzia, Ribeir�o Pires - SP, 09405-380'),
	   ('7','21/03/1972','79799299004','545662667','11 954368769','Alameda dos Arapan�s, 945 - Indian�polis, S�o Paulo - SP, 04524-001'),
	   ('8','05/03/2018','13771913039','545662668',NULL,'R Sao Antonio, 232 - Vila Universal, Barueri - SP, 06407-140')
GO
--Atualizou os registros que n�o possuem data de nascimento conforme especificado pelo cliente
UPDATE PACIENTE
   SET dataNascimento = '05/03/2018'
 WHERE idPaciente = 7
    GO

--CONSULTA
INSERT INTO CONSULTA (idMedico,idSituacao,idPaciente,dataConsulta,descricao)
VALUES(3,3,7,'20/01/2020 15:00','psicologica'),
(2,2,2,'06/01/2020 10:00','psicologica'),
(2,3,3,'07/02/2018 10:00','psicologica'),
(2,3,2,'06/02/2018 10:00','psicologica'),
(1,2,4,'07/02/2019 11:00','psicologica'),
(3,1,7,'08/03/2020 15:00','psicologica'),
(1,1,4,'09/03/2020 11:00','psicologica')
GO
