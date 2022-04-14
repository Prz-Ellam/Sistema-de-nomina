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
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public DepartmentsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarDepartamento";
            update = "sp_ActualizarDepartamento";
            delete = "sp_EliminarDepartamento";
            readAll = "sp_LeerDepartamentos";
        }

        public int Create(Departments department)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", department.Name);
            sqlParams.Add("@sueldo_base", department.BaseSalary);
            sqlParams.Add("@id_empresa", department.Company_id);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Departments department)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", department.Id);
            sqlParams.Add("@nombre", department.Name);
            sqlParams.Add("@sueldo_base", department.BaseSalary);

            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", id);

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
                    Id = Convert.ToInt32(row["id_departamento"]),
                    Name = row["nombre"].ToString(),
                    BaseSalary = Convert.ToDecimal(row["sueldo_base"])

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
