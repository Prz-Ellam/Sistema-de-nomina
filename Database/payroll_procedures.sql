USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_CrearNomina')
	DROP PROCEDURE sp_CrearNomina;
GO

CREATE PROCEDURE sp_CrearNomina(
	@id_empresa				INT,
	@fecha					DATE
)
AS

	DECLARE @fecha_nomina DATE;
	SET @fecha_nomina = dbo.OBTENERFECHAACTUAL(@id_empresa);
	
	IF (DATEFROMPARTS(YEAR(@fecha_nomina), MONTH(@fecha_nomina), 1) <> DATEFROMPARTS(YEAR(@fecha), MONTH(@fecha), 1))
		BEGIN
			RAISERROR('No se puede iniciar una nómina fuera del periodo actual de nómina', 11, 1);
			RETURN;
		END

	INSERT INTO percepciones_aplicadas(
			numero_empleado,
			id_percepcion,
			cantidad, 
			fecha
	)
	SELECT 
			e.numero_empleado, 
			p.id_percepcion, 
			IIF(p.tipo_monto = 'F', p.fijo, p.porcentual * e.sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado)),
			@fecha
	FROM 
			empleados AS e
			CROSS JOIN percepciones AS p
	WHERE 
			DATEFROMPARTS(YEAR(e.fecha_contratacion), MONTH(e.fecha_contratacion), 1) <= @fecha AND e.activo = 1 AND p.tipo_duracion = 'B';



	INSERT INTO deducciones_aplicadas(
			numero_empleado, 
			id_deduccion,
			cantidad,
			fecha
	)
	SELECT 
			e.numero_empleado, 
			d.id_deduccion, 
			IIF(d.tipo_monto = 'F', d.fijo, d.porcentual * e.sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado)),
			@fecha
	FROM 
			empleados AS e
			CROSS JOIN deducciones AS d
	WHERE 
			DATEFROMPARTS(YEAR(e.fecha_contratacion), MONTH(e.fecha_contratacion), 1) <= @fecha AND e.activo = 1 AND d.tipo_duracion = 'B';

GO














IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_GenerarNomina')
	DROP PROCEDURE sp_GenerarNomina;
GO

-- La empresa a la que pertenecen
CREATE PROCEDURE sp_GenerarNomina(
	@id_empresa				INT,
	@fecha					DATE
)
AS

	DECLARE @fecha_nomina DATE;
	SET @fecha_nomina = dbo.OBTENERFECHAACTUAL(@id_empresa);

	IF (DATEFROMPARTS(YEAR(@fecha_nomina), MONTH(@fecha_nomina), 1) <> DATEFROMPARTS(YEAR(@fecha), MONTH(@fecha), 1))
		BEGIN
			RAISERROR('No se puede generar la nómina fuera del periodo actual de nómina', 11, 1);
			RETURN;
		END


	INSERT INTO nominas(
			sueldo_diario, 
			sueldo_bruto, 
			sueldo_neto, 
			banco, 
			numero_cuenta, 
			fecha, 
			numero_empleado, 
			id_departamento, 
			id_puesto)
	SELECT
			e.sueldo_diario,
			e.sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado),
			dbo.TOTALPERCEPCIONES(@fecha, e.numero_empleado) - dbo.TOTALDEDUCCIONES(@fecha, e.numero_empleado),
			e.banco,
			e.numero_cuenta,
			@fecha,
			e.numero_empleado,
			e.id_departamento,
			e.id_puesto
	FROM
			empleados AS e
			JOIN departamentos AS d
			ON d.id_departamento = e.id_departamento
	WHERE
			d.id_empresa = @id_empresa 
			AND dbo.PRIMERDIAFECHA(e.fecha_contratacion) <= dbo.PRIMERDIAFECHA(@fecha) 
			AND e.activo = 1;
	
	


	/*
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
	SET @sueldo_neto = @total_percepciones - @total_deducciones;
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
	*/
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
		SELECT fecha_inicio [Fecha]
		FROM empresas
		WHERE id_empresa = @id_empresa;
	ELSE
		RAISERROR('La empresa aun no existe', 11, 1);
		RETURN;

GO


CREATE FUNCTION OBTENERFECHAACTUAL(
	@id_empresa				INT
)
RETURNS DATE
AS
	BEGIN

	DECLARE @fecha		DATE;

	IF (EXISTS(SELECT id_nomina FROM nominas))	
		SET @fecha = (SELECT DISTINCT TOP 1 DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(fecha), MONTH(fecha), 1)) [Fecha]
		FROM nominas AS n
		JOIN empleados AS e
		ON n.numero_empleado = e.numero_empleado
		JOIN departamentos AS d
		ON d.id_departamento = e.id_departamento
		WHERE d.id_empresa = @id_empresa
		ORDER BY [Fecha] DESC);
	ELSE IF (EXISTS(SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa))
		SET @fecha = (SELECT fecha_inicio [Fecha]
		FROM empresas
		WHERE id_empresa = @id_empresa);
	ELSE
		SET @fecha = NULL;

	RETURN @fecha;
	END
GO

DECLARE @a BIT;
SET @a = sp_NominaEnProceso;


-- Si han sido creadas percepciones y deducciones (las basicas que son obligatorias) significa que una nomina está en proceso, caso contrario
-- si no hay ninguna creada se debe tomar como que la nomina no esta en proceso
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_NominaEnProceso')
	DROP PROCEDURE sp_NominaEnProceso;
GO

CREATE PROCEDURE sp_NominaEnProceso(
	@id_empresa				INT
)
AS
BEGIN

	SELECT 
			COUNT(0) 
	FROM
			(SELECT fecha FROM percepciones_aplicadas
					UNION
			SELECT fecha FROM deducciones_aplicadas) AS U
	WHERE
			fecha = dbo.OBTENERFECHAACTUAL(@id_empresa);

END
GO


EXEC sp_NominaEnProceso 1;
SELECT dbo.OBTENERFECHAACTUAL(1);


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerReciboNomina')
	DROP PROCEDURE sp_ObtenerReciboNomina;
GO

CREATE PROCEDURE sp_ObtenerReciboNomina(
	@fecha				DATE
)
AS

SELECT 
		[Nombre de empresa], 
		[RFC de empresa], 
		[Registro patronal], 
		[Domicilio fiscal parte 1], 
		[Domicilio fiscal parte 2], 
		[Numero de empleado], 
		[Nombre de empleado], 
		[Numero de seguro social], 
		[CURP], 
		[RFC de empleado],
		[Fecha de ingreso],
		[Departamento], 
		[Puesto], 
		[Sueldo diario],
		[Sueldo bruto], 
		[Sueldo neto], 
		[Periodo], 
		[ID de nomina] 
FROM 
		vw_ReciboNomina
WHERE
		[Periodo] = DATEFROMPARTS(YEAR(@fecha), MONTH(@fecha), 1);

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
