USE sistema_de_nomina;

ALTER TABLE empresas
	DROP CONSTRAINT fk_empresa_domicilio;

ALTER TABLE empresas
	DROP CONSTRAINT fk_administrador_empresa;

ALTER TABLE departamentos
	DROP CONSTRAINT fk_departmento_empresa;

ALTER TABLE puestos
	DROP CONSTRAINT fk_puesto_empresa;

ALTER TABLE empleados
	DROP CONSTRAINT fk_empleado_domicilio,
		CONSTRAINT fk_empleado_banco,
		CONSTRAINT fk_empleado_departamento,
		CONSTRAINT fk_empleado_puesto;

ALTER TABLE percepciones_aplicadas
	DROP CONSTRAINT fk_percepcion_empleado,
		CONSTRAINT fk_percepcion_percepcion,
		CONSTRAINT fk_percepcion_nomina;

ALTER TABLE deducciones_aplicadas
	DROP CONSTRAINT fk_deduccion_empleado,
		CONSTRAINT fk_deduccion_deduccion,
		CONSTRAINT fk_deduccion_nomina;

ALTER TABLE nominas
	DROP CONSTRAINT fk_nomina_banco,
		CONSTRAINT fk_nomina_empleado,
		CONSTRAINT fk_nomina_departamento,
		CONSTRAINT fk_nomina_puesto;

ALTER TABLE telefonos_empresas
	DROP CONSTRAINT fk_telefono_empresa;

ALTER TABLE telefonos_empleados
	DROP CONSTRAINT fk_telefono_empleado;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'empresas' AND type = 'u')
	DROP TABLE empresas;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'administradores' AND type = 'u')
	DROP TABLE administradores;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'departamentos' AND type = 'u')
	DROP TABLE departamentos;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'puestos' AND type = 'u')
	DROP TABLE puestos;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'empleados' AND type = 'u')
	DROP TABLE empleados;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'percepciones' AND type = 'u')
	DROP TABLE percepciones;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'deducciones' AND type = 'u')
	DROP TABLE deducciones;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'nominas' AND type = 'u')
	DROP TABLE nominas;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'percepciones_aplicadas' AND type = 'u')
	DROP TABLE percepciones_aplicadas;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'deducciones_aplicadas' AND type = 'u')
	DROP TABLE deducciones_aplicadas;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'domicilios' AND type = 'u')
	DROP TABLE domicilios;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'telefonos_empresas' AND type = 'u')
	DROP TABLE telefonos_empresas;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'telefonos_empleados' AND type = 'u')
	DROP TABLE telefonos_empleados;

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'bancos' AND type = 'u')
	DROP TABLE bancos;
