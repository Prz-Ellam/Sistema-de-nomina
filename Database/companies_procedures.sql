USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_VerificarEmpresa')
	DROP PROCEDURE sp_VerificarEmpresa;
GO

CREATE PROCEDURE sp_VerificarEmpresa(
	@id_administrador			INT
)
AS

	SELECT COUNT(0) FROM empresas WHERE id_administrador = @id_administrador AND activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerEmpresa')
	DROP PROCEDURE sp_ObtenerEmpresa;
GO

CREATE PROCEDURE sp_ObtenerEmpresa(
	@id_administrador			INT
)
AS

	SELECT id_empresa FROM empresas WHERE id_administrador = @id_administrador AND activo = 1;

GO

select*from empresas;
exec sp_ObtenerEmpresa 1;




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarEmpresa')
	DROP PROCEDURE sp_AgregarEmpresa;
GO

CREATE PROCEDURE sp_AgregarEmpresa(
	@razon_social				VARCHAR(60),
	@correo_electronico			VARCHAR(30),
	@rfc						VARCHAR(18),
	@registro_patronal			VARCHAR(30),
	@fecha_inicio				DATE,
	@id_administrador			INT,
	@calle						VARCHAR(30),
	@numero						VARCHAR(10),
	@colonia					VARCHAR(30),
	@ciudad						VARCHAR(30),
	@estado						VARCHAR(30),
	@codigo_postal				VARCHAR(5),
	@telefonos					dbo.Telefonos READONLY
)
AS

	-- Solo puede haber una empresa para el proyecto, esta validacion impide crear mas
	IF (SELECT COUNT(0) FROM empresas WHERE activo = 1) > 0
	BEGIN
		RAISERROR('Ya existe una empresa', 11, 1);
		RETURN;
	END;

	EXEC sp_AgregarDomicilio @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal;

	INSERT INTO empresas(
			razon_social,
			domicilio_fiscal,
			correo_electronico,
			rfc,
			registro_patronal,
			fecha_inicio,
			id_administrador
	)
	VALUES(
			@razon_social,
			IDENT_CURRENT('Domicilios'),
			@correo_electronico,
			@rfc,
			@registro_patronal,
			@fecha_inicio,
			@id_administrador
	);

	DECLARE @min INT = (SELECT MIN(row_count) FROM @telefonos);
	DECLARE @max INT = (SELECT MAX(row_count) FROM @telefonos)
	DECLARE @count INT = @min;

	WHILE (@count <= @max)
	BEGIN

		DECLARE @numtel VARCHAR(12) = (SELECT telefono FROM @telefonos WHERE row_count = @count);

		INSERT INTO telefonos_empresas(telefono, id_empresa)
		VALUES(@numtel, IDENT_CURRENT('empresas'));

		SET @count = @count + 1;

	END
		
GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarEmpresa')
	DROP PROCEDURE sp_ActualizarEmpresa;
GO

CREATE PROCEDURE sp_ActualizarEmpresa(
	@id_empresa						INT,
	@razon_social					VARCHAR(60),
	@correo_electronico				VARCHAR(30),
	@rfc							VARCHAR(12),
	@registro_patronal				VARCHAR(30),
	@fecha_inicio					DATE,
	@calle							VARCHAR(30),
	@numero							VARCHAR(10),
	@colonia						VARCHAR(30),
	@ciudad							VARCHAR(30),
	@estado							VARCHAR(30),
	@codigo_postal					VARCHAR(5),
	@telefonos						dbo.Telefonos READONLY
)
AS

	DECLARE @id_domicilio INT;
	SET @id_domicilio = (SELECT domicilio_fiscal FROM empresas WHERE id_empresa = @id_empresa);

	EXEC sp_ActualizarDomicilio @id_domicilio, @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal;

	UPDATE 
			empresas
	SET
			razon_social				= ISNULL(@razon_social, razon_social),
			correo_electronico			= ISNULL(@correo_electronico, correo_electronico),
			rfc							= ISNULL(@rfc, rfc),
			registro_patronal			= ISNULL(@registro_patronal, registro_patronal),
			fecha_inicio				= ISNULL(@fecha_inicio, fecha_inicio)
	WHERE 
			id_empresa = @id_empresa;

	DELETE FROM 
			telefonos_empresas
	WHERE 
			id_empresa = @id_empresa;

	DECLARE @min INT = (SELECT MIN(row_count) FROM @telefonos);
	DECLARE @max INT = (SELECT MAX(row_count) FROM @telefonos)
	DECLARE @count INT = @min;

	WHILE (@count <= @max)
	BEGIN

		DECLARE @numtel VARCHAR(12) = (SELECT telefono FROM @telefonos WHERE row_count = @count);

		INSERT INTO telefonos_empresas(telefono, id_empresa)
		VALUES(@numtel, @id_empresa);

		SET @count = @count + 1;

	END

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerEmpresa')
	DROP PROCEDURE sp_LeerEmpresa;
GO

CREATE PROCEDURE sp_LeerEmpresa(
	@id_empresa					INT
)
AS

	IF (SELECT COUNT(0) FROM empresas WHERE activo = 1) < 1
	BEGIN
		RAISERROR('Aun no existe una empresa', 11, 1);
		RETURN;
	END;

	SELECT e.id_empresa, e.razon_social, d.calle, d.numero, d.colonia, d.ciudad, d.estado, d.codigo_postal, 
	e.correo_electronico, e.registro_patronal, e.rfc, e.fecha_inicio
	FROM empresas AS e
	JOIN domicilios AS d
	ON e.domicilio_fiscal = d.id_domicilio
	WHERE id_empresa = @id_empresa AND activo = 1;

GO