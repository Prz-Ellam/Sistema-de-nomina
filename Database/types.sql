USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sys.types WHERE is_table_type = 1 AND name ='Telefonos') 
	DROP TYPE dbo.Telefonos;
GO

CREATE TYPE dbo.Telefonos
AS TABLE
(
	row_count			INT,
	telefono			VARCHAR(12)
);