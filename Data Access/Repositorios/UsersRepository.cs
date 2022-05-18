using Data_Access.Connections;
using Data_Access.Entities;
using Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string login;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public UsersRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            login = "sp_Login";
        }

        public Users Login(string email, string password)
        {
            sqlParams.Start();
            sqlParams.Add("@correo_electronico", email);
            sqlParams.Add("@contrasena", password);

            DataTable table = mainRepository.ExecuteReader(login, sqlParams);
            foreach (DataRow row in table.Rows)
            {
                return new Users
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Email = row["Correo electrónico"].ToString(),
                    Position = row["Posición"].ToString(),
                    CompanyId = Convert.ToInt32(row["ID Empresa"])
                };
            }

            return null;
        }
    }
}