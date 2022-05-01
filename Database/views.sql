USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_ReporteGeneralNomina')
	DROP VIEW vw_ReporteGeneralNomina;
GO

CREATE VIEW vw_ReporteGeneralNomina
AS 
	SELECT 
	d.nombre [Departamento], 
	p.nombre [Puesto], 
	CONCAT(e.nombre, ' ', + e.apellido_paterno, ' ', e.apellido_materno) [Nombre del empleado], 
	e.fecha_contratacion [Fecha de ingreso], 
	DATEDIFF(hour,fecha_nacimiento,GETDATE())/8766 [Edad], 
	e.sueldo_diario [Salario diario]
	FROM empleados AS e
	JOIN departamentos AS d
	ON e.id_departamento = d.id_departamento
	JOIN puestos AS p
	ON e.id_puesto = p.id_puesto;
GO





IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_Headcounter1')
	DROP VIEW vw_Headcounter1;
GO

CREATE VIEW vw_Headcounter1
AS
	SELECT 
			d.nombre AS [Departamento],
			d.id_departamento AS [idd],
			p.nombre AS [Puesto],
			n.fecha AS [Fecha],
			COUNT(n.numero_empleado) AS [Cantidad de empleados]
	FROM 
			nominas AS n
			LEFT JOIN departamentos AS d
			ON n.id_departamento = d.id_departamento
			LEFT JOIN puestos AS p
			ON n.id_puesto = p.id_puesto
	GROUP BY
			d.id_departamento, d.nombre, p.id_puesto, p.nombre, n.fecha
GO



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_Headcounter2')
	DROP VIEW vw_Headcounter2;
GO

CREATE VIEW vw_Headcounter2
AS
	SELECT 
			d.nombre AS [Departamento],
			d.id_departamento AS [id departamento],
			n.fecha AS [Fecha],
			COUNT(n.numero_empleado) AS [Cantidad de empleados]
	FROM 
			nominas AS n
			LEFT JOIN departamentos AS d
			ON n.id_departamento = d.id_departamento
	GROUP BY
			d.id_departamento, d.nombre, n.fecha
GO













IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_ReporteNomina')
	DROP VIEW vw_ReporteNomina;
GO

CREATE VIEW vw_ReporteNomina
AS 
	SELECT 
		d.nombre AS [Departamento], 
		YEAR(n.fecha) AS [A�o], 
		ISNULL(DATENAME(month, n.fecha), '') AS [Mes],
		SUM(n.sueldo_bruto) AS [Sueldo bruto], 
		SUM(n.sueldo_neto) AS [Sueldo neto]
	FROM 
		nominas AS n
		JOIN departamentos AS d
		ON n.id_departamento = d.id_departamento
	GROUP BY 
		d.nombre, n.fecha;
GO












IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReporteGeneralNomina')
	DROP PROCEDURE sp_ReporteGeneralNomina;
GO

CREATE PROCEDURE sp_ReporteGeneralNomina(
	@fecha				DATE
)
AS

	SELECT [Departamento], [Puesto], [Nombre del empleado], [Fecha de ingreso], [Edad], [Salario diario] FROM vw_ReporteGeneralNomina
	WHERE DATEADD(day, -DAY([Fecha de ingreso]) + 1, [Fecha de ingreso]) <= DATEADD(day, -DAY(@fecha) + 1, @fecha)
	ORDER BY [Departamento] ASC, [Puesto] ASC;

GO












IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_ReciboNomina')
	DROP VIEW vw_ReciboNomina;
GO

CREATE VIEW vw_ReciboNomina
AS

	SELECT
			c.razon_social AS 'Nombre de empresa',
			c.rfc AS 'RFC de empresa',
			c.registro_patronal AS 'Registro patronal',
			CONCAT(do.calle, ' ', do.numero, ' ', do.colonia) AS 'Domicilio fiscal parte 1',
			CONCAT(do.codigo_postal, ' ', do.ciudad, ' ', do.estado) AS 'Domicilio fiscal parte 2',
			e.numero_empleado AS 'Numero de empleado',
			CONCAT(e.apellido_paterno, ' ', e.apellido_materno, ', ', e.nombre) AS 'Nombre de empleado',
			e.nss AS 'Numero de seguro social',
			e.curp AS 'CURP',
			e.rfc AS 'RFC de empleado',
			e.fecha_contratacion AS 'Fecha de ingreso',
			d.nombre AS 'Departamento',
			p.nombre AS 'Puesto',
			n.sueldo_diario AS 'Sueldo diario',
			n.sueldo_bruto AS 'Sueldo bruto',
			n.sueldo_neto AS 'Sueldo neto',
			DATEFROMPARTS(YEAR(n.fecha), MONTH(n.fecha), 1) AS 'Periodo',
			n.id_nomina AS 'ID de nomina'
	FROM
			empleados AS e
			JOIN departamentos AS d 
			ON e.id_departamento = d.id_departamento
			JOIN puestos AS p
			ON e.id_puesto = p.id_puesto 
			JOIN empresas AS c
			ON d.id_empresa = c.id_empresa
			JOIN domicilios AS do
			ON c.domicilio_fiscal = do.id_domicilio
			JOIN nominas AS n
			ON e.numero_empleado = n.numero_empleado
	WHERE
			e.activo = 1;

GO