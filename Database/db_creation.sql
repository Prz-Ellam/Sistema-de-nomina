--CREATE DATABASE sistema_de_nomina;
USE sistema_de_nomina;

-- Tablas primas
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'empresas' AND type = 'U')
	DROP TABLE empresas;
GO

CREATE TABLE empresas(
	id_empresa					INT IDENTITY(1,1) NOT NULL,
	razon_social				VARCHAR(60) NOT NULL,
	correo_electronico			VARCHAR(60) NOT NULL,
	registro_patronal			VARCHAR(11) NOT NULL,
	rfc							VARCHAR(12) NOT NULL,
	fecha_inicio				DATE NOT NULL,
	id_administrador			INT NOT NULL,
	domicilio_fiscal			INT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	fecha_eliminacion			DATE,
	id_eliminado				UNIQUEIDENTIFIER DEFAULT NULL,

	CONSTRAINT pk_empresas
		PRIMARY KEY (id_empresa),
	CONSTRAINT unique_correo
		UNIQUE (correo_electronico, id_eliminado),
	CONSTRAINT unique_registro_patronal
		UNIQUE (registro_patronal, id_eliminado),
	CONSTRAINT unique_rfc
		UNIQUE (rfc, id_eliminado)
);



IF EXISTS(SELECT name FROM sysobjects WHERE name = 'administradores' AND type = 'U')
	DROP TABLE administradores;
GO

CREATE TABLE administradores(
	id_administrador			INT IDENTITY(1,1) NOT NULL,
	correo_electronico			VARCHAR(60) UNIQUE NOT NULL,
	contrasena					VARCHAR(30) NOT NULL,
	activo						BIT DEFAULT 1
	
	CONSTRAINT pk_administradores
		PRIMARY KEY (id_administrador)
);


IF EXISTS (SELECT name FROM sysobjects WHERE name = 'departamentos' AND type = 'U')
	DROP TABLE departamentos;
GO

CREATE TABLE departamentos(
	id_departamento				INT IDENTITY(1,1) NOT NULL,
	nombre						VARCHAR(30) NOT NULL,
	sueldo_base					MONEY NOT NULL,
	id_empresa					INT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	fecha_creacion				DATE,
	fecha_eliminacion			DATE,
	id_eliminado				UNIQUEIDENTIFIER DEFAULT NULL

	CONSTRAINT pk_departamentos
		PRIMARY KEY (id_departamento)
);



IF EXISTS(SELECT name FROM sysobjects WHERE name = 'puestos' AND type = 'U')
	DROP TABLE puestos;
GO

CREATE TABLE puestos(
	id_puesto					INT IDENTITY(1,1) NOT NULL,
	nombre						VARCHAR(30) NOT NULL,
	nivel_salarial				FLOAT NOT NULL,
	id_empresa					INT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	fecha_creacion				DATE,
	fecha_eliminacion			DATE,
	id_eliminado				UNIQUEIDENTIFIER DEFAULT NULL

	CONSTRAINT pk_puestos
		PRIMARY KEY (id_puesto)
);



IF EXISTS(SELECT name FROM sysobjects WHERE name = 'empleados' AND type = 'U')
	DROP TABLE empleados;
GO

CREATE TABLE empleados(
	numero_empleado				INT IDENTITY(1,1) NOT NULL,
	nombre						VARCHAR(30) NOT NULL,
	apellido_paterno			VARCHAR(30) NOT NULL,
	apellido_materno			VARCHAR(30) NOT NULL,
	fecha_nacimiento			DATE NOT NULL,
	curp						VARCHAR(18) NOT NULL,
	nss							VARCHAR(11) NOT NULL,
	rfc							VARCHAR(13) NOT NULL,
	numero_cuenta				VARCHAR(16) NOT NULL,
	correo_electronico			VARCHAR(60) NOT NULL,
	contrasena					VARCHAR(30) NOT NULL,
	sueldo_diario				MONEY NOT NULL,
	fecha_contratacion			DATE NOT NULL,
	domicilio					INT NOT NULL,
	banco						INT NOT NULL,
	id_departamento				INT NOT NULL,
	id_puesto					INT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	fecha_eliminacion			DATE,
	id_eliminado				UNIQUEIDENTIFIER DEFAULT NULL,

	CONSTRAINT pk_empleados
		PRIMARY KEY (numero_empleado),
	CONSTRAINT unique_curp
		UNIQUE (curp, id_eliminado),
	CONSTRAINT unique_nss
		UNIQUE (nss, id_eliminado),
	CONSTRAINT unique_rfc_empleado
		UNIQUE (rfc, id_eliminado),
	CONSTRAINT unique_numero_cuenta
		UNIQUE (numero_cuenta, id_eliminado),
	CONSTRAINT unique_correo_electronico
		UNIQUE (correo_electronico, id_eliminado)
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'percepciones' AND type = 'U')
	DROP TABLE percepciones;
GO

CREATE TABLE percepciones(
	id_percepcion				INT IDENTITY(1,1) NOT NULL,
	nombre						VARCHAR(30) NOT NULL,
	tipo_monto					CHAR NOT NULL,
	fijo						MONEY,
	porcentual					FLOAT,
	tipo_duracion				CHAR DEFAULT 'S' NOT NULL, -- 'B' es de basic (basico) y 'S' es de special (Especial)
	id_empresa					INT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	fecha_creacion				DATE,
	fecha_eliminacion			DATE,
	id_eliminado				UNIQUEIDENTIFIER DEFAULT NULL

	CONSTRAINT pk_percepciones
		PRIMARY KEY (id_percepcion),
	CONSTRAINT chk_percepcion_tipo
		CHECK (tipo_monto in ('F', 'P')),
	CONSTRAINT chk_percepcion_duracion
		CHECK (tipo_duracion in ('S', 'B'))
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'deducciones' AND type = 'U')
	DROP TABLE deducciones;
