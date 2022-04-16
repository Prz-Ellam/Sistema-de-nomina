﻿using Data_Access.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioPercepcionesAplicadas
    {
        private readonly string applyEmployee, undoEmployee;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioPercepcionesAplicadas()
        {
            mainRepository = MainConnection.GetInstance();
            applyEmployee = "sp_AplicarEmpleadoPercepcion";
            undoEmployee = "sp_UndoEmployee";

            sqlParams = new RepositoryParameters();
        }

        public int ApplyEmployeePerception(int employeeNumber, int perceptionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_percepcion", perceptionId);
            sqlParams.Add("@fecha", date);

            return mainRepository.ExecuteNonQuery(applyEmployee, sqlParams);
        }

    }
}
