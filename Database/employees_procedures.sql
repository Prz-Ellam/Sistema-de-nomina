USE sistema_de_nomina;

select*from telefonos_empleados;


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarEmpleado')
	DROP PROCEDURE sp_AgregarEmpleado;
GO

CREATE PROCEDURE sp_AgregarEmpleado(
	@nombre					VARCHAR(30),
	@apellido_paterno		VARCHAR(30),
	@apellido_materno		VARCHAR(30),
	@fecha_nacimiento		DATE,
	@curp					VARCHAR(20),
	@nss					VARCHAR(20),
	@rfc					VARCHAR(20),
	@calle					VARCHAR(30),
	@numero					VARCHAR(10),
	@colonia				VARCHAR(30),
	@ciudad					VARCHAR(30),
	@estado					VARCHAR(30),
	@codigo_postal			VARCHAR(5),
	@banco					INT,
	@numero_cuenta			INT,
	@correo_electronico		VARCHAR(60),
	@contrasena				VARCHAR(30),
	@id_departamento		INT,
	@id_puesto				INT,
	@fecha_contratacion		DATE,
	@telefonos				dbo.Telefonos READONLY
)
AS

	EXEC sp_AgregarDomicilio @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal;

	DECLARE @sueldo_diario	MONEY
	SET @sueldo_diario = (SELECT sueldo_base FROM departamentos WHERE id_departamento = @id_departamento) *
						(SELECT nivel_salarial FROM puestos WHERE id_puesto = @id_puesto);

	INSERT INTO empleados(nombre, apellido_paterno, apellido_materno, fecha_nacimiento, curp, nss, rfc,
		domicilio, banco, numero_cuenta, correo_electronico, contrasena, id_departamento, id_puesto, sueldo_diario, fecha_contratacion)
	VALUES (@nombre, @apellido_paterno, @apellido_materno, @fecha_nacimiento, @curp, @nss, @rfc, IDENT_CURRENT('Domicilios'), 
		@banco, @numero_cuenta, @correo_electronico, @contrasena, @id_departamento, @id_puesto, @sueldo_diario, @fecha_contratacion);


	DECLARE @min INT = (SELECT MIN(row_count) FROM @telefonos);
	DECLARE @max INT = (SELECT MAX(row_count) FROM @telefonos)
	DECLARE @count INT = @min;

	WHILE (@count <= @max)
	BEGIN

		DECLARE @numtel VARCHAR(12) = (SELECT telefono FROM @telefonos WHERE row_count = @count);

		INSERT INTO telefonos_empleados(telefono, numero_empleado)
		VALUES(@numtel, IDENT_CURRENT('empleados'));
		

		SET @count = @count + 1;
	END

GO


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarEmpleado')
	DROP PROCEDURE sp_ActualizarEmpleado;
GO

