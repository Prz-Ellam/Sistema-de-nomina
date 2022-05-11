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
    public class RepositorioDepartamentos
    {
        private readonly string create, update, delete, readAll, readPayrolls;
        private MainConnection repositorio;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioDepartamentos()
        {
            repositorio = MainConnection.GetInstance();
            create = "sp_AgregarDepartamento";
            update = "sp_ActualizarDepartamento";
            delete = "sp_EliminarDepartamento";
            readAll = "sp_LeerDepartamentos";
            readPayrolls = "sp_LeerDepartamentosNominas";
        }

        public bool Create(Departamentos departmento)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", departmento.Nombre);
            sqlParams.Add("@sueldo_base", departmento.SueldoBase);
            sqlParams.Add("@id_empresa", departmento.IdEmpresa);

            int rowCount = repositorio.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Departamentos department)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", department.IdDepartamento);
            sqlParams.Add("@nombre", department.Nombre);
            sqlParams.Add("@sueldo_base", department.SueldoBase);

            int rowCount = repositorio.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", id);

            int rowCount = repositorio.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public List<DepartmentsViewModel> ReadAll(string like, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", like);
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = repositorio.ExecuteReader(readAll, sqlParams);
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

        public List<DepartmentsViewModel> ReadPayrolls(int companyId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@fecha", date);

            DataTable table = repositorio.ExecuteReader(readPayrolls, sqlParams);

            if (table == null)
            {
                return new List<DepartmentsViewModel>();
            }

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

    }
}
