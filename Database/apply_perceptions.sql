USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarEmpleadoPercepcion')
	DROP PROCEDURE sp_AplicarEmpleadoPercepcion;
GO

CREATE PROCEDURE sp_AplicarEmpleadoPercepcion(
	@numero_empleado			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	IF (EXISTS (SELECT id_percepcion_aplicada FROM percepciones_aplicadas WHERE fecha = @fecha 
	AND id_percepcion = @id_percepcion AND numero_empleado = @numero_empleado))
		BEGIN
			RAISERROR('Ya fue aplicada una percepcion en esta fecha', 11, 1);
			RETURN;
		END

	DECLARE @tipo_monto		CHAR;
	DECLARE @cantidad		MONEY;
	SET @tipo_monto = (SELECT tipo_monto FROM percepciones WHERE id_percepcion = @id_percepcion AND activo = 1);
	
	IF @tipo_monto = 'F'
		SET @cantidad = (SELECT fijo FROM percepciones WHERE id_percepcion = @id_percepcion AND activo = 1)
	ELSE IF @tipo_monto = 'P'
		SET @cantidad = (SELECT sueldo_diario FROM empleados WHERE numero_empleado = @numero_empleado AND activo = 1) *
						 dbo.DIASTRABAJADOSEMPLEADO(@fecha, @numero_empleado) *
						(SELECT porcentual FROM percepciones WHERE id_percepcion = @id_percepcion AND activo = 1)


	INSERT INTO percepciones_aplicadas(numero_empleado, id_percepcion, fecha, cantidad)
	VALUES(@numero_empleado, @id_percepcion, @fecha, @cantidad);

GO







IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepcionesAplicadas')
	DROP PROCEDURE sp_LeerPercepcionesAplicadas;
GO

CREATE PROCEDURE sp_LeerPercepcionesAplicadas(
	@filtro						INT,
	@numero_empleado			INT,
	@fecha						DATE
)
AS

	IF (NOT EXISTS (SELECT numero_empleado FROM empleados WHERE numero_empleado = @numero_empleado))
		RETURN;

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
