USE sistema_de_nomina;

-- Administradores
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual del administrador, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Contrase�a del administrador para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'contrasena';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electr�nico del administrador para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'correo_electronico';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los administradores',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'id_administrador';
GO



-- Bancos
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los bancos',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'bancos',
@level2type = N'Column', @level2name = 'id_banco';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre del banco',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'bancos',
@level2type = N'Column', @level2name = 'nombre';
GO



-- Deducciones
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual de la deducci�n, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dada de alta la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dada de baja la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad fija que va a aplicar la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'fijo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de las deducciones',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'id_deduccion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la empresa a la que pertenece la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre de la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Porcentaje en relaci�n con el sueldo bruto del empleado que va a aplicar la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'porcentual';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Indica si una deducci�n es b�sica con B o especial con S',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'tipo_duracion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Forma en la que va a aplicar sobre el sueldo la deducci�n, F para fijo, P para porcentual',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'tipo_monto';
GO



-- Deducciones aplicadas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Total, que rest� la deducci�n al sueldo',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'cantidad';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o que en el que se aplic� la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'fecha';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la deducci�n que fue aplicada',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'id_deduccion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de las deducciones aplicadas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'id_deduccion_aplicada';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la n�mina en la que se aplic� la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'id_nomina';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del empleado al que se le aplic� la deducci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'numero_empleado';
GO



-- Departamentos
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual del departamento, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dado de alta el departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dado de baja el departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los departamentos',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la empresa a la que pertenece el departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre del departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad de dinero que hace referencia al sueldo fijo de un departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'sueldo_base';
GO



-- Domicilios
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Calle del domicilio',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'calle';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Municipio del domicilio',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'ciudad';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'C�digo de 5 digitos que identifica la zona del domicilio',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'codigo_postal';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Colonia del domicilio',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'colonia';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado del domicilio',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'estado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los domicilios',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'id_domicilio';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'N�mero de casa del domicilio',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'numero';
GO



-- Empleados
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual del empleado, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Apellido materno del empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'apellido_materno';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Apellido paterno del empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'apellido_paterno';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del banco al que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'banco';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Contrase�a del empleado para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'contrasena';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electr�nico del empleado para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'correo_electronico';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'C�digo de 18 d�gitos utilizado para identificar oficialmente a los ciudadanos en M�xico',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'curp';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del domicilio a la que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'domicilio';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'D�a, mes y a�o cuando fue dado de alta el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'fecha_contratacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dado de baja el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha en que naci� el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'fecha_nacimiento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del departamento al que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del puesto al que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'id_puesto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre del empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'C�digo �nico que recibe cada trabajador que est� inscrito al IMSS',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'nss';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'C�digo de la cuenta bancaria a la que se le va a depositar el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'numero_cuenta';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los empleados',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'numero_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave que requiere toda persona en M�xico para llevar a cabo actividades econ�micas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'rfc';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo diario que recibe el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'sueldo_diario';
GO



-- Empresas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual de la empresa, puede estar activa o eliminada',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Dato de contacto de la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'correo_electronico';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del domicilio al que pertenece la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'domicilio_fiscal';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dada de baja la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dada de alta la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'fecha_inicio';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del administrador que gestiona la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'id_administrador';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de las empresas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre con el que una empresa est� registrada legalmente',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'razon_social';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'C�digo de c�digo de 11 digitos que se le asigna un patron cuando se registra en el IMSS',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'registro_patronal';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave que requiere toda persona en M�xico para llevar a cabo actividades econ�micas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'rfc';
GO



-- Nominas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del banco en el que se deposita la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'banco';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o en la que fue generada la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'fecha';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del departamento en el que el empleado trabajaba cuando se aplic� la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de las n�minas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'id_nomina';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del puesto en el que el empleado trabajaba cuando se aplic� la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'id_puesto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cuenta a la que se le deposita la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'numero_cuenta';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del empleado al que se le aplic� la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'numero_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo diario multiplicado por el total de d�as trabajados',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'sueldo_bruto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo diario de la n�mina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'sueldo_diario';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo bruto m�s las percepciones y menos las deducciones aplicadas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'sueldo_neto';
GO



-- Deducciones
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual de la percepci�n, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dada de alta la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dada de baja la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad fija que va a aplicar la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'fijo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la empresa a la que pertenece la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de las percepciones',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'id_percepcion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre de la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Porcentaje en relaci�n con el sueldo bruto del empleado que va a aplicar la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'porcentual';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Indica si una percepci�n es b�sica con B o especial con S',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'tipo_duracion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Forma en la que va a aplicar sobre el sueldo la percepci�n, F para fijo, P para porcentual',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'tipo_monto';
GO



-- Percepciones aplicadas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Total, que rest� la percepci�n al sueldo',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'cantidad';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o que en el que se aplic� la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'fecha';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la n�mina en la que se aplic� la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'id_nomina';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la percepci�n que fue aplicada',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'id_percepcion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de las percepciones aplicadas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'id_percepcion_aplicada';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico del empleado al que se le aplic� la percepci�n',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'numero_empleado';
GO



-- Puestos
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual del puesto, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dado de alta el puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y a�o cuando fue dado de baja el puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de la empresa a la que pertenece el puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los puestos',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'id_puesto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad porcentual que va a recibir un empleado en proporci�n al sueldo base de su departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'nivel_salarial';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre del puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'nombre';
GO



-- Telefonos empleados
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los telefonos de empleados',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empleados',
@level2type = N'Column', @level2name = 'id_telefono_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del empleado al que le pertenece el tel�fono',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empleados',
@level2type = N'Column', @level2name = 'numero_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tel�fono del empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empleados',
@level2type = N'Column', @level2name = 'telefono';
GO



-- Telefonos empresas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador de la empresa al que le pertenece el tel�fono',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empresas',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador �nico de los telefonos de empresas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empresas',
@level2type = N'Column', @level2name = 'id_telefono_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Tel�fono de la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empresas',
@level2type = N'Column', @level2name = 'telefono';
GO