using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Helpers;
using Data_Access.Interfaces;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class PositionsRepository : IPositionsRepository
    {
        private readonly string create, update, delete, read, readPair;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public PositionsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            create = "sp_AgregarPuesto";
            update = "sp_ActualizarPuesto";
            delete = "sp_EliminarPuesto";
            read = "sp_LeerPuestos";
            readPair = "sp_LeerPuestosPar";
        }

        public bool Create(Positions position)
        {
            sqlParams.Start();
            sqlParams.Add("@nombre", position.Name);
            sqlParams.Add("@nivel_salarial", position.WageLevel);
            sqlParams.Add("@id_empresa", position.CompanyId);

            int rowCount = mainRepository.ExecuteNonQuery(create, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Update(Positions position)
        {
            sqlParams.Start();
            sqlParams.Add("@id_puesto", position.PositionId);
            sqlParams.Add("@nombre", position.Name);
            sqlParams.Add("@nivel_salarial", position.WageLevel);

            int rowCount = mainRepository.ExecuteNonQuery(update, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool Delete(int id)
        {
            sqlParams.Start();
            sqlParams.Add("@id_puesto", id);

            int rowCount = mainRepository.ExecuteNonQuery(delete, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public IEnumerable<PositionsViewModel> Read(string filter, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(read, sqlParams);
            List<PositionsViewModel> departments = new List<PositionsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                departments.Add(new PositionsViewModel
                {
                    Id = Convert.ToInt32(row["ID Puesto"]),
                    Name = row["Nombre"].ToString(),
                    WageLevel = Convert.ToDecimal(row["Nivel salarial"])

                });
            }

            return departments;
        }

        public IEnumerable<PairItem> ReadPair()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readPair, sqlParams);
            List<PairItem> positions = new List<PairItem>();
            foreach (DataRow row in table.Rows)
            {
                positions.Add(new PairItem(
                    row["Nombre"].ToString(),
                    Convert.ToInt32(row["ID Puesto"])
                ));
            }

            return positions;
        }
    }
}
