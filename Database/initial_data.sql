USE sistema_de_nomina;

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

