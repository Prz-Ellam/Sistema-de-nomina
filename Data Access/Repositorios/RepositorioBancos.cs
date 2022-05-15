using Data_Access.Connections;
using Data_Access.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioBancos
    {
        private readonly string readAll;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioBancos()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            readAll = "sp_LeerBancos";
        }

        public List<Banks> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);

            List<Banks> bancos = new List<Banks>();
            foreach(DataRow row in table.Rows)
            {
                bancos.Add(new Banks
                {
                    BankId = Convert.ToInt32(row["ID Banco"]),
                    Name = row["Nombre"].ToString()
                });
            }

            return bancos;
        }
    }
}
