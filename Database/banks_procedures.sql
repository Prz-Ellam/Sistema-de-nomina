USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerBancosPar')
	DROP PROCEDURE sp_LeerBancosPar;
GO

CREATE PROCEDURE sp_LeerBancosPar
AS

	SELECT
			'Seleccionar'	[Nombre],
			-1				[ID Banco]
	UNION
	SELECT
			nombre			[Nombre],
			id_banco		[ID Banco]
	FROM 
			bancos;

GO