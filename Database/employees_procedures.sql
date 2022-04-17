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

	EXEC sp_ActualizarDomicilio @id_domicilio, @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal

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
	fecha_contratacion		= ISNULL(@fecha_contratacion, fecha_contratacion)
	WHERE numero_empleado = @numero_empleado AND activo = 1;

GO



select*From departamentos;

select*from puestos;






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










	SELECT 
	e.numero_empleado [Numero de empleado],
	CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) [Nombre de empleado],
	d.nombre [Departamento],
	p.nombre [Puesto],
	e.sueldo_diario [Sueldo diario]
	--SUM(pa.cantidad) [Si]
	FROM empleados AS e
	JOIN departamentos AS d
	ON e.id_departamento = d.id_departamento
	JOIN puestos AS p
	ON e.id_puesto = p.id_puesto
	LEFT JOIN percepciones_aplicadas AS pa
	ON e.numero_empleado = pa.numero_empleado
	WHERE e.activo = 1;



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerEmpleadosNominas')
	DROP PROCEDURE sp_LeerEmpleadosNominas;
GO

CREATE PROCEDURE sp_LeerEmpleadosNominas(
	@fecha				DATE
)
AS
	SELECT 
	e.numero_empleado, 
	CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS Nombre, 
	d.nombre [Departamento],
	p.nombre [Puesto],
	e.sueldo_diario [Sueldo diario],
	dbo.GETMONTHLENGTH(YEAR(@fecha), MONTH(@fecha)) [Dias trabajados],
	e.sueldo_diario * dbo.GETMONTHLENGTH(YEAR(@fecha), MONTH(@fecha)) [Sueldo bruto],
	ISNULL(SUM(pa.cantidad), 0) [Total percepciones],
	ISNULL(SUM(da.cantidad), 0) [Total deducciones],
	(e.sueldo_diario * dbo.GETMONTHLENGTH(YEAR(@fecha), MONTH(@fecha))) + ISNULL(SUM(pa.cantidad), 0) - ISNULL(SUM(da.cantidad), 0) [Sueldo neto]
	FROM empleados AS e
	JOIN departamentos AS d
	ON e.id_departamento = d.id_departamento
	JOIN puestos AS p
	ON e.id_puesto = p.id_puesto
	LEFT JOIN percepciones_aplicadas AS pa
	ON e.numero_empleado = pa.numero_empleado AND YEAR(pa.fecha) = YEAR(@fecha) AND MONTH(pa.fecha) = MONTH(@fecha)
	LEFT JOIN deducciones_aplicadas AS da
	ON e.numero_empleado = da.numero_empleado
	GROUP BY e.numero_empleado, e.nombre, e.apellido_paterno, e.apellido_materno, d.nombre, p.nombre, e.sueldo_diario;
GO


EXEC sp_LeerEmpleadosNominas '20220401';


SELECT*FROM percepciones_aplicadas;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepcionesAplicadas')
	DROP PROCEDURE sp_LeerPercepcionesAplicadas;
GO

CREATE PROCEDURE sp_LeerPercepcionesAplicadas(
	@filtro						INT,
	@numero_empleado			INT,
	@fecha						DATE
)
AS

	IF @filtro = 1
		SELECT IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		p.id_percepcion, p.nombre, p.tipo_monto, p.fijo, p.porcentual 
		FROM percepciones AS p
		LEFT JOIN percepciones_aplicadas AS pa
		ON p.id_percepcion = pa.id_percepcion AND YEAR(pa.fecha) = YEAR(@fecha) AND MONTH(pa.fecha) = MONTH(@fecha) AND pa.numero_empleado = @numero_empleado;
	ELSE IF @filtro = 2
		SELECT IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		p.id_percepcion, p.nombre, p.tipo_monto, p.fijo, p.porcentual 
		FROM percepciones AS p
		LEFT JOIN percepciones_aplicadas AS pa
		ON p.id_percepcion = pa.id_percepcion AND YEAR(pa.fecha) = YEAR(@fecha) AND MONTH(pa.fecha) = MONTH(@fecha) AND pa.numero_empleado = @numero_empleado
		WHERE IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') = 'true';
	ELSE IF @filtro = 3
		SELECT IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		p.id_percepcion, p.nombre, p.tipo_monto, p.fijo, p.porcentual 
		FROM percepciones AS p
		LEFT JOIN percepciones_aplicadas AS pa
		ON p.id_percepcion = pa.id_percepcion AND YEAR(pa.fecha) = YEAR(@fecha) AND MONTH(pa.fecha) = MONTH(@fecha) AND pa.numero_empleado = @numero_empleado
		WHERE IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') = 'false';
	ELSE 
		RAISERROR('Filtro inválido', 11, 1);

GO

EXEC sp_LeerPercepcionesAplicadas 4, 3, '20220301'



SELECT da.id_deduccion_aplicada,d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
FROM deducciones_aplicadas AS da
RIGHT JOIN deducciones AS d
ON da.id_deduccion = d.id_deduccion
WHERE da.numero_empleado = 3;

