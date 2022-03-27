using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access.Entities;
using Data_Access.ViewModels;

namespace Data_Access.Repositories
{
    public class EmployeesRepository
    {
        private readonly string create, update, delete, readAll, readLike;
        private MainRepository mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public EmployeesRepository()
        {
            mainRepository = MainRepository.GetInstance();
            create = "sp_AddEmployee";
            update = "sp_UpdateEmployee";
            delete = "sp_DeleteEmployee";
            readAll = "sp_ReadEmployees";
        }

        public int Create(Employees employee)
        {
            sqlParams.Start();
            sqlParams.Add("@name", employee.Name);
            sqlParams.Add("@father_last_name", employee.FatherLastName);
            sqlParams.Add("@mother_last_name", employee.MotherLastName);
            sqlParams.Add("@date_of_birth", employee.DateOfBirth);
            sqlParams.Add("@curp", employee.Curp);
            sqlParams.Add("@nss", employee.Nss);
            sqlParams.Add("@rfc", employee.Rfc);
            sqlParams.Add("@address", employee.Address);
            sqlParams.Add("@bank", employee.Bank);
            sqlParams.Add("@account_number", employee.AccountNumber);
            sqlParams.Add("@email", employee.Email);
            sqlParams.Add("@password", employee.Password);
            sqlParams.Add("@department_id", employee.DepartmentId);
            sqlParams.Add("@position_id", employee.PositionId);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Employees employee)
        {
            sqlParams.Start();
            sqlParams.Add("@employee_number", employee.EmployeeNumber);
            sqlParams.Add("@name", employee.Name);
            sqlParams.Add("@father_last_name", employee.FatherLastName);
            sqlParams.Add("@mother_last_name", employee.MotherLastName);
            sqlParams.Add("@date_of_birth", employee.DateOfBirth);
            sqlParams.Add("@curp", employee.Curp);
            sqlParams.Add("@nss", employee.Nss);
            sqlParams.Add("@rfc", employee.Rfc);
            sqlParams.Add("@address", employee.Address);
            sqlParams.Add("@bank", employee.Bank);
            sqlParams.Add("@account_number", employee.AccountNumber);
            sqlParams.Add("@email", employee.Email);
            sqlParams.Add("@password", employee.Password);
            sqlParams.Add("@department_id", employee.DepartmentId);
            sqlParams.Add("@position_id", employee.PositionId);

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
                    HiringDate = Convert.ToDateTime(row["Fecha de contratacion"])
                });
            }

            return departments;
        }


    }
}