CREATE PROCEDURE sp_ActualizarEmpleado(
	@numero_empleado		INT,
	@nombre					VARCHAR(30),
	@apellido_paterno		VARCHAR(30),
	@apellido_materno		VARCHAR(30),
	@fecha_nacimiento		DATE,
	@curp					VARCHAR(20),
	@nss					VARCHAR(20),
	@rfc					VARCHAR(20),
	@calle					VARCHAR(30),
	@numero					VARCHAR(10),
	@colonia				VARCHAR(30),
	@ciudad					VARCHAR(30),
	@estado					VARCHAR(30),
	@codigo_postal			VARCHAR(5),
	@banco					INT,
	@numero_cuenta			INT,
	@correo_electronico		VARCHAR(60),
	@contrasena				VARCHAR(30),
	@id_departamento		INT,
	@id_puesto				INT,
	@fecha_contratacion		DATE
)
AS

	DECLARE @id_domicilio INT;
	SET @id_domicilio = (SELECT domicilio FROM empleados WHERE numero_empleado = @numero_empleado);

	EXEC sp_ActualizarDomicilio @id_domicilio, @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal;

	DECLARE @sueldo_diario	MONEY
	SET @sueldo_diario = (SELECT sueldo_base FROM departamentos WHERE id_departamento = @id_departamento) *
						(SELECT nivel_salarial FROM puestos WHERE id_puesto = @id_puesto);

	UPDATE empleados
	SET
	nombre					= ISNULL(@nombre, nombre),
	apellido_paterno		= ISNULL(@apellido_paterno, apellido_paterno),
	apellido_materno		= ISNULL(@apellido_materno, apellido_materno),
	fecha_nacimiento		= ISNULL(@fecha_nacimiento, fecha_nacimiento),
	curp					= ISNULL(@curp, curp),
	nss						= ISNULL(@nss, nss),
	rfc						= ISNULL(@rfc, rfc),
	banco					= ISNULL(@banco, banco),
	numero_cuenta			= ISNULL(@numero_cuenta, numero_cuenta),
	correo_electronico		= ISNULL(@correo_electronico, correo_electronico),
	contrasena				= ISNULL(@contrasena, contrasena),
	id_departamento			= ISNULL(@id_departamento, id_departamento),
	id_puesto				= ISNULL(@id_puesto, id_puesto),
	fecha_contratacion		= ISNULL(@fecha_contratacion, fecha_contratacion),
	sueldo_diario			= ISNULL(@sueldo_diario, sueldo_diario)
	WHERE numero_empleado = @numero_empleado AND activo = 1;

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarEmpleado')
	DROP PROCEDURE sp_ActualizarEmpleado;
GO

CREATE PROCEDURE sp_EliminarEmpleado(
	@numero_empleado		INT
)
AS

	UPDATE empleados
	SET
	activo = 0
	WHERE numero_empleado = @numero_empleado;

GO





IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerEmpleados')
	DROP PROCEDURE sp_LeerEmpleados;
GO

CREATE PROCEDURE sp_LeerEmpleados

AS

	SELECT	E.numero_empleado [ID], 
			E.nombre [Nombre], 
			E.apellido_paterno [Apellido paterno],
			E.apellido_materno [Apellido materno],
			E.fecha_nacimiento [Fecha de nacimiento],
			E.curp [CURP],
			E.nss [NSS],
			E.rfc [RFC],
			A.calle [Calle],
			A.numero [Numero],
			A.colonia [Colonia],
			A.ciudad [Municipio],
			A.estado [Estado],
			A.codigo_postal [Codigo postal],
			B.nombre [Banco],
			B.id_banco [ID Banco],
			E.numero_cuenta [Numero de cuenta],
			E.correo_electronico [Correo electronico],
			D.nombre [Departamento],
			D.id_departamento [ID Departamento],
			P.nombre [Puesto],
			P.id_puesto [ID Puesto],
			E.fecha_contratacion [Fecha de contratacion],
			E.sueldo_diario [Sueldo diario],
			D.sueldo_base [Sueldo base],
			P.nivel_salarial [Nivel salarial]
	FROM empleados AS E
	JOIN domicilios AS A
	ON A.id_domicilio = E.domicilio
	JOIN bancos AS B
	ON B.id_banco = E.banco
	JOIN departamentos AS D
	ON D.id_departamento = E.id_departamento
	JOIN puestos AS P
	ON P.id_puesto = E.id_puesto
	WHERE E.activo = 1;

GO


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerEmpleados')
	DROP PROCEDURE sp_ObtenerEmpleados;
GO

CREATE PROCEDURE sp_ObtenerEmpleados
AS

	SELECT numero_empleado FROM empleados WHERE activo = 1;

GO





IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerEmpleadoPorId')
	DROP PROCEDURE sp_ObtenerEmpleadoPorId;
GO

