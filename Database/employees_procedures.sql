USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarEmpleado')
	DROP PROCEDURE sp_AgregarEmpleado;
GO

CREATE PROCEDURE sp_AgregarEmpleado(
	@nombre						VARCHAR(30),
	@apellido_paterno			VARCHAR(30),
	@apellido_materno			VARCHAR(30),
	@fecha_nacimiento			DATE,
	@curp						VARCHAR(18),
	@nss						VARCHAR(11),
	@rfc						VARCHAR(13),
	@calle						VARCHAR(30),
	@numero						VARCHAR(10),
	@colonia					VARCHAR(30),
	@ciudad						VARCHAR(30),
	@estado						VARCHAR(30),
	@codigo_postal				VARCHAR(5),
	@banco						INT,
	@numero_cuenta				VARCHAR(16),
	@correo_electronico			VARCHAR(60),
	@contrasena					VARCHAR(30),
	@id_departamento			INT,
	@id_puesto					INT,
	@fecha_contratacion			DATE,
	@telefonos					dbo.Telefonos READONLY
)
AS

	BEGIN TRY

		BEGIN TRAN

		DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO((SELECT id_empresa FROM departamentos WHERE id_departamento = @id_departamento));
		IF @status_nomina = 1
			BEGIN
				RAISERROR('No se puede añadir el puesto debido a que hay una nómina en proceso', 11, 1);
				RETURN;
			END

		IF EXISTS (SELECT id_administrador FROM administradores WHERE correo_electronico = @correo_electronico)
			BEGIN
				RAISERROR('El correo electrónico que ingresó ya está siendo utilizado por otro usuario', 11, 1);
				RETURN;
			END

		EXEC sp_AgregarDomicilio @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal;

		DECLARE @sueldo_diario	MONEY
		SET @sueldo_diario = (SELECT sueldo_base FROM departamentos WHERE id_departamento = @id_departamento) *
							(SELECT nivel_salarial FROM puestos WHERE id_puesto = @id_puesto);

		INSERT INTO empleados(
				nombre,
				apellido_paterno,
				apellido_materno,
				fecha_nacimiento,
				curp,
				nss,
				rfc,
				domicilio,
				banco,
				numero_cuenta,
				correo_electronico,
				contrasena,
				id_departamento,
				id_puesto,
				sueldo_diario,
				fecha_contratacion
		)
		VALUES(
				@nombre,
				@apellido_paterno,
				@apellido_materno,
				@fecha_nacimiento,
				@curp,
				@nss,
				@rfc,
				IDENT_CURRENT('Domicilios'), 
				@banco,
				@numero_cuenta,
				@correo_electronico,
				@contrasena,
				@id_departamento,
				@id_puesto,
				@sueldo_diario,
				@fecha_contratacion
		);

		DECLARE @min INT = (SELECT MIN(row_count) FROM @telefonos);
		DECLARE @max INT = (SELECT MAX(row_count) FROM @telefonos);
		DECLARE @count INT = @min;
		DECLARE @numero_empleado INT = IDENT_CURRENT('empleados');

		WHILE (@count <= @max)
		BEGIN

			DECLARE @numtel VARCHAR(12) = (SELECT telefono FROM @telefonos WHERE row_count = @count);
			EXEC sp_AgregarTelefono @numtel,  @numero_empleado, 'E';
			SET @count = @count + 1;

		END

		COMMIT TRAN

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN
		DECLARE @message VARCHAR(200) = ERROR_MESSAGE();
		RAISERROR(@message, 11, 1)
		RETURN;

	END CATCH

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ActualizarEmpleado')
	DROP PROCEDURE sp_ActualizarEmpleado;
GO

