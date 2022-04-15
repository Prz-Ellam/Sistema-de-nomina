USE sistema_de_nomina;



-- Usuarios
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Login')
	DROP PROCEDURE sp_Login;
GO

CREATE PROCEDURE sp_Login
	@correo_electronico			VARCHAR(60),
	@contrasena					VARCHAR(30)
AS

	SELECT id_administrador [ID], correo_electronico [Correo], 'Administrador' [Posicion] FROM administradores
	WHERE correo_electronico = @correo_electronico AND contrasena = @contrasena
	UNION
	SELECT numero_empleado [ID], correo_electronico [Correo], 'Empleado' [Posicion] FROM empleados
	WHERE correo_electronico = @correo_electronico AND contrasena = @contrasena;
	

GO


exec sp_Login 'a@a.com', '123';


-- Empresas
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarEmpresa')
	DROP PROCEDURE sp_AgregarEmpresa;
GO

CREATE PROCEDURE sp_AgregarEmpresa
	@razon_social				VARCHAR(30),
	@domicilio_fiscal			INT,
	@correo_electronico			VARCHAR(30),
	@rfc						VARCHAR(18),
	@registro_patronal			VARCHAR(30),
	@fecha_inicio				DATE
AS

	-- Solo puede haber una empresa para el proyecto, esta validacion impide crear mas
	IF (SELECT COUNT(0) FROM empresas) > 0
	BEGIN
		RAISERROR(15600, 1, 1, 'Error', 'Ya existe la empresa');
		RETURN;
	END;

	IF NOT EXISTS(SELECT 1 FROM domicilios WHERE id_domicilio = @domicilio_fiscal)
	BEGIN
		RAISERROR(15600, 1, 1, 'Error', 'Direccion no existe');
		RETURN;
	END

	INSERT INTO empresas(razon_social, domicilio_fiscal, correo_electronico, rfc, registro_patronal, fecha_inicio)
	VALUES(@razon_social, @domicilio_fiscal, @correo_electronico, @rfc, @registro_patronal, @fecha_inicio);
		
GO








-- Domicilios
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDomicilio')
	DROP PROCEDURE sp_AgregarDomicilio;
GO

CREATE PROCEDURE sp_AgregarDomicilio
	@calle				VARCHAR(30),
	@numero				VARCHAR(10),
	@colonia			VARCHAR(30),
	@ciudad				VARCHAR(30),
	@estado				VARCHAR(30),
	@codigo_postal		VARCHAR(5)
AS

	INSERT INTO domicilios(calle, numero, colonia, ciudad, estado, codigo_postal)
	VALUES(@calle, @numero, @colonia, @ciudad, @estado, @codigo_postal);
	SELECT SCOPE_IDENTITY();

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDomicilio')
	DROP PROCEDURE sp_ActualizarDomicilio;
GO

CREATE PROCEDURE sp_ActualizarDomicilio
	@id_domicilio		INT,
	@calle				VARCHAR(30),
	@numero				VARCHAR(10),
	@colonia			VARCHAR(30),
	@ciudad				VARCHAR(30),
	@estado				VARCHAR(30),
	@codigo_postal		VARCHAR(5)
AS

	UPDATE domicilios
	SET
	calle = ISNULL(@calle, calle),
	numero = ISNULL(@numero, numero),
	colonia = ISNULL(@colonia, colonia),
	ciudad = ISNULL(@ciudad, ciudad),
	estado = ISNULL(@estado, estado),
	codigo_postal = ISNULL(@codigo_postal, codigo_postal)
	WHERE id_domicilio = @id_domicilio;

GO












select*from domicilios;


-- Departamentos
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDepartamento')
	DROP PROCEDURE sp_AgregarDepartamento;
GO

CREATE PROCEDURE sp_AgregarDepartamento
	@nombre				VARCHAR(60),
	@sueldo_base		MONEY,
	@id_empresa			INT
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa)
		BEGIN
			RAISERROR(15600,1,1,'Error','No existe la empresa');
			RETURN;
		END;

	INSERT INTO departamentos(nombre, sueldo_base, id_empresa)
	VALUES (@nombre, @sueldo_base, @id_empresa);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDepartamento')
	DROP PROCEDURE sp_ActualizarDepartamento;
GO

CREATE PROCEDURE sp_ActualizarDepartamento
	@id_departamento			INT,
	@nombre						VARCHAR(60),
	@sueldo_base				MONEY
AS

	UPDATE departamentos
	SET
	nombre					= ISNULL(@nombre, nombre),
	sueldo_base				= ISNULL(@sueldo_base, sueldo_base)
	WHERE id_departamento	= @id_departamento;

GO

/*
exec sp_EliminarDepartamento 1;

SELECT * FROM master.dbo.sysmessages;


sys.sp_addmessage
@msgnum = 50001,
@severity = 11,
@msgtext = 'No se puede eliminar Departamento porque un empleado lo usa'
GO
*/

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartamento')
	DROP PROCEDURE sp_EliminarDepartamento;
GO

CREATE PROCEDURE sp_EliminarDepartamento
	@id_departamento					INT
AS

	IF (SELECT COUNT(0) FROM empleados WHERE id_departamento = @id_departamento AND activo = 1) > 0
		BEGIN
			RAISERROR ('No se puede eliminar el departamento porque un empleado pertenece a el', 11, 1)
			RETURN;
		END
	
	UPDATE departamentos
	SET
	activo = 0
	WHERE id_departamento = @id_departamento;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDepartamentos')
	DROP PROCEDURE sp_LeerDepartamentos;
GO

CREATE PROCEDURE sp_LeerDepartamentos
AS

	SELECT id_departamento, nombre, sueldo_base
	FROM departamentos
	WHERE activo = 1;

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_FiltrarDepartamentos')
	DROP PROCEDURE sp_FiltrarDepartmentos;
GO

CREATE PROCEDURE sp_FiltrarDepartmentos
	@filtro						VARCHAR(100)
AS

	SELECT id_departamento, nombre, sueldo_base
	FROM departamentos
	WHERE activo = 1 AND nombre LIKE CONCAT('%', @filtro, '%');

GO








-- Puestos
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarPuesto')
	DROP PROCEDURE sp_AgregarPuesto;
GO

CREATE PROCEDURE sp_AgregarPuesto
	@nombre					VARCHAR(60),
	@nivel_salarial			FLOAT,
	@id_empresa				INT
AS

	IF NOT EXISTS (SELECT id_empresa FROM empresas WHERE id_empresa = @id_empresa)
		BEGIN
			RAISERROR('La empresa aún no existe', 11, 1);
			RETURN;
		END;

	INSERT INTO puestos(nombre, nivel_salarial, id_empresa)
	VALUES (@nombre, @nivel_salarial, @id_empresa);

GO





IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarPuesto')
	DROP PROCEDURE sp_ActualizarPuesto;
GO

CREATE PROCEDURE sp_ActualizarPuesto
	@id_puesto					INT,
	@nombre				VARCHAR(60),
	@nivel_salarial			FLOAT
AS

	UPDATE puestos
	SET
	nombre =			ISNULL(@nombre, nombre),
	nivel_salarial =	ISNULL(@nivel_salarial, nivel_salarial)
	WHERE id_puesto = @id_puesto;

GO







IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarPuesto')
	DROP PROCEDURE sp_EliminarPuesto;
GO

CREATE PROCEDURE sp_EliminarPuesto
	@id_puesto					INT
AS

	IF (SELECT COUNT(0) FROM empleados WHERE id_puesto = @id_puesto AND activo = 1) > 0
		BEGIN
			RAISERROR ('No se puede eliminar el puesto porque un empleado pertenece a el', 11, 1)
			RETURN;
		END
	
	UPDATE puestos
	SET
	activo = 0
	WHERE id_puesto = @id_puesto;

GO






IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPuestos')
	DROP PROCEDURE sp_LeerPuestos;
GO

CREATE PROCEDURE sp_LeerPuestos

AS

	SELECT id_puesto, nombre, nivel_salarial
	FROM puestos
	WHERE activo = 1;


GO




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
	@domicilio				INT,
	@banco					INT,
	@numero_cuenta			INT,
	@correo_electronico		VARCHAR(60),
	@contrasena				VARCHAR(30),
	@id_departamento		INT,
	@id_puesto				INT,
	@fecha_contratacion		DATE
)
AS

	DECLARE @sueldo_diario	MONEY
	SET @sueldo_diario = (SELECT sueldo_base FROM departamentos WHERE id_departamento = @id_departamento) *
						(SELECT nivel_salarial FROM puestos WHERE id_puesto = @id_puesto);

	INSERT INTO empleados(nombre, apellido_paterno, apellido_materno, fecha_nacimiento, curp, nss, rfc,
		domicilio, banco, numero_cuenta, correo_electronico, contrasena, id_departamento, id_puesto, sueldo_diario, fecha_contratacion)
	VALUES (@nombre, @apellido_paterno, @apellido_materno, @fecha_nacimiento, @curp, @nss, @rfc, @domicilio, 
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








IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarPercepcion')
	DROP PROCEDURE sp_AgregarPercepcion;
GO

CREATE PROCEDURE sp_AgregarPercepcion(
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT
) 
AS

	INSERT INTO percepciones(nombre, tipo_monto, fijo, porcentual)
	VALUES(@nombre, @tipo_monto, @fijo, @porcentual);

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarPercepcion')
	DROP PROCEDURE sp_ActualizarPercepcion;
GO

CREATE PROCEDURE sp_ActualizarPercepcion(
	@id_percepcion		INT,
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT
)
AS

	UPDATE percepciones
	SET
	nombre = ISNULL(@nombre, nombre),
	tipo_monto = ISNULL(@tipo_monto, tipo_monto),
	fijo = ISNULL(@fijo, fijo),
	porcentual = ISNULL(@porcentual, porcentual)
	WHERE id_percepcion = @id_percepcion;

GO





IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarPercepcion')
	DROP PROCEDURE sp_EliminarPercepcion;
GO

CREATE PROCEDURE sp_EliminarPercepcion(
	@id_percepcion		INT
)
AS

	UPDATE percepciones
	SET
	activo = 0
	WHERE id_percepcion = @id_percepcion;

GO








IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepciones')
	DROP PROCEDURE sp_LeerPercepciones;
GO

CREATE PROCEDURE sp_LeerPercepciones
AS

	SELECT id_percepcion, nombre, tipo_monto, ISNULL(fijo, 0), ISNULL(porcentual, 0)
	FROM percepciones
	WHERE activo = 1;

GO






IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarDeduccion')
	DROP PROCEDURE sp_AgregarDeduccion;
GO

CREATE PROCEDURE sp_AgregarDeduccion(
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT
) 
AS

	INSERT INTO deducciones(nombre, tipo_monto, fijo, porcentual)
	VALUES(@nombre, @tipo_monto, @fijo, @porcentual);

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDeduccion')
	DROP PROCEDURE sp_ActualizarDeduccion;
GO

CREATE PROCEDURE sp_ActualizarDeduccion(
	@id_deduccion		INT,
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT
)
AS

	UPDATE deducciones
	SET
	nombre = ISNULL(@nombre, nombre),
	tipo_monto = ISNULL(@tipo_monto, tipo_monto),
	fijo = ISNULL(@fijo, fijo),
	porcentual = ISNULL(@porcentual, porcentual)
	WHERE id_deduccion = @id_deduccion;

GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDeduccion')
	DROP PROCEDURE sp_EliminarDeduccion;
GO

CREATE PROCEDURE sp_EliminarDeduccion(
	@id_deduccion		INT
)
AS

	UPDATE deducciones
	SET
	activo = 0
	WHERE id_deduccion = @id_deduccion;

GO







IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeducciones')
	DROP PROCEDURE sp_LeerDeducciones;
GO

CREATE PROCEDURE sp_LeerDeducciones
AS

	SELECT id_deduccion [ID], nombre [Nombre], tipo_monto [Tipo de monto], fijo [Fijo], porcentual [Porcentual]
	FROM deducciones
	WHERE activo = 1;

GO



INSERT INTO empleados(nombre, apellido_paterno, apellido_materno, fecha_nacimiento, curp, nss, rfc, domicilio, banco,
numero_cuenta, correo_electronico, contrasena, sueldo_diario, fecha_contratacion, id_departamento, id_puesto)
VALUES('Eliam', 'Rodríguez', 'Pérez', '20011026', 'A', 'B', 'C', 1, 1, 1, 'PerezAlex088@outlook.com', '123', 1, '20211026', 1, 1);


SELECT*FROM empleados;

INSERT INTO bancos(nombre)
VALUES('Santander');








IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarEmpleadoPercepcion')
	DROP PROCEDURE sp_ApplyEmployeeDeduction;
GO

CREATE PROCEDURE sp_ApplyEmployeeDeduction(
	@numero_empleado			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	INSERT INTO percepciones_aplicadas(numero_empleado, id_percepcion, fecha)
	VALUES(@numero_empleado, @id_percepcion, @fecha);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarEmpleadoDeduccion')
	DROP PROCEDURE sp_AplicarEmpleadoDeduccion;
GO

CREATE PROCEDURE sp_AplicarEmpleadoDeduccion(
	@numero_empleado			INT,
	@id_deduccion				INT,
	@fecha						DATE
)
AS

	INSERT INTO deducciones_aplicadas(numero_empleado, id_deduccion, fecha)
	VALUES(@numero_empleado, @id_deduccion, @fecha);

GO









IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_GenerarNomina')
	DROP PROCEDURE sp_GenerarNomina;
GO

CREATE PROCEDURE sp_GenerarNomina(
	@numero_empleado	INT,
	@fecha			DATE
)
AS

	DECLARE @anio		INT;
	DECLARE @mes		INT;
	DECLARE @dias		INT;

	SET @anio = YEAR(@fecha);
	SET @mes = MONTH(@fecha);
	SET @dias =  dbo.GETMONTHLENGTH(@anio, @mes);

	--SELECT @year, @month, @days;
	/*
	DECLARE @daily_salary	MONEY;
	SET @daily_salary = (SELECT D.base_salary * P.wage_level 
						FROM employees
						JOIN departments AS D
						ON employees.department_id = D.id
						JOIN positions AS P
						ON employees.position_id = P.id
						WHERE employees.employee_number = @employee_number);

	DECLARE @gross_salary	MONEY;
	SET @gross_salary = @daily_salary * @days;

	SELECT @days AS [Dias], @daily_salary AS [Salario Diario], @gross_salary AS [Sueldo bruto]


	SELECT perceptions.id
	FROM perceptions
	JOIN employees_perceptions
	ON employees_perceptions.perception_id = perceptions.id
	JOIN employees
	ON employees_perceptions.employee_id = employees.employee_number
	WHERE YEAR(employees_perceptions.actual_date) = @year
	AND MONTH(employees_perceptions.actual_date) = @month;

	SELECT deductions.id
	FROM deductions
	JOIN employees_deductions
	ON employees_deductions.deduction_id = deduction_id
	JOIN employees
	ON employees_deductions.employee_id = employees.employee_number
	WHERE YEAR(employees_deductions.actual_date) = @year
	AND MONTH(employees_deductions.actual_date) = @month;


	DECLARE @total_perception_fixed	MONEY;
	SET @total_perception_fixed = (SELECT SUM(p.fixed) FROM perceptions AS p
	JOIN employees_perceptions AS ep
	ON ep.perception_id = p.id
	JOIN employees AS e
	ON ep.employee_id = e.employee_number
	WHERE ep.employee_id = @employee_number AND ep.perception_id = p.id AND ep.actual_date = @gen_date
	AND amount_type = 'F');

	DECLARE @total_perception_percentage	FLOAT;
	SET @total_perception_percentage = (SELECT SUM(p.percentage) FROM perceptions AS p
	JOIN employees_perceptions AS ep
	ON ep.perception_id = p.id
	JOIN employees AS e
	ON ep.employee_id = e.employee_number
	WHERE ep.employee_id = @employee_number AND ep.perception_id = p.id AND ep.actual_date = @gen_date
	AND amount_type = 'P');

	DECLARE @total_deduction_fixed		MONEY;
	DECLARE @total_deduction_percentage	FLOAT;

	DECLARE @net_salary	MONEY;
	SET @net_salary = @gross_salary + @total_perception_fixed + (@total_perception_percentage * @gross_salary);
	/*- @total_deduction_fixed - (@total_deduction_fixed * @total_deduction_percentage);*/


	SELECT ROUND(@net_salary, 2);
	*/
GO