CREATE PROCEDURE sp_ObtenerEmpleadoPorId(
	@numero_empleado				INT
)
AS

	SELECT	E.numero_empleado [ID], 
			E.nombre [Nombre], 
			E.apellido_paterno [Apellido paterno],
			E.apellido_materno [Apellido materno],
			E.fecha_nacimiento [Fecha de nacimiento],
			E.curp [CURP],
			E.nss [NSS],
			E.rfc [RFC],
			A.calle [Calle],
			A.numero [Numero],
			A.colonia [Colonia],
			A.ciudad [Municipio],
			A.estado [Estado],
			A.codigo_postal [Codigo postal],
			B.nombre [Banco],
			B.id_banco [ID Banco],
			E.numero_cuenta [Numero de cuenta],
			E.correo_electronico [Correo electronico],
			D.nombre [Departamento],
			D.id_departamento [ID Departamento],
			P.nombre [Puesto],
			P.id_puesto [ID Puesto],
			E.fecha_contratacion [Fecha de contratacion],
			E.sueldo_diario [Sueldo diario],
			D.sueldo_base [Sueldo base],
			P.nivel_salarial [Nivel salarial]
	FROM empleados AS E
	JOIN domicilios AS A
	ON A.id_domicilio = E.domicilio
	JOIN bancos AS B
	ON B.id_banco = E.banco
	JOIN departamentos AS D
	ON D.id_departamento = E.id_departamento
	JOIN puestos AS P
	ON P.id_puesto = E.id_puesto
	WHERE E.activo = 1 AND numero_empleado = @numero_empleado;

GO
















EXEC sp_LeerEmpleadosNominas '20220401';


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerEmpleadosNominas')
	DROP PROCEDURE sp_LeerEmpleadosNominas;
GO

CREATE PROCEDURE sp_LeerEmpleadosNominas(
	@fecha				DATE
)
AS
	SELECT
	e.numero_empleado [Numero de Empleado], 
	CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS [Nombre de Empleado], 
	d.nombre [Departamento],
	p.nombre [Puesto],
	e.sueldo_diario [Sueldo diario],
	dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado) [Dias trabajados],
	e.sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado) [Sueldo bruto],
	dbo.TOTALPERCEPCIONES(@fecha, e.numero_empleado) [Total percepciones],
	dbo.TOTALDEDUCCIONES(@fecha, e.numero_empleado) [Total deducciones],
	(dbo.TOTALPERCEPCIONES(@fecha, e.numero_empleado) - dbo.TOTALDEDUCCIONES(@fecha, e.numero_empleado)) [Sueldo neto]
	FROM empleados AS e
	JOIN departamentos AS d
	ON e.id_departamento = d.id_departamento
	JOIN puestos AS p
	ON e.id_puesto = p.id_puesto
	WHERE e.activo = 1 AND DATEADD(day, -DAY(e.fecha_contratacion) + 1, e.fecha_contratacion) <= @fecha
	GROUP BY e.numero_empleado, e.nombre, e.apellido_paterno, e.apellido_materno, d.nombre, p.nombre, e.sueldo_diario, e.fecha_contratacion
	
GO




SELECT ISNULL(SUM(cantidad), 0) FROM deducciones_aplicadas;




SELECT d.nombre [Departamento], 
		p.nombre [Puesto], 
		COUNT(e.numero_empleado) [Cantidad de empleados]
FROM departamentos AS d
FULL OUTER JOIN puestos AS p
ON d.id_empresa = p.id_empresa
LEFT JOIN empleados AS e
ON e.id_departamento = d.id_departamento AND e.id_puesto = p.id_puesto AND 
DATEADD(day, -DAY(e.fecha_contratacion) + 1, e.fecha_contratacion) <= '20220401' AND
e.activo = 1
GROUP BY d.nombre, p.nombre;


SELECT d.nombre [Departamento], 
		COUNT(e.numero_empleado) [Cantidad de empleados] 
FROM departamentos AS d
LEFT JOIN empleados AS e
ON d.id_departamento = e.id_departamento
GROUP BY d.nombre;







CREATE DATABASE a;
USE a;

CREATE TABLE b(
	id		INT IDENTITY(1,1),
	b		INT UNIQUE
);

INSERT INTO b(b) VALUES(1);
INSERT INTO b(b) VALUES(95);
INSERT INTO b(b) VALUES(34);
INSERT INTO b(b) VALUES(95);
INSERT INTO b(b) VALUES(84);

SELECT * FROM b;


BEGIN TRAN;
	INSERT INTO b(b) VALUES(54);

	IF @@ERROR = 1
		ROLLBACK;