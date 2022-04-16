USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerBancos')
	DROP PROCEDURE sp_LeerBancos;
GO

CREATE PROCEDURE sp_LeerBancos
AS

	SELECT id_banco, nombre
	FROM bancos;

GO