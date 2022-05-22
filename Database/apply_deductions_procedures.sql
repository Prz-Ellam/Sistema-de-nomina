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

	IF EXISTS (SELECT id_deduccion_aplicada FROM deducciones_aplicadas WHERE fecha = @fecha 
				AND id_deduccion = @id_deduccion AND numero_empleado = @numero_empleado)
		BEGIN
			RAISERROR('Ya fue aplicada una deducción en esta fecha', 11, 1);
			RETURN;
		END

	INSERT INTO deducciones_aplicadas(
			numero_empleado,
			id_deduccion,
			fecha,
			cantidad
	)
	SELECT TOP 1
			e.numero_empleado,
			d.id_deduccion,
			@fecha,
			IIF(d.tipo_monto = 'F', d.fijo, d.porcentual * e.sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado))
	FROM
			empleados AS e
			INNER JOIN deducciones AS d
			ON d.id_deduccion = @id_deduccion
	WHERE
			d.id_deduccion = @id_deduccion AND
			e.numero_empleado = @numero_empleado AND
			e.activo = 1 AND
			d.activo = 1;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarDepartamentoDeduccion')
	DROP PROCEDURE sp_AplicarDepartamentoDeduccion;
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

	DELETE FROM 
			deducciones_aplicadas
	WHERE 
			numero_empleado = @numero_empleado AND 
			id_deduccion = @id_deduccion AND 
			dbo.PRIMERDIAFECHA(fecha) = dbo.PRIMERDIAFECHA(@fecha);

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
			INNER JOIN empleados AS e
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

	IF NOT EXISTS (SELECT numero_empleado FROM empleados WHERE numero_empleado = @numero_empleado)
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
				dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND
				da.numero_empleado = @numero_empleado
		WHERE 
				d.tipo_duracion = 'S' AND 
				dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL);
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
				dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND
				da.numero_empleado = @numero_empleado
		WHERE 
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'true' AND
				d.tipo_duracion = 'S' AND 
				dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL);
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
				dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND
				da.numero_empleado = @numero_empleado
		WHERE 
				IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'false' AND
				d.tipo_duracion = 'S' AND 
				dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL);
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
				IIF(ISNULL(dda.[Cantidad empleados], 0) >= hc.[Cantidad] AND ISNULL(dda.[Cantidad empleados], 0) <> 0, 'true', 'false') [Aplicada],
				ddf.id_deduccion,
				d.nombre
		FROM 
				vw_DepartamentosDeduccionesAplicadas AS dda
				RIGHT JOIN vw_DepartamentosDeduccionesFechas AS ddf
				ON dda.id_departamento = ddf.id_departamento AND 
				dda.id_deduccion = ddf.id_deduccion AND
				dbo.PRIMERDIAFECHA(dda.fecha) = dbo.PRIMERDIAFECHA(ddf.fecha)
				INNER JOIN deducciones AS d
				ON ddf.id_deduccion = d.id_deduccion
				INNER JOIN vw_Headcounter2 AS hc
				ON ddf.id_departamento = hc.[ID Departamento] AND dbo.PRIMERDIAFECHA(ddf.fecha) = dbo.PRIMERDIAFECHA(hc.[Fecha])
		WHERE
				ddf.fecha = @fecha AND
				ddf.id_departamento = @id_departamento AND
				dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL)

	ELSE IF @filtro = 2

		SELECT 
				IIF(ISNULL(dda.[Cantidad empleados], 0) >= hc.[Cantidad] AND ISNULL(dda.[Cantidad empleados], 0) <> 0, 'true', 'false') [Aplicada],
				ddf.id_deduccion,
				d.nombre
		FROM 
				vw_DepartamentosDeduccionesAplicadas AS dda
				RIGHT JOIN vw_DepartamentosDeduccionesFechas AS ddf
				ON dda.id_departamento = ddf.id_departamento AND 
				dda.id_deduccion = ddf.id_deduccion AND
				dbo.PRIMERDIAFECHA(dda.fecha) = dbo.PRIMERDIAFECHA(ddf.fecha)
				INNER JOIN deducciones AS d
				ON ddf.id_deduccion = d.id_deduccion
				INNER JOIN vw_Headcounter2 AS hc
				ON ddf.id_departamento = hc.[ID Departamento] AND dbo.PRIMERDIAFECHA(ddf.fecha) = dbo.PRIMERDIAFECHA(hc.[Fecha])
		WHERE
				ddf.fecha = @fecha AND
				ddf.id_departamento = @id_departamento AND
				dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL) AND
				IIF(ISNULL(dda.[Cantidad empleados], 0) >= hc.[Cantidad] AND ISNULL(dda.[Cantidad empleados], 0) <> 0, 'true', 'false') = 'true';

	ELSE IF @filtro = 3

		SELECT 
				IIF(ISNULL(dda.[Cantidad empleados], 0) >= hc.[Cantidad] AND ISNULL(dda.[Cantidad empleados], 0) <> 0, 'true', 'false') [Aplicada],
				ddf.id_deduccion,
				d.nombre
		FROM 
				vw_DepartamentosDeduccionesAplicadas AS dda
				RIGHT JOIN vw_DepartamentosDeduccionesFechas AS ddf
				ON dda.id_departamento = ddf.id_departamento AND 
				dda.id_deduccion = ddf.id_deduccion AND
				dbo.PRIMERDIAFECHA(dda.fecha) = dbo.PRIMERDIAFECHA(ddf.fecha)
				INNER JOIN deducciones AS d
				ON ddf.id_deduccion = d.id_deduccion
				INNER JOIN vw_Headcounter2 AS hc
				ON ddf.id_departamento = hc.[ID Departamento] AND dbo.PRIMERDIAFECHA(ddf.fecha) = dbo.PRIMERDIAFECHA(hc.[Fecha])
		WHERE
				ddf.fecha = @fecha AND
				ddf.id_departamento = @id_departamento AND
				dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL) AND
				IIF(ISNULL(dda.[Cantidad empleados], 0) >= hc.[Cantidad] AND ISNULL(dda.[Cantidad empleados], 0) <> 0, 'true', 'false') = 'false';

	ELSE
		RAISERROR('Filtro inválido', 11, 1);
GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeduccionesRecibo')
	DROP PROCEDURE sp_LeerDeduccionesRecibo;
GO

CREATE PROCEDURE sp_LeerDeduccionesRecibo(
	@id_nomina			INT
)
AS

	SELECT 
			d.id_deduccion		[Clave], 
			d.nombre			[Concepto], 
			da.cantidad			[Importe]
	FROM 
			deducciones_aplicadas AS da
			INNER JOIN deducciones AS d
			ON da.id_deduccion = d.id_deduccion
	WHERE 
			id_nomina = @id_nomina;

GO