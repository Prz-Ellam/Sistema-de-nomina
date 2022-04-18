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

SELECT dbo.DIASTRABAJADOSEMPLEADO('20220701', 1);