USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarEmpleadoPercepcion')
	DROP PROCEDURE sp_AplicarEmpleadoPercepcion;
GO

CREATE PROCEDURE sp_AplicarEmpleadoPercepcion(
	@numero_empleado			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	IF EXISTS (SELECT id_percepcion_aplicada FROM percepciones_aplicadas WHERE fecha = @fecha 
				AND id_percepcion = @id_percepcion AND numero_empleado = @numero_empleado)
		BEGIN
			RAISERROR('Ya fue aplicada una percepcion en esta fecha', 11, 1);
			RETURN;
		END

	INSERT INTO percepciones_aplicadas(
			numero_empleado,
			id_percepcion,
			fecha,
			cantidad
	)
	SELECT TOP 1
			e.numero_empleado,
			p.id_percepcion,
			@fecha,
			IIF(p.tipo_monto = 'F', p.fijo, p.porcentual * e.sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado))
	FROM
			empleados AS e
			INNER JOIN percepciones AS p
			ON p.id_percepcion = @id_percepcion
	WHERE
			p.id_percepcion = @id_percepcion AND
			e.numero_empleado = @numero_empleado AND
			e.activo = 1 AND
			p.activo = 1;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarDepartamentoPercepcion')
	DROP PROCEDURE sp_AplicarDepartamentoPercepcion;
GO

CREATE PROCEDURE sp_AplicarDepartamentoPercepcion(
	@id_departamento			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS
BEGIN

	INSERT INTO percepciones_aplicadas(
			numero_empleado,
			id_percepcion,
			fecha,
			cantidad
	)
	SELECT 
			e.numero_empleado,
			@id_percepcion,
			@fecha,
			sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, numero_empleado) * p.porcentual + p.fijo
	FROM 
			empleados AS e
			INNER JOIN percepciones AS p
			ON p.id_percepcion = @id_percepcion
	WHERE
			e.id_departamento = @id_departamento AND 
			e.activo = 1 AND
			(SELECT COUNT(0) FROM percepciones_aplicadas 
			WHERE numero_empleado = e.numero_empleado AND id_percepcion = @id_percepcion AND fecha = @fecha) = 0;

END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarEmpleadoPercepcion')
	DROP PROCEDURE sp_EliminarEmpleadoPercepcion;
GO

CREATE PROCEDURE sp_EliminarEmpleadoPercepcion(
	@numero_empleado			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	DELETE FROM 
			percepciones_aplicadas
	WHERE 
			numero_empleado = @numero_empleado AND
			id_percepcion = @id_percepcion AND
			dbo.PRIMERDIAFECHA(fecha) = dbo.PRIMERDIAFECHA(@fecha);

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartamentoPercepcion')
	DROP PROCEDURE sp_EliminarDepartamentoPercepcion;
GO

CREATE PROCEDURE sp_EliminarDepartamentoPercepcion(
	@id_departamento			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	DELETE 
			percepciones_aplicadas 
	FROM 
			percepciones_aplicadas AS pa
			JOIN empleados AS e
			ON pa.numero_empleado = e.numero_empleado
	WHERE 
			e.id_departamento = @id_departamento AND 
			pa.id_percepcion = @id_percepcion AND 
			dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(@fecha);

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepcionesAplicadas')
	DROP PROCEDURE sp_LeerPercepcionesAplicadas;
GO

CREATE PROCEDURE sp_LeerPercepcionesAplicadas(
	@filtro						INT,
	@numero_empleado			INT,
	@fecha						DATE
)
AS

	IF NOT EXISTS (SELECT numero_empleado FROM empleados WHERE numero_empleado = @numero_empleado)
		RETURN;

	IF @filtro = 1
		SELECT 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				p.id_percepcion,
				p.nombre,
				p.tipo_monto,
				p.fijo,
				p.porcentual
		FROM 
				percepciones AS p
				LEFT JOIN percepciones_aplicadas AS pa
				ON p.id_percepcion = pa.id_percepcion AND 
				dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND
				pa.numero_empleado = @numero_empleado
		WHERE 
				p.tipo_duracion = 'S' AND 
				dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL);
	ELSE IF @filtro = 2
		SELECT 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				p.id_percepcion,
				p.nombre,
				p.tipo_monto,
				p.fijo,
				p.porcentual 
		FROM 
				percepciones AS p
				LEFT JOIN percepciones_aplicadas AS pa
				ON p.id_percepcion = pa.id_percepcion AND 
				dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND
				pa.numero_empleado = @numero_empleado
		WHERE 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') = 'true' AND 
				p.tipo_duracion = 'S' AND 
				dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL);
	ELSE IF @filtro = 3
		SELECT 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				p.id_percepcion,
				p.nombre,
				p.tipo_monto,
				p.fijo,
				p.porcentual 
		FROM 
				percepciones AS p
				LEFT JOIN percepciones_aplicadas AS pa
				ON p.id_percepcion = pa.id_percepcion AND 
				dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND
				pa.numero_empleado = @numero_empleado
		WHERE 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') = 'false' AND 
				p.tipo_duracion = 'S' AND 
				dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL);
	ELSE 
		RAISERROR('Filtro inválido', 11, 1);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartamentosPercepcionesAplicadas')
	DROP PROCEDURE sp_LeerDepartamentosPercepcionesAplicadas;
GO

CREATE PROCEDURE sp_LeerDepartamentosPercepcionesAplicadas(
	@filtro						INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

	IF @filtro = 1

		SELECT 
				IIF(ISNULL(dpa.[Cantidad empleados], 0) >= dc.Cantidad AND ISNULL(dpa.[Cantidad empleados], 0) <> 0, 'true', 'false') [Aplicada],
				dpf.id_percepcion,
				p.nombre
		FROM 
				vw_DepartamentosPercepcionesAplicadas AS dpa
				RIGHT JOIN vw_DepartamentosPercepcionesFechas AS dpf
				ON dpa.id_departamento = dpf.id_departamento AND 
				dpa.id_percepcion = dpf.id_percepcion AND
				dbo.PRIMERDIAFECHA(dpa.fecha) = dbo.PRIMERDIAFECHA(dpf.fecha)
				INNER JOIN percepciones AS p
				ON dpf.id_percepcion = p.id_percepcion
				INNER JOIN vw_DepartmentsCount AS dc
				ON dpf.id_departamento = dc.id_departamento AND dpf.fecha = dc.Fecha
		WHERE
				dpf.fecha = @fecha AND
				dpf.id_departamento = @id_departamento AND
				dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL)

	ELSE IF @filtro = 2

		SELECT 
				IIF(ISNULL(dpa.[Cantidad empleados], 0) >= dc.Cantidad AND ISNULL(dpa.[Cantidad empleados], 0) <> 0, 'true', 'false') [Aplicada],
				dpf.id_percepcion,
				p.nombre
		FROM 
				vw_DepartamentosPercepcionesAplicadas AS dpa
				RIGHT JOIN vw_DepartamentosPercepcionesFechas AS dpf
				ON dpa.id_departamento = dpf.id_departamento AND 
				dpa.id_percepcion = dpf.id_percepcion AND
				dbo.PRIMERDIAFECHA(dpa.fecha) = dbo.PRIMERDIAFECHA(dpf.fecha)
				INNER JOIN percepciones AS p
				ON dpf.id_percepcion = p.id_percepcion
				INNER JOIN vw_DepartmentsCount AS dc
				ON dpf.id_departamento = dc.id_departamento AND dpf.fecha = dc.Fecha
		WHERE
				dpf.fecha = @fecha AND
				dpf.id_departamento = @id_departamento AND
				dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL) AND
				IIF(ISNULL(dpa.[Cantidad empleados], 0) >= dc.Cantidad AND ISNULL(dpa.[Cantidad empleados], 0) <> 0, 'true', 'false') = 'true'

	ELSE IF @filtro = 3

				SELECT 
				IIF(ISNULL(dpa.[Cantidad empleados], 0) >= dc.Cantidad AND ISNULL(dpa.[Cantidad empleados], 0) <> 0, 'true', 'false') [Aplicada],
				dpf.id_percepcion,
				p.nombre
		FROM 
				vw_DepartamentosPercepcionesAplicadas AS dpa
				RIGHT JOIN vw_DepartamentosPercepcionesFechas AS dpf
				ON dpa.id_departamento = dpf.id_departamento AND 
				dpa.id_percepcion = dpf.id_percepcion AND
				dbo.PRIMERDIAFECHA(dpa.fecha) = dbo.PRIMERDIAFECHA(dpf.fecha)
				INNER JOIN percepciones AS p
				ON dpf.id_percepcion = p.id_percepcion
				INNER JOIN vw_DepartmentsCount AS dc
				ON dpf.id_departamento = dc.id_departamento AND dpf.fecha = dc.Fecha
		WHERE
				dpf.fecha = @fecha AND
				dpf.id_departamento = @id_departamento AND
				dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL) AND
				IIF(ISNULL(dpa.[Cantidad empleados], 0) >= dc.Cantidad AND ISNULL(dpa.[Cantidad empleados], 0) <> 0, 'true', 'false') = 'false'

	ELSE
		RAISERROR('Filtro inválido', 11, 1);
GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepcionesRecibo')
	DROP PROCEDURE sp_LeerPercepcionesRecibo;
GO

CREATE PROCEDURE sp_LeerPercepcionesRecibo(
	@id_nomina					INT
)
AS

	SELECT 
			p.id_percepcion		[Clave], 
			p.nombre			[Concepto], 
			pa.cantidad			[Importe]
	FROM 
			percepciones_aplicadas AS pa
			INNER JOIN percepciones AS p
			ON pa.id_percepcion = p.id_percepcion
	WHERE 
			id_nomina = @id_nomina;

GO