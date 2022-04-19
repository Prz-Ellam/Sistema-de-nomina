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
    public class RepositorioNominas
    {
        private readonly string generate, readByDate, generalPayrollReport, getDate, getPayrollReceipt;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioNominas()
        {
            mainRepository = MainConnection.GetInstance();
            generate = "sp_GenerarNomina";
            readByDate = "sp_ObtenerNominasPorFecha";
            generalPayrollReport = "sp_ReporteGeneralNomina";
            getDate = "sp_ObtenerFechaActual";
            getPayrollReceipt = "sp_ObtenerReciboNomina";
        }

        public int GeneratePayrolls(DateTime date, int employeeNumber, int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@numero_empleado", employeeNumber);
            sqlParams.Add("@fecha", date);

            return mainRepository.ExecuteNonQuery(generate, sqlParams);
        }

        public List<PayrollViewModel> ReadByDate(DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(readByDate, sqlParams);
            List<PayrollViewModel> payrolls = new List<PayrollViewModel>();

            foreach(DataRow row in table.Rows)
            {
                payrolls.Add(new PayrollViewModel
                {
                    NumeroEmpleado = Convert.ToInt32(row[0]),
                    NombreEmpleado = row[1].ToString(),
                    Fecha = Convert.ToDateTime(row[2]),
                    Cantidad = Convert.ToDecimal(row[3]),
                    Banco = row[4].ToString(),
                    NumeroCuenta = row[5].ToString()
                });
            }

            return payrolls;
        }

        public List<GeneralPayrollReportsViewModel> GeneralPayrollReport(DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(generalPayrollReport, sqlParams);
            List<GeneralPayrollReportsViewModel> report = new List<GeneralPayrollReportsViewModel>();

            foreach(DataRow row in table.Rows)
            {
                report.Add(new GeneralPayrollReportsViewModel
                {
                    Departamento = row[0].ToString(),
                    Puesto = row[1].ToString(),
                    NombreEmpleado = row[2].ToString(),
                    FechaIngreso = Convert.ToDateTime(row[3]),
                    Edad = Convert.ToUInt32(row[4]),
                    SalarioDiario = Convert.ToDecimal(row[5])
                });
            }

            return report;

        }

        public DateTime GetDate(int companyId)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);

            DataTable table = mainRepository.ExecuteReader(getDate, sqlParams);

            foreach (DataRow row in table.Rows)
            {
                return Convert.ToDateTime(row["Fecha"]);
            }

            return DateTime.MinValue; // ? 
        }

        public PayrollReceiptViewModel GetPayrollReceipt(DateTime date)
        {
            sqlParams.Start();
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
                    IdNomina = Convert.ToInt32(row["ID de nomina"]),
                };
            }

            return null;
        }

    }
}
