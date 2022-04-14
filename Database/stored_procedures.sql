USE sistema_de_nomina;

-- Departamentos
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDepartamento')
	DROP PROCEDURE sp_AgregarDepartamento;
GO

CREATE PROCEDURE sp_AgregarDepartamento
	@nombre				VARCHAR(60),
	@sueldo_base		MONEY,
	@id_empresa			INT
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa)
		BEGIN
			RAISERROR(15600,1,1,'Error','No existe la empresa');
			RETURN;
		END;

	INSERT INTO departamentos(nombre, sueldo_base, id_empresa)
	VALUES (@nombre, @sueldo_base, @id_empresa);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDepartmento')
	DROP PROCEDURE sp_ActualizarDepartmento;
GO

CREATE PROCEDURE sp_ActualizarDepartmento
	@id_departamento			INT,
	@nombre						VARCHAR(60),
	@sueldo_base				MONEY
AS

	UPDATE departamentos
	SET
	nombre =			ISNULL(@nombre, nombre),
	sueldo_base =	ISNULL(@sueldo_base, sueldo_base)
	WHERE id_departamento = @id_departamento;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartmento')
	DROP PROCEDURE sp_EliminarDepartmento;
GO

CREATE PROCEDURE sp_EliminarDepartmento
	@id_departamento					INT
AS
	
	UPDATE departamentos
	SET
	activo = 0
	WHERE id_departamento = @id_departamento;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartmentos')
	DROP PROCEDURE sp_LeerDepartmentos;
GO

CREATE PROCEDURE sp_LeerDepartmentos

AS

	SELECT id_departamento, nombre, sueldo_base
	FROM departamentos
	WHERE activo = 1;

GO

