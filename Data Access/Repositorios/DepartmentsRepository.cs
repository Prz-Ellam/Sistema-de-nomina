using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Interfaces;
using Data_Access.ViewModels;

namespace Data_Access.Repositorios
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly string create, update, delete, read, readPayrolls;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public DepartmentsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            create = "sp_AgregarDepartamento";
            update = "sp_ActualizarDepartamento";
            delete = "sp_EliminarDepartamento";
            read = "sp_LeerDepartamentos";
            readPayrolls = "sp_LeerDepartamentosNominas";
        }

        public bool Create(Departments department)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", department.Name);
            sqlParams.Add("@sueldo_base", department.BaseSalary);
            sqlParams.Add("@id_empresa", department.CompanyId);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Departments department)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", department.DepartmentId);
            sqlParams.Add("@nombre", department.Name);
            sqlParams.Add("@sueldo_base", department.BaseSalary);
            sqlParams.Add("@id_empresa", department.CompanyId);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int departmentId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", departmentId);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public List<DepartmentsViewModel> Read(string like, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", like);
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);
            List<DepartmentsViewModel> departments = new List<DepartmentsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new DepartmentsViewModel
                {
                    Id = Convert.ToInt32(row["ID Departamento"]),
                    Name = row["Nombre"].ToString(),
                    BaseSalary = Convert.ToDecimal(row["Sueldo Base"])

                });
            }

            return departments;
        }

        public List<DepartmentsPayrollViewModel> ReadPayrolls(int companyId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readPayrolls, sqlParams);
            List<DepartmentsPayrollViewModel> departments = new List<DepartmentsPayrollViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new DepartmentsPayrollViewModel
                {
                    Id = Convert.ToInt32(row["ID Departamento"]),
                    Name = row["Nombre"].ToString()
                });
            }

            return departments;
        }

    }
}