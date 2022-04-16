using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.ViewModels;

namespace Data_Access.Repositorios
{
    public class RepositorioEmpleados
    {
        private readonly string create, update, delete, readAll, readLike, getEmployeesId;
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
        }

        public int Create(Empleados employee, Domicilios address)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", employee.Nombre);
            sqlParams.Add("@apellido_paterno", employee.ApellidoPaterno);
            sqlParams.Add("@apellido_materno", employee.ApellidoMaterno);
            sqlParams.Add("@fecha_nacimiento", employee.FechaNacimiento);
            sqlParams.Add("@curp", employee.Curp);
            sqlParams.Add("@nss", employee.Nss);
            sqlParams.Add("@rfc", employee.Rfc);

            sqlParams.Add("@calle", address.Calle);
            sqlParams.Add("@numero", address.Numero);
            sqlParams.Add("@colonia", address.Colonia);
            sqlParams.Add("@ciudad", address.Ciudad);
            sqlParams.Add("@estado", address.Estado);
            sqlParams.Add("@codigo_postal", address.CodigoPostal);

            sqlParams.Add("@banco", employee.Banco);
            sqlParams.Add("@numero_cuenta", employee.NumeroCuenta);
            sqlParams.Add("@correo_electronico", employee.CorreoElectronico);
            sqlParams.Add("@contrasena", employee.Contrasena);
            sqlParams.Add("@id_departamento", employee.IdDepartamento);
            sqlParams.Add("@id_puesto", employee.IdPuesto);
            sqlParams.Add("@fecha_contratacion", employee.FechaContratacion);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Empleados employee)
        {
            sqlParams.Start();
            sqlParams.Add("@employee_number", employee.NumeroEmpleado);
            sqlParams.Add("@name", employee.Nombre);
            sqlParams.Add("@father_last_name", employee.ApellidoPaterno);
            sqlParams.Add("@mother_last_name", employee.ApellidoMaterno);
            sqlParams.Add("@date_of_birth", employee.FechaNacimiento);
            sqlParams.Add("@curp", employee.Curp);
            sqlParams.Add("@nss", employee.Nss);
            sqlParams.Add("@rfc", employee.Rfc);
            sqlParams.Add("@address", employee.Domicilio);
            sqlParams.Add("@bank", employee.Banco);
            sqlParams.Add("@account_number", employee.NumeroCuenta);
            sqlParams.Add("@email", employee.CorreoElectronico);
            sqlParams.Add("@password", employee.Contrasena);
            sqlParams.Add("@department_id", employee.IdDepartamento);
            sqlParams.Add("@position_id", employee.IdPuesto);

            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int employeeNumber)
        {
            sqlParams.Start();
            sqlParams.Add("@employee_number", employeeNumber);

            return mainRepository.ExecuteNonQuery(delete, sqlParams);
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
                    Bank = row["Banco"].ToString(),
                    AccountNumber = Convert.ToInt32(row["Numero de cuenta"]),
                    Email = row["Correo electronico"].ToString(),
                    Department = row["Departamento"].ToString(),
                    Position = row["Puesto"].ToString(),
                    HiringDate = Convert.ToDateTime(row["Fecha de contratacion"]),
                    SueldoDiario = Convert.ToDecimal(row["Sueldo diario"])
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


    }
}
