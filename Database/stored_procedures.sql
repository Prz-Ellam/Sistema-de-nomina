USE payroll_system;

-- Usuarios
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Login')
	DROP PROCEDURE sp_Login;
GO

CREATE PROCEDURE sp_Login
	@type			CHAR(1),
	@email			VARCHAR(60),
	@password		VARCHAR(30)
AS

	IF @type = 'A'

		SELECT id, email FROM administrators
		WHERE email = @email AND password = @password;

	ELSE IF @type = 'E'

		SELECT employee_number, email FROM employees
		WHERE email = @email AND password = @password;

GO



-- Empresas
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AddCompany')
	DROP PROCEDURE sp_AddCompany;
GO

CREATE PROCEDURE sp_AddCompany
	@business_name			VARCHAR(30),
	@address				INT,
	@email					VARCHAR(30),
	@rfc					VARCHAR(18),
	@employer_registration	VARCHAR(30),
	@start_date				DATE
AS

	-- Solo puede haber una empresa para el proyecto, esta validacion impide crear mas
	IF (SELECT COUNT(1) FROM companies) > 0
	BEGIN
		RAISERROR(15600, 1, 1, 'Error', 'Ya existe la empresa');
		RETURN;
	END;

	IF NOT EXISTS(SELECT 1 FROM addresses WHERE id = @address)
	BEGIN
		RAISERROR(15600, 1, 1, 'Error', 'Direccion no existe');
		RETURN;
	END

	INSERT INTO companies(business_name, address, email, rfc, employer_registration, start_date)
	VALUES(@business_name, @address, @email, @rfc, @employer_registration, @start_date);
		
GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReadCompany')
	DROP PROCEDURE sp_ReadCompany;
GO

CREATE PROCEDURE sp_ReadCompany
	@company_id			INT
AS

	SELECT id, business_name, address, email, rfc, employer_registration, start_date, active
	FROM companies WHERE id = @company_id AND active = 1;

GO



-- Departamentos
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AddDepartment')
	DROP PROCEDURE sp_AddDepartment;
GO

CREATE PROCEDURE sp_AddDepartment
	@name				VARCHAR(60),
	@base_salary		MONEY,
	@company_id			INT
AS

	IF NOT EXISTS (SELECT id FROM companies WHERE id = @company_id)
		BEGIN
			RAISERROR(15600,1,1,'Error','No existe la empresa');
			RETURN;
		END;

	INSERT INTO departments(name, base_salary, company_id)
	VALUES (@name, @base_salary, @company_id);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UpdateDepartment')
	DROP PROCEDURE sp_UpdateDepartment;
GO

CREATE PROCEDURE sp_UpdateDepartment
	@id					INT,
	@name				VARCHAR(60),
	@base_salary		MONEY
AS

	UPDATE departments
	SET
	name =			ISNULL(@name, name),
	base_salary =	ISNULL(@base_salary, base_salary)
	WHERE id = @id;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_DeleteDepartments')
	DROP PROCEDURE sp_DeleteDepartment;
GO

CREATE PROCEDURE sp_DeleteDepartment
	@id					INT
AS
	
	UPDATE departments
	SET
	active = 0
	WHERE id = @id;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReadDepartments')
	DROP PROCEDURE sp_ReadDepartments;
GO

CREATE PROCEDURE sp_ReadDepartments

AS

	SELECT id [ID], name [Nombre], base_salary [Sueldo base]
	FROM departments
	WHERE active = 1;

GO



-- Puestos
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AddPosition')
	DROP PROCEDURE sp_AddPosition;
GO

CREATE PROCEDURE sp_AddPosition
	@name				VARCHAR(60),
	@wage_level			FLOAT,
	@company_id			INT
AS

	IF NOT EXISTS (SELECT id FROM companies WHERE id = @company_id)
		BEGIN
			RAISERROR(15600, 1, 1, 'Error', 'No existe la empresa');
			RETURN;
		END;

	INSERT INTO positions(name, wage_level, company_id)
	VALUES (@name, @wage_level, @company_id);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UpdatePosition')
	DROP PROCEDURE sp_UpdatePosition;
GO

CREATE PROCEDURE sp_UpdatePosition
	@id					INT,
	@name				VARCHAR(60),
	@wage_level			FLOAT
AS

	UPDATE positions
	SET
	name =			ISNULL(@name, name),
	wage_level =	ISNULL(@wage_level, wage_level)
	WHERE id = @id;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_DeletePosition')
	DROP PROCEDURE sp_DeletePosition;
GO

CREATE PROCEDURE sp_DeletePosition
	@id					INT
AS
	
	UPDATE positions
	SET
	active = 0
	WHERE id = @id;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReadPositions')
	DROP PROCEDURE sp_ReadPositions;
GO

CREATE PROCEDURE sp_ReadPositions

AS

	SELECT id [ID], name [Nombre], wage_level [Nivel salarial]
	FROM positions
	WHERE active = 1;


GO


INSERT INTO banks(name) VALUES('Bancomer');
SELECT*FROM banks;
SELECT*FROM employees;
EXEC sp_AddEmployee 'Eliam', 'Rodriguez', 'Perez', '20011026', '1', '2', '3', 1, 1, 10, 'PerezAlex088@outlook.com',
'123', 3, 2, 1;

-- Empleados

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AddEmployee')
	DROP PROCEDURE sp_AddEmployee;
GO

CREATE PROCEDURE sp_AddEmployee
	@name				VARCHAR(30),
	@father_last_name	VARCHAR(30),
	@mother_last_name	VARCHAR(30),
	@date_of_birth		DATE,
	@curp				VARCHAR(20),
	@nss				VARCHAR(20),
	@rfc				VARCHAR(20),
	@address			INT,
	@bank				INT,
	@account_number		INT,
	@email				VARCHAR(60),
	@password			VARCHAR(30),
	@department_id		INT,
	@position_id		INT
AS

	IF NOT EXISTS (SELECT id FROM departments WHERE id = @department_id)
		BEGIN
			RAISERROR(15600,1,1,'Error','No existe el departamento');
			RETURN;
		END;

	IF NOT EXISTS (SELECT id FROM positions WHERE id = @position_id)
		BEGIN
			RAISERROR(15600,1,1,'Error','No existe el puesto');
			RETURN;
		END;

	INSERT INTO employees(name, father_last_name, mother_last_name, date_of_birth, curp, nss, rfc,
		address, bank, account_number, email, password, department_id, position_id)
	VALUES (@name, @father_last_name, @mother_last_name, @date_of_birth, @curp, @nss, @rfc, @address, 
		@bank, @account_number, @email, @password, @department_id, @position_id);

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UpdateEmployee')
	DROP PROCEDURE sp_UpdateEmployee;
GO

CREATE PROCEDURE sp_UpdateEmployee
	@employee_number	INT,
	@name				VARCHAR(30),
	@father_last_name	VARCHAR(30),
	@mother_last_name	VARCHAR(30),
	@date_of_birth		DATE,
	@curp				VARCHAR(20),
	@nss				VARCHAR(20),
	@rfc				VARCHAR(20),
	@address			INT,
	@bank				INT,
	@account_number		INT,
	@email				VARCHAR(60),
	@password			VARCHAR(30),
	@department_id		INT,
	@position_id		INT
AS

	UPDATE employees 
	SET
	name					= ISNULL(@name, name),
	father_last_name		= ISNULL(@father_last_name, father_last_name),
	mother_last_name		= ISNULL(@mother_last_name, mother_last_name),
	date_of_birth			= ISNULL(@date_of_birth, date_of_birth),
	curp					= ISNULL(@curp, curp),
	nss						= ISNULL(@nss, nss),
	rfc						= ISNULL(@rfc, rfc),
	address					= ISNULL(@address, address),
	bank					= ISNULL(@bank, bank),
	account_number			= ISNULL(@account_number, account_number),
	email					= ISNULL(@email, email),
	password				= ISNULL(@password, password),
	department_id			= ISNULL(@department_id, department_id),
	position_id				= ISNULL(@position_id, position_id)
	WHERE employee_number = @employee_number;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_DeleteEmployee')
	DROP PROCEDURE sp_DeleteEmployee;
GO

CREATE PROCEDURE sp_DeleteEmployee
	@employee_number			INT
AS
	
	UPDATE employees
	SET
	active = 0
	WHERE employee_number = @employee_number;

GO



IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ReadEmployees')
	DROP PROCEDURE sp_ReadEmployees;
GO

CREATE PROCEDURE sp_ReadEmployees

AS

	SELECT	E.employee_number [ID], 
			E.name [Nombre], 
			E.father_last_name [Apellido paterno],
			E.mother_last_name [Apellido materno],
			E.date_of_birth [Fecha de nacimiento],
			E.curp [CURP],
			E.nss [NSS],
			E.rfc [RFC],
			A.street [Calle],
			A.number [Numero],
			A.suburb [Colonia],
			A.city [Municipio],
			A.state [Estado],
			A.postal_code [Codigo postal],
			B.name [Banco],
			E.account_number [Numero de cuenta],
			E.email [Correo electronico],
			D.name [Departamento],
			P.name [Puesto],
			E.hiring_date [Fecha de contratacion]
	FROM employees AS E
	JOIN addresses AS A
	ON A.id = E.address
	JOIN banks AS B
	ON B.id = E.bank
	JOIN departments AS D
	ON D.id = E.department_id
	JOIN positions AS P
	ON P.id = E.position_id
	WHERE E.active = 1;

GO

EXEC sp_ReadEmployees;






IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AddPerception')
	DROP PROCEDURE sp_AddPerception;
GO

CREATE PROCEDURE sp_AddPerception(
	@name				VARCHAR(30),
	@amount_type		CHAR(1),
	@fixed				MONEY,
	@percentage			FLOAT
) 
AS

	INSERT INTO perceptions(name, amount_type, fixed, percentage)
	VALUES(@name, @amount_type, @fixed, @percentage);

GO






IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UpdatePerception')
	DROP PROCEDURE sp_UpdatePerception;
GO

CREATE PROCEDURE sp_UpdatePerception(
	@id					INT,
	@name				VARCHAR(30),
	@amount_type		CHAR(1),
	@fixed				MONEY,
	@percentage			FLOAT
)
AS

	UPDATE perceptions
	SET
	name = ISNULL(@name, name),
	amount_type = ISNULL(@amount_type, amount_type),
	fixed = ISNULL(@fixed, fixed),
	percentage = ISNULL(@percentage, percentage)
	WHERE
	id = @id;

GO

-- CREATE PROCEDURE sp_DeletePerception

-- CREATE PROCEDURE sp_ReadPerceptions

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_AddDeduction')
	DROP PROCEDURE sp_AddDeduction;
GO

CREATE PROCEDURE sp_AddDeduction(
	@name				VARCHAR(30),
	@amount_type		CHAR(1),
	@fixed				MONEY,
	@percentage			FLOAT
) 
AS

	-- Validar que amount_type solo sea los dos caracteres aceptados

	INSERT INTO deductions(name, amount_type, fixed, percentage)
	VALUES(@name, @amount_type, @fixed, @percentage);

GO


IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UpdateDeduction')
	DROP PROCEDURE sp_UpdateDeduction;
GO

CREATE PROCEDURE sp_UpdateDeduction(
	@id					INT,
	@name				VARCHAR(30),
	@amount_type		CHAR(1),
	@fixed				MONEY,
	@percentage			FLOAT
)
AS

	UPDATE deductions
	SET
	name = ISNULL(@name, name),
	amount_type = ISNULL(@amount_type, amount_type),
	fixed = ISNULL(@fixed, fixed),
	percentage = ISNULL(@percentage, percentage)
	WHERE
	id = @id;

GO

-- CREATE PROCEDURE sp_DeleteDeduction

-- CREATE PROCEDURE sp_ReadDeductions


-- Validaciones de que pasa si una percepcion o deduccion ya fue aplicada a un empleado !!!

-- CREATE PROCEDURE sp_ApplyEmployeePerception
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ApplyEmployeePerception')
	DROP PROCEDURE sp_ApplyEmployeePerception;
GO

CREATE PROCEDURE sp_ApplyEmployeePerception(
	@employee_id			INT,
	@perception_id			INT,
	@actual_date			DATE
)
AS

	INSERT INTO employees_perceptions(employee_id, perception_id, actual_date)
	VALUES(@employee_id, @perception_id, @actual_date);

GO

-- CREATE PROCEDURE sp_ApplyEmployeeDeduction
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_ApplyEmployeeDeduction')
	DROP PROCEDURE sp_ApplyEmployeeDeduction;
GO

CREATE PROCEDURE sp_ApplyEmployeeDeduction(
	@employee_id			INT,
	@deduction_id			INT,
	@actual_date			DATE
)
AS

	INSERT INTO employees_perceptions(employee_id, perception_id, actual_date)
	VALUES(@employee_id, @deduction_id, @actual_date);

GO

-- CREATE PROCEDURE sp_UndoEmployeePerception
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UndoEmployeePerception')
	DROP PROCEDURE sp_UndoEmployeePerception;
GO

CREATE PROCEDURE sp_UndoEmployeePerception(
	@employee_id			INT,
	@perception_id			INT,
	@actual_date			DATE
)
AS

	DELETE FROM employees_perceptions
	WHERE employee_id = @employee_id AND perception_id = @perception_id AND actual_date = @actual_date;

GO

-- CREATE PROCEDURE sp_UndoEmployeeDeduction
IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_UndoEmployeeDeduction')
	DROP PROCEDURE sp_UndoEmployeeDeduction;
GO

CREATE PROCEDURE sp_UndoEmployeeDeduction(
	@employee_id			INT,
	@deduction_id			INT,
	@actual_date			DATE
)
AS

	DELETE FROM employees_deductions
	WHERE employee_id = @employee_id AND deduction_id = @deduction_id AND actual_date = @actual_date;

GO


-- CREATE PROCEDURE sp_ApplyDepartmentPerception

-- CREATE PROCEDURE sp_ApplyDepartmentDeduction

-- CREATE PROCEDURE sp_UndoDepartmentPerception

-- CREATE PROCEDURE sp_UndoDepartmentDeduction



-- CREATE PROCEDURE sp_GeneratePayroll

IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_GeneratePayroll')
	DROP PROCEDURE sp_GeneratePayroll;
GO

CREATE PROCEDURE sp_GeneratePayroll(
	@employee_number	INT,
	@gen_date			DATE
)
AS

	DECLARE @year		INT;
	DECLARE @month		INT;
	DECLARE @days		INT;

	SET @year = YEAR(@gen_date);
	SET @month = MONTH(@gen_date);
	SET @days =  dbo.GETMONTHLENGTH(@year, @month);

	--SELECT @year, @month, @days;

	DECLARE @daily_salary	MONEY;
	SET @daily_salary = (SELECT D.base_salary * P.wage_level 
						FROM employees
						JOIN departments AS D
						ON employees.department_id = D.id
						JOIN positions AS P
						ON employees.position_id = P.id
						WHERE employees.employee_number = @employee_number);

	DECLARE @gross_salary	MONEY;
	SET @gross_salary = @daily_salary * @days;

	SELECT @days AS [Dias], @daily_salary AS [Salario Diario], @gross_salary AS [Sueldo bruto]

/*
	SELECT perceptions.id
	FROM perceptions
	JOIN employees_perceptions
	ON employees_perceptions.perception_id = perceptions.id
	JOIN employees
	ON employees_perceptions.employee_id = employees.employee_number
	WHERE YEAR(employees_perceptions.actual_date) = @year
	AND MONTH(employees_perceptions.actual_date) = @month;

	SELECT deductions.id
	FROM deductions
	JOIN employees_deductions
	ON employees_deductions.deduction_id = deduction_id
	JOIN employees
	ON employees_deductions.employee_id = employees.employee_number
	WHERE YEAR(employees_deductions.actual_date) = @year
	AND MONTH(employees_deductions.actual_date) = @month;
*/

	DECLARE @total_perception_fixed	MONEY;
	SET @total_perception_fixed = (SELECT SUM(p.fixed) FROM perceptions AS p
	JOIN employees_perceptions AS ep
	ON ep.perception_id = p.id
	JOIN employees AS e
	ON ep.employee_id = e.employee_number
	WHERE ep.employee_id = @employee_number AND ep.perception_id = p.id AND ep.actual_date = @gen_date
	AND amount_type = 'F');

	DECLARE @total_perception_percentage	FLOAT;
	SET @total_perception_percentage = (SELECT SUM(p.percentage) FROM perceptions AS p
	JOIN employees_perceptions AS ep
	ON ep.perception_id = p.id
	JOIN employees AS e
	ON ep.employee_id = e.employee_number
	WHERE ep.employee_id = @employee_number AND ep.perception_id = p.id AND ep.actual_date = @gen_date
	AND amount_type = 'P');

	DECLARE @total_deduction_fixed		MONEY;
	DECLARE @total_deduction_percentage	FLOAT;

	DECLARE @net_salary	MONEY;
	SET @net_salary = @gross_salary + @total_perception_fixed + (@total_perception_percentage * @gross_salary)
	- @total_deduction_fixed - (@total_deduction_fixed * @total_deduction_percentage);


GO

EXEC sp_GeneratePayroll 1, '2022-03-01';

-- Esta es para obtener todas las nominas de una determinada fecha
-- CREATE PROCEDURE sp_ReadPayrollsByDate

-- Esta es para obtener la nomina de un empleado en cierto mes y anio
-- CREATE PROCEDURE sp_ReadEmployeePayroll





-- CREATE PROCEDURE sp_AddBank

-- CREATE PROCEDURE sp_UpdateBank

-- CREATE PROCEDURE sp_DeleteBank

-- CREATE PROCEDURE sp_ReadBanks

-- CREATE PROCEDURE sp_AddAddress

-- CREATE PROCEDURE sp_UpdateAddress

-- CREATE PROCEDURE sp_DeleteAddress




















DECLARE @sueldo_base	MONEY;
SET @sueldo_base = (SELECT D.base_salary 
					FROM employees AS E 
					JOIN departments AS D
					ON E.department_id = D.id
					WHERE E.employee_number = 1);

SELECT @sueldo_base;

DECLARE @wage_level		FLOAT;
SET @wage_level = (SELECT P.wage_level 
					FROM employees AS E 
					JOIN positions AS P
					ON E.position_id = P.id
					WHERE E.employee_number = 1);

SELECT @wage_level;

SELECT @wage_level * @sueldo_base;


DECLARE @money1		MONEY
SET @money1 = 2.54;

DECLARE @money2		MONEY
SET @money2 = 4.9644;

SELECT ROUND(@money1 * @money2, 2);








IF EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name = 'sp_Employees')
	DROP PROCEDURE sp_Employees;
GO

