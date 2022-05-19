using Data_Access.Connections;
using Data_Access.Interfaces;
using Data_Access.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class PayrollsRepository : IPayrollsRepository
    {
        private readonly string startPayroll, deletePayroll, generate;
        private readonly string readByDate, getDate, getPayrollReceipt, payrollProcess;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public PayrollsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            generate = "sp_GenerarNomina";
            readByDate = "sp_ObtenerNominasPorFecha";
            getDate = "sp_ObtenerFechaActual";
            getPayrollReceipt = "sp_ObtenerReciboNomina";
            startPayroll = "sp_CrearNomina";
            deletePayroll = "sp_EliminarNomina";
            payrollProcess = "sp_NominaEnProceso";
        }

        public bool GeneratePayrolls(DateTime date, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(generate, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public IEnumerable<PayrollViewModel> ReadByDate(DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readByDate, sqlParams);
            List<PayrollViewModel> payrolls = new List<PayrollViewModel>();
            foreach(DataRow row in table.Rows)
            {
                payrolls.Add(new PayrollViewModel
                {
                    NumeroEmpleado = Convert.ToInt32(row["Numero de empleado"]),
                    NombreEmpleado = row["Nombre"].ToString(),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Cantidad = Convert.ToDecimal(row["Sueldo neto"]),
                    Banco = row["Banco"].ToString(),
                    NumeroCuenta = row["Numero de cuenta"].ToString()
                });
            }

            return payrolls;
        }

        public DateTime GetDate(int companyId, bool firstDay)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@primer_dia", firstDay);

            DataTable table = mainRepository.ExecuteReader(getDate, sqlParams);
            if (table == null)
            {
                return new DateTime(1970, 1, 1);
            }

            foreach (DataRow row in table.Rows)
            {
                return Convert.ToDateTime(row["Fecha"]);
            }

            return new DateTime(1970, 1, 1); // ? 
        }

        public bool IsPayrollProcess(int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(payrollProcess, sqlParams);
            foreach (DataRow row in table.Rows)
            {
                return Convert.ToBoolean(row[0]);
            }

            return false;
        }

        public PayrollReceiptViewModel GetPayrollReceipt(int employeeNumber, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(getPayrollReceipt, sqlParams);

            foreach (DataRow row in table.Rows)
            {
                return new PayrollReceiptViewModel
                {
                    NombreEmpresa = row["Nombre de empresa"].ToString(),
                    RfcEmpresa = row["RFC de empresa"].ToString(),
                    RegistroPatronal = row["Registro patronal"].ToString(),
                    DomicilioFiscalParte1 = row["Domicilio fiscal parte 1"].ToString(),
                    DomicilioFiscalParte2 = row["Domicilio fiscal parte 2"].ToString(),
                    NumeroEmpleado = Convert.ToInt32(row["Numero de empleado"]),
                    NombreEmpleado = row["Nombre de empleado"].ToString(),
                    NssEmpleado = row["Numero de seguro social"].ToString(),
                    CurpEmpleado = row["CURP"].ToString(),
                    RfcEmpleado = row["RFC de empleado"].ToString(),
                    FechaIngreso = Convert.ToDateTime(row["Fecha de ingreso"]),
                    Departamento = row["Departamento"].ToString(),
                    Puesto = row["Puesto"].ToString(),
                    SueldoDiario = Convert.ToDecimal(row["Sueldo diario"]),
                    SueldoBruto = Convert.ToDecimal(row["Sueldo bruto"]),
                    SueldoNeto = Convert.ToDecimal(row["Sueldo neto"]),
                    Periodo = Convert.ToDateTime(row["Periodo"]),
                    FinalPeriod = Convert.ToDateTime(row["Periodo final"]),
                    IdNomina = Convert.ToInt32(row["ID de nomina"]),
                };
            }

            return null;
        }

        public bool StartPayroll(int companyId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(startPayroll, sqlParams);
            return (rowCount > 0) ? true : false;
        }

        public bool DeletePayroll(int companyId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@fecha", date);

            int rowCount = mainRepository.ExecuteNonQuery(deletePayroll, sqlParams);
            return (rowCount > 0) ? true : false;
        }

    }
}