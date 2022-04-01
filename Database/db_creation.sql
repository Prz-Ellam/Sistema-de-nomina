-- DROP DATABASE payroll_system;
-- CREATE DATABASE payroll_system;
USE payroll_system;

-- Tablas
IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'administrators' AND type = 'u')
	DROP TABLE administrators;

CREATE TABLE administrators(
	administrator_id		INT IDENTITY(1,1) NOT NULL,
	email					VARCHAR(60) UNIQUE NOT NULL,
	password				VARCHAR(30) NOT NULL,
	active					BIT DEFAULT 1,
	company_id				INT NOT NULL

	CONSTRAINT PK_Administrator
		PRIMARY KEY (administrator_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'companies' AND type = 'u')
	DROP TABLE companies;

CREATE TABLE companies(
	company_id				INT IDENTITY(1,1) NOT NULL,
	business_name			VARCHAR(60) NOT NULL,
	address					INT NOT NULL,
	email					VARCHAR(30) UNIQUE NOT NULL,
	rfc						VARCHAR(18) UNIQUE NOT NULL,
	employer_registration	VARCHAR(30) NOT NULL,
	start_date				DATE NOT NULL,
	active					BIT DEFAULT 1

	CONSTRAINT PK_Company
		PRIMARY KEY (company_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'departments' AND type = 'u')
	DROP TABLE departments;

CREATE TABLE departments(
	department_id			INT IDENTITY(1,1) NOT NULL,
	name					VARCHAR(30) NOT NULL,
	base_salary				MONEY NOT NULL,
	active					BIT DEFAULT 1 NOT NULL,
	company_id				INT NOT NULL

	CONSTRAINT PK_Department
		PRIMARY KEY (department_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'positions' AND type = 'u')
	DROP TABLE positions;

CREATE TABLE positions(
	position_id				INT IDENTITY(1,1) NOT NULL,
	name					VARCHAR(30) NOT NULL,
	wage_level				FLOAT NOT NULL,
	active					BIT DEFAULT 1 NOT NULL,
	company_id				INT NOT NULL

	CONSTRAINT PK_Position
		PRIMARY KEY (position_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'employees' AND type = 'u')
	DROP TABLE employees;

CREATE TABLE employees(
	employee_number			INT IDENTITY(1,1) NOT NULL,
	name					VARCHAR(30) NOT NULL,
	father_last_name		VARCHAR(30) NOT NULL,
	mother_last_name		VARCHAR(30) NOT NULL,
	date_of_birth			DATE NOT NULL,
	curp					VARCHAR(20) UNIQUE NOT NULL,
	nss						VARCHAR(20)	UNIQUE NOT NULL,
	rfc						VARCHAR(20)	UNIQUE NOT NULL,
	address					INT NOT NULL,
	bank					INT NOT NULL,
	account_number			INT UNIQUE NOT NULL,
	email					VARCHAR(60) UNIQUE NOT NULL,
	password				VARCHAR(30) NOT NULL,
	active					BIT DEFAULT 1,
	department_id			INT NOT NULL,
	position_id				INT NOT NULL,
	hiring_date				DATE NOT NULL

	CONSTRAINT PK_Employee
		PRIMARY KEY (employee_number)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'perceptions' AND type = 'u')
	DROP TABLE perceptions;

CREATE TABLE perceptions(
	perception_id			INT IDENTITY(1,1) NOT NULL,
	name					VARCHAR(30) NOT NULL,
	amount_type				CHAR NOT NULL,
	fixed					MONEY,
	percentage				FLOAT,
	duration_type			CHAR DEFAULT 'S' NOT NULL, -- 'B' es de basic (basico) y 'S' es de special (Especial)
	active					BIT DEFAULT 1 NOT NULL

	CONSTRAINT PK_Perception
		PRIMARY KEY (perception_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'deductions' AND type = 'u')
	DROP TABLE deductions;

CREATE TABLE deductions(
	id						INT IDENTITY(1,1) NOT NULL,
	name					VARCHAR(30) NOT NULL,
	amount_type				CHAR NOT NULL,
	fixed					MONEY,
	percentage				FLOAT,
	duration_type			CHAR DEFAULT 'P' NOT NULL,
	active					BIT DEFAULT 1 NOT NULL

	CONSTRAINT PK_Deduction
		PRIMARY KEY (id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'apply_perceptions' AND type = 'u')
	DROP TABLE apply_perceptions;

CREATE TABLE apply_perceptions(
	apply_perception_id		INT IDENTITY(1,1) NOT NULL,
	employee_id				INT NOT NULL,
	perception_id			INT NOT NULL,
	payroll_id				INT,
	amount					MONEY NOT NULL,
	actual_date				DATE NOT NULL

	CONSTRAINT PK_Employees_Perceptions
		PRIMARY KEY (apply_perception_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'apply_deductions' AND type = 'u')
	DROP TABLE apply_deductions;

CREATE TABLE apply_deductions(
	apply_deduction_id		INT IDENTITY(1,1) NOT NULL,
	employee_id				INT NOT NULL,
	deduction_id			INT NOT NULL,
	payroll_id				INT,
	amount					MONEY NOT NULL,
	actual_date				DATE NOT NULL

	CONSTRAINT PK_Employees_Deductions
		PRIMARY KEY (apply_deduction_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'payrolls' AND type = 'u')
	DROP TABLE payrolls;

CREATE TABLE payrolls(
	payroll_id				INT IDENTITY(1,1) NOT NULL,
	daily_salary			MONEY NOT NULL,
	gross_salary			MONEY NOT NULL,
	net_salary				MONEY NOT NULL,
	bank					INT NOT NULL,
	account_number			INT NOT NULL,
	actual_date				DATE NOT NULL,
	employee_id				INT NOT NULL,
	department_id			INT NOT NULL,
	position_id				INT NOT NULL

	CONSTRAINT PK_Payrolls
		PRIMARY KEY (payroll_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'addresses' AND type = 'u')
	DROP TABLE addresses;

CREATE TABLE addresses(
	address_id				INT IDENTITY(1,1) NOT NULL,
	street					VARCHAR(30) NOT NULL,
	number					VARCHAR(10) NOT NULL,
	suburb					VARCHAR(30) NOT NULL,
	city					VARCHAR(30) NOT NULL,
	state					VARCHAR(30) NOT NULL,
	postal_code				VARCHAR(5) NOT NULL

	CONSTRAINT PK_Addresses
		PRIMARY KEY (address_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'phones_companies' AND type = 'u')
	DROP TABLE phones_companies;

CREATE TABLE phones_companies(
	phone_company_id		INT IDENTITY(1,1) NOT NULL,
	phone					VARCHAR(12) NOT NULL,
	company_id				INT NOT NULL

	CONSTRAINT PK_Phones_Companies
		PRIMARY KEY (phone_company_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'phones_employees' AND type = 'u')
	DROP TABLE phones_employees;

CREATE TABLE phones_employees(
	phone_employee_id		INT IDENTITY(1,1) NOT NULL,
	phone					VARCHAR(12) NOT NULL,
	employee_id				INT NOT NULL

	CONSTRAINT PK_Phones_Employees
		PRIMARY KEY (phone_employee_id)
);

IF EXISTS(SELECT 1 FROM sysobjects WHERE name = 'banks' AND type = 'u')
	DROP TABLE banks;

CREATE TABLE banks(
	bank_id					INT IDENTITY(1,1) NOT NULL,
	name					VARCHAR(30) NOT NULL

	CONSTRAINT PK_Banks
		PRIMARY KEY (bank_id)
);


-- Llaves foraneas
ALTER TABLE administrators
	ADD CONSTRAINT FK_Admin_Company
		FOREIGN KEY (company_id)
		REFERENCES Companies(company_id);

ALTER TABLE companies
	ADD CONSTRAINT FK_Company_Address
		FOREIGN KEY (address)
		REFERENCES addresses(address_id);

ALTER TABLE departments
	ADD CONSTRAINT FK_Department_Company
		FOREIGN KEY (company_id)
		REFERENCES companies(company_id);

ALTER TABLE positions
	ADD CONSTRAINT FK_Position_Company
		FOREIGN KEY (company_id)
		REFERENCES companies(company_id);

ALTER TABLE employees
	ADD CONSTRAINT FK_Employee_Address
		FOREIGN KEY (address)
		REFERENCES addresses(address_id),
		CONSTRAINT FK_Employee_Bank
		FOREIGN KEY (bank)
		REFERENCES banks(bank_id),
		CONSTRAINT FK_Employee_Department
		FOREIGN KEY (department_id)
		REFERENCES departments(department_id),
		CONSTRAINT FK_Employee_Position
		FOREIGN KEY (position_id)
		REFERENCES positions(position_id);

ALTER TABLE apply_perceptions
	ADD CONSTRAINT FK_P_Employee
		FOREIGN KEY (employee_id)
		REFERENCES employees(employee_number),
		CONSTRAINT FK_P_Perception
		FOREIGN KEY (perception_id)
		REFERENCES perceptions(perception_id),
		CONSTRAINT FK_P_Payroll
		FOREIGN KEY (payroll_id)
		REFERENCES payrolls(payroll_id);

ALTER TABLE apply_deductions
	ADD CONSTRAINT FK_D_Employee
		FOREIGN KEY (employee_id)
		REFERENCES employees(employee_number),
		CONSTRAINT FK_D_Deduction
		FOREIGN KEY (deduction_id)
		REFERENCES deductions(id),
		CONSTRAINT FK_D_Payroll
		FOREIGN KEY (payroll_id)
		REFERENCES payrolls(payroll_id);

ALTER TABLE payrolls
	ADD CONSTRAINT FK_Payroll_Bank
		FOREIGN KEY (bank)
		REFERENCES banks(bank_id),
		CONSTRAINT FK_Payroll_Employee
		FOREIGN KEY (employee_id)
		REFERENCES employees(employee_number),
		CONSTRAINT FK_Payroll_Department
		FOREIGN KEY (department_id)
		REFERENCES departments(department_id),
		CONSTRAINT FK_Payroll_Position
		FOREIGN KEY (position_id)
		REFERENCES positions(position_id);

ALTER TABLE phones_companies
	ADD CONSTRAINT PK_Phone_Company
		FOREIGN KEY (company_id)
		REFERENCES companies(company_id);

ALTER TABLE phones_employees
	ADD CONSTRAINT PK_Phone_Employee
		FOREIGN KEY (employee_id)
		REFERENCES employees(employee_number);




/*
ALTER TABLE companies
	DROP CONSTRAINT FK_Company_Address;

ALTER TABLE departments
	DROP CONSTRAINT FK_Department_Company;

ALTER TABLE positions
	DROP CONSTRAINT FK_Position_Company;

ALTER TABLE employees
	DROP CONSTRAINT FK_Employee_Address,
		CONSTRAINT FK_Employee_Bank,
		CONSTRAINT FK_Employee_Department,
		CONSTRAINT FK_Employee_Position;

ALTER TABLE employees_perceptions
	DROP CONSTRAINT FK_EP_Employee,
		CONSTRAINT FK_EP_Perception;

ALTER TABLE employees_deductions
	DROP CONSTRAINT FK_ED_Employee,
		CONSTRAINT FK_ED_Deduction;

ALTER TABLE payrolls
	DROP CONSTRAINT FK_Payroll_Bank,
		CONSTRAINT FK_Payroll_Employee,
		CONSTRAINT FK_Payroll_Department,
		CONSTRAINT FK_Payroll_Position;
		
ALTER TABLE payrolls_perceptions
	DROP CONSTRAINT PK_PP_Payroll,
		CONSTRAINT PK_PP_Perception;

ALTER TABLE payrolls_deductions
	DROP CONSTRAINT PK_PD_Payroll,
		CONSTRAINT PK_PD_Deduction;


ALTER TABLE phones_companies
	DROP CONSTRAINT PK_Phone_Company;

ALTER TABLE phones_employees
	DROP CONSTRAINT PK_Phone_Employee;
*/


SELECT * FROM addresses;
SELECT * FROM companies;


INSERT INTO addresses(street, number, suburb, city, state, postal_code)
VALUES('Montes de Leon', '935', 'Las Puentes 6to Sector', 'San Nicolas de los Garza', 'Nuevo Leon', '66460');

INSERT INTO companies(business_name, address, email, rfc, employer_registration, start_date)
VALUES('Crystal Soft Development S.A. de C.V.', 1, 'crystal@domain.com', 'MOV1004082C1', 'Y5499995107',
'20101026');

INSERT INTO administrators(email, password, company_id)
VALUES('PerezAlex088@outlook.com', '123', '1');




SELECT schemas.name AS SchemaName
,all_objects.name AS TableName ,syscolumns.id AS ColumnId
,syscolumns.name AS ColumnName ,systypes.name AS DataType
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


SELECT IC.COLUMN_NAME, IC.Data_TYPE, EP.[Value] as [MS_Description], IKU.CONSTRAINT_NAME,
ITC.CONSTRAINT_TYPE, IC.IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS IC
INNER JOIN sys.columns sc ON OBJECT_ID(QUOTENAME(IC.TABLE_SCHEMA) + '.' +
QUOTENAME(IC.TABLE_NAME)) = sc.[object_id] AND IC.COLUMN_NAME = sc.name
LEFT OUTER JOIN sys.extended_properties EP ON sc.[object_id] = EP.major_id AND
sc.[column_id] = EP.minor_id AND EP.name = 'MS_Description' AND EP.class = 1
LEFT OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE IKU ON IKU.COLUMN_NAME =
IC.COLUMN_NAME and IKU.TABLE_NAME = IC.TABLE_NAME and IKU.TABLE_CATALOG = IC.TABLE_CATALOG
LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ITC ON ITC.TABLE_NAME =
IKU.TABLE_NAME and ITC.CONSTRAINT_NAME = IKU.CONSTRAINT_NAME
WHERE IC.TABLE_CATALOG = 'base_datos'
and IC.TABLE_SCHEMA = 'payroll_system'
--and IC.TABLE_NAME = 'Table'
order by IC.ORDINAL_POSITION
