using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class MainConnection : Connection
    {
        private static MainConnection _instance;

        public static MainConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainConnection();
            }

            return _instance;
        }

        private MainConnection()
        {
        }

        public int ExecuteNonQuery(string query, RepositoryParameters parameters)
        {
            using (var connection = GetConnection())
            {
                try
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

                        return command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    return -1;
                }
                finally
                {
                    connection.Close();
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
