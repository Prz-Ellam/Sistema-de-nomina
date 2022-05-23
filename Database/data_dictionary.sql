USE sistema_de_nomina;

-- Administradores
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual del administrador, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Contraseña del administrador para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'contrasena';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electrónico del administrador para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'correo_electronico';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de los administradores',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'administradores',
@level2type = N'Column', @level2name = 'id_administrador';
GO



-- Bancos
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de los bancos',
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
@name = N'MS_Description', @value = 'Estado actual de la deducción, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dada de alta la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dada de baja la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad fija que va a aplicar la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'fijo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de las deducciones',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'id_deduccion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la empresa a la que pertenece la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre de la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Porcentaje en relación con el sueldo bruto del empleado que va a aplicar la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'porcentual';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Indica si una deducción es básica con B o especial con S',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'tipo_duracion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Forma en la que va a aplicar sobre el sueldo la deducción, F para fijo, P para porcentual',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones',
@level2type = N'Column', @level2name = 'tipo_monto';
GO



-- Deducciones aplicadas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Total, que restó la deducción al sueldo',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'cantidad';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año que en el que se aplicó la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'fecha';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la deducción que fue aplicada',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'id_deduccion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de las deducciones aplicadas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'id_deduccion_aplicada';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la nómina en la que se aplicó la deducción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'deducciones_aplicadas',
@level2type = N'Column', @level2name = 'id_nomina';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del empleado al que se le aplicó la deducción',
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
@name = N'MS_Description', @value = 'Mes y año cuando fue dado de alta el departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dado de baja el departamento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de los departamentos',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'departamentos',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la empresa a la que pertenece el departamento',
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
@name = N'MS_Description', @value = 'Código de 5 digitos que identifica la zona del domicilio',
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
@name = N'MS_Description', @value = 'Identificador único de los domicilios',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'domicilios',
@level2type = N'Column', @level2name = 'id_domicilio';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Número de casa del domicilio',
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
@name = N'MS_Description', @value = 'Identificador único del banco al que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'banco';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Contraseña del empleado para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'contrasena';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Correo electrónico del empleado para ingresar al sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'correo_electronico';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Código de 18 dígitos utilizado para identificar oficialmente a los ciudadanos en México',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'curp';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del domicilio a la que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'domicilio';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Día, mes y año cuando fue dado de alta el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'fecha_contratacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dado de baja el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Fecha en que nació el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'fecha_nacimiento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del departamento al que pertenece el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del puesto al que pertenece el empleado',
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
@name = N'MS_Description', @value = 'Código único que recibe cada trabajador que está inscrito al IMSS',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'nss';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Código de la cuenta bancaria a la que se le va a depositar el empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'numero_cuenta';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de los empleados',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empleados',
@level2type = N'Column', @level2name = 'numero_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave que requiere toda persona en México para llevar a cabo actividades económicas',
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
@name = N'MS_Description', @value = 'Identificador único del domicilio al que pertenece la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'domicilio_fiscal';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dada de baja la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dada de alta la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'fecha_inicio';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del administrador que gestiona la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'id_administrador';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de las empresas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre con el que una empresa está registrada legalmente',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'razon_social';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Código de código de 11 digitos que se le asigna un patron cuando se registra en el IMSS',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'registro_patronal';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Clave que requiere toda persona en México para llevar a cabo actividades económicas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'empresas',
@level2type = N'Column', @level2name = 'rfc';
GO



-- Nominas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del banco en el que se deposita la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'banco';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año en la que fue generada la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'fecha';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del departamento en el que el empleado trabajaba cuando se aplicó la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de las nóminas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'id_nomina';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del puesto en el que el empleado trabajaba cuando se aplicó la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'id_puesto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cuenta a la que se le deposita la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'numero_cuenta';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del empleado al que se le aplicó la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'numero_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo diario multiplicado por el total de días trabajados',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'sueldo_bruto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo diario de la nómina',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'sueldo_diario';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Sueldo bruto más las percepciones y menos las deducciones aplicadas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'nominas',
@level2type = N'Column', @level2name = 'sueldo_neto';
GO



-- Deducciones
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Estado actual de la percepción, puede estar activo o eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dada de alta la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dada de baja la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad fija que va a aplicar la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'fijo';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la empresa a la que pertenece la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de las percepciones',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'id_percepcion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Nombre de la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Porcentaje en relación con el sueldo bruto del empleado que va a aplicar la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'porcentual';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Indica si una percepción es básica con B o especial con S',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'tipo_duracion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Forma en la que va a aplicar sobre el sueldo la percepción, F para fijo, P para porcentual',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones',
@level2type = N'Column', @level2name = 'tipo_monto';
GO



-- Percepciones aplicadas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Total, que restó la percepción al sueldo',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'cantidad';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año que en el que se aplicó la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'fecha';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la nómina en la que se aplicó la percepción',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'id_nomina';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la percepción que fue aplicada',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'id_percepcion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de las percepciones aplicadas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'percepciones_aplicadas',
@level2type = N'Column', @level2name = 'id_percepcion_aplicada';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único del empleado al que se le aplicó la percepción',
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
@name = N'MS_Description', @value = 'Mes y año cuando fue dado de alta el puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'fecha_creacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Mes y año cuando fue dado de baja el puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'fecha_eliminacion';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de un registro eliminado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'id_eliminado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de la empresa a la que pertenece el puesto',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de los puestos',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'puestos',
@level2type = N'Column', @level2name = 'id_puesto';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Cantidad porcentual que va a recibir un empleado en proporción al sueldo base de su departamento',
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
@name = N'MS_Description', @value = 'Identificador único de los telefonos de empleados',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empleados',
@level2type = N'Column', @level2name = 'id_telefono_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador del empleado al que le pertenece el teléfono',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empleados',
@level2type = N'Column', @level2name = 'numero_empleado';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Teléfono del empleado',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empleados',
@level2type = N'Column', @level2name = 'telefono';
GO



-- Telefonos empresas
EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador de la empresa al que le pertenece el teléfono',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empresas',
@level2type = N'Column', @level2name = 'id_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Identificador único de los telefonos de empresas',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empresas',
@level2type = N'Column', @level2name = 'id_telefono_empresa';
GO

EXEC sp_addextendedproperty
@name = N'MS_Description', @value = 'Teléfono de la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table', @level1name = 'telefonos_empresas',
@level2type = N'Column', @level2name = 'telefono';
GO