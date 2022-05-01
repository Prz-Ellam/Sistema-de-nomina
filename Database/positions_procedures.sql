USE sistema_de_nomina;

-- Puestos
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarPuesto')
	DROP PROCEDURE sp_AgregarPuesto;
GO

CREATE PROCEDURE sp_AgregarPuesto
	@nombre					VARCHAR(30),
	@nivel_salarial			FLOAT,
	@id_empresa				INT
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa AND activo = 1)
		BEGIN
			RAISERROR('La empresa aún no existe', 11, 1);
			RETURN;
		END;

	DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO(@id_empresa);

	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede añadir el puesto debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END

	INSERT INTO puestos(
			nombre,
			nivel_salarial,
			id_empresa)
	VALUES (
			@nombre,
			@nivel_salarial,
			@id_empresa
	);

GO





IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarPuesto')
	DROP PROCEDURE sp_ActualizarPuesto;
GO

CREATE PROCEDURE sp_ActualizarPuesto
	@id_puesto					INT,
	@nombre						VARCHAR(30),
	@nivel_salarial				FLOAT
AS

	UPDATE 
			puestos
	SET
			nombre				= ISNULL(@nombre, nombre),
			nivel_salarial		= ISNULL(@nivel_salarial, nivel_salarial)
	WHERE 
			id_puesto = @id_puesto;

GO







IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarPuesto')
	DROP PROCEDURE sp_EliminarPuesto;
GO

CREATE PROCEDURE sp_EliminarPuesto
	@id_puesto					INT
AS

	IF (EXISTS (SELECT numero_empleado FROM empleados WHERE id_puesto = @id_puesto AND activo = 1))
		BEGIN
			RAISERROR ('No se puede eliminar el puesto porque un empleado pertenece a el', 11, 1)
			RETURN;
		END
	
	UPDATE
			puestos
	SET
			activo = 0
	WHERE
			id_puesto = @id_puesto AND activo = 1;

GO






IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPuestos')
	DROP PROCEDURE sp_LeerPuestos;
GO

CREATE PROCEDURE sp_LeerPuestos(
	@id_empresa					INT
)
AS

	SELECT 
			id_puesto [ID Puesto], 
			nombre [Nombre], 
			nivel_salarial [Nivel salarial]
	FROM 
			puestos
	WHERE 
			id_empresa = @id_empresa AND activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_FiltrarPuestos')
	DROP PROCEDURE sp_FiltrarPuestos;
GO

CREATE PROCEDURE sp_FiltrarPuestos
	@filtro						VARCHAR(100),
	@id_empresa					INT
AS

	SELECT 
			id_puesto [ID Puesto], 
			nombre [Nombre], 
			nivel_salarial [Nivel salarial]
	FROM 
			puestos
	WHERE 
			id_empresa = @id_empresa AND activo = 1 AND nombre LIKE CONCAT('%', @filtro, '%');

GO
