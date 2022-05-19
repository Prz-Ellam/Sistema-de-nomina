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
    public class ApplyDeductionsRepository
    {
        private readonly string applyEmployee, undoEmployee, applyDepartment, undoDepartment;
        private readonly string readApplyEmployee, readApplyDepartment, deductionsReceipt;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public ApplyDeductionsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            applyEmployee = "sp_AplicarEmpleadoDeduccion";
            undoEmployee = "sp_EliminarEmpleadoDeduccion";

            applyDepartment = "sp_AplicarDepartamentoDeduccion";
            undoDepartment = "sp_EliminarDepartamentoDeduccion";

            readApplyEmployee = "sp_LeerDeduccionesAplicadas";
            readApplyDepartment = "sp_LeerDepartamentosDeduccionesAplicadas";

            deductionsReceipt = "sp_LeerDeduccionesRecibo";

            sqlParams = new RepositoryParameters();
        }

        public bool ApplyEmployeeDeduction(int employeeNumber, int deductionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_deduccion", deductionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(applyEmployee, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool ApplyDepartmentDeduction(int departmentId, int deductionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@id_deduccion", deductionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(applyDepartment, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool UndoEmployeeDeduction(int employeeNumber, int deductionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_deduccion", deductionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(undoEmployee, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool UndoDepartmentDeduction(int departmentId, int deductionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@id_percepcion", deductionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(undoDepartment, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public List<ApplyDeductionsViewModel> ReadDeductions(int filter, int entityId, EntityType entityType, DateTime date)
        {
            switch (entityType)
            {
                case EntityType.Employee:
                {
                    return ReadEmployeeDeductions(filter, entityId, date);
                }
                case EntityType.Department:
                {
                    return ReadDepartmentDeductions(filter, entityId, date);
                }
                default:
                {
                    return null;
                }
            }
        }

        public List<ApplyDeductionsViewModel> ReadEmployeeDeductions(int filter, int employeeNumber, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readApplyEmployee, sqlParams);
            List<ApplyDeductionsViewModel> applyDeductions = new List<ApplyDeductionsViewModel>();

            foreach (DataRow row in table.Rows)
            {
                applyDeductions.Add(new ApplyDeductionsViewModel
                {
                    Aplicada = Convert.ToBoolean(row[0]),
                    IdDeduccion = Convert.ToInt32(row[1]),
                    Nombre = row[2].ToString(),
                    //TipoMonto = Convert.ToChar(row[3]),
                    //Fijo = Convert.ToDecimal(row[4]),
                    //Porcentual = Convert.ToDecimal(row[5])
                });
            }

            return applyDeductions;
        }

        public List<ApplyDeductionsViewModel> ReadDepartmentDeductions(int filter, int departmentId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@filtro", filter);
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readApplyDepartment, sqlParams);
            List<ApplyDeductionsViewModel> applyDeductions = new List<ApplyDeductionsViewModel>();

            foreach (DataRow row in table.Rows)
            {
                applyDeductions.Add(new ApplyDeductionsViewModel
                {
                    Aplicada = Convert.ToBoolean(row[0]),
                    IdDeduccion = Convert.ToInt32(row[1]),
                    Nombre = row[2].ToString(),
                    //TipoMonto = Convert.ToChar(row[3]),
                    //Fijo = Convert.ToDecimal(row[4]),
                    //Porcentual = Convert.ToDecimal(row[5])
                });
            }

            return applyDeductions;
        }

        public List<PayrollDeductionViewModel> ReadPayrollDeductions(int payrollId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_nomina", payrollId);

            DataTable table = mainRepository.ExecuteReader(deductionsReceipt, sqlParams);
            List<PayrollDeductionViewModel> deductions = new List<PayrollDeductionViewModel>();

            foreach (DataRow row in table.Rows)
            {
                deductions.Add(new PayrollDeductionViewModel
                {
                    IdDeduccion = Convert.ToInt32(row["Clave"]),
                    Concepto = row["Concepto"].ToString(),
                    Importe = Convert.ToDecimal(row["Importe"]),
                });
            }

            return deductions;
        }

    }
}
