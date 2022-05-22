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

ALTER TABLE percepciones
	DROP CONSTRAINT fk_percepcion_empresa;

ALTER TABLE deducciones
	DROP CONSTRAINT fk_deduccion_empresa;

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



TRUNCATE TABLE empresas;
TRUNCATE TABLE administradores;
TRUNCATE TABLE departamentos;
TRUNCATE TABLE puestos;
TRUNCATE TABLE empleados;
TRUNCATE TABLE percepciones;
TRUNCATE TABLE deducciones;
TRUNCATE TABLE nominas;
TRUNCATE TABLE percepciones_aplicadas;
TRUNCATE TABLE deducciones_aplicadas;
TRUNCATE TABLE domicilios;
TRUNCATE TABLE telefonos_empresas;
TRUNCATE TABLE telefonos_empleados;
TRUNCATE TABLE bancos;

ALTER TABLE empresas
	ADD CONSTRAINT fk_empresa_domicilio
		FOREIGN KEY (domicilio_fiscal)
		REFERENCES domicilios(id_domicilio);

ALTER TABLE empresas
	ADD CONSTRAINT fk_administrador_empresa
		FOREIGN KEY (id_administrador)
		REFERENCES administradores(id_administrador);

ALTER TABLE departamentos
	ADD CONSTRAINT fk_departmento_empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE puestos
	ADD CONSTRAINT fk_puesto_empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE empleados
	ADD CONSTRAINT fk_empleado_domicilio
		FOREIGN KEY (domicilio)
		REFERENCES domicilios(id_domicilio),
		CONSTRAINT fk_empleado_banco
		FOREIGN KEY (banco)
		REFERENCES bancos(id_banco),
		CONSTRAINT fk_empleado_departamento
		FOREIGN KEY (id_departamento)
		REFERENCES departamentos(id_departamento),
		CONSTRAINT fk_empleado_puesto
		FOREIGN KEY (id_puesto)
		REFERENCES puestos(id_puesto);

ALTER TABLE percepciones
	ADD CONSTRAINT fk_percepcion_empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE deducciones
	ADD CONSTRAINT fk_deduccion_empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE percepciones_aplicadas
	ADD CONSTRAINT fk_percepcion_empleado
		FOREIGN KEY (numero_empleado)
		REFERENCES empleados(numero_empleado),
		CONSTRAINT fk_percepcion_percepcion
		FOREIGN KEY (id_percepcion)
		REFERENCES percepciones(id_percepcion),
		CONSTRAINT fk_percepcion_nomina
		FOREIGN KEY (id_nomina)
		REFERENCES nominas(id_nomina);

ALTER TABLE deducciones_aplicadas
	ADD CONSTRAINT fk_deduccion_empleado
		FOREIGN KEY (numero_empleado)
		REFERENCES empleados(numero_empleado),
		CONSTRAINT fk_deduccion_deduccion
		FOREIGN KEY (id_deduccion)
		REFERENCES deducciones(id_deduccion),
		CONSTRAINT fk_deduccion_nomina
		FOREIGN KEY (id_nomina)
		REFERENCES nominas(id_nomina);

ALTER TABLE nominas
	ADD CONSTRAINT fk_nomina_banco
		FOREIGN KEY (banco)
		REFERENCES bancos(id_banco),
		CONSTRAINT fk_nomina_empleado
		FOREIGN KEY (numero_empleado)
		REFERENCES empleados(numero_empleado),
		CONSTRAINT fk_nomina_departamento
		FOREIGN KEY (id_departamento)
		REFERENCES departamentos(id_departamento),
		CONSTRAINT fk_nomina_puesto
		FOREIGN KEY (id_puesto)
		REFERENCES puestos(id_puesto);

ALTER TABLE telefonos_empresas
	ADD CONSTRAINT fk_telefono_empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE telefonos_empleados
	ADD CONSTRAINT fk_telefono_empleado
		FOREIGN KEY (numero_empleado)
		REFERENCES empleados(numero_empleado);