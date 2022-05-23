USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDomicilio')
	DROP PROCEDURE sp_AgregarDomicilio;
GO

CREATE PROCEDURE sp_AgregarDomicilio
	@calle				VARCHAR(30),
	@numero				VARCHAR(10),
	@colonia			VARCHAR(30),
	@ciudad				VARCHAR(30),
	@estado				VARCHAR(30),
	@codigo_postal		VARCHAR(5)
AS

	INSERT INTO domicilios(
			calle,
			numero,
			colonia,
			ciudad,
			estado,
			codigo_postal
	)
	VALUES(
			@calle,
			@numero,
			@colonia,
			@ciudad,
			@estado,
			@codigo_postal
	);

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDomicilio')
	DROP PROCEDURE sp_ActualizarDomicilio;
GO

CREATE PROCEDURE sp_ActualizarDomicilio
	@id_domicilio		INT,
	@calle				VARCHAR(30),
	@numero				VARCHAR(10),
	@colonia			VARCHAR(30),
	@ciudad				VARCHAR(30),
	@estado				VARCHAR(30),
	@codigo_postal		VARCHAR(5)
AS

	UPDATE
			domicilios
	SET
			calle			= ISNULL(@calle, calle),
			numero			= ISNULL(@numero, numero),
			colonia			= ISNULL(@colonia, colonia),
			ciudad			= ISNULL(@ciudad, ciudad),
			estado			= ISNULL(@estado, estado),
			codigo_postal	= ISNULL(@codigo_postal, codigo_postal)
	WHERE 
			id_domicilio = @id_domicilio;

GO