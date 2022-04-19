using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Helpers;
using Data_Access.ViewModels;

namespace Data_Access.Repositorios
{
    public class RepositorioEmpleados
    {
        private readonly string create, update, delete, readAll, readLike, getEmployeesId, readPayrolls, getById;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioEmpleados()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarEmpleado";
            update = "sp_ActualizarEmpleado";
            delete = "sp_EliminarEmpleado";
            readAll = "sp_LeerEmpleados";
            getEmployeesId = "sp_ObtenerEmpleados";
            readPayrolls = "sp_LeerEmpleadosNominas";
            getById = "sp_ObtenerEmpleadoPorId";
        }

        public bool Create(Empleados employee)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", employee.Nombre);
            sqlParams.Add("@apellido_paterno", employee.ApellidoPaterno);
            sqlParams.Add("@apellido_materno", employee.ApellidoMaterno);
            sqlParams.Add("@fecha_nacimiento", employee.FechaNacimiento);
            sqlParams.Add("@curp", employee.Curp);
            sqlParams.Add("@nss", employee.Nss);
            sqlParams.Add("@rfc", employee.Rfc);

            sqlParams.Add("@calle", employee.Calle);
            sqlParams.Add("@numero", employee.Numero);
            sqlParams.Add("@colonia", employee.Colonia);
            sqlParams.Add("@ciudad", employee.Ciudad);
            sqlParams.Add("@estado", employee.Estado);
            sqlParams.Add("@codigo_postal", employee.CodigoPostal);

