﻿using Data_Access.Connections;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioDeduccionesAplicadas
    {
        private readonly string applyEmployee, undoEmployee, readApplyEmployee, readPayrollDeduction;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public RepositorioDeduccionesAplicadas()
        {
            mainRepository = MainConnection.GetInstance();
            applyEmployee = "sp_AplicarEmpleadoDeduccion";
            undoEmployee = "sp_EliminarEmpleadoDeduccion";
            readApplyEmployee = "sp_LeerDeduccionesAplicadas";

            readPayrollDeduction = "sp_LeerDeduccionesNomina";

            sqlParams = new RepositoryParameters();
        }

        public bool ApplyEmployeeDeduction(int employeeNumber, int deductionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_deduccion", deductionId);
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

        public bool UndoEmployeeDeduction(int employeeNumber, int deductionId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@id_deduccion", deductionId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(undoEmployee, sqlParams);
            if (rowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ApplyDeductionsViewModel> ReadApplyDeductions(int filter, int employeeNumber, DateTime date)
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
                    TipoMonto = Convert.ToChar(row[3]),
                    Fijo = Convert.ToDecimal(row[4]),
                    Porcentual = Convert.ToDecimal(row[5])
                });
            }

            return applyDeductions;
        }

        public List<PayrollDeductionViewModel> ReadPayrollDeductions(int payrollId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_nomina", payrollId);

            DataTable table = mainRepository.ExecuteReader(readPayrollDeduction, sqlParams);
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
