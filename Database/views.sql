USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_RegistroEmpleado')
	DROP VIEW vw_RegistroEmpleado;
GO

CREATE VIEW vw_RegistroEmpleado
AS
	SELECT
			E.numero_empleado [Numero de empleado], 
			E.nombre [Nombre], 
			E.apellido_paterno [Apellido paterno],
			E.apellido_materno [Apellido materno],
			E.fecha_nacimiento [Fecha de nacimiento],
			E.curp [CURP],
			E.nss [NSS],
			E.rfc [RFC],
			A.calle [Calle],
			A.numero [Numero],
			A.colonia [Colonia],
			A.ciudad [Municipio],
			A.estado [Estado],
			A.codigo_postal [Codigo postal],
			B.nombre [Banco],
			B.id_banco [ID Banco],
			E.numero_cuenta [Numero de cuenta],
			E.correo_electronico [Correo electronico],
			E.contrasena [Contraseña],
			D.nombre [Departamento],
			D.id_departamento [ID Departamento],
			P.nombre [Puesto],
			P.id_puesto [ID Puesto],
			E.fecha_contratacion [Fecha de contratacion],
			E.sueldo_diario [Sueldo diario],
			D.sueldo_base [Sueldo base],
			P.nivel_salarial [Nivel salarial],
			E.activo [Activo]
	FROM 
			empleados AS E
			INNER JOIN domicilios AS A
			ON A.id_domicilio = E.domicilio
			INNER JOIN bancos AS B
			ON B.id_banco = E.banco
			INNER JOIN departamentos AS D
			ON D.id_departamento = E.id_departamento
			INNER JOIN puestos AS P
			ON P.id_puesto = E.id_puesto;
GO



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
			DATEDIFF(HOUR, fecha_nacimiento, DATEFROMPARTS(YEAR(n.fecha), MONTH(n.fecha), dbo.GETMONTHLENGTH(YEAR(n.fecha), MONTH(n.fecha)))) / 8766 [Edad], 
			n.sueldo_diario [Sueldo diario],
			n.fecha [Fecha]
	FROM
			nominas AS n
			INNER JOIN empleados AS e
			ON n.numero_empleado = e.numero_empleado
			INNER JOIN departamentos AS d
			ON n.id_departamento = d.id_departamento
			INNER JOIN puestos AS p
			ON n.id_puesto = p.id_puesto;
GO



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosFechas')
	DROP VIEW vw_DepartamentosFechas;
GO

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



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosPuestosFechas')
	DROP VIEW vw_DepartamentosPuestosFechas;
GO

CREATE VIEW vw_DepartamentosPuestosFechas
AS
SELECT DISTINCT
		d.id_departamento,
		p.id_puesto,
		d.id_empresa,
		d.nombre [Departamento],
		p.nombre [Puesto],
		n.fecha
FROM
		nominas AS n
		CROSS JOIN departamentos AS d
		CROSS JOIN puestos AS p
WHERE
		dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(n.fecha) AND 
		(d.fecha_eliminacion > dbo.PRIMERDIAFECHA(n.fecha) OR d.fecha_eliminacion IS NULL) AND
		dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(n.fecha) AND
		(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(n.fecha) OR p.fecha_eliminacion IS NULL)
GO



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosFechaActual')
	DROP VIEW vw_DepartamentosFechaActual;
GO

CREATE VIEW vw_DepartamentosFechaActual
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



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_EmpleadosDepartamentosCantidad')
	DROP VIEW vw_EmpleadosDepartamentosCantidad;
GO

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



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_EmpleadosDepartamentosPuestosCantidad')
	DROP VIEW vw_EmpleadosDepartamentosPuestosCantidad;
GO

CREATE VIEW vw_EmpleadosDepartamentosPuestosCantidad
AS
SELECT
		d.id_departamento,
		p.id_puesto,
		d.nombre [Departamento],
		p.nombre [Puesto],
		n.fecha,
		COUNT(n.numero_empleado) [Cantidad]
FROM
		departamentos AS d
		INNER JOIN nominas AS n
		ON d.id_departamento = n.id_departamento
		INNER JOIN puestos AS p
		ON p.id_puesto = n.id_puesto
GROUP BY
		d.id_departamento, p.id_puesto, d.nombre, p.nombre, n.fecha
GO




IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_ReporteNomina')
	DROP VIEW vw_ReporteNomina;
GO

CREATE VIEW vw_ReporteNomina
AS 
	SELECT 
			d.nombre AS [Departamento], 
			YEAR(n.fecha) AS [Año], 
			ISNULL(DATENAME(MONTH, n.fecha), '') AS [Mes],
			SUM(n.sueldo_bruto) AS [Sueldo bruto], 
			SUM(n.sueldo_neto) AS [Sueldo neto]
	FROM 
			nominas AS n
			INNER JOIN departamentos AS d
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

	SELECT 
			[Departamento],
			[Puesto],
			[Nombre del empleado],
			[Fecha de ingreso],
			[Edad],
			[Salario diario] 
	FROM 
			vw_ReporteGeneralNomina
	WHERE 
			 dbo.PRIMERDIAFECHA([Fecha de ingreso]) <= dbo.PRIMERDIAFECHA(@fecha)
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
			dbo.PRIMERDIAFECHA(n.fecha) AS 'Periodo',
			DATEFROMPARTS(YEAR(n.fecha), MONTH(n.fecha), dbo.GETMONTHLENGTH(YEAR(n.fecha), MONTH(n.fecha))) AS 'Periodo final',
			n.id_nomina AS 'ID de nomina'
	FROM
			empleados AS e
			JOIN nominas AS n
			ON e.numero_empleado = n.numero_empleado
			JOIN departamentos AS d 
			ON n.id_departamento = d.id_departamento
			JOIN puestos AS p
			ON n.id_puesto = p.id_puesto 
			JOIN empresas AS c
			ON d.id_empresa = c.id_empresa
			JOIN domicilios AS do
			ON c.domicilio_fiscal = do.id_domicilio
	WHERE
			e.activo = 1;

GO