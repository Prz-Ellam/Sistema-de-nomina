USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarTelefono')
	DROP PROCEDURE sp_AgregarTelefono;
GO

CREATE PROCEDURE sp_AgregarTelefono
	@telefono			VARCHAR(12),
	@id_propietario		INT,
	@propietario		CHAR(1)
AS

	IF @propietario = 'E'

		INSERT INTO telefonos_empleados(telefono, numero_empleado)
		VALUES(@telefono, @id_propietario);

	ELSE IF @propietario = 'C'

		INSERT INTO telefonos_empresas(telefono, id_empresa)
		VALUES(@telefono, @id_propietario);

	ELSE 
		BEGIN
			RAISERROR('Tipo de propietario no válido', 11, 1);
		END

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerTelefonosEmpleados')
	DROP PROCEDURE sp_LeerTelefonosEmpleados;
GO

CREATE PROCEDURE sp_LeerTelefonosEmpleados(
	@numero_empleado			INT
)
AS
BEGIN

	SELECT 
			id_telefono_empleado [ID],
			telefono [Telefono],
			numero_empleado [Numero de empleado]
	FROM 
			telefonos_empleados
	WHERE 
			numero_empleado = @numero_empleado

END
GO

SELECT * FROM telefonos_empresas;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerTelefonosEmpresas')
	DROP PROCEDURE sp_LeerTelefonosEmpresas;
GO

CREATE PROCEDURE sp_LeerTelefonosEmpresas(
	@id_empresa				INT
)
AS
BEGIN

	SELECT 
			id_telefono_empresa [ID],
			telefono [Telefono],
			id_empresa [ID Empresa]
	FROM 
			telefonos_empresas
	WHERE 
			id_empresa = @id_empresa

END
GO













DROP TYPE dbo.Telefonos;

CREATE TYPE dbo.Telefonos
AS TABLE
(
	row_count	INT,
	telefono VARCHAR(12)
);

DECLARE @telefonos dbo.Telefonos;

INSERT INTO @telefonos(id_telefono, telefono)
VALUES(1, '1234', 2);
INSERT INTO @telefonos(id_telefono, telefono)
VALUES(2, '5678', 3);
INSERT INTO @telefonos(id_telefono, telefono)
VALUES(7, '90123', 4);

SELECT telefono, 1 FROM @telefonos;


DECLARE @min INT;
SET @min = (SELECT MIN(row_count) FROM @telefonos);

DECLARE @max INT;
SET @max = (SELECT MAX(row_count) FROM @telefonos)

DECLARE @count INT;
SET @count = @min;

WHILE (@count <= @max)
BEGIN

	SELECT * FROM @telefonos WHERE row_count = @count;
	SET @count = @count + 1;
END
