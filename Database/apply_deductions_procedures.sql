USE sistema_de_nomina;



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarEmpleadoDeduccion')
	DROP PROCEDURE sp_AplicarEmpleadoDeduccion;
GO

CREATE PROCEDURE sp_AplicarEmpleadoDeduccion(
	@numero_empleado			INT,
	@id_deduccion				INT,
	@fecha						DATE
)
AS

	IF (EXISTS (SELECT id_deduccion_aplicada FROM deducciones_aplicadas WHERE fecha = @fecha 
	AND id_deduccion = @id_deduccion AND numero_empleado = @numero_empleado))
		BEGIN
			RAISERROR('Ya fue aplicada una deducción en esta fecha', 11, 1);
			RETURN;
		END

	DECLARE @tipo_monto		CHAR;
	DECLARE @cantidad		MONEY;
	SET @tipo_monto = (SELECT tipo_monto FROM deducciones WHERE id_deduccion = @id_deduccion AND activo = 1);
	
	IF @tipo_monto = 'F'
		SET @cantidad = (SELECT fijo FROM deducciones WHERE id_deduccion = @id_deduccion AND activo = 1)
	ELSE IF @tipo_monto = 'P'
		SET @cantidad = (SELECT sueldo_diario FROM empleados WHERE numero_empleado = @numero_empleado AND activo = 1) *
						 dbo.DIASTRABAJADOSEMPLEADO(@fecha, @numero_empleado) *
						(SELECT porcentual FROM deducciones WHERE id_deduccion = @id_deduccion AND activo = 1)


	INSERT INTO deducciones_aplicadas(numero_empleado, id_deduccion, fecha, cantidad)
	VALUES(@numero_empleado, @id_deduccion, @fecha, @cantidad);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarEmpleadoDeduccion')
	DROP PROCEDURE sp_EliminarEmpleadoDeduccion;
GO

CREATE PROCEDURE sp_EliminarEmpleadoDeduccion(
	@numero_empleado			INT,
	@id_deduccion				INT,
	@fecha						DATE
)
AS

	DELETE FROM deducciones_aplicadas
	WHERE numero_empleado = @numero_empleado AND id_deduccion = @id_deduccion AND YEAR(fecha) = YEAR(@fecha) AND MONTH(fecha) = MONTH(@fecha)

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeduccionesAplicadas')
	DROP PROCEDURE sp_LeerDeduccionesAplicadas;
GO

CREATE PROCEDURE sp_LeerDeduccionesAplicadas(
	@filtro						INT,
	@numero_empleado			INT,
	@fecha						DATE
)
AS

	IF (NOT EXISTS (SELECT numero_empleado FROM empleados WHERE numero_empleado = @numero_empleado))
		RETURN;

	IF @filtro = 1
		SELECT IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
		FROM deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion AND YEAR(da.fecha) = YEAR(@fecha) AND MONTH(da.fecha) = MONTH(@fecha) AND da.numero_empleado = @numero_empleado;
	ELSE IF @filtro = 2
		SELECT IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
		FROM deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion AND YEAR(da.fecha) = YEAR(@fecha) AND MONTH(da.fecha) = MONTH(@fecha) AND da.numero_empleado = @numero_empleado
		WHERE IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'true';
	ELSE IF @filtro = 3
		SELECT IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
		FROM deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion AND YEAR(da.fecha) = YEAR(@fecha) AND MONTH(da.fecha) = MONTH(@fecha) AND da.numero_empleado = @numero_empleado
		WHERE IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'false';
	ELSE 
		RAISERROR('Filtro inválido', 11, 1);

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeduccionesNomina')
	DROP PROCEDURE sp_LeerDeduccionesNomina;
GO

CREATE PROCEDURE sp_LeerDeduccionesNomina(
	@id_nomina			INT
)
AS

	SELECT 
			d.id_deduccion		[Clave], 
			d.nombre			[Concepto], 
			da.cantidad			[Importe]
	FROM 
			deducciones_aplicadas AS da
			JOIN deducciones AS d
			ON da.id_deduccion = d.id_deduccion
	WHERE 
			id_nomina = @id_nomina;

GO