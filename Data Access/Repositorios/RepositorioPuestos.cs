using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioPuestos
    {
        private readonly string create, update, delete, readAll, readLike;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioPuestos()
        {
            mainRepository = MainConnection.GetInstance();
            create = "sp_AgregarPuesto";
            update = "sp_ActualizarPuesto";
            delete = "sp_EliminarPuesto";
            readAll = "sp_LeerPuestos";
            readLike = "sp_FiltrarPuestos";
        }

        public int Create(Puestos position)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", position.Nombre);
            sqlParams.Add("@nivel_salarial", position.NivelSalarial);
            sqlParams.Add("@id_empresa", position.IdEmpresa);

            return mainRepository.ExecuteNonQuery(create, sqlParams);
        }

        public int Update(Puestos position)
        {
            sqlParams.Start();
            sqlParams.Add("@id_puesto", position.IdPuesto);
            sqlParams.Add("@nombre", position.Nombre);
            sqlParams.Add("@nivel_salarial", position.NivelSalarial);

            return mainRepository.ExecuteNonQuery(update, sqlParams);
        }

        public int Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_puesto", id);

            return mainRepository.ExecuteNonQuery(delete, sqlParams);
        }

        public List<PositionsViewModel> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);
            List<PositionsViewModel> departments = new List<PositionsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new PositionsViewModel
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = row[1].ToString(),
                    WageLevel = Convert.ToDecimal(row[2])

                });
            }

            return departments;
        }

        public List<PositionsViewModel> ReadLike(string filter)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);

            DataTable table = mainRepository.ExecuteReader(readLike, sqlParams);
            List<PositionsViewModel> departments = new List<PositionsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new PositionsViewModel
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = row[1].ToString(),
                    WageLevel = Convert.ToDecimal(row[2])

                });
            }

            return departments;
        }


    }
}
