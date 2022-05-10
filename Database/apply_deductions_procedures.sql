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




CREATE PROCEDURE sp_AplicarDepartamentoDeduccion(
	@id_departamento			INT,
	@id_deduccion				INT,
	@fecha						DATE
)
AS

	INSERT INTO deducciones_aplicadas(
			numero_empleado,
			id_deduccion,
			fecha,
			cantidad
	)
	SELECT 
			e.numero_empleado,
			@id_deduccion,
			@fecha,
			sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, numero_empleado) * p.porcentual + p.fijo
	FROM 
			empleados AS e
			INNER JOIN deducciones AS p
			ON p.id_deduccion = @id_deduccion
	WHERE
			e.id_departamento = @id_departamento AND 
			e.activo = 1 AND
			(SELECT COUNT(0) FROM deducciones_aplicadas 
			WHERE numero_empleado = e.numero_empleado AND id_deduccion = @id_deduccion AND fecha = @fecha) = 0;

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



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartamentoDeduccion')
	DROP PROCEDURE sp_EliminarDepartamentoDeduccion;
GO

CREATE PROCEDURE sp_EliminarDepartamentoDeduccion(
	@id_departamento			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	DELETE 
			deducciones_aplicadas 
	FROM 
			deducciones_aplicadas AS da
			JOIN empleados AS e
			ON da.numero_empleado = e.numero_empleado
	WHERE 
			e.id_departamento = @id_departamento AND 
			da.id_deduccion = @id_percepcion AND 
			dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha);

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
		SELECT 
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				d.id_deduccion,
				d.nombre,
				d.tipo_monto,
				d.fijo,
				d.porcentual 
		FROM 
				deducciones AS d
				LEFT JOIN deducciones_aplicadas AS da
				ON d.id_deduccion = da.id_deduccion AND 
				YEAR(da.fecha) = YEAR(@fecha) AND 
				MONTH(da.fecha) = MONTH(@fecha) AND 
				da.numero_empleado = @numero_empleado
		WHERE 
				d.tipo_duracion = 'S' AND 
				d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL);
	ELSE IF @filtro = 2
		SELECT 
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				d.id_deduccion,
				d.nombre,
				d.tipo_monto,
				d.fijo,
				d.porcentual 
		FROM 
				deducciones AS d
				LEFT JOIN deducciones_aplicadas AS da
				ON d.id_deduccion = da.id_deduccion AND
				YEAR(da.fecha) = YEAR(@fecha) AND
				MONTH(da.fecha) = MONTH(@fecha) AND
				da.numero_empleado = @numero_empleado
		WHERE 
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'true' AND
				d.tipo_duracion = 'S' AND 
				d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL);
	ELSE IF @filtro = 3
		SELECT
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				d.id_deduccion,
				d.nombre,
				d.tipo_monto,
				d.fijo,
				d.porcentual 
		FROM 
				deducciones AS d
				LEFT JOIN deducciones_aplicadas AS da
				ON d.id_deduccion = da.id_deduccion AND
				YEAR(da.fecha) = YEAR(@fecha) AND
				MONTH(da.fecha) = MONTH(@fecha) AND
				da.numero_empleado = @numero_empleado
		WHERE 
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'false' AND
				d.tipo_duracion = 'S' AND 
				d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL);
	ELSE 
		RAISERROR('Filtro inválido', 11, 1);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartamentosDeduccionesAplicadas')
	DROP PROCEDURE sp_LeerDepartamentosDeduccionesAplicadas;
GO

CREATE PROCEDURE sp_LeerDepartamentosDeduccionesAplicadas(
	@filtro						INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

	IF @filtro = 1

		SELECT
				IIF(COUNT(e.numero_empleado) >= 
				(SELECT Cantidad FROM vw_DepartmentsCount WHERE id_departamento = @id_departamento AND fecha = @fecha) AND
				COUNT(e.numero_empleado) <> 0, 
				'true', 'false') [Aplicada],
				d.id_deduccion,
				d.nombre,
				d.tipo_monto,
				d.fijo,
				d.porcentual
		FROM 
				deducciones AS d
				LEFT JOIN deducciones_aplicadas AS da
				ON d.id_deduccion = da.id_deduccion AND
				dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha)
				LEFT JOIN empleados AS e
				ON da.numero_empleado = e.numero_empleado AND
				e.id_departamento = @id_departamento
		WHERE
				d.tipo_duracion = 'S' AND 
				d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL)
		GROUP BY 
				d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual

	ELSE IF @filtro = 2

		SELECT
				IIF(COUNT(e.numero_empleado) >= 
				(SELECT Cantidad FROM vw_DepartmentsCount WHERE id_departamento = @id_departamento AND fecha = @fecha) AND
				COUNT(e.numero_empleado) <> 0, 
				'true', 'false') [Aplicada],
				d.id_deduccion,
				d.nombre,
				d.tipo_monto,
				d.fijo,
				d.porcentual
		FROM 
				deducciones AS d
				LEFT JOIN deducciones_aplicadas AS da
				ON d.id_deduccion = da.id_deduccion AND
				dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha)
				LEFT JOIN empleados AS e
				ON da.numero_empleado = e.numero_empleado AND
				e.id_departamento = @id_departamento
		WHERE
				d.tipo_duracion = 'S' AND
				d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL)
		GROUP BY 
				d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual
		HAVING
				COUNT(e.numero_empleado) >= 
				(SELECT Cantidad FROM vw_DepartmentsCount WHERE id_departamento = @id_departamento AND fecha = @fecha);

	ELSE IF @filtro = 3

				SELECT
				IIF(COUNT(e.numero_empleado) >= 
				(SELECT Cantidad FROM vw_DepartmentsCount WHERE id_departamento = @id_departamento AND fecha = @fecha) AND
				COUNT(e.numero_empleado) <> 0, 
				'true', 'false') [Aplicada],
				d.id_deduccion,
				d.nombre,
				d.tipo_monto,
				d.fijo,
				d.porcentual
		FROM 
				deducciones AS d
				LEFT JOIN deducciones_aplicadas AS da
				ON d.id_deduccion = da.id_deduccion AND
				dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha)
				LEFT JOIN empleados AS e
				ON da.numero_empleado = e.numero_empleado AND
				e.id_departamento = @id_departamento
		WHERE
				d.tipo_duracion = 'S' AND
				d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL)
		GROUP BY
				d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual
		HAVING
				COUNT(e.numero_empleado) < 
				(SELECT Cantidad FROM vw_DepartmentsCount WHERE id_departamento = @id_departamento AND fecha = @fecha);

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