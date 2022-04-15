using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Repositorios;

namespace Data_Access.Connections
{
    public class MainConnection : Connection
    {
        private static MainConnection instance;

        public static MainConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new MainConnection();
            }

            return instance;
        }

        private MainConnection()
        {
        }

        public int ExecuteNonQuery(string query, RepositoryParameters parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 9000;

                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }

            }
            
        }

        public DataTable ExecuteReader(string query, RepositoryParameters parameters)
        {
            DataTable dataTable = new DataTable();
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 9000;

                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(dataTable);
                    }
                }
                catch (SqlException e)
                {
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }

            return dataTable;
        }

    }
}
