USE sistema_de_nomina;

IF EXISTS (SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_InsertarEmpresa')
	DROP TRIGGER tr_InsertarEmpresa;
GO

CREATE TRIGGER tr_InsertarEmpresa
ON empresas
AFTER INSERT
AS

	DECLARE @id_empresa INT = (SELECT TOP 1 id_empresa FROM inserted);

	INSERT INTO percepciones(
			nombre,
			tipo_monto,
			fijo,
			porcentual,
			tipo_duracion,
			id_empresa
	)
	VALUES(
			'Salario',
			'P',
			0,
			1.0,
			'B',
			 @id_empresa
	);

	INSERT INTO deducciones(
			nombre,
			tipo_monto,
			fijo,
			porcentual,
			tipo_duracion,
			id_empresa
	)
	VALUES(
			'ISR',
			'P',
			0,
			0.12,
			'B',
			 @id_empresa
	);

	INSERT INTO deducciones(
			nombre,
			tipo_monto,
			fijo,
			porcentual,
			tipo_duracion,
			id_empresa
	)
	VALUES(
			'IMSS',
			'F',
			200.0,
			0,
			'B',
			 @id_empresa
	);

GO


IF EXISTS (SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_TipoMontoPercepciones')
	DROP TRIGGER tr_TipoMontoPercepciones;
GO

CREATE TRIGGER tr_TipoMontoPercepciones
ON percepciones
AFTER INSERT, UPDATE
AS

	UPDATE
			p
	SET
			p.fijo			= IIF(p.tipo_monto = 'F', p.fijo, 0),
			p.porcentual	= IIF(p.tipo_monto = 'P', p.porcentual, 0)
	FROM
			percepciones AS p
			INNER JOIN inserted AS i
			ON p.id_percepcion = i.id_percepcion;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_TipoMontoDeducciones')
	DROP TRIGGER tr_TipoMontoDeducciones;
GO

CREATE TRIGGER tr_TipoMontoDeducciones
ON deducciones
AFTER INSERT, UPDATE
AS

	UPDATE
			d
	SET
			d.fijo			= IIF(d.tipo_monto = 'F', d.fijo, 0),
			d.porcentual	= IIF(d.tipo_monto = 'P', d.porcentual, 0)
	FROM
			deducciones AS d
			INNER JOIN inserted AS i
			ON d.id_deduccion = i.id_deduccion;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_AsignarNominaConceptos')
	DROP TRIGGER tr_AsignarNominaConceptos;
GO

CREATE TRIGGER tr_AsignarNominaConceptos
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
			ON dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(n.fecha) 
			AND pa.numero_empleado = n.numero_empleado
	WHERE
			dbo.PRIMERDIAFECHA(pa.fecha) = dbo.PRIMERDIAFECHA(n.fecha);


	UPDATE 
			deducciones_aplicadas
	SET
			id_nomina = n.id_nomina
	FROM
			deducciones_aplicadas AS da
			INNER JOIN inserted AS n
			ON dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(n.fecha)
			AND da.numero_empleado = n.numero_empleado
	WHERE
			dbo.PRIMERDIAFECHA(da.fecha) = dbo.PRIMERDIAFECHA(n.fecha);
		
GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_ActualizarSueldosDepartamento')
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
			INNER JOIN inserted AS d
			ON e.id_departamento = d.id_departamento 
			INNER JOIN puestos AS p
			ON e.id_puesto = p.id_puesto;

GO



IF EXISTS (SELECT name FROM sysobjects WHERE type = 'TR' AND name = 'tr_ActualizarSueldosPuesto')
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
			INNER JOIN departamentos AS d
			ON e.id_departamento = d.id_departamento 
			INNER JOIN inserted AS p
			ON e.id_puesto = p.id_puesto;

GO