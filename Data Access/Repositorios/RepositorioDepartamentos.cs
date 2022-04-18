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
        private readonly string agregar, actualizar, eliminar, leer, filtrar;
        private MainConnection repositorio;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioDepartamentos()
        {
            repositorio = MainConnection.GetInstance();
            agregar = "sp_AgregarDepartamento";
            actualizar = "sp_ActualizarDepartamento";
            eliminar = "sp_EliminarDepartamento";
            leer = "sp_LeerDepartamentos";
            filtrar = "sp_FiltrarDepartamentos";
        }

        public bool Create(Departamentos departmento)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", departmento.Nombre);
            sqlParams.Add("@sueldo_base", departmento.SueldoBase);
            sqlParams.Add("@id_empresa", departmento.IdEmpresa);

            int rowCount = repositorio.ExecuteNonQuery(agregar, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Departamentos department)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", department.IdDepartamento);
            sqlParams.Add("@nombre", department.Nombre);
            sqlParams.Add("@sueldo_base", department.SueldoBase);

            int rowCount = repositorio.ExecuteNonQuery(actualizar, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", id);

            int rowCount = repositorio.ExecuteNonQuery(eliminar, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DepartmentsViewModel> ReadAll(int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = repositorio.ExecuteReader(leer, sqlParams);
            List<DepartmentsViewModel> departments = new List<DepartmentsViewModel>();
            foreach(DataRow row in table.Rows)
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

        public List<DepartmentsViewModel> ReadLike(string like, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", like);
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = repositorio.ExecuteReader(filtrar, sqlParams);
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
