USE sistema_de_nomina;



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDeduccion')
	DROP PROCEDURE sp_AgregarDeduccion;
GO

CREATE PROCEDURE sp_AgregarDeduccion(
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT
) 
AS

	INSERT INTO deducciones(nombre, tipo_monto, fijo, porcentual)
	VALUES(@nombre, @tipo_monto, @fijo, @porcentual);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDeduccion')
	DROP PROCEDURE sp_ActualizarDeduccion;
GO

CREATE PROCEDURE sp_ActualizarDeduccion(
	@id_deduccion		INT,
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT
)
AS

	UPDATE deducciones
	SET
	nombre = ISNULL(@nombre, nombre),
	tipo_monto = ISNULL(@tipo_monto, tipo_monto),
	fijo = ISNULL(@fijo, fijo),
	porcentual = ISNULL(@porcentual, porcentual)
	WHERE id_deduccion = @id_deduccion;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDeduccion')
	DROP PROCEDURE sp_EliminarDeduccion;
GO

CREATE PROCEDURE sp_EliminarDeduccion(
	@id_deduccion		INT
)
AS

	UPDATE deducciones
	SET
	activo = 0
	WHERE id_deduccion = @id_deduccion;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeducciones')
	DROP PROCEDURE sp_LeerDeducciones;
GO

CREATE PROCEDURE sp_LeerDeducciones
AS

	SELECT 
			id_deduccion [ID Deduccion], 
			nombre [Nombre],
			tipo_monto [Tipo de monto], 
			ISNULL(fijo, 0) [Fijo], 
			ISNULL(porcentual, 0) [Porcentual]
	FROM 
			deducciones
	WHERE 
			activo = 1 AND tipo_duracion = 'S';

GO