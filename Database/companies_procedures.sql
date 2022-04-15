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

CREATE PROCEDURE sp_AgregarEmpresa
	@razon_social				VARCHAR(30),
	@domicilio_fiscal			INT,
	@correo_electronico			VARCHAR(30),
	@rfc						VARCHAR(18),
	@registro_patronal			VARCHAR(30),
	@fecha_inicio				DATE,
	@id_administrador			INT
AS

	-- Solo puede haber una empresa para el proyecto, esta validacion impide crear mas
	IF (SELECT COUNT(0) FROM empresas WHERE activo = 1) > 0
	BEGIN
		RAISERROR('Ya existe una empresa', 11, 1);
		RETURN;
	END;

	IF NOT EXISTS(SELECT 1 FROM domicilios WHERE id_domicilio = @domicilio_fiscal)
	BEGIN
		RAISERROR('La direccion que escribió no existe', 11, 1);
		RETURN;
	END

	INSERT INTO empresas(razon_social, domicilio_fiscal, correo_electronico, rfc, registro_patronal, fecha_inicio, id_administrador)
	VALUES(@razon_social, @domicilio_fiscal, @correo_electronico, @rfc, @registro_patronal, @fecha_inicio, @id_administrador);
		
GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarEmpresa')
	DROP PROCEDURE sp_ActualizarEmpresa;
GO

CREATE PROCEDURE sp_ActualizarEmpresa(
	@id_empresa						INT,
	@razon_social					VARCHAR(30),
	@correo_electronico				VARCHAR(30),
	@rfc							VARCHAR(12),
	@registro_patronal				VARCHAR(30)
)
AS

	UPDATE empresas
	SET
	razon_social = ISNULL(@razon_social, razon_social),
	correo_electronico = ISNULL(@correo_electronico, correo_electronico),
	rfc = ISNULL(@rfc, rfc),
	registro_patronal = ISNULL(@registro_patronal, registro_patronal)
	WHERE id_empresa = @id_empresa;

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