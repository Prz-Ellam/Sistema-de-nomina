USE sistema_de_nomina;



-- Usuarios
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Login')
	DROP PROCEDURE sp_Login;
GO

CREATE PROCEDURE sp_Login
	@tipo						CHAR(1),
	@correo_electronico			VARCHAR(60),
	@contrasena					VARCHAR(30)
AS

	IF @tipo = 'A'

		SELECT id_administrador, correo_electronico FROM administradores
		WHERE correo_electronico = @correo_electronico AND contrasena = @contrasena;

	ELSE IF @tipo = 'E'

		SELECT numero_empleado, correo_electronico FROM empleados
		WHERE correo_electronico = @correo_electronico AND contrasena = @contrasena;

GO





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
	IF (SELECT COUNT(1) FROM companies) > 0
	BEGIN
		RAISERROR(15600, 1, 1, 'Error', 'Ya existe la empresa');
		RETURN;
	END;

	IF NOT EXISTS(SELECT 1 FROM addresses WHERE id = @address)
	BEGIN
		RAISERROR(15600, 1, 1, 'Error', 'Direccion no existe');
		RETURN;
	END

	INSERT INTO companies(business_name, address, email, rfc, employer_registration, start_date)
	VALUES(@business_name, @address, @email, @rfc, @employer_registration, @start_date);
		
GO












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



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarDepartmento')
	DROP PROCEDURE sp_ActualizarDepartmento;
GO

CREATE PROCEDURE sp_ActualizarDepartmento
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
			RAISERROR(15600, 1, 1, 'Error', 'No existe la empresa');
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









IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepciones')
	DROP PROCEDURE sp_LeerPercepciones;
GO

CREATE PROCEDURE sp_LeerPercepciones
AS

	SELECT id_percepcion, nombre, tipo_monto, fijo, porcentual
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



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReadEmployees')
	DROP PROCEDURE sp_ReadEmployees;
GO

CREATE PROCEDURE sp_ReadEmployees

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
			E.fecha_contratacion [Fecha de contratacion]
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





