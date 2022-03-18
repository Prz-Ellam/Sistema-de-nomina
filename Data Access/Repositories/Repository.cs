using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Data_Access.Repositories
{
    public abstract class Repository
    {
        private readonly string connection;

        public Repository()
        {
            connection = ConfigurationManager.ConnectionStrings["PayrollSystem"].ToString();
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connection);
        }

    }
}
