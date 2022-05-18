USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Login')
	DROP PROCEDURE sp_Login;
GO

CREATE PROCEDURE sp_Login
	@correo_electronico			VARCHAR(60),
	@contrasena					VARCHAR(30)
AS

	SELECT 
			a.id_administrador [ID],
			a.correo_electronico [Correo],
			'Administrador' [Posicion],
			ISNULL(e.id_empresa, -1) [ID Empresa]
	FROM
			administradores AS a
			LEFT JOIN empresas AS e
			ON a.id_administrador = e.id_administrador
	WHERE 
			a.correo_electronico = @correo_electronico AND 
			a.contrasena = @contrasena AND
			a.activo = 1
	UNION
	SELECT 
			e.numero_empleado [ID],
			e.correo_electronico [Correo],
			'Empleado' [Posicion],
			d.id_empresa [ID Empresa]
	FROM
			empleados AS e
			INNER JOIN departamentos AS d
			ON e.id_departamento = d.id_departamento
	WHERE
			e.correo_electronico = @correo_electronico AND 
			e.contrasena = @contrasena AND
			e.activo = 1;

GO