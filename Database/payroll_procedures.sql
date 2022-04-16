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
	SET @dias =  dbo.GETMONTHLENGTH(@anio, @mes);

	--SELECT @anio, @mes, @dias, sueldo_diario FROM empleados WHERE numero_empleado = @numero_empleado;
	DECLARE @total_percepciones MONEY;
	DECLARE @total_deducciones MONEY;

	SET @total_percepciones = (SELECT ISNULL(SUM(cantidad), 0) FROM percepciones_aplicadas WHERE YEAR(fecha) = @anio AND MONTH(fecha) = @mes AND numero_empleado = @numero_empleado);
	SET @total_deducciones = (SELECT ISNULL(SUM(cantidad), 0) FROM deducciones_aplicadas WHERE YEAR(fecha) = @anio AND MONTH(fecha) = @mes AND numero_empleado = @numero_empleado);

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





