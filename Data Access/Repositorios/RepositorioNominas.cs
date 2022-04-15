using Data_Access.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioNominas
    {
        private readonly string generate;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioNominas()
        {
            generate = "sp_GenerarNominas";
        }

        public DataTable GeneratePayrolls(DateTime date, List<int> employeesId)
        {

            foreach(int employeeId in employeesId)
            {
                sqlParams.Start();
                sqlParams.Add("@numero_empleado", employeesId);
                sqlParams.Add("@fecha", date);

                mainRepository.ExecuteNonQuery(generate, sqlParams);
            }

            return null;
        }


    }
}
