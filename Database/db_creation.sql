--CREATE DATABASE sistema_de_nomina;
USE sistema_de_nomina;

SELECT*FROM empresas;

-- Tablas primas
IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'empresas' AND type = 'u')
	DROP TABLE empresas;

CREATE TABLE empresas(
	id_empresa				INT IDENTITY(1,1) NOT NULL,
	razon_social			VARCHAR(60) NOT NULL,
	domicilio_fiscal		INT NOT NULL,
	correo_electronico		VARCHAR(30) UNIQUE NOT NULL,
	registro_patronal		VARCHAR(11) UNIQUE NOT NULL,
	rfc						VARCHAR(12) UNIQUE NOT NULL,
	fecha_inicio			DATE NOT NULL,
	id_administrador		INT NOT NULL,
	activo					BIT DEFAULT 1

	CONSTRAINT PK_Company
		PRIMARY KEY (id_empresa)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'administradores' AND type = 'u')
	DROP TABLE administradores;

CREATE TABLE administradores(
	id_administrador			INT IDENTITY(1,1),
	correo_electronico			VARCHAR(60) UNIQUE NOT NULL,
	contrasena					VARCHAR(30) NOT NULL,
	activo						BIT DEFAULT 1,
	
	CONSTRAINT PK_Administrator
		PRIMARY KEY (id_administrador)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'departamentos' AND type = 'u')
	DROP TABLE departamentos;

CREATE TABLE departamentos(
	id_departamento				INT IDENTITY(1,1),
	nombre						VARCHAR(30) NOT NULL,
	sueldo_base					MONEY NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	id_empresa					INT NOT NULL

	CONSTRAINT PK_Department
		PRIMARY KEY (id_departamento)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'puestos' AND type = 'u')
	DROP TABLE puestos;

CREATE TABLE puestos(
	id_puesto					INT IDENTITY(1,1) NOT NULL,
	nombre						VARCHAR(30) NOT NULL,
	nivel_salarial				FLOAT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	id_empresa					INT NOT NULL

	CONSTRAINT PK_Position
		PRIMARY KEY (id_puesto)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'empleados' AND type = 'u')
	DROP TABLE empleados;

CREATE TABLE empleados(
	numero_empleado				INT IDENTITY(1,1),
	nombre						VARCHAR(30) NOT NULL,
	apellido_paterno			VARCHAR(30) NOT NULL,
	apellido_materno			VARCHAR(30) NOT NULL,
	fecha_nacimiento			DATE NOT NULL,
	curp						VARCHAR(18) NOT NULL,
	nss							VARCHAR(11) NOT NULL,
	rfc							VARCHAR(13) NOT NULL,
	domicilio					INT NOT NULL,
	banco						INT NOT NULL,
	numero_cuenta				VARCHAR(10) NOT NULL,
	correo_electronico			VARCHAR(60) NOT NULL,
	contrasena					VARCHAR(30) NOT NULL,
	sueldo_diario				MONEY NOT NULL,
	fecha_contratacion			DATE NOT NULL,
	activo						BIT DEFAULT 1,
	id_departamento				INT NOT NULL,
	id_puesto					INT NOT NULL,

	CONSTRAINT PK_Employee
		PRIMARY KEY (numero_empleado),
	CONSTRAINT Unique_Curp
		UNIQUE (curp),
	CONSTRAINT Unique_Nss
		UNIQUE (nss),
	CONSTRAINT Unique_Rfc
		UNIQUE (rfc),
	CONSTRAINT Unique_Numero_Cuenta
		UNIQUE (numero_cuenta),
	CONSTRAINT Unique_Correo_electronico
		UNIQUE (correo_electronico)
);



IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'percepciones' AND type = 'u')
	DROP TABLE percepciones;

CREATE TABLE percepciones(
	id_percepcion				INT IDENTITY(1,1),
	nombre						VARCHAR(30) NOT NULL,
	tipo_monto					CHAR NOT NULL,
	fijo						MONEY,
	porcentual					FLOAT,
	tipo_duracion				CHAR DEFAULT 'S' NOT NULL, -- 'B' es de basic (basico) y 'S' es de special (Especial)
	activo						BIT DEFAULT 1 NOT NULL,

	CONSTRAINT PK_Perception
		PRIMARY KEY (id_percepcion),
	CONSTRAINT Chk_Percepcion_Tipo
		CHECK (tipo_monto in ('F', 'P')),
	CONSTRAINT Chk_Percepcion_Duracion
		CHECK (tipo_duracion in ('S', 'B'))
);


IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'deducciones' AND type = 'u')
	DROP TABLE deducciones;

CREATE TABLE deducciones(
	id_deduccion				INT IDENTITY(1,1),
	nombre						VARCHAR(30) NOT NULL,
	tipo_monto					CHAR NOT NULL,
	fijo						MONEY,
	porcentual					FLOAT,
	tipo_duracion				CHAR DEFAULT 'S' NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,

	CONSTRAINT PK_Deduction
		PRIMARY KEY (id_deduccion),
	CONSTRAINT Chk_Deduccion_Tipo
		CHECK (tipo_monto in ('F', 'P')),
	CONSTRAINT Chk_Deduccion_Duracion
		CHECK (tipo_duracion in ('S', 'B'))
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'nominas' AND type = 'u')
	DROP TABLE nominas;

CREATE TABLE nominas(
	id_nomina					INT IDENTITY(1,1) NOT NULL,
	sueldo_diario				MONEY NOT NULL,
	sueldo_bruto				MONEY NOT NULL,
	sueldo_neto					MONEY NOT NULL,
	banco						INT NOT NULL,
	numero_cuenta				VARCHAR(11) NOT NULL,
	fecha						DATE NOT NULL,
	numero_empleado				INT NOT NULL,
	id_departamento				INT NOT NULL,
	id_puesto					INT NOT NULL

	CONSTRAINT PK_Payrolls
		PRIMARY KEY (id_nomina)
);


-- Tablas asociativas
IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'percepciones_aplicadas' AND type = 'u')
	DROP TABLE percepciones_aplicadas;

CREATE TABLE percepciones_aplicadas(
	id_percepcion_aplicada		INT IDENTITY(1,1),
	numero_empleado				INT NOT NULL,
	id_percepcion				INT NOT NULL,
	id_nomina					INT,
	cantidad					MONEY NOT NULL,
	fecha						DATE NOT NULL

	CONSTRAINT PK_Employees_Perceptions
		PRIMARY KEY (id_percepcion_aplicada)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'deducciones_aplicadas' AND type = 'u')
	DROP TABLE deducciones_aplicadas;

CREATE TABLE deducciones_aplicadas(
	id_deduccion_aplicada		INT IDENTITY(1,1),
	numero_empleado				INT NOT NULL,
	id_deduccion				INT NOT NULL,
	id_nomina					INT,
	cantidad					MONEY NOT NULL,
	fecha						DATE NOT NULL

	CONSTRAINT PK_Employees_Deductions
		PRIMARY KEY (id_deduccion_aplicada)
);



-- Tablas de Normalizacion
IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'domicilios' AND type = 'u')
	DROP TABLE domicilios;

CREATE TABLE domicilios(
	id_domicilio				INT IDENTITY(1,1),
	calle						VARCHAR(30) NOT NULL,
	numero						VARCHAR(10) NOT NULL,
	colonia						VARCHAR(30) NOT NULL,
	ciudad						VARCHAR(30) NOT NULL,
	estado						VARCHAR(30) NOT NULL,
	codigo_postal				VARCHAR(5) NOT NULL

	CONSTRAINT PK_Addresses
		PRIMARY KEY (id_domicilio)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'telefonos_empresas' AND type = 'u')
	DROP TABLE telefonos_empresas;

CREATE TABLE telefonos_empresas(
	id_telefono_empresa			INT IDENTITY(1,1),
	telefono					VARCHAR(12) NOT NULL,
	id_empresa					INT NOT NULL

	CONSTRAINT PK_Phones_Companies
		PRIMARY KEY (id_telefono_empresa)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'telefonos_empleados' AND type = 'u')
	DROP TABLE telefonos_empleados;

CREATE TABLE telefonos_empleados(
	id_telefono_empleado		INT IDENTITY(1,1),
	telefono					VARCHAR(12) NOT NULL,
	numero_empleado				INT NOT NULL

	CONSTRAINT PK_Phones_Employees
		PRIMARY KEY (id_telefono_empleado)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'bancos' AND type = 'u')
	DROP TABLE bancos;

CREATE TABLE bancos(
	id_banco					INT IDENTITY(1,1),
	nombre						VARCHAR(30) NOT NULL

	CONSTRAINT PK_Banks
		PRIMARY KEY (id_banco)
);



-- Llaves foraneas
ALTER TABLE empresas
	ADD CONSTRAINT FK_Empresa_Domicilio
		FOREIGN KEY (domicilio_fiscal)
		REFERENCES domicilios(id_domicilio);

ALTER TABLE empresas
	ADD CONSTRAINT FK_Admin_Empresa
		FOREIGN KEY (id_administrador)
		REFERENCES administradores(id_administrador);

ALTER TABLE departamentos
	ADD CONSTRAINT FK_Departmento_Empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE puestos
	ADD CONSTRAINT FK_Puesto_Empresa
		FOREIGN KEY (id_empresa)
		REFERENCES empresas(id_empresa);

ALTER TABLE empleados
	ADD CONSTRAINT FK_Empleado_Domicilio
		FOREIGN KEY (domicilio)
		REFERENCES domicilios(id_domicilio),
		CONSTRAINT FK_Empleado_Banco
		FOREIGN KEY (banco)
		REFERENCES bancos(id_banco),
		CONSTRAINT FK_Empleado_Departamento
		FOREIGN KEY (id_departamento)
		REFERENCES departamentos(id_departamento),
		CONSTRAINT FK_Empleado_Puesto
		FOREIGN KEY (id_puesto)
		REFERENCES puestos(id_puesto);

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




		



/*
ALTER TABLE empresas
	DROP CONSTRAINT FK_Empresa_Domicilio;

ALTER TABLE empresas
	DROP CONSTRAINT FK_Admin_Empresa;

ALTER TABLE departamentos
	DROP CONSTRAINT FK_Departmento_Empresa;

ALTER TABLE puestos
	DROP CONSTRAINT FK_Puesto_Empresa;

ALTER TABLE empleados
	DROP CONSTRAINT FK_Empleado_Domicilio,
		CONSTRAINT FK_Empleado_Banco,
		CONSTRAINT FK_Empleado_Departamento,
		CONSTRAINT FK_Empleado_Puesto;

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

*/


/*
SELECT all_objects.name AS TableName,
syscolumns.name AS ColumnName ,systypes.name AS DataType
,syscolumns.length AS CharacterMaximumLength
,sysproperties.[value] AS ColumnDescription
,syscomments.TEXT AS ColumnDefault ,syscolumns.isnullable AS IsNullable
FROM syscolumns INNER JOIN sys.systypes ON syscolumns.xtype = systypes.xtype
LEFT JOIN sys.all_objects ON syscolumns.id = all_objects.[object_id]
LEFT OUTER JOIN sys.extended_properties AS sysproperties ON (sysproperties.minor_id =
syscolumns.colid AND sysproperties.major_id = syscolumns.id)
LEFT OUTER JOIN sys.syscomments ON syscolumns.cdefault = syscomments.id
LEFT OUTER JOIN sys.schemas ON schemas.[schema_id] = all_objects.[schema_id]
WHERE syscolumns.id IN (SELECT id
FROM sysobjects
WHERE xtype = 'U') AND (systypes.name <> 'sysname')
ORDER BY TableName ASC;
*/


INSERT INTO domicilios(calle, numero, colonia, ciudad, estado, codigo_postal)
VALUES('Montes de Leon', '935', 'Las Puentes 6to Sector', 'San Nicolas de los Garza', 'Nuevo Leon', '66460');

INSERT INTO empresas(razon_social, domicilio_fiscal, correo_electronico, rfc, registro_patronal, fecha_inicio, id_administrador)
VALUES('Crystal Soft Development S.A. de C.V.', 1, 'crystal@domain.com', 'MOV1004082C1', 'Y5499995107','20101026', 1);

select top 1 * from empresas;

SELECT * FROM departamentos;
SELECT * FROM puestos;
select*from domicilios;
select*from empresas;

INSERT INTO administradores(correo_electronico, contrasena)
VALUES('a@a.com', '123');

INSERT INTO Bancos (Nombre) VALUES ('Banorte');
INSERT INTO Bancos (Nombre) VALUES ('Santander');
INSERT INTO Bancos (Nombre) VALUES ('BBVA');
INSERT INTO Bancos (Nombre) VALUES ('Citibanamex');
INSERT INTO Bancos (Nombre) VALUES ('Afirme');


INSERT INTO percepciones(nombre, tipo_monto, fijo, porcentual, tipo_duracion)
VALUES('Salario', 'P', 0, 1.0, 'B');
INSERT INTO deducciones(nombre, tipo_monto, fijo, porcentual, tipo_duracion)
VALUES('ISR', 'P', 0, 0.2, 'B');
INSERT INTO deducciones(nombre, tipo_monto, fijo, porcentual, tipo_duracion)
VALUES('IMSS', 'F', 100.0, 0, 'B');

SELECT*FROM deducciones;
