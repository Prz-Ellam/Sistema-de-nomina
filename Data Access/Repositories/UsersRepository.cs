using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class UsersRepository
    {
        private readonly string login;
        private MainRepository mainRepository;

        public UsersRepository()
        {
            mainRepository = MainRepository.GetInstance();
            login = "sp_Login";
        }


        public int Login(char type, string email, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@type", type));
            parameters.Add(new SqlParameter("@email", email));
            parameters.Add(new SqlParameter("@password", password));

            //DataTable table = mainRepository.ExecuteReader(login, parameters);




            return 0;
        }


    }
}