            sqlParams.Add("@banco", employee.Banco);
            sqlParams.Add("@numero_cuenta", employee.NumeroCuenta);
            sqlParams.Add("@correo_electronico", employee.CorreoElectronico);
            sqlParams.Add("@contrasena", employee.Contrasena);
            sqlParams.Add("@id_departamento", employee.IdDepartamento);
            sqlParams.Add("@id_puesto", employee.IdPuesto);
            sqlParams.Add("@fecha_contratacion", employee.FechaContratacion);
            sqlParams.Add("@telefonos", employee.Telefonos);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Empleados employee)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employee.NumeroEmpleado);
            sqlParams.Add("@nombre", employee.Nombre);
            sqlParams.Add("@apellido_paterno", employee.ApellidoPaterno);
            sqlParams.Add("@apellido_materno", employee.ApellidoMaterno);
            sqlParams.Add("@fecha_nacimiento", employee.FechaNacimiento);
            sqlParams.Add("@curp", employee.Curp);
            sqlParams.Add("@nss", employee.Nss);
            sqlParams.Add("@rfc", employee.Rfc);

            sqlParams.Add("@calle", employee.Calle);
            sqlParams.Add("@numero", employee.Numero);
            sqlParams.Add("@colonia", employee.Colonia);
            sqlParams.Add("@ciudad", employee.Ciudad);
            sqlParams.Add("@estado", employee.Estado);
            sqlParams.Add("@codigo_postal", employee.CodigoPostal);

            sqlParams.Add("@banco", employee.Banco);
            sqlParams.Add("@numero_cuenta", employee.NumeroCuenta);
            sqlParams.Add("@correo_electronico", employee.CorreoElectronico);
            sqlParams.Add("@contrasena", employee.Contrasena);
            sqlParams.Add("@id_departamento", employee.IdDepartamento);
            sqlParams.Add("@id_puesto", employee.IdPuesto);
            sqlParams.Add("@fecha_contratacion", employee.FechaContratacion);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int employeeNumber)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public List<EmployeesViewModel> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);
            List<EmployeesViewModel> departments = new List<EmployeesViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new EmployeesViewModel
                {
                    EmployeeNumber = Convert.ToInt32(row["ID"]),
                    Name = row["Nombre"].ToString(),
                    FatherLastName = row["Apellido Paterno"].ToString(),
                    MotherLastName = row["Apellido Materno"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["Fecha de nacimiento"]),
                    Curp = row["CURP"].ToString(),
                    Nss = row["NSS"].ToString(),
                    Rfc = row["RFC"].ToString(),
                    Street = row["Calle"].ToString(),
                    Number = row["Numero"].ToString(),
                    Suburb = row["Colonia"].ToString(),
                    City = row["Municipio"].ToString(),
                    State = row["Estado"].ToString(),
                    PostalCode = row["Codigo postal"].ToString(),
                    Bank = new PairItem(row["Banco"].ToString(), Convert.ToInt32(row["ID Banco"])),
                    AccountNumber = row["Numero de cuenta"].ToString(),
                    Email = row["Correo electronico"].ToString(),
                    Department = new PairItem(row["Departamento"].ToString(), Convert.ToInt32(row["ID Departamento"])),
                    Position = new PairItem(row["Puesto"].ToString(), Convert.ToInt32(row["ID Puesto"])),
                    HiringDate = Convert.ToDateTime(row["Fecha de contratacion"]),
                    SueldoDiario = Convert.ToDecimal(row["Sueldo diario"]),
                    BaseSalary = Convert.ToDecimal(row["Sueldo base"]),
                    WageLevel = Convert.ToDecimal(row["Nivel salarial"])
                });
            }

            return departments;
        }


        public List<int> GetEmployeesId()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(getEmployeesId, sqlParams);

            List<int> employeesId = new List<int>();
            foreach (DataRow row in table.Rows)
            {
                employeesId.Add(Convert.ToInt32(row[0]));
            }

            return employeesId;
        }

        public List<EmployeePayrollsViewModel> ReadEmployeePayrolls(DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readPayrolls, sqlParams);

            List<EmployeePayrollsViewModel> employeePayrolls = new List<EmployeePayrollsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                employeePayrolls.Add(new EmployeePayrollsViewModel
                {
                    NumeroEmpleado = Convert.ToInt32(row[0]),
                    NombreEmpleado = row[1].ToString(),
                    Departamento = row[2].ToString(),
                    Puesto = row[3].ToString(),
                    SueldoDiario = Convert.ToDecimal(row[4]),
                    DiasTrabajados = Convert.ToUInt32(row[5]),
                    SueldoBruto = Convert.ToDecimal(row[6]),
                    TotalPercepciones = Convert.ToDecimal(row[7]),
                    TotalDeducciones = Convert.ToDecimal(row[8]),
                    SueldoNeto = Convert.ToDecimal(row[9])
                });
            }

            return employeePayrolls;
        }

        public EmployeesViewModel GetEmployeeById(int employeeId)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeId);

            DataTable table = mainRepository.ExecuteReader(getById, sqlParams);
            foreach (DataRow row in table.Rows)
            {
                return new EmployeesViewModel
                {
                    EmployeeNumber = Convert.ToInt32(row["ID"]),
                    Name = row["Nombre"].ToString(),
                    FatherLastName = row["Apellido Paterno"].ToString(),
                    MotherLastName = row["Apellido Materno"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["Fecha de nacimiento"]),
                    Curp = row["CURP"].ToString(),
                    Nss = row["NSS"].ToString(),
                    Rfc = row["RFC"].ToString(),
                    Street = row["Calle"].ToString(),
                    Number = row["Numero"].ToString(),
                    Suburb = row["Colonia"].ToString(),
                    City = row["Municipio"].ToString(),
                    State = row["Estado"].ToString(),
                    PostalCode = row["Codigo postal"].ToString(),
                    Bank = new PairItem(row["Banco"].ToString(), Convert.ToInt32(row["ID Banco"])),
                    AccountNumber = row["Numero de cuenta"].ToString(),
                    Email = row["Correo electronico"].ToString(),
                    Department = new PairItem(row["Departamento"].ToString(), Convert.ToInt32(row["ID Departamento"])),
                    Position = new PairItem(row["Puesto"].ToString(), Convert.ToInt32(row["ID Puesto"])),
                    HiringDate = Convert.ToDateTime(row["Fecha de contratacion"]),
                    SueldoDiario = Convert.ToDecimal(row["Sueldo diario"]),
                    BaseSalary = Convert.ToDecimal(row["Sueldo base"]),
                    WageLevel = Convert.ToDecimal(row["Nivel salarial"])
                };
            }

            return null;
        }
    }
}
