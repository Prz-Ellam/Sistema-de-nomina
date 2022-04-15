using Data_Access.Connections;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class UsersRepository
    {
        private readonly string login;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public UsersRepository()
        {
            mainRepository = MainConnection.GetInstance();
            login = "sp_Login";
        }


        public Users Login(string email, string password)
        {
            sqlParams.Start();
            sqlParams.Add("@correo_electronico", email);
            sqlParams.Add("@contrasena", password);

            DataTable table = mainRepository.ExecuteReader(login, sqlParams);

            Users user;
            foreach (DataRow row in table.Rows)
            {
                user = new Users
                {
                    Id = Convert.ToInt32(row["ID"]),
                    Email = row["Correo"].ToString(),
                    Position = row["Posicion"].ToString()
                };

                return user;
            }

            return null;
        }

    }
}
