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
            filtrar = "sp_FiltrarDepartmentos";
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

        public int Update(Departamentos department)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", department.IdDepartamento);
            sqlParams.Add("@nombre", department.Nombre);
            sqlParams.Add("@sueldo_base", department.SueldoBase);

            return repositorio.ExecuteNonQuery(actualizar, sqlParams);
        }

        public int Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", id);

            return repositorio.ExecuteNonQuery(eliminar, sqlParams);
        }

        public List<DepartmentsViewModel> ReadAll()
        {
            sqlParams.Start();

            DataTable table = repositorio.ExecuteReader(leer, sqlParams);
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
            sqlParams.Start();
            sqlParams.Add("@filtro", like);

            DataTable table = repositorio.ExecuteReader(filtrar, sqlParams);
            List<DepartmentsViewModel> departments = new List<DepartmentsViewModel>();
            foreach (DataRow row in table.Rows)
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

    }
}
