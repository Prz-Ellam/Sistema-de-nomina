USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDepartamento')
	DROP PROCEDURE sp_AgregarDepartamento;
GO

CREATE PROCEDURE sp_AgregarDepartamento
	@nombre						VARCHAR(30),
	@sueldo_base				MONEY,
	@id_empresa					INT
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa AND activo = 1)
		BEGIN
			RAISERROR('La empresa aún no existe', 11, 1);
			RETURN;
		END;

	DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO(@id_empresa);
	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede añadir el departamento debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END


	INSERT INTO departamentos(
			nombre, 
			sueldo_base, 
			id_empresa,
			fecha_creacion
	)
	VALUES (
			@nombre, 
			@sueldo_base,
			@id_empresa,
			dbo.OBTENERFECHAACTUAL(@id_empresa)
	);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDepartamento')
	DROP PROCEDURE sp_ActualizarDepartamento;
GO

CREATE PROCEDURE sp_ActualizarDepartamento
	@id_departamento			INT,
	@nombre						VARCHAR(30),
	@sueldo_base				MONEY
AS

	DECLARE @status_nomina BIT;
	SET @status_nomina = dbo.NOMINAENPROCESO((SELECT id_empresa FROM departamentos 
												WHERE id_departamento = @id_departamento AND activo = 1));

	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede editar el departamento debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END

	UPDATE
			departamentos
	SET
			nombre				= ISNULL(@nombre, nombre),
			sueldo_base			= ISNULL(@sueldo_base, sueldo_base)
	WHERE 
			id_departamento	= @id_departamento AND 
			activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartamento')
	DROP PROCEDURE sp_EliminarDepartamento;
GO

CREATE PROCEDURE sp_EliminarDepartamento
	@id_departamento			INT
AS

	IF EXISTS (SELECT numero_empleado FROM empleados WHERE id_departamento = @id_departamento AND activo = 1)
		BEGIN
			RAISERROR ('No se puede eliminar el departamento porque un empleado pertenece a el', 11, 1)
			RETURN;
		END

	DECLARE @status_nomina BIT;
	SET @status_nomina = dbo.NOMINAENPROCESO((SELECT id_empresa FROM departamentos 
												WHERE id_departamento = @id_departamento AND activo = 1));

	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede eliminar el departamento debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END
	
	UPDATE
			departamentos
	SET
			activo = 0,
			fecha_eliminacion = dbo.OBTENERFECHAACTUAL(id_empresa),
			id_eliminado = NEWID()
			
	WHERE 
			id_departamento = @id_departamento AND
			activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartamentos')
	DROP PROCEDURE sp_LeerDepartamentos;
GO

CREATE PROCEDURE sp_LeerDepartamentos(
	@filtro						VARCHAR(100),
	@id_empresa					INT
)
AS

	SELECT 
			id_departamento		[ID Departamento], 
			nombre				[Nombre], 
			sueldo_base			[Sueldo base]
	FROM 
			departamentos
	WHERE 
			id_empresa = @id_empresa AND 
			activo = 1 AND 
			nombre LIKE CONCAT('%', @filtro, '%');

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartamentosNominas')
	DROP PROCEDURE sp_LeerDepartamentosNominas;
GO

CREATE PROCEDURE sp_LeerDepartamentosNominas(
	@id_empresa					INT,
	@fecha						DATE
)
AS

	IF (dbo.PRIMERDIAFECHA(@fecha) >= dbo.OBTENERFECHAACTUAL(@id_empresa) AND 
		(dbo.NOMINAENPROCESO(@id_empresa) = 0 OR dbo.PRIMERDIAFECHA(@fecha) > dbo.OBTENERFECHAACTUAL(@id_empresa)))
		BEGIN
			RAISERROR('No se puede iniciar una nómina fuera del periodo actual de nómina', 11, 1);
			RETURN;
		END

	SELECT
			id_departamento		[ID Departamento], 
			nombre				[Nombre], 
			sueldo_base			[Sueldo base]
	FROM
			departamentos
	WHERE
			id_empresa = @id_empresa AND
			fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
			(fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR fecha_eliminacion IS NULL);	

GO