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