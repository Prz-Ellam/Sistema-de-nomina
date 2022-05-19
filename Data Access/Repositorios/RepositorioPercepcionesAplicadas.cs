using Data_Access.Connections;
using Data_Access.ViewModels;
using Presentation.Helpers;
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
        private readonly string applyEmployee, applyDepartment, undoEmployee, undoDepartment;
        private readonly string readApplyEmployee, readApplyDepartment, perceptionsReceipt;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioPercepcionesAplicadas()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            applyEmployee = "sp_AplicarEmpleadoPercepcion";
            undoEmployee = "sp_EliminarEmpleadoPercepcion";

            applyDepartment = "sp_AplicarDepartamentoPercepcion";
            undoDepartment = "sp_EliminarDepartamentoPercepcion";

            readApplyEmployee = "sp_LeerPercepcionesAplicadas";
            readApplyDepartment = "sp_LeerDepartamentosPercepcionesAplicadas";

            perceptionsReceipt = "sp_LeerPercepcionesRecibo";
        }

        public bool ApplyEmployeePerception(int employeeNumber, int perceptionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_percepcion", perceptionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(applyEmployee, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool ApplyDepartmentPerception(int departmentId, int perceptionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@id_percepcion", perceptionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(applyDepartment, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool UndoEmployeePerception(int employeeNumber, int perceptionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_percepcion", perceptionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(undoEmployee, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool UndoDepartmentPerception(int departmentId, int perceptionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@id_percepcion", perceptionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(undoDepartment, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public List<ApplyPerceptionViewModel> ReadPerceptions(int filter, int entityId, EntityType entityType, DateTime date)
        {
            switch (entityType)
            {
                case EntityType.Employee:
                {
                    return ReadEmployeePerceptions(filter, entityId, date);
                }
                case EntityType.Department:
                {
                    return ReadDepartmentPerceptions(filter, entityId, date);
                }
                default:
                {
                    return null;
                }
            }
        }

        public List<ApplyPerceptionViewModel> ReadEmployeePerceptions(int filter, int employeeNumber, DateTime date)
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
                });
            }

            return applyPerceptions;
        }

        public List<ApplyPerceptionViewModel> ReadDepartmentPerceptions(int filter, int departmentId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readApplyDepartment, sqlParams);
            List<ApplyPerceptionViewModel> applyPerceptions = new List<ApplyPerceptionViewModel>();

            foreach (DataRow row in table.Rows)
            {
                applyPerceptions.Add(new ApplyPerceptionViewModel
                {
                    Aplicada = Convert.ToBoolean(row[0]),
                    IdPercepcion = Convert.ToInt32(row[1]),
                    Nombre = row[2].ToString(),
                    //TipoMonto = Convert.ToChar(row[3]),
                    //Fijo = Convert.ToDecimal(row[4]),
                    //Porcentual = Convert.ToDecimal(row[5])
                });
            }

            return applyPerceptions;
        }

        public List<PayrollPerceptionViewModel> ReadPayrollPerceptions(int payrollId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_nomina", payrollId);

            DataTable table = mainRepository.ExecuteReader(perceptionsReceipt, sqlParams);
            List<PayrollPerceptionViewModel> perceptions = new List<PayrollPerceptionViewModel>();
            foreach (DataRow row in table.Rows)
            {
                perceptions.Add(new PayrollPerceptionViewModel
                {
                    IdPercepcion = Convert.ToInt32(row["Clave"]),
                    Concepto = row["Concepto"].ToString(),
                    Importe = Convert.ToDecimal(row["Importe"]),
                });
            }

            return perceptions;
        }

    }
}
