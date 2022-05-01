USE sistema_de_nomina;

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
GROUP BY d.nombre, p.nombre;

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






