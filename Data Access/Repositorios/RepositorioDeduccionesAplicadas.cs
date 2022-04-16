using Data_Access.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioDeduccionesAplicadas
    {
        private readonly string applyEmployee, undoEmployee;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioDeduccionesAplicadas()
        {
            mainRepository = MainConnection.GetInstance();
            applyEmployee = "sp_AplicarEmpleadoDeduccion";
            undoEmployee = "sp_UndoEmployee";

            sqlParams = new RepositoryParameters();
        }

        public int ApplyEmployeePerception()
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", 1);
            sqlParams.Add("@id_deduccion", 1);
            sqlParams.Add("@fecha", null);

            return mainRepository.ExecuteNonQuery(applyEmployee, sqlParams);
        }

    }
}
