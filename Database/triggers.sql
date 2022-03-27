CREATE TRIGGER tr_perception_insert
ON perceptions
AFTER INSERT
AS

	DECLARE @amount_type	CHAR(1);
	SET @amount_type = (SELECT amount_type FROM inserted);

	IF @amount_type = 'F'
		UPDATE perceptions
		SET
		percentage = 0
		FROM perceptions
		INNER JOIN inserted ON perceptions.id = inserted.id 
		WHERE
		perceptions.id = inserted.id;
	ELSE IF @amount_type = 'P'
		UPDATE perceptions
		SET
		fixed = 0
		FROM perceptions
		INNER JOIN inserted ON perceptions.id = inserted.id 
		WHERE
		perceptions.id = inserted.id;

GO


-- tr_perceptions_update
-- tr_deductions_insert
-- tr_deductions_update