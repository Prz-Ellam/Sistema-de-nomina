using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Data_Access.ViewModels;

namespace Data_Access.Repositories
{
    public class DepartmentsRepository
    {
        private readonly string create, update, delete, readAll, readLike;
        private MainRepository mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public DepartmentsRepository()
        {
            mainRepository = MainRepository.GetInstance();
            create = "sp_AddDepartment";
            update = "sp_UpdateDepartment";
            delete = "sp_DeleteDepartment";
            readAll = "sp_ReadDepartments";
        }

        public int Create(Departments department)
        {
            sqlParams.Start();
            sqlParams.Add("@name", department.Name);
            sqlParams.Add("@base_salary", department.BaseSalary);
            sqlParams.Add("@company_id", department.Company_id);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Departments department)
        {
            sqlParams.Start();
            sqlParams.Add("@id", department.Id);
            sqlParams.Add("@name", department.Name);
            sqlParams.Add("@base_salary", department.BaseSalary);

            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id", id);

            return mainRepository.ExecuteNonQuery(delete, sqlParams);
        }

        public List<DepartmentsViewModel> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);
            List<DepartmentsViewModel> departments = new List<DepartmentsViewModel>();
            foreach(DataRow row in table.Rows)
            {
                departments.Add(new DepartmentsViewModel
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Name = row["Nombre"].ToString(),
                    BaseSalary = Convert.ToDecimal(row["Sueldo base"])

                });
            }

            return departments;
        }

        public List<DepartmentsViewModel> ReadLike(string like)
        {
            return ReadAll().FindAll(e => e.Name.Contains(like));
        }

    }
}
