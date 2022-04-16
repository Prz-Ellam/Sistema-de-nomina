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
        private string readAll;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioBancos()
        {
            mainRepository = MainConnection.GetInstance();
            readAll = "sp_LeerBancos";
            sqlParams = new RepositoryParameters();
        }

        public List<Bancos> ReadAll()
        {
            sqlParams.Start();

            DataTable table = mainRepository.ExecuteReader(readAll, sqlParams);

            List<Bancos> bancos = new List<Bancos>();
            foreach(DataRow row in table.Rows)
            {
                bancos.Add(new Bancos
                {
                    IdBanco = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString()
                });
            }

            return bancos;
        }
    }
}
