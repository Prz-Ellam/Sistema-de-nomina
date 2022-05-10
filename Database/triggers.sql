USE sistema_de_nomina;
/*
IF (EXISTS(SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_perception_insert'))
	DROP TRIGGER tr_perception_insert;
GO

CREATE TRIGGER tr_PercepcionInsert
ON percepciones
AFTER INSERT
AS

	DECLARE @amount_type	CHAR(1);
	SET @amount_type = (SELECT tipo_monto FROM inserted);

	IF @amount_type = 'F'
		UPDATE percepciones
		SET
		porcentual = 0
		FROM percepciones
		INNER JOIN inserted ON percepciones.id_percepcion = inserted.id_percepcion
		WHERE
		percepciones.id_percepcion = inserted.id;
	ELSE IF @amount_type = 'P'
		UPDATE perceptions
		SET
		fixed = 0
		FROM perceptions
		INNER JOIN inserted ON perceptions.id = inserted.id 
		WHERE
		perceptions.id = inserted.id;

GO


-- tr_perceptions_update
-- tr_deductions_insert
-- tr_deductions_update


*/

IF (EXISTS(SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_AsignarNominaPercepciones'))
	DROP TRIGGER tr_AsignarNominaPercepciones;
GO

CREATE TRIGGER tr_AsignarNominaPercepciones
ON nominas
AFTER INSERT
AS

	UPDATE
		percepciones_aplicadas
	SET
		id_nomina = n.id_nomina
	FROM
		percepciones_aplicadas AS pa
		INNER JOIN inserted AS n
		ON pa.fecha = n.fecha AND pa.numero_empleado = n.numero_empleado
	WHERE
		dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(n.fecha);


	UPDATE 
		deducciones_aplicadas
	SET
		id_nomina = n.id_nomina
	FROM
		deducciones_aplicadas AS da
		INNER JOIN inserted AS n
		ON da.fecha = n.fecha AND da.numero_empleado = n.numero_empleado
	WHERE
		dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(n.fecha);


GO



IF (EXISTS(SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_ActualizarSueldosDepartamento'))
	DROP TRIGGER tr_ActualizarSueldosDepartamento;
GO

CREATE TRIGGER tr_ActualizarSueldosDepartamento
ON departamentos
AFTER UPDATE
AS

	UPDATE
			empleados
	SET
			sueldo_diario = d.sueldo_base * p.nivel_salarial
	FROM
			empleados AS e
			JOIN inserted AS d
			ON e.id_departamento = d.id_departamento 
			JOIN puestos AS p
			ON e.id_puesto = p.id_puesto;

GO



IF (EXISTS(SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_ActualizarSueldosPuesto'))
	DROP TRIGGER tr_ActualizarSueldosPuesto;
GO

CREATE TRIGGER tr_ActualizarSueldosPuesto
ON puestos
AFTER UPDATE
AS

	UPDATE
			empleados
	SET
			sueldo_diario = d.sueldo_base * p.nivel_salarial
	FROM
			empleados AS e
			JOIN departamentos AS d
			ON e.id_departamento = d.id_departamento 
			JOIN inserted AS p
			ON e.id_puesto = p.id_puesto;

GO