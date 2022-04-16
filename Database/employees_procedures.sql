USE sistema_de_nomina;


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
	@fecha_contratacion		DATE
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
			E.numero_cuenta [Numero de cuenta],
			E.correo_electronico [Correo electronico],
			D.nombre [Departamento],
			P.nombre [Puesto],
			E.fecha_contratacion [Fecha de contratacion],
			E.sueldo_diario [Sueldo diario]
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

