USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_GenerarNomina')
	DROP PROCEDURE sp_GenerarNomina;
GO

CREATE PROCEDURE sp_GenerarNomina(
	@numero_empleado	INT,
	@fecha			DATE
)
AS

	DECLARE @anio		INT;
	DECLARE @mes		INT;
	DECLARE @dias		INT;

	SET @anio = YEAR(@fecha);
	SET @mes = MONTH(@fecha);
	SET @dias =  dbo.DIASTRABAJADOSEMPLEADO(DATEFROMPARTS(@anio, @mes, 1), @numero_empleado);

	--SELECT @anio, @mes, @dias, sueldo_diario FROM empleados WHERE numero_empleado = @numero_empleado;
	DECLARE @total_percepciones MONEY;
	DECLARE @total_deducciones MONEY;

	SET @total_percepciones = dbo.TOTALPERCEPCIONES(DATEFROMPARTS(@anio, @mes, 1), @numero_empleado);
	SET @total_deducciones = dbo.TOTALDEDUCCIONES(DATEFROMPARTS(@anio, @mes, 1), @numero_empleado);

	DECLARE @sueldo_diario MONEY
	SET @sueldo_diario = (SELECT sueldo_diario FROM empleados WHERE numero_empleado = @numero_empleado AND activo = 1);

	DECLARE @sueldo_bruto MONEY;
	SET @sueldo_bruto = @sueldo_diario * @dias;
	SELECT @sueldo_bruto;

	DECLARE @sueldo_neto MONEY;
	SET @sueldo_neto = @sueldo_bruto + @total_percepciones - @total_deducciones;
	SELECT @sueldo_neto;

	INSERT INTO nominas(sueldo_diario, sueldo_bruto, sueldo_neto, banco, numero_cuenta, fecha, numero_empleado, id_departamento, id_puesto)
	SELECT @sueldo_diario, @sueldo_bruto, @sueldo_neto, banco, numero_cuenta, @fecha, @numero_empleado, id_departamento, id_puesto
	FROM empleados WHERE numero_empleado = @numero_empleado AND activo = 1;


	UPDATE percepciones_aplicadas
	SET
	id_nomina = IDENT_CURRENT('nominas')
	WHERE YEAR(fecha) = @anio AND MONTH(fecha) = @mes AND numero_empleado = @numero_empleado;

	UPDATE deducciones_aplicadas
	SET
	id_nomina = IDENT_CURRENT('nominas')
	WHERE YEAR(fecha) = @anio AND MONTH(fecha) = @mes AND numero_empleado = @numero_empleado;

GO


EXEC sp_ObtenerNominasPorFecha '20220401';

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerNominasPorFecha')
	DROP PROCEDURE sp_ObtenerNominasPorFecha;
GO

CREATE PROCEDURE sp_ObtenerNominasPorFecha(
	@fecha				DATE
)
AS

	SELECT n.numero_empleado, 
			CONCAT(e.nombre, ' ', + e.apellido_paterno, ' ', e.apellido_materno) [Nombre], 
			n.fecha,
			n.sueldo_neto,
			b.nombre,
			n.numero_cuenta
	FROM nominas AS n
	JOIN empleados AS e
	ON n.numero_empleado = e.numero_empleado
	JOIN bancos AS b
	ON n.banco = b.id_banco
	WHERE YEAR(fecha) = YEAR(@fecha) AND MONTH(fecha) = MONTH(@fecha);

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerFechaActual')
	DROP PROCEDURE sp_ObtenerFechaActual;
GO

CREATE PROCEDURE sp_ObtenerFechaActual
	@id_empresa				INT
AS

	IF (EXISTS(SELECT id_nomina FROM nominas))	
		SELECT DISTINCT TOP 1 DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(fecha), MONTH(fecha), 1)) [Fecha]
		FROM nominas AS n
		JOIN empleados AS e
		ON n.numero_empleado = e.numero_empleado
		JOIN departamentos AS d
		ON d.id_departamento = e.id_departamento
		WHERE d.id_empresa = @id_empresa
		ORDER BY [Fecha] DESC;
	ELSE IF (EXISTS(SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa))
		SELECT DATEFROMPARTS(YEAR(fecha_inicio), MONTH(fecha_inicio), 1) [Fecha]
		FROM empresas
		WHERE id_empresa = @id_empresa;
	ELSE
		RAISERROR('La empresa aun no existe', 11, 1);

GO




/*
CREATE DATABASE pruebas;
USE pruebas;

CREATE TABLE fechas(
	id		INT IDENTITY(1,1) PRIMARY KEY,
	fecha	DATE
)

DROP TABLE fechas;
INSERT INTO fechas(fecha) VALUES('20220401');
INSERT INTO fechas(fecha) VALUES('20220501');
INSERT INTO fechas(fecha) VALUES('20220501');
INSERT INTO fechas(fecha) VALUES('20220601');
INSERT INTO fechas(fecha) VALUES('20220601');
--INSERT INTO(fechas) VALUES('20220601');

SELECT DISTINCT TOP 1 fecha FROM fechas
ORDER BY fecha DESC;
*/