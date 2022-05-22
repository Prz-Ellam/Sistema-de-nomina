USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDeduccion')
	DROP PROCEDURE sp_AgregarDeduccion;
GO

CREATE PROCEDURE sp_AgregarDeduccion(
	@nombre						VARCHAR(30),
	@tipo_monto					CHAR(1),
	@fijo						MONEY,
	@porcentual					FLOAT,
	@id_empresa					INT
) 
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa AND activo = 1)
		BEGIN
			RAISERROR('La empresa aún no existe', 11, 1);
			RETURN;
		END;

	DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO(@id_empresa);
	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede añadir la deducción debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END

	INSERT INTO deducciones(
			nombre,
			tipo_monto,
			fijo,
			porcentual,
			id_empresa,
			fecha_creacion
	)
	VALUES(
			@nombre,
			@tipo_monto,
			@fijo,
			@porcentual,
			@id_empresa,
			dbo.OBTENERFECHAACTUAL(@id_empresa)
	);

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDeduccion')
	DROP PROCEDURE sp_ActualizarDeduccion;
GO

CREATE PROCEDURE sp_ActualizarDeduccion(
	@id_deduccion				INT,
	@nombre						VARCHAR(30),
	@tipo_monto					CHAR(1),
	@fijo						MONEY,
	@porcentual					FLOAT
)
AS

	DECLARE @status_nomina BIT;
	SET @status_nomina = dbo.NOMINAENPROCESO((SELECT id_empresa FROM deducciones 
												WHERE id_deduccion = @id_deduccion AND activo = 1));
	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede editar la deducción debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END

	UPDATE 
			deducciones
	SET
			nombre				= ISNULL(@nombre, nombre),
			tipo_monto			= ISNULL(@tipo_monto, tipo_monto),
			fijo				= ISNULL(@fijo, fijo),
			porcentual			= ISNULL(@porcentual, porcentual)
	WHERE 
			id_deduccion = @id_deduccion AND
			activo = 1;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDeduccion')
	DROP PROCEDURE sp_EliminarDeduccion;
GO

CREATE PROCEDURE sp_EliminarDeduccion(
	@id_deduccion				INT
)
AS

	DECLARE @status_nomina BIT;
	SET @status_nomina = dbo.NOMINAENPROCESO((SELECT id_empresa FROM deducciones 
												WHERE id_deduccion = @id_deduccion AND activo = 1));
	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede eliminar la deducción debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END

	UPDATE 
			deducciones
	SET
			activo = 0,
			fecha_eliminacion = dbo.OBTENERFECHAACTUAL(id_empresa),
			id_eliminado = NEWID()
	WHERE 
			id_deduccion = @id_deduccion AND
			activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeducciones')
	DROP PROCEDURE sp_LeerDeducciones;
GO

CREATE PROCEDURE sp_LeerDeducciones(
	@filtro						VARCHAR(100),
	@id_empresa					INT
)
AS

	SELECT 
			id_deduccion			[ID Deduccion], 
			nombre					[Nombre],
			tipo_monto				[Tipo de monto], 
			ISNULL(fijo, 0)			[Fijo], 
			ISNULL(porcentual, 0)	[Porcentual],
			tipo_duracion			[Tipo de duración]
	FROM 
			deducciones
	WHERE 
			activo = 1 AND
			--tipo_duracion = 'S' AND
			id_empresa = @id_empresa AND
			nombre LIKE CONCAT('%', @filtro, '%');

GO