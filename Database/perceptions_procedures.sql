USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AgregarPercepcion')
	DROP PROCEDURE sp_AgregarPercepcion;
GO

CREATE PROCEDURE sp_AgregarPercepcion(
	@nombre				VARCHAR(30),
	@tipo_monto			CHAR(1),
	@fijo				MONEY,
	@porcentual			FLOAT,
	@id_empresa			INT
) 
AS

	DECLARE @status_nomina BIT = dbo.NOMINAENPROCESO(@id_empresa);

	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede a�adir la percepci�n debido a que hay una n�mina en proceso', 11, 1);
			RETURN;
		END

	INSERT INTO percepciones(
			nombre,
			tipo_monto,
			fijo,
			porcentual,
			fecha_creacion
	)
	VALUES(
			@nombre,
			@tipo_monto,
			@fijo,
			@porcentual,
			dbo.OBTENERFECHAACTUAL(@id_empresa)
	);

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

	DECLARE @status_nomina BIT;
	SET @status_nomina = dbo.NOMINAENPROCESO((SELECT id_empresa FROM percepciones 
												WHERE id_percepcion = 6 AND activo = 1));

	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede editar la percepci�n debido a que hay una n�mina en proceso', 11, 1);
			RETURN;
		END

	UPDATE 
			percepciones
	SET
			nombre		= ISNULL(@nombre, nombre),
			tipo_monto	= ISNULL(@tipo_monto, tipo_monto),
			fijo		= ISNULL(@fijo, fijo),
			porcentual	= ISNULL(@porcentual, porcentual)
	WHERE
			id_percepcion = @id_percepcion;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_EliminarPercepcion')
	DROP PROCEDURE sp_EliminarPercepcion;
GO

CREATE PROCEDURE sp_EliminarPercepcion(
	@id_percepcion		INT
)
AS

	DECLARE @status_nomina BIT;
	SET @status_nomina = dbo.NOMINAENPROCESO((SELECT id_empresa FROM percepciones 
												WHERE id_percepcion = 6 AND activo = 1));

	IF @status_nomina = 1
		BEGIN
			RAISERROR('No se puede editar la percepci�n debido a que hay una n�mina en proceso', 11, 1);
			RETURN;
		END

	UPDATE 
			percepciones
	SET
			activo = 0,
			fecha_eliminacion = dbo.OBTENERFECHAACTUAL(id_empresa),
			id_eliminado = NEWID()
	WHERE
			id_percepcion = @id_percepcion;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerPercepciones')
	DROP PROCEDURE sp_LeerPercepciones;
GO

CREATE PROCEDURE sp_LeerPercepciones
AS

	SELECT
			id_percepcion [ID Percepcion], 
			nombre [Nombre], 
			tipo_monto [Tipo de monto], 
			ISNULL(fijo, 0) [Fijo], 
			ISNULL(porcentual, 0) [Porcentual]
	FROM
			percepciones
	WHERE
			activo = 1 AND 
			tipo_duracion = 'S';

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_FiltrarPercepciones')
	DROP PROCEDURE sp_FiltrarPercepciones;
GO

CREATE PROCEDURE sp_FiltrarPercepciones(
	@filtro					VARCHAR(100),
	@id_empresa				INT
)
AS

	SELECT 
			id_percepcion [ID Percepcion], 
			nombre [Nombre],
			tipo_monto [Tipo de monto], 
			ISNULL(fijo, 0) [Fijo], 
			ISNULL(porcentual, 0) [Porcentual]
	FROM 
			percepciones
	WHERE 
			activo = 1 AND
			tipo_duracion = 'S' AND
			id_empresa = @id_empresa AND
			nombre LIKE CONCAT('%', @filtro, '%');

GO