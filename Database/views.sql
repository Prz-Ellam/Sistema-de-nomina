USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_RegistroEmpleado')
	DROP VIEW vw_RegistroEmpleado;
GO

CREATE VIEW vw_RegistroEmpleado
AS
	SELECT
			e.numero_empleado		[Numero de empleado], 
			e.nombre				[Nombre], 
			e.apellido_paterno		[Apellido paterno],
			e.apellido_materno		[Apellido materno],
			e.fecha_nacimiento		[Fecha de nacimiento],
			e.curp					[CURP],
			e.nss					[NSS],
			e.rfc					[RFC],
			a.calle					[Calle],
			a.numero				[Numero],
			a.colonia				[Colonia],
			a.ciudad				[Municipio],
			a.estado				[Estado],
			a.codigo_postal			[Codigo postal],
			b.nombre				[Banco],
			b.id_banco				[ID Banco],
			e.numero_cuenta			[Numero de cuenta],
			e.correo_electronico	[Correo electronico],
			e.contrasena			[Contraseña],
			d.nombre				[Departamento],
			d.id_departamento		[ID Departamento],
			p.nombre				[Puesto],
			p.id_puesto				[ID Puesto],
			e.fecha_contratacion	[Fecha de contratacion],
			e.sueldo_diario			[Sueldo diario],
			d.sueldo_base			[Sueldo base],
			p.nivel_salarial		[Nivel salarial],
			e.activo				[Activo],
			d.id_empresa			[ID Empresa]
	FROM 
			empleados AS e
			INNER JOIN domicilios AS a
			ON a.id_domicilio = e.domicilio
			INNER JOIN bancos AS b
			ON b.id_banco = e.banco
			INNER JOIN departamentos AS d
			ON d.id_departamento = e.id_departamento
			INNER JOIN puestos AS p
			ON p.id_puesto = e.id_puesto;
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_ReporteGeneralNomina')
	DROP VIEW vw_ReporteGeneralNomina;
GO

CREATE VIEW vw_ReporteGeneralNomina
AS 
	SELECT
			d.nombre [Departamento],
			p.nombre [Puesto],
			CONCAT(e.apellido_paterno, ' ', e.apellido_materno, ' ', e.nombre) [Nombre del empleado],
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
			ON n.id_puesto = p.id_puesto
	UNION
	SELECT
			d.nombre [Departamento],
			p.nombre [Puesto],
			CONCAT(e.apellido_paterno, ' ', e.apellido_materno, ' ', e.nombre) [Nombre del empleado],
			e.fecha_contratacion [Fecha de ingreso],
			DATEDIFF(HOUR, fecha_nacimiento, DATEFROMPARTS(YEAR(dbo.OBTENERFECHAACTUAL(d.id_empresa)), MONTH(dbo.OBTENERFECHAACTUAL(d.id_empresa)), dbo.GETMONTHLENGTH(YEAR(dbo.OBTENERFECHAACTUAL(d.id_empresa)), MONTH(dbo.OBTENERFECHAACTUAL(d.id_empresa))))) / 8766 [Edad], 
			e.sueldo_diario [Sueldo diario],
			dbo.OBTENERFECHAACTUAL(d.id_empresa) [Fecha]
	FROM
			empleados AS e
			INNER JOIN departamentos AS d
			ON e.id_departamento = d.id_departamento
			INNER JOIN puestos AS p
			ON e.id_puesto = p.id_puesto
	WHERE
			e.activo = 1
GO



-- Todas las posibles combinaciones de departamentos y fechas hasta la ultima fecha de nomina
IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosFechas')
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
			dbo.PRIMERDIAFECHA(d.fecha_creacion) <= dbo.PRIMERDIAFECHA(n.fecha) AND 
			(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(n.fecha) OR d.fecha_eliminacion IS NULL)
GO



-- Todas las posibles combinaciones de departamentos, puestos y fechas hasta la ultima fecha de nomina
IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosPuestosFechas')
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
			(dbo.PRIMERDIAFECHA(d.fecha_eliminacion) > dbo.PRIMERDIAFECHA(n.fecha) OR d.fecha_eliminacion IS NULL) 
			AND
			dbo.PRIMERDIAFECHA(p.fecha_creacion) <= dbo.PRIMERDIAFECHA(n.fecha) AND
			(dbo.PRIMERDIAFECHA(p.fecha_eliminacion) > dbo.PRIMERDIAFECHA(n.fecha) OR p.fecha_eliminacion IS NULL)
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_EmpleadosDepartamentosCantidad')
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
			INNER JOIN nominas AS n
			ON d.id_departamento = n.id_departamento
	GROUP BY
			d.id_departamento, d.nombre, n.fecha
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_EmpleadosDepartamentosPuestosCantidad')
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
	WHERE
			d.activo = 1
			AND p.activo = 1
	GROUP BY
			d.id_departamento, p.id_puesto, d.nombre, p.nombre, n.fecha
