using Data_Access.Connections;
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


        public int Login(char type, string email, string password)
        {
            sqlParams.Start();
            sqlParams.Add("@tipo", type);
            sqlParams.Add("@correo_electronico", email);
            sqlParams.Add("@contrasena", password);

            DataTable table = mainRepository.ExecuteReader(login, sqlParams);






            return 0;
        }


    }
}
