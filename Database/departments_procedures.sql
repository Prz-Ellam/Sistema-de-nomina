USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDepartamento')
	DROP PROCEDURE sp_AgregarDepartamento;
GO

CREATE PROCEDURE sp_AgregarDepartamento
	@nombre				VARCHAR(30),
	@sueldo_base		MONEY,
	@id_empresa			INT
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa AND activo = 1)
		BEGIN
			RAISERROR('La empresa aún no existe', 11, 1);
			RETURN;
		END;

	INSERT INTO departamentos(nombre, sueldo_base, id_empresa)
	VALUES (@nombre, @sueldo_base, @id_empresa);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDepartamento')
	DROP PROCEDURE sp_ActualizarDepartamento;
GO

CREATE PROCEDURE sp_ActualizarDepartamento
	@id_departamento			INT,
	@nombre						VARCHAR(30),
	@sueldo_base				MONEY
AS

	UPDATE departamentos
	SET
	nombre					= ISNULL(@nombre, nombre),
	sueldo_base				= ISNULL(@sueldo_base, sueldo_base)
	WHERE id_departamento	= @id_departamento;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartamento')
	DROP PROCEDURE sp_EliminarDepartamento;
GO

CREATE PROCEDURE sp_EliminarDepartamento
	@id_departamento					INT
AS

	IF (EXISTS (SELECT numero_empleado FROM empleados WHERE id_departamento = @id_departamento AND activo = 1))
		BEGIN
			RAISERROR ('No se puede eliminar el departamento porque un empleado pertenece a el', 11, 1)
			RETURN;
		END
	
	UPDATE departamentos
	SET
	activo = 0
	WHERE id_departamento = @id_departamento;

GO


EXEC sp_LeerDepartamentos 1
USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartamentos')
	DROP PROCEDURE sp_LeerDepartamentos;
GO

CREATE PROCEDURE sp_LeerDepartamentos(
	@id_empresa					INT
)
AS

	SELECT 
			id_departamento [ID Departamento], 
			nombre [Nombre], 
			sueldo_base [Sueldo base]
	FROM 
			departamentos
	WHERE 
			id_empresa = @id_empresa AND activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_FiltrarDepartamentos')
	DROP PROCEDURE sp_FiltrarDepartamentos;
GO

CREATE PROCEDURE sp_FiltrarDepartamentos
	@filtro						VARCHAR(100),
	@id_empresa					INT
AS

	SELECT
			id_departamento [ID Departamento], 
			nombre [Nombre], 
			sueldo_base [Sueldo base]
	FROM 
			departamentos
	WHERE 
			id_empresa = @id_empresa AND activo = 1 AND nombre LIKE CONCAT('%', @filtro, '%');

GO

/*
exec sp_EliminarDepartamento 1;

SELECT * FROM master.dbo.sysmessages;


sys.sp_addmessage
@msgnum = 50001,
@severity = 11,
@msgtext = 'No se puede eliminar Departamento porque un empleado lo usa'
GO
*/