GO

CREATE TABLE deducciones(
	id_deduccion				INT IDENTITY(1,1) NOT NULL,
	nombre						VARCHAR(30) NOT NULL,
	tipo_monto					CHAR NOT NULL,
	fijo						MONEY,
	porcentual					FLOAT,
	tipo_duracion				CHAR DEFAULT 'S' NOT NULL,
	id_empresa					INT NOT NULL,
	activo						BIT DEFAULT 1 NOT NULL,
	fecha_creacion				DATE,
	fecha_eliminacion			DATE,
	id_eliminado				UNIQUEIDENTIFIER DEFAULT NULL

	CONSTRAINT pk_deducciones
		PRIMARY KEY (id_deduccion),
	CONSTRAINT chk_deduccion_tipo
		CHECK (tipo_monto in ('F', 'P')),
	CONSTRAINT chk_deduccion_duracion
		CHECK (tipo_duracion in ('S', 'B'))
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'nominas' AND type = 'U')
	DROP TABLE nominas;
GO

CREATE TABLE nominas(
	id_nomina					INT IDENTITY(1,1) NOT NULL,
	sueldo_diario				MONEY NOT NULL,
	sueldo_bruto				MONEY NOT NULL,
	sueldo_neto					MONEY NOT NULL,
	banco						INT NOT NULL,
	numero_cuenta				VARCHAR(16) NOT NULL,
	fecha						DATE NOT NULL,
	numero_empleado				INT NOT NULL,
	id_departamento				INT NOT NULL,
	id_puesto					INT NOT NULL

	CONSTRAINT pk_nominas
		PRIMARY KEY (id_nomina),
	CONSTRAINT chk_nomina_sueldo
		CHECK (sueldo_neto > 0)
);



-- Tablas asociativas
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'percepciones_aplicadas' AND type = 'U')
	DROP TABLE percepciones_aplicadas;
GO

CREATE TABLE percepciones_aplicadas(
	id_percepcion_aplicada		INT IDENTITY(1,1) NOT NULL,
	numero_empleado				INT NOT NULL,
	id_percepcion				INT NOT NULL,
	id_nomina					INT,
	cantidad					MONEY NOT NULL,
	fecha						DATE NOT NULL

	CONSTRAINT pk_percepciones_aplicadas
		PRIMARY KEY (id_percepcion_aplicada)
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'deducciones_aplicadas' AND type = 'U')
	DROP TABLE deducciones_aplicadas;
GO

CREATE TABLE deducciones_aplicadas(
	id_deduccion_aplicada		INT IDENTITY(1,1) NOT NULL,
	numero_empleado				INT NOT NULL,
	id_deduccion				INT NOT NULL,
	id_nomina					INT,
	cantidad					MONEY NOT NULL,
	fecha						DATE NOT NULL

	CONSTRAINT pk_deducciones_aplicadas
		PRIMARY KEY (id_deduccion_aplicada)
);



-- Tablas de Normalizacion
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'domicilios' AND type = 'U')
	DROP TABLE domicilios;
GO

CREATE TABLE domicilios(
	id_domicilio				INT IDENTITY(1,1) NOT NULL,
	calle						VARCHAR(30) NOT NULL,
	numero						VARCHAR(10) NOT NULL,
	colonia						VARCHAR(30) NOT NULL,
	ciudad						VARCHAR(60) NOT NULL,
	estado						VARCHAR(60) NOT NULL,
	codigo_postal				VARCHAR(5) NOT NULL

	CONSTRAINT pk_domicilios
		PRIMARY KEY (id_domicilio)
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'telefonos_empresas' AND type = 'U')
	DROP TABLE telefonos_empresas;
GO

CREATE TABLE telefonos_empresas(
	id_telefono_empresa			INT IDENTITY(1,1) NOT NULL,
	telefono					VARCHAR(12) NOT NULL,
	id_empresa					INT NOT NULL

	CONSTRAINT pk_telefonos_empresas
		PRIMARY KEY (id_telefono_empresa)
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'telefonos_empleados' AND type = 'U')
	DROP TABLE telefonos_empleados;
GO

CREATE TABLE telefonos_empleados(
	id_telefono_empleado		INT IDENTITY(1,1),
	telefono					VARCHAR(12) NOT NULL,
	numero_empleado				INT NOT NULL

	CONSTRAINT pk_telefonos_empleados
		PRIMARY KEY (id_telefono_empleado)
);



IF EXISTS (SELECT name FROM sysobjects WHERE name = 'bancos' AND type = 'U')
	DROP TABLE bancos;

CREATE TABLE bancos(
	id_banco					INT IDENTITY(1,1),
	nombre						VARCHAR(30) NOT NULL

	CONSTRAINT pk_bancos
		PRIMARY KEY (id_banco)
);



-- Llaves foraneas
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


--INSERT INTO domicilios(calle, numero, colonia, ciudad, estado, codigo_postal)
--VALUES('Montes de Leon', '935', 'Las Puentes 6to Sector', 'San Nicolas de los Garza', 'Nuevo Leon', '66460');

--INSERT INTO empresas(razon_social, domicilio_fiscal, correo_electronico, rfc, registro_patronal, fecha_inicio, id_administrador)
--VALUES('Crystal Soft Development S.A. de C.V.', 1, 'crystal@domain.com', 'MOV1004082C1', 'Y5499995107','20101026', 1);
