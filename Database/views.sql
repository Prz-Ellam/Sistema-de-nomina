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








IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReporteGeneralNomina')
	DROP PROCEDURE sp_ReporteGeneralNomina;
GO

CREATE PROCEDURE sp_ReporteGeneralNomina(
	@fecha				DATE
)
AS

	SELECT [Departamento], [Puesto], [Nombre del empleado], [Fecha de ingreso], [Edad], [Salario diario] FROM vw_ReporteGeneralNomina
	WHERE DATEADD(day, -DAY([Fecha de ingreso]) + 1, [Fecha de ingreso]) <= DATEADD(day, -DAY(@fecha) + 1, @fecha);

GO