GO



-- La cantidad de departamentos en la fecha actual
IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosFechaActual')
	DROP VIEW vw_DepartamentosFechaActual;
GO

CREATE VIEW vw_DepartamentosFechaActual
AS
	SELECT
			d.id_empresa,
			d.id_departamento,
			d.nombre,
			COUNT(e.numero_empleado) [Cantidad]
	FROM
			departamentos AS d
			LEFT JOIN empleados AS e
			ON d.id_departamento = e.id_departamento
			AND e.activo = 1
	WHERE
			d.activo = 1
	GROUP BY
			d.id_empresa, d.id_departamento, d.nombre;
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosPuestosFechaActual')
	DROP VIEW vw_DepartamentosPuestosFechaActual;
GO

CREATE VIEW vw_DepartamentosPuestosFechaActual
AS
	SELECT
			d.id_empresa,
			d.id_departamento,
			p.id_puesto,
			d.nombre [Departamento],
			p.nombre [Puesto],
			COUNT(e.numero_empleado) [Cantidad]
	FROM
			departamentos AS d
			INNER JOIN puestos AS p
			ON d.id_empresa = p.id_empresa
			LEFT JOIN empleados AS e
			ON d.id_departamento = e.id_departamento 
			AND p.id_puesto = e.id_puesto 
			AND e.activo = 1
	WHERE
			d.activo = 1 AND p.activo = 1
	GROUP BY
			d.id_empresa, d.id_departamento, p.id_puesto, d.nombre, p.nombre
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_Headcounter1')
	DROP VIEW vw_Headcounter1;
GO

CREATE VIEW vw_Headcounter1
AS
	SELECT
			dpf.id_empresa [ID Empresa],
			dpf.id_departamento [ID Departamento],
			dpf.Departamento [Departamento],
			dpf.id_puesto [ID Puesto],
			dpf.Puesto [Puesto],
			ISNULL(dpf2.Cantidad, 0) [Cantidad],
			dpf.fecha [Fecha]
	FROM
			vw_DepartamentosPuestosFechas AS dpf
			LEFT JOIN vw_EmpleadosDepartamentosPuestosCantidad AS dpf2
			ON dpf.id_departamento = dpf2.id_departamento AND
			dpf.id_puesto = dpf2.id_puesto AND
			dbo.PRIMERDIAFECHA(dpf.fecha) = dbo.PRIMERDIAFECHA(dpf2.fecha)
	UNION
	SELECT
			id_empresa [ID Empresa],
			id_departamento [ID Departamento],
			[Departamento],
			id_puesto [ID Puesto],
			[Puesto],
			[Cantidad],
			dbo.OBTENERFECHAACTUAL(id_empresa) [Fecha]
	FROM
			vw_DepartamentosPuestosFechaActual
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_Headcounter2')
	DROP VIEW vw_Headcounter2;
GO

CREATE VIEW vw_Headcounter2
AS
	SELECT
			df.id_empresa [ID Empresa],
			df.id_departamento [ID Departamento],
			df.nombre [Departamento],
			ISNULL(df2.Cantidad, 0) [Cantidad],
			df.fecha [Fecha]
	FROM
			vw_DepartamentosFechas AS df
			LEFT JOIN vw_EmpleadosDepartamentosCantidad AS df2
			ON df.id_departamento = df2.id_departamento AND 
			dbo.PRIMERDIAFECHA(df.fecha) = dbo.PRIMERDIAFECHA(df2.fecha)
	UNION
	SELECT
			id_empresa [ID Empresa],
			id_departamento [ID Departamento],
			nombre [Departamento],
			Cantidad [Cantidad],
			dbo.OBTENERFECHAACTUAL(id_empresa) [Fecha]
	FROM
			vw_DepartamentosFechaActual
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_ReporteNomina')
	DROP VIEW vw_ReporteNomina;
GO

CREATE VIEW vw_ReporteNomina
AS 
	SELECT 
			d.nombre								[Departamento], 
			YEAR(d.fecha)							[Año], 
			ISNULL(DATENAME(MONTH, d.fecha), '')	[Mes],
			ISNULL(SUM(n.sueldo_bruto), 0)			[Sueldo bruto], 
			ISNULL(SUM(n.sueldo_neto), 0)			[Sueldo neto]
	FROM 
			nominas AS n
			RIGHT JOIN vw_DepartamentosFechas AS d
			ON n.id_departamento = d.id_departamento AND 
			dbo.PRIMERDIAFECHA(n.fecha) = dbo.PRIMERDIAFECHA(d.fecha)
	GROUP BY 
			d.nombre, d.fecha;
GO



IF EXISTS(SELECT name FROM sys.all_views WHERE name = 'vw_ReciboNomina')
	DROP VIEW vw_ReciboNomina;
GO

CREATE VIEW vw_ReciboNomina
AS

	SELECT
			c.razon_social [Nombre de empresa],
			c.rfc [RFC de empresa],
			c.registro_patronal [Registro patronal],
			CONCAT(do.calle, ' ', do.numero, ' ', do.colonia) [Domicilio fiscal parte 1],
			CONCAT(do.codigo_postal, ' ', do.ciudad, ' ', do.estado) [Domicilio fiscal parte 2],
			e.numero_empleado [Numero de empleado],
			CONCAT(e.apellido_paterno, ' ', e.apellido_materno, ', ', e.nombre) [Nombre de empleado],
			e.nss [Numero de seguro social],
			e.curp [CURP],
			e.rfc [RFC de empleado],
			e.fecha_contratacion [Fecha de ingreso],
			d.nombre [Departamento],
			p.nombre [Puesto],
			n.sueldo_diario [Sueldo diario],
			n.sueldo_bruto [Sueldo bruto],
			n.sueldo_neto [Sueldo neto],
			dbo.PRIMERDIAFECHA(n.fecha) [Periodo],
			DATEFROMPARTS(YEAR(n.fecha), MONTH(n.fecha), dbo.GETMONTHLENGTH(YEAR(n.fecha), MONTH(n.fecha))) AS [Periodo final],
			n.id_nomina AS [ID de nomina]
	FROM
			empleados AS e
			INNER JOIN nominas AS n
			ON e.numero_empleado = n.numero_empleado
			INNER JOIN departamentos AS d 
			ON n.id_departamento = d.id_departamento
			INNER JOIN puestos AS p
			ON n.id_puesto = p.id_puesto  
			INNER JOIN empresas AS c
			ON d.id_empresa = c.id_empresa
			INNER JOIN domicilios AS do
			ON c.domicilio_fiscal = do.id_domicilio
	WHERE
			e.activo = 1;

GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosPercepcionesAplicadas')
	DROP VIEW vw_DepartamentosPercepcionesAplicadas;
GO

CREATE VIEW vw_DepartamentosPercepcionesAplicadas
AS
SELECT
		IIF(n.id_departamento IS NULL, e.id_departamento, n.id_departamento) [id_departamento],
		pa.id_percepcion,
		pa.fecha,
		IIF(n.id_departamento IS NULL, COUNT(e.numero_empleado), COUNT(n.numero_empleado)) [Cantidad empleados]
FROM
		percepciones AS p
		LEFT JOIN percepciones_aplicadas AS pa
		ON p.id_percepcion = pa.id_percepcion
		LEFT JOIN nominas AS n
		ON pa.numero_empleado = n.numero_empleado AND
		pa.fecha = n.fecha
		LEFT JOIN empleados AS e
		ON pa.numero_empleado = e.numero_empleado
GROUP BY
		n.id_departamento, pa.id_percepcion, pa.fecha, p.tipo_duracion, e.id_departamento
HAVING
		p.tipo_duracion = 'S';
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosPercepcionesFechas')
	DROP VIEW vw_DepartamentosPercepcionesFechas;
GO

CREATE VIEW vw_DepartamentosPercepcionesFechas
AS
SELECT DISTINCT
		d.id_departamento,
		p.id_percepcion,
		pa.fecha
FROM
		departamentos AS d
		CROSS JOIN percepciones AS p
		CROSS JOIN percepciones_aplicadas AS pa
WHERE
		p.tipo_duracion = 'S'
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosDeduccionesAplicadas')
	DROP VIEW vw_DepartamentosDeduccionesAplicadas;
GO

CREATE VIEW vw_DepartamentosDeduccionesAplicadas
AS
SELECT
		IIF(n.id_departamento IS NULL, e.id_departamento, n.id_departamento) [id_departamento],
		da.id_deduccion,
		da.fecha,
		IIF(n.id_departamento IS NULL, COUNT(e.numero_empleado), COUNT(n.numero_empleado)) [Cantidad empleados]
FROM
		deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion
		LEFT JOIN nominas AS n
		ON da.numero_empleado = n.numero_empleado AND
		da.fecha = n.fecha
		LEFT JOIN empleados AS e
		ON da.numero_empleado = e.numero_empleado
GROUP BY
		n.id_departamento, da.id_deduccion, da.fecha, d.tipo_duracion, e.id_departamento
HAVING
		d.tipo_duracion = 'S';
GO



IF EXISTS (SELECT name FROM sys.all_views WHERE name = 'vw_DepartamentosDeduccionesFechas')
	DROP VIEW vw_DepartamentosDeduccionesFechas;
GO

CREATE VIEW vw_DepartamentosDeduccionesFechas
AS
SELECT DISTINCT
		d.id_departamento,
		de.id_deduccion,
		da.fecha
FROM
		departamentos AS d
		CROSS JOIN deducciones AS de
		CROSS JOIN deducciones_aplicadas AS da
WHERE
		de.tipo_duracion = 'S'
GO