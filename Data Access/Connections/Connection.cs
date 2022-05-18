using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Data_Access.Connections
{
    public abstract class Connection
    {
        private readonly string connection;

        public Connection()
        {
            connection = ConfigurationManager.ConnectionStrings["SistemaDeNomina"].ToString();
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connection);
        }
    }
}