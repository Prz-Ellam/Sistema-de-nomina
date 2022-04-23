USE sistema_de_nomina;

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

	SELECT
			id_percepcion [ID Percepcion], 
			nombre [Nombre], 
			tipo_monto [Tipo de monto], 
			ISNULL(fijo, 0) [Fijo], 
			ISNULL(porcentual, 0) [Porcentual]
	FROM
			percepciones
	WHERE
			activo = 1 AND tipo_duracion = 'S';

GO