USE sistema_de_nomina;

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


SELECT dbo.GETMONTHLENGTH(2021, 10);


GO
CREATE FUNCTION DIASTRABAJADOSEMPLEADO(
	@date					DATE,
	@numero_empleado		INT
)
RETURNS INT
AS
	BEGIN
		DECLARE @dias		INT;
		SET @dias = (SELECT
		IIF(YEAR(@date) = YEAR(fecha_contratacion) AND MONTH(@date) = MONTH(fecha_contratacion),
		dbo.GETMONTHLENGTH(YEAR(@date), MONTH(@date)) - DAY(fecha_contratacion), 
		dbo.GETMONTHLENGTH(YEAR(@date), MONTH(@date))) 
		FROM empleados
		WHERE numero_empleado = @numero_empleado);

		RETURN @dias;
	END
GO



GO
CREATE FUNCTION TOTALPERCEPCIONES(
	@fecha					DATE,
	@numero_empleado		INT
)
RETURNS MONEY
AS
	BEGIN
		DECLARE @total MONEY;
		SET @total = (SELECT ISNULL(SUM(cantidad), 0) FROM percepciones_aplicadas 
		WHERE numero_empleado = @numero_empleado AND YEAR(fecha) = YEAR(@fecha) AND MONTH(fecha) = MONTH(@fecha));

		RETURN @total;
	END
GO



GO
CREATE FUNCTION TOTALDEDUCCIONES(
	@fecha					DATE,
	@numero_empleado		INT
)
RETURNS MONEY
AS
	BEGIN
		DECLARE @total MONEY;
		SET @total = (SELECT ISNULL(SUM(cantidad), 0) FROM deducciones_aplicadas 
		WHERE numero_empleado = @numero_empleado AND YEAR(fecha) = YEAR(@fecha) AND MONTH(fecha) = MONTH(@fecha));

		RETURN @total;
	END
GO