CREATE PROCEDURE sp_ActualizarEmpleado(
	@numero_empleado			INT,
	@nombre						VARCHAR(30),
	@apellido_paterno			VARCHAR(30),
	@apellido_materno			VARCHAR(30),
	@fecha_nacimiento			DATE,
	@curp						VARCHAR(18),
	@nss						VARCHAR(11),
	@rfc						VARCHAR(13),
	@calle						VARCHAR(30),
	@numero						VARCHAR(10),
	@colonia					VARCHAR(30),
	@ciudad						VARCHAR(30),
	@estado						VARCHAR(30),
	@codigo_postal				VARCHAR(5),
	@banco						INT,
	@numero_cuenta				VARCHAR(16),
	@correo_electronico			VARCHAR(60),
	@contrasena					VARCHAR(30),
	@id_departamento			INT,
	@id_puesto					INT,
	@fecha_contratacion			DATE,
	@telefonos					dbo.Telefonos READONLY
)
AS

	BEGIN TRY

		BEGIN TRAN

		DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO((SELECT id_empresa FROM departamentos WHERE id_departamento = @id_departamento));
		IF @status_nomina = 1
			BEGIN
				RAISERROR('No se puede añadir el puesto debido a que hay una nómina en proceso', 11, 1);
				RETURN;
			END

		IF EXISTS (SELECT id_administrador FROM administradores WHERE correo_electronico = @correo_electronico)
			BEGIN
				RAISERROR('El correo electrónico que ingresó ya está siendo utilizado por otro usuario', 11, 1);
				RETURN;
			END

		DECLARE @id_domicilio INT;
		SET @id_domicilio = (SELECT domicilio FROM empleados WHERE numero_empleado = @numero_empleado);

		EXEC sp_ActualizarDomicilio @id_domicilio, @calle, @numero, @colonia, @ciudad, @estado, @codigo_postal;

		DECLARE @sueldo_diario	MONEY
		SET @sueldo_diario = (SELECT sueldo_base FROM departamentos WHERE id_departamento = @id_departamento) *
							(SELECT nivel_salarial FROM puestos WHERE id_puesto = @id_puesto);

		UPDATE 
				empleados
		SET
				nombre				= ISNULL(@nombre, nombre),
				apellido_paterno	= ISNULL(@apellido_paterno, apellido_paterno),
				apellido_materno	= ISNULL(@apellido_materno, apellido_materno),
				fecha_nacimiento	= ISNULL(@fecha_nacimiento, fecha_nacimiento),
				curp				= ISNULL(@curp, curp),
				nss					= ISNULL(@nss, nss),
				rfc					= ISNULL(@rfc, rfc),
				banco				= ISNULL(@banco, banco),
				numero_cuenta		= ISNULL(@numero_cuenta, numero_cuenta),
				correo_electronico	= ISNULL(@correo_electronico, correo_electronico),
				contrasena			= ISNULL(@contrasena, contrasena),
				id_departamento		= ISNULL(@id_departamento, id_departamento),
				id_puesto			= ISNULL(@id_puesto, id_puesto),
				fecha_contratacion	= ISNULL(@fecha_contratacion, fecha_contratacion),
				sueldo_diario		= ISNULL(@sueldo_diario, sueldo_diario)
		WHERE 
				numero_empleado = @numero_empleado AND activo = 1;

		DELETE FROM 
				telefonos_empleados
		WHERE 
				numero_empleado = @numero_empleado;

		DECLARE @min INT = (SELECT MIN(row_count) FROM @telefonos);
		DECLARE @max INT = (SELECT MAX(row_count) FROM @telefonos)
		DECLARE @count INT = @min;

		WHILE (@count <= @max)
		BEGIN

			DECLARE @numtel VARCHAR(12) = (SELECT telefono FROM @telefonos WHERE row_count = @count);
			EXEC sp_AgregarTelefono @numtel,  @numero_empleado, 'E';
			SET @count = @count + 1;

		END

		COMMIT TRAN

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN
		DECLARE @message VARCHAR(200) = ERROR_MESSAGE();
		RAISERROR(@message, 11, 1)
		RETURN;

	END CATCH

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarEmpleado')
	DROP PROCEDURE sp_EliminarEmpleado;
GO

CREATE PROCEDURE sp_EliminarEmpleado(
	@numero_empleado			INT
)
AS

	DECLARE @id_empresa	INT = (SELECT d.id_empresa FROM empleados AS e JOIN departamentos AS d 
	ON e.id_departamento = d.id_departamento WHERE e.numero_empleado = @numero_empleado)

	DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO(@id_empresa);
	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede añadir el puesto debido a que hay una nómina en proceso', 11, 1);
			RETURN;
		END

	UPDATE
			empleados
	SET
			activo = 0,
			fecha_eliminacion = dbo.OBTENERFECHAACTUAL(@id_empresa),
			id_eliminado = NEWID()
	WHERE 
			numero_empleado = @numero_empleado;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerEmpleados')
	DROP PROCEDURE sp_LeerEmpleados;
GO

CREATE PROCEDURE sp_LeerEmpleados(
	@filtro						VARCHAR(100)
)
AS

	SELECT
			[Numero de empleado], 
			[Nombre], 
			[Apellido paterno],
			[Apellido materno],
			[Fecha de nacimiento],
			[CURP],
			[NSS],
			[RFC],
			[Calle],
			[Numero],
			[Colonia],
			[Municipio],
			[Estado],
			[Codigo postal],
			[Banco],
			[ID Banco],
			[Numero de cuenta],
			[Correo electronico],
			[Contraseña],
			[Departamento],
			[ID Departamento],
			[Puesto],
			[ID Puesto],
			[Fecha de contratacion],
			[Sueldo diario],
			[Sueldo base],
			[Nivel salarial]
	FROM 
			vw_RegistroEmpleado
	WHERE
			[Activo] = 1 AND
			([Nombre] LIKE CONCAT('%', @filtro, '%') OR
			[Apellido paterno] LIKE CONCAT('%', @filtro, '%') OR
			[Apellido materno] LIKE CONCAT('%', @filtro, '%'));

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerEmpleadoPorId')
	DROP PROCEDURE sp_ObtenerEmpleadoPorId;
GO

CREATE PROCEDURE sp_ObtenerEmpleadoPorId(
	@numero_empleado				INT
)
AS

	SELECT
			[Numero de empleado], 
			[Nombre], 
			[Apellido paterno],
			[Apellido materno],
			[Fecha de nacimiento],
			[CURP],
			[NSS],
			[RFC],
			[Calle],
			[Numero],
			[Colonia],
			[Municipio],
			[Estado],
			[Codigo postal],
			[Banco],
			[ID Banco],
			[Numero de cuenta],
			[Correo electronico],
			[Contraseña],
			[Departamento],
			[ID Departamento],
			[Puesto],
			[ID Puesto],
			[Fecha de contratacion],
			[Sueldo diario],
			[Sueldo base],
			[Nivel salarial]
	FROM 
			vw_RegistroEmpleado
	WHERE
			[Activo] = 1 AND
			[Numero de empleado] = @numero_empleado;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerEmpleadosNominas')
	DROP PROCEDURE sp_LeerEmpleadosNominas;
GO

CREATE PROCEDURE sp_LeerEmpleadosNominas(
	@fecha				DATE,
	@id_empresa			INT
)
AS

	IF (dbo.PRIMERDIAFECHA(@fecha) >= dbo.PRIMERDIAFECHA(dbo.OBTENERFECHAACTUAL(@id_empresa)) AND 
		(dbo.NOMINAENPROCESO(@id_empresa) = 0 OR dbo.PRIMERDIAFECHA(@fecha) > dbo.PRIMERDIAFECHA(dbo.OBTENERFECHAACTUAL(@id_empresa))))
		BEGIN
			RAISERROR('La nómina solicitada aún no existe', 11, 1);
			RETURN;
		END

	IF dbo.PRIMERDIAFECHA(@fecha) = dbo.PRIMERDIAFECHA(dbo.OBTENERFECHAACTUAL(@id_empresa))
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
		FROM 
				empleados AS e
				JOIN departamentos AS d
				ON e.id_departamento = d.id_departamento
				JOIN puestos AS p
				ON e.id_puesto = p.id_puesto
		WHERE
				e.activo = 1 AND 
				dbo.PRIMERDIAFECHA(e.fecha_contratacion) <=  dbo.PRIMERDIAFECHA(@fecha)
	ELSE
		SELECT
				n.numero_empleado [Numero de empleado],
				CONCAT(e.nombre, ' ', e.apellido_paterno, ' ', e.apellido_materno) AS [Nombre de Empleado], 
				d.nombre [Departamento],
				p.nombre [Puesto],
				n.sueldo_diario [Sueldo diario],
				dbo.DIASTRABAJADOSEMPLEADO(@fecha, e.numero_empleado) [Dias trabajados],
				n.sueldo_bruto [Sueldo bruto],
				dbo.TOTALPERCEPCIONES(@fecha, e.numero_empleado) [Total percepciones],
				dbo.TOTALDEDUCCIONES(@fecha, e.numero_empleado) [Total deducciones],
				n.sueldo_neto [Sueldo neto]
		FROM
				nominas AS n
				JOIN departamentos AS d
				ON n.id_departamento = d.id_departamento
				JOIN puestos AS p
				ON n.id_puesto = p.id_puesto
				JOIN empleados AS e
				ON n.numero_empleado = e.numero_empleado
		WHERE
				dbo.PRIMERDIAFECHA(n.fecha) = dbo.PRIMERDIAFECHA(@fecha)
GO


IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerFechaValidaEmpleado')
	DROP PROCEDURE sp_ObtenerFechaValidaEmpleado;
GO

CREATE PROCEDURE sp_ObtenerFechaValidaEmpleado(
	@numero_empleado				INT,
	@id_empresa						INT
)
AS

	IF EXISTS (SELECT numero_empleado FROM nominas WHERE numero_empleado = @numero_empleado) 

		SELECT TOP 1
				DATEADD(DAY, -1, DATEADD(YEAR, -18, fecha)) [Fecha]
		FROM
				nominas
		WHERE
				numero_empleado = @numero_empleado
		ORDER BY
				fecha ASC;
	ELSE
		SELECT
				DATEADD(DAY, -1, DATEADD(YEAR, -18, dbo.OBTENERFECHAACTUAL(@id_empresa))) [Fecha];

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ObtenerFechaContratacion')
	DROP PROCEDURE sp_ObtenerFechaContratacion;
GO

CREATE PROCEDURE sp_ObtenerFechaContratacion(
	@numero_empleado				INT
)
AS

	SELECT
			dbo.PRIMERDIAFECHA(fecha_contratacion) [Fecha contratacion]
	FROM
			empleados
	WHERE
			numero_empleado = @numero_empleado;

GO