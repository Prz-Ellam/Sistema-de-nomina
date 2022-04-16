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
        private readonly string generate, readByDate, generalPayrollReport;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams = new RepositoryParameters();

        public RepositorioNominas()
        {
            mainRepository = MainConnection.GetInstance();
            generate = "sp_GenerarNomina";
            readByDate = "sp_ObtenerNominasPorFecha";
            generalPayrollReport = "sp_ReporteGeneralNomina";
        }

        public int GeneratePayrolls(DateTime date, int employeeNumber)
        {
            sqlParams.Start();
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


    }
}
