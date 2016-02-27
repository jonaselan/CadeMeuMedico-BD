CREATE DATABASE [CadeMeuMedicoBD]
GO
USE [CadeMeuMedicoBD]
GO

/*
insert into Cidades values ('Cidade Y')
insert into Cidades values ('Cidade X')
insert into Especialidades values ('Psicologo')
insert into Especialidades values ('Pediatra')

insert into Medicos values  ('123', 'Jonas Elan', 'R. Sim', 'B. Nao', 'jonas@sim.com', 0, 1, 'sim.com', 2, 1, 'senha')
insert into Medicos values  ('321', 'Teixeira Alves', 'R. Nao', 'B. sim', 'jonas@nao.com', 0, 1, 'nao.com', 1, 2, 'senha123')

*/

/*
REFERENCIA DE TABELAS
alter table Medicos
	add constraint fk_EspMed foreign key (IDEspecialidade)
	references Especialidades (IDEspecialidade)
	*/

/*
PERMITIR A Criacao do diagrama
use CadeMeuMedicoBD
go
exec sp_changedbowner 'sa'
go
*/

/* CRIAR TABELA DE ADMINISTRADOR
create table Administrador(
	IDAdministrador int not null identity(1,1),
	Nome varchar(80),
	Senha vatchar(80) not null
)

*/

/*
ADICIONAR COLUNA SENHA
alter table Medicos
	add Senha varchar(80) not null
*/

--�rea de procedimentos no banco CadeMeuMedico
CREATE PROCEDURE [dbo].[InserirMedico]
@CRM varchar(30),
@Nome varchar(80),
@Endereco varchar(100),
@Bairro varchar(60),
@Email varchar(100),
@AtendePorConvenio bit,
@TemClinica bit,
@WebSiteBlog varchar(80),
@IDCidade int,
@IDEspecialidade int
AS
INSERT INTO Medicos VALUES (@CRM, @Nome, @Endereco, @Bairro, @Email,
@AtendePorConvenio,
 @TemClinica, @WebSiteBlog, @IDCidade, @IDEspecialidade)
GO

--Atualizar o registro do m�dico (ID)
Create Procedure [dbo].[AtualizarMedico]
@IDMedico bigint,
@CRM varchar(30),
@Nome varchar(80),
@Endereco varchar(100),
@Bairro varchar(60),
@Email varchar(100),
@AtendePorConvenio bit,
@TemClinica bit,
@WebSiteBlog varchar(80),
@IDCidade int,
@IDEspecialidade int
AS
UPDATE Medicos Set CRM = @CRM,
 Nome = @Nome,
 Endereco = @Endereco,
 Bairro = @Bairro,
 Email = @Email,
 AtendePorConvenio = @AtendePorConvenio,
 TemClinica = @TemClinica,
 WebSiteBlog = @WebSiteBlog,
 IDCidade = @IDCidade,
 IDEspecialidade = @IDEspecialidade
Where IDMedico = @IDMedico
GO

--Selecionar um m�dico pelo ID
CREATE PROCEDURE SelecionarMedicoPorID
@IDMedico bigint
AS

--Deletar um medico pelo ID
CREATE PROCEDURE ExcluirMedicoPorID
@IDMedico bigint
AS
 DELETE FROM Medicos WHERE IDMedico = @IDMedico
GO