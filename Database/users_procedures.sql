USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Login')
	DROP PROCEDURE sp_Login;
GO

CREATE PROCEDURE sp_Login
	@correo_electronico			VARCHAR(60),
	@contrasena					VARCHAR(30)
AS

	SELECT TOP 1
			a.id_administrador [ID],
			a.correo_electronico [Correo electrónico],
			'Administrador' [Posición],
			ISNULL(e.id_empresa, -1) [ID Empresa]
	FROM
			administradores AS a
			LEFT JOIN empresas AS e
			ON a.id_administrador = e.id_administrador
	WHERE 
			a.correo_electronico = @correo_electronico COLLATE SQL_Latin1_General_CP1_CS_AS
			AND a.contrasena = @contrasena COLLATE SQL_Latin1_General_CP1_CS_AS
			AND a.activo = 1
	UNION
	SELECT 
			e.numero_empleado [ID],
			e.correo_electronico [Correo electrónico],
			'Empleado' [Posición],
			d.id_empresa [ID Empresa]
	FROM
			empleados AS e
			INNER JOIN departamentos AS d
			ON e.id_departamento = d.id_departamento
	WHERE
			e.correo_electronico = @correo_electronico COLLATE SQL_Latin1_General_CP1_CS_AS
			AND e.contrasena = @contrasena COLLATE SQL_Latin1_General_CP1_CS_AS
			AND e.activo = 1;

GO