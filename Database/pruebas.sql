SELECT*FROM departments;
SELECT*FROM positions;
SELECT*FROM employees;
SELECT*FROM perceptions;
SELECT*FROM employees_perceptions;









EXEC sp_AddPosition 'Gerente', 0.5, 1;
EXEC sp_AddPosition 'Presidente', 1.5, 1;

EXEC sp_ApplyEmployeePerception 1, 1, '2022-03-01';
EXEC sp_ApplyEmployeePerception 1, 3, '2022-03-01';
EXEC sp_ApplyEmployeePerception 1, 3, '2022-02-01';

EXEC sp_UpdateDepartment 1, null, 128.32;
EXEC sp_UpdatePosition 1, null, 0.32;


EXEC sp_AddEmployee 'Eliam', 'Rodriguez', 'Perez', '2001-10-26', 'A', 'B', 'C', 1, 1, 1, 'PerezAlex088@outlook.com',
'123', 1, 1;

INSERT INTO banks(name) VALUES('Banorte');


EXEC sp_AddPerception 'Bono de navidad', 'F', 220.20, 0;
EXEC sp_AddPerception 'Bono de puntualidad', 'P', 0, 0.4;