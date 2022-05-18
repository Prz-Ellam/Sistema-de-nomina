using Data_Access.Connections;
using Data_Access.Entidades;
using Data_Access.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class BanksRepository
    {
        private readonly string readPair;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public BanksRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            readPair = "sp_LeerBancosPar";
        }

        public List<PairItem> ReadPair()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readPair, sqlParams);
            List<PairItem> banks = new List<PairItem>();
            foreach(DataRow row in table.Rows)
            {
                banks.Add(new PairItem(
                    row["Nombre"].ToString(),
                    Convert.ToInt32(row["ID Banco"])
                ));
            }

            return banks;
        }
    }
}
