using Data_Access.Connections;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioPercepcionesAplicadas
    {
        private readonly string applyEmployee, undoEmployee, readApplyEmployee;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioPercepcionesAplicadas()
        {
            mainRepository = MainConnection.GetInstance();
            applyEmployee = "sp_AplicarEmpleadoPercepcion";
            undoEmployee = "sp_UndoEmployee";
            readApplyEmployee = "sp_LeerPercepcionesAplicadas";

            sqlParams = new RepositoryParameters();
        }

        public bool ApplyEmployeePerception(int employeeNumber, int perceptionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_percepcion", perceptionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(applyEmployee, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ApplyPerceptionViewModel> ReadApplyPerceptions(int filter, int employeeNumber, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readApplyEmployee, sqlParams);
            List<ApplyPerceptionViewModel> applyPerceptions = new List<ApplyPerceptionViewModel>();

            foreach(DataRow row in table.Rows)
            {
                applyPerceptions.Add(new ApplyPerceptionViewModel
                {
                    Aplicada = Convert.ToBoolean(row[0]),
                    IdPercepcion = Convert.ToInt32(row[1]),
                    Nombre = row[2].ToString(),
                    TipoMonto = Convert.ToChar(row[3]),
                    Fijo = Convert.ToDecimal(row[4]),
                    Porcentual = Convert.ToDecimal(row[5])
                });
            }

            return applyPerceptions;
        }

    }
}
