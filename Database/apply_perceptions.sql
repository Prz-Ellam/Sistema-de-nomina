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


	INSERT INTO percepciones_aplicadas(
			numero_empleado,
			id_percepcion,
			fecha,
			cantidad
	)
	VALUES(
			@numero_empleado,
			@id_percepcion,
			@fecha,
			@cantidad
	);

GO



EXEC sp_AplicarDepartamentoPercepcion 1, 3, '20220501';
EXEC sp_EliminarDepartamentoPercepcion 1, 3, '20220501';


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AplicarDepartamentoPercepcion')
	DROP PROCEDURE sp_AplicarDepartamentoPercepcion;
GO

CREATE PROCEDURE sp_AplicarDepartamentoPercepcion(
	@id_departamento			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS
BEGIN

	INSERT INTO percepciones_aplicadas(
			numero_empleado,
			id_percepcion,
			fecha,
			cantidad
	)
	SELECT 
			e.numero_empleado,
			@id_percepcion,
			@fecha,
			sueldo_diario * dbo.DIASTRABAJADOSEMPLEADO(@fecha, numero_empleado) * p.porcentual + p.fijo
	FROM 
			empleados AS e
			INNER JOIN percepciones AS p
			ON p.id_percepcion = @id_percepcion
	WHERE
			e.id_departamento = @id_departamento AND 
			e.activo = 1 AND
			(SELECT COUNT(0) FROM percepciones_aplicadas 
			WHERE numero_empleado = e.numero_empleado AND id_percepcion = @id_percepcion AND fecha = @fecha) = 0;

END
GO




IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarEmpleadoPercepcion')
	DROP PROCEDURE sp_EliminarEmpleadoPercepcion;
GO

CREATE PROCEDURE sp_EliminarEmpleadoPercepcion(
	@numero_empleado			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	DELETE FROM 
			percepciones_aplicadas
	WHERE 
			numero_empleado = @numero_empleado AND
			id_percepcion = @id_percepcion AND
			YEAR(fecha) = YEAR(@fecha) AND
			MONTH(fecha) = MONTH(@fecha);

GO


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarDepartamentoPercepcion')
	DROP PROCEDURE sp_EliminarDepartamentoPercepcion;
GO

CREATE PROCEDURE sp_EliminarDepartamentoPercepcion(
	@id_departamento			INT,
	@id_percepcion				INT,
	@fecha						DATE
)
AS

	DELETE 
			percepciones_aplicadas 
	FROM 
			percepciones_aplicadas AS pa
			JOIN empleados AS e
			ON pa.numero_empleado = e.numero_empleado
	WHERE 
			e.id_departamento = @id_departamento AND 
			pa.id_percepcion = @id_percepcion AND 
			dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(@fecha);

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
		SELECT 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				p.id_percepcion,
				p.nombre,
				p.tipo_monto,
				p.fijo,
				p.porcentual
		FROM 
				percepciones AS p
				LEFT JOIN percepciones_aplicadas AS pa
				ON p.id_percepcion = pa.id_percepcion AND 
				YEAR(pa.fecha) = YEAR(@fecha) AND 
				MONTH(pa.fecha) = MONTH(@fecha) AND 
				pa.numero_empleado = @numero_empleado
		WHERE 
				p.tipo_duracion = 'S' AND 
				p.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL);
	ELSE IF @filtro = 2
		SELECT 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				p.id_percepcion,
				p.nombre,
				p.tipo_monto,
				p.fijo,
				p.porcentual 
		FROM 
				percepciones AS p
				LEFT JOIN percepciones_aplicadas AS pa
				ON p.id_percepcion = pa.id_percepcion AND YEAR(pa.fecha) = YEAR(@fecha) AND MONTH(pa.fecha) = MONTH(@fecha) AND pa.numero_empleado = @numero_empleado
		WHERE 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') = 'true' AND 
				p.tipo_duracion = 'S' AND 
				p.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL);
	ELSE IF @filtro = 3
		SELECT 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
				p.id_percepcion,
				p.nombre,
				p.tipo_monto,
				p.fijo,
				p.porcentual 
		FROM 
				percepciones AS p
				LEFT JOIN percepciones_aplicadas AS pa
				ON p.id_percepcion = pa.id_percepcion AND YEAR(pa.fecha) = YEAR(@fecha) AND MONTH(pa.fecha) = MONTH(@fecha) AND pa.numero_empleado = @numero_empleado
		WHERE 
				IIF(pa.id_percepcion_aplicada IS NULL, 'false', 'true') = 'false' AND 
				p.tipo_duracion = 'S' AND 
				p.fecha_creacion <= dbo.PRIMERDIAFECHA(@fecha) AND 
				(p.fecha_eliminacion > dbo.PRIMERDIAFECHA(@fecha) OR p.fecha_eliminacion IS NULL);
	ELSE 
		RAISERROR('Filtro inválido', 11, 1);

GO









IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepcionesNomina')
	DROP PROCEDURE sp_LeerPercepcionesNomina;
GO

CREATE PROCEDURE sp_LeerPercepcionesNomina(
	@id_nomina				INT
)
AS

	SELECT 
			p.id_percepcion		[Clave], 
			p.nombre			[Concepto], 
			pa.cantidad			[Importe]
	FROM 
			percepciones_aplicadas AS pa
			JOIN percepciones AS p
			ON pa.id_percepcion = p.id_percepcion
	WHERE 
			id_nomina = @id_nomina;

GO

