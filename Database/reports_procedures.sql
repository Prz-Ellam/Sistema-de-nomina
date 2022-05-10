USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReporteGeneralNomina')
	DROP PROCEDURE sp_ReporteGeneralNomina;
GO

CREATE PROCEDURE sp_ReporteGeneralNomina(
	@fecha					DATE
)
AS

	SELECT
			[Departamento],
			[Puesto],
			[Nombre del empleado],
			[Fecha de ingreso],
			[Edad],
			[Sueldo diario]
	FROM
			vw_ReporteGeneralNomina
	WHERE
			dbo.PRIMERDIAFECHA([Fecha]) = dbo.PRIMERDIAFECHA(@fecha);

GO





IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_HeadCounter1')
	DROP PROCEDURE sp_HeadCounter1;
GO

CREATE PROCEDURE sp_HeadCounter1(
	@fecha			DATE
)
AS

SELECT	d.nombre [Departamento], 
		p.nombre [Puesto], 
		COUNT(e.numero_empleado) [Cantidad de empleados]
FROM 
		departamentos AS d
		FULL OUTER JOIN puestos AS p
		ON d.id_empresa = p.id_empresa
		LEFT JOIN empleados AS e
		ON e.id_departamento = d.id_departamento AND e.id_puesto = p.id_puesto AND 
		DATEADD(day, -DAY(e.fecha_contratacion) + 1, e.fecha_contratacion) <= '20021201' AND e.activo = 1
GROUP BY 
		d.nombre, p.nombre;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_HeadCounter1')
	DROP PROCEDURE sp_HeadCounter1;
GO

CREATE PROCEDURE sp_HeadCounter1(
	@id_empresa					INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

	SET LANGUAGE 'spanish';

	SELECT 
			[Departamento],
			[Puesto],
			[Cantidad de empleados]
	FROM
			vw_Headcounter1 AS hc
			JOIN departamentos AS d
			ON hc.[idd] = d.id_departamento
	WHERE
			([id_departamento] = @id_departamento OR @id_departamento = -1) AND
			dbo.PRIMERDIAFECHA([Fecha]) = dbo.PRIMERDIAFECHA(@fecha) AND
			d.id_empresa = @id_empresa;

	SET LANGUAGE 'us_english';

GO


DECLARE @fecha DATE = '20230201';

SELECT
		d.nombre					[Departamento],
		COUNT(n.numero_empleado)	[Empleados],
		n.fecha
FROM 
		nominas AS n
		RIGHT JOIN departamentos AS d
		ON n.id_departamento = d.id_departamento AND n.fecha = @fecha
WHERE
		d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
		(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL)
GROUP BY 
		d.nombre, d.id_departamento, n.fecha
ORDER BY
		[Departamento] ASC;



SELECT
		d.nombre					[Departamento],
		COUNT(n.numero_empleado)	[Empleados],
		n.fecha						[Fecha]
FROM
		nominas AS n
		FULL OUTER JOIN departamentos AS d
		ON n.id_departamento = d.id_departamento
GROUP BY
		d.nombre, d.id_departamento, n.fecha


/*
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_HeadCounter2')
	DROP PROCEDURE sp_HeadCounter2;
GO

CREATE PROCEDURE sp_HeadCounter2(
	@id_empresa					INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

	SET LANGUAGE 'spanish';

	SELECT 
			[Departamento],
			[Cantidad de empleados]
	FROM
			vw_Headcounter2 AS hc
			JOIN departamentos AS d
			ON hc.[id departamento] = d.id_departamento
	WHERE
			([id_departamento] = @id_departamento OR @id_departamento = -1) AND
			dbo.PRIMERDIAFECHA([Fecha]) = dbo.PRIMERDIAFECHA(@fecha) AND
			d.id_empresa = @id_empresa;

	SET LANGUAGE 'us_english';

GO
*/

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_HeadCounter2')
	DROP PROCEDURE sp_HeadCounter2;
GO

CREATE PROCEDURE sp_HeadCounter2(
	@id_empresa					INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

SELECT
		df.nombre,
		ISNULL(df2.Cantidad, 0) [Cantidad]
FROM
		vw_DepartamentosFechas AS df
		LEFT JOIN vw_DepartamentosFechas2 AS df2
		ON df.id_departamento = df2.id_departamento AND df.fecha = df2.fecha
WHERE
		dbo.PRIMERDIAFECHA(df.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND 
		(df.id_departamento = @id_departamento OR @id_departamento = -1) AND
		df.id_empresa = @id_empresa;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReporteNomina')
	DROP PROCEDURE sp_ReporteNomina;
GO

CREATE PROCEDURE sp_ReporteNomina(
	@anio			INT
)
AS

	SET LANGUAGE 'spanish';

	SELECT
			[Departamento],
			CAST([Año] AS VARCHAR),
			[Mes],
			[Sueldo bruto],
			[Sueldo neto]
	FROM
			vw_ReporteNomina
	WHERE
			[Año] = @anio
	UNION ALL
	SELECT
			'' [Departamento],
			'' [Año],
			'' [Mes],
			ISNULL(SUM([Sueldo bruto]), 0) [Sueldo bruto],
			ISNULL(SUM([Sueldo neto]), 0) [Sueldo neto]
	FROM
			vw_ReporteNomina
	WHERE
			[Año] = @anio;

	SET LANGUAGE 'us_english';

GO



/*

DECLARE @fecha DATE = '20230201';

SELECT
		d.nombre					[Departamento],
		COUNT(n.numero_empleado)	[Empleados]
FROM 
		nominas AS n
		RIGHT JOIN departamentos AS d
		ON n.id_departamento = d.id_departamento AND n.fecha = @fecha
WHERE
		d.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
		(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR d.fecha_eliminacion IS NULL)
GROUP BY 
		d.nombre, d.id_departamento, n.fecha
ORDER BY
		[Departamento] ASC;



SELECT
		p.id_percepcion,
		p.nombre,
		e.id_departamento,
		pa.fecha,
		COUNT(e.id_departamento) [Cantidad de empleados]
FROM
		percepciones_aplicadas AS pa
		RIGHT JOIN percepciones AS p
		ON pa.id_percepcion = p.id_percepcion
		RIGHT JOIN empleados AS e
		ON pa.numero_empleado = e.numero_empleado
GROUP BY
		p.id_percepcion, p.nombre, e.id_departamento, pa.fecha;



SELECT
		d.id_departamento,
		d.nombre,
		COUNT(e.numero_empleado) [Cantidad]
FROM
		departamentos AS d
		LEFT JOIN empleados AS e
		ON d.id_departamento = e.id_departamento
GROUP BY
		d.id_departamento, d.nombre;



SELECT
		df.id_departamento,
		df.nombre,
		ISNULL(df2.Cantidad, 0) [Cantidad]
FROM
		vw_DepartamentosFechas AS df
		LEFT JOIN vw_DepartamentosFechas2 AS df2
		ON df.id_departamento = df2.id_departamento AND df.fecha = df2.fecha
WHERE
		df.fecha = '20221201';



*/



DROP VIEW vw_DepartamentosFechas;
CREATE VIEW vw_DepartamentosFechas
AS
SELECT DISTINCT
		d.id_departamento,
		d.id_empresa,
		d.nombre,
		n.fecha
FROM
		nominas AS n
		CROSS JOIN departamentos AS d
WHERE
		d.fecha_creacion <= dbo.PRIMERDIAFECHA(n.fecha) AND 
		(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(n.fecha) OR d.fecha_eliminacion IS NULL)
GO



DROP VIEW vw_EmpleadosDepartamentosCantidad;
CREATE VIEW vw_EmpleadosDepartamentosCantidad
AS
SELECT
		d.id_departamento,
		d.nombre,
		n.fecha,
		COUNT(n.numero_empleado) [Cantidad]
FROM
		departamentos AS d
		JOIN nominas AS n
		ON d.id_departamento = n.id_departamento
GROUP BY
		d.id_departamento, d.nombre, n.fecha
GO



DROP VIEW vw_DepartamentosFechas3;
CREATE VIEW vw_DepartamentosFechas3
AS
SELECT
		d.id_departamento,
		d.nombre,
		COUNT(e.numero_empleado) [Cantidad]
FROM
		departamentos AS d
		LEFT JOIN empleados AS e
		ON d.id_departamento = e.id_departamento
GROUP BY
		d.id_departamento, d.nombre;
GO





SELECT
		d.id_departamento,
		d.nombre,
		COUNT(e.numero_empleado) [Cantidad]
FROM
		departamentos AS d
		LEFT JOIN empleados AS e
		ON d.id_departamento = e.id_departamento
GROUP BY
		d.id_departamento, d.nombre;


/*
SELECT
		p.id_percepcion,
		p.nombre,
		pa.fecha,
		e.id_departamento,
		IIF(COUNT(e.id_departamento) >= df.Cantidad, 1, 0) [Si]
FROM
		percepciones AS p
		LEFT JOIN percepciones_aplicadas AS pa
		ON p.id_percepcion = pa.id_percepcion
		LEFT JOIN empleados AS e
		ON pa.numero_empleado = e.numero_empleado
		INNER JOIN vw_DepartamentosFechas2 AS df
		ON e.id_departamento = df.id_departamento AND pa.fecha = df.fecha
WHERE
		tipo_duracion = 'S'
GROUP BY
		p.id_percepcion, p.nombre, pa.fecha, e.id_departamento, df.Cantidad;
*/








DROP VIEW vw_DepartmentsCount;

CREATE VIEW vw_DepartmentsCount
AS
SELECT
		df.id_departamento,
		df.nombre,
		df.fecha,
		ISNULL(df2.Cantidad, 0) [Cantidad]
FROM
		vw_DepartamentosFechas AS df
		LEFT JOIN vw_EmpleadosDepartamentosCantidad AS df2
		ON df.id_departamento = df2.id_departamento AND
		df.fecha = df2.fecha
UNION ALL
SELECT
		id_departamento,
		nombre,
		dbo.OBTENERFECHAACTUAL(1),
		ISNULL(Cantidad, 0) [Cantidad]
FROM
		vw_DepartamentosFechas3;