USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReporteGeneralNomina')
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
			[Departamento],
			[Puesto],
			[Cantidad]
	FROM
			vw_Headcounter1
	WHERE
			dbo.PRIMERDIAFECHA([Fecha]) = dbo.PRIMERDIAFECHA(@fecha) AND 
			([ID Departamento] = @id_departamento OR @id_departamento = -1) AND
			[ID Empresa] = @id_empresa
	ORDER BY
			[Departamento] ASC, [Puesto] ASC;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Headcounter2')
	DROP PROCEDURE sp_Headcounter2;
GO

CREATE PROCEDURE sp_Headcounter2(
	@id_empresa					INT,
	@id_departamento			INT,
	@fecha						DATE
)
AS

	SELECT
			[Departamento],
			[Cantidad]
	FROM
			vw_Headcounter2
	WHERE
			dbo.PRIMERDIAFECHA([Fecha]) = dbo.PRIMERDIAFECHA(@fecha) AND
			([ID Departamento] = @id_departamento OR @id_departamento = -1) AND
			[ID Empresa] = @id_empresa
	ORDER BY
			[Departamento] ASC;

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