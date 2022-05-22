USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE name = 'PRIMERDIAFECHA' AND type = 'FN')
	DROP FUNCTION PRIMERDIAFECHA;
GO

CREATE FUNCTION PRIMERDIAFECHA(
	@fecha					DATE
)
RETURNS DATE
AS
	BEGIN
		RETURN DATEFROMPARTS(YEAR(@fecha), MONTH(@fecha), 1);
	END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'GETMONTHLENGTH' AND type = 'FN')
	DROP FUNCTION GETMONTHLENGTH;
GO

CREATE FUNCTION GETMONTHLENGTH(
	@year		INT,
	@month		TINYINT
)
RETURNS INT
AS
	BEGIN
		DECLARE @ADate DATE;

		SET @ADate = CONCAT(CAST(@year AS VARCHAR(100)), '-', CONCAT(CAST(@month AS VARCHAR(100)),
		'-', 1));

		RETURN DAY(EOMONTH(@ADate));
	END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'DIASTRABAJADOSEMPLEADO' AND type = 'FN')
	DROP FUNCTION DIASTRABAJADOSEMPLEADO;
GO

CREATE FUNCTION DIASTRABAJADOSEMPLEADO(
	@date					DATE,
	@numero_empleado		INT
)
RETURNS INT
AS
	BEGIN
		DECLARE @dias INT;
		SET @dias = (
		SELECT
				IIF(dbo.PRIMERDIAFECHA(@date) = dbo.PRIMERDIAFECHA(fecha_contratacion),
					dbo.GETMONTHLENGTH(YEAR(@date), MONTH(@date)) - DAY(fecha_contratacion) + 1, 
					dbo.GETMONTHLENGTH(YEAR(@date), MONTH(@date))
					) 
		FROM
				empleados
		WHERE
				numero_empleado = @numero_empleado);

		RETURN @dias;
	END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'TOTALPERCEPCIONES' AND type = 'FN')
	DROP FUNCTION TOTALPERCEPCIONES;
GO

CREATE FUNCTION TOTALPERCEPCIONES(
	@fecha					DATE,
	@numero_empleado		INT
)
RETURNS MONEY
AS
	BEGIN
		DECLARE @total MONEY;
		SET @total = (
		SELECT
				ISNULL(SUM(cantidad), 0)
		FROM
				percepciones_aplicadas 
		WHERE 
				numero_empleado = @numero_empleado 
				AND dbo.PRIMERDIAFECHA(fecha) = dbo.PRIMERDIAFECHA(@fecha));

		RETURN @total;
	END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'TOTALDEDUCCIONES' AND type = 'FN')
	DROP FUNCTION TOTALDEDUCCIONES;
GO

CREATE FUNCTION TOTALDEDUCCIONES(
	@fecha					DATE,
	@numero_empleado		INT
)
RETURNS MONEY
AS
	BEGIN
		DECLARE @total MONEY;
		SET @total = (
		SELECT
				ISNULL(SUM(cantidad), 0)
		FROM
				deducciones_aplicadas 
		WHERE
				numero_empleado = @numero_empleado 
				AND dbo.PRIMERDIAFECHA(fecha) = dbo.PRIMERDIAFECHA(@fecha));

		RETURN @total;
	END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'NOMINAENPROCESO' AND type = 'FN')
	DROP FUNCTION NOMINAENPROCESO;
GO

CREATE FUNCTION NOMINAENPROCESO(
	@id_empresa				INT
)
RETURNS BIT
AS
BEGIN
	
	RETURN (
	SELECT 
			COUNT(0) 
	FROM
			(SELECT fecha FROM percepciones_aplicadas
							UNION
			SELECT fecha FROM deducciones_aplicadas) AS U
	WHERE
			dbo.PRIMERDIAFECHA(fecha) = dbo.PRIMERDIAFECHA(dbo.OBTENERFECHAACTUAL(@id_empresa))
	);

END
GO



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'OBTENERFECHAACTUAL' AND type = 'FN')
	DROP FUNCTION OBTENERFECHAACTUAL;
GO

CREATE FUNCTION OBTENERFECHAACTUAL(
	@id_empresa				INT
)
RETURNS DATE
AS
	BEGIN

	DECLARE @fecha		DATE;

	IF EXISTS (SELECT id_nomina FROM nominas AS n INNER JOIN departamentos AS d ON n.id_departamento = d.id_departamento WHERE d.id_empresa = @id_empresa)
		SET @fecha = (SELECT DISTINCT TOP 1 DATEADD(MONTH, 1, dbo.PRIMERDIAFECHA(fecha)) [Fecha]
		FROM nominas AS n
		INNER JOIN empleados AS e
		ON n.numero_empleado = e.numero_empleado
		INNER JOIN departamentos AS d
		ON d.id_departamento = e.id_departamento
		WHERE d.id_empresa = @id_empresa
		ORDER BY [Fecha] DESC);
	ELSE IF EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa)
		SET @fecha = (SELECT fecha_inicio [Fecha]
		FROM empresas
		WHERE id_empresa = @id_empresa);
	ELSE
		SET @fecha = NULL;

	RETURN @fecha;
	END
GO