USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerBancosPar')
	DROP PROCEDURE sp_LeerBancosPar;
GO

CREATE PROCEDURE sp_LeerBancosPar
AS

	SELECT 
			id_banco [ID Banco],
			nombre [Nombre]
	FROM 
			bancos;

GO