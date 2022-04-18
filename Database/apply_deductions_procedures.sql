USE sistema_de_nomina;

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_LeerDeduccionesAplicadas')
	DROP PROCEDURE sp_LeerDeduccionesAplicadas;
GO

CREATE PROCEDURE sp_LeerDeduccionesAplicadas(
	@filtro						INT,
	@numero_empleado			INT,
	@fecha						DATE
)
AS

	IF (NOT EXISTS (SELECT numero_empleado FROM empleados WHERE numero_empleado = @numero_empleado))
		RETURN;

	IF @filtro = 1
		SELECT IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
		FROM deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion AND YEAR(da.fecha) = YEAR(@fecha) AND MONTH(da.fecha) = MONTH(@fecha) AND da.numero_empleado = @numero_empleado;
	ELSE IF @filtro = 2
		SELECT IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
		FROM deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion AND YEAR(da.fecha) = YEAR(@fecha) AND MONTH(da.fecha) = MONTH(@fecha) AND da.numero_empleado = @numero_empleado
		WHERE IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'true';
	ELSE IF @filtro = 3
		SELECT IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') AS [Aplicada], 
		d.id_deduccion, d.nombre, d.tipo_monto, d.fijo, d.porcentual 
		FROM deducciones AS d
		LEFT JOIN deducciones_aplicadas AS da
		ON d.id_deduccion = da.id_deduccion AND YEAR(da.fecha) = YEAR(@fecha) AND MONTH(da.fecha) = MONTH(@fecha) AND da.numero_empleado = @numero_empleado
		WHERE IIF(da.id_deduccion_aplicada IS NULL, 'false', 'true') = 'false';
	ELSE 
		RAISERROR('Filtro inválido', 11, 1);

GO
