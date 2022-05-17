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
			dbo.PRIMERDIAFECHA([Fecha]) = dbo.PRIMERDIAFECHA(@fecha)
	ORDER BY
			[Departamento] ASC, [Puesto] ASC, [Nombre del empleado] ASC;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_HeadCounter1')
	DROP PROCEDURE sp_HeadCounter1;
GO

CREATE PROCEDURE sp_HeadCounter1(
	@id_empresa					INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

SELECT
		dpf.Departamento,
		dpf.Puesto,
		ISNULL(dpf2.Cantidad, 0) [Cantidad],
		dpf2.fecha
FROM
		vw_DepartamentosPuestosFechas AS dpf
		LEFT JOIN vw_EmpleadosDepartamentosPuestosCantidad AS dpf2
		ON dpf.id_departamento = dpf2.id_departamento AND dpf.id_puesto = dpf2.id_puesto AND dpf.fecha = dpf2.fecha
WHERE
		dbo.PRIMERDIAFECHA(dpf.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND 
		(dpf.id_departamento = @id_departamento OR @id_departamento = -1) AND
		dpf.id_empresa = @id_empresa
ORDER BY
		dpf.Departamento ASC, dpf.Puesto ASC;

GO



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
		LEFT JOIN vw_EmpleadosDepartamentosCantidad AS df2
		ON df.id_departamento = df2.id_departamento AND dbo.PRIMERDIAFECHA(df.fecha) = dbo.PRIMERDIAFECHA(df2.fecha)
WHERE
		dbo.PRIMERDIAFECHA(df.fecha) = dbo.PRIMERDIAFECHA(@fecha) AND 
		(df.id_departamento = @id_departamento OR @id_departamento = -1) AND
		df.id_empresa = @id_empresa
ORDER BY
		df.nombre ASC;

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





CREATE VIEW vw_DepartmentsCount
AS
SELECT
		df.id_departamento,
		df.nombre,
		dbo.PRIMERDIAFECHA(df.fecha) [Fecha],
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
		dbo.PRIMERDIAFECHA(dbo.OBTENERFECHAACTUAL((SELECT TOP 1 id_empresa FROM empresas))) [Fecha],
		ISNULL(Cantidad, 0) [Cantidad]
FROM
		vw_DepartamentosFechaActual;