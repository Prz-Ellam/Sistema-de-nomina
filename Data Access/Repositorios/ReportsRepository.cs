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
    public class ReportsRepository
    {
        private readonly string generalPayrollReport, payrollReport, headcounter1, headcounter2;
        private MainConnection mainRepository;
        private RepositoryParameters sqlParams;

        public ReportsRepository()
        {
            mainRepository = MainConnection.GetInstance();
            sqlParams = new RepositoryParameters();
            generalPayrollReport = "sp_ReporteGeneralNomina";
            payrollReport = "sp_ReporteNomina";
            headcounter1 = "sp_Headcounter1";
            headcounter2 = "sp_Headcounter2";
        }

        public List<GeneralPayrollReportsViewModel> GeneralPayrollReport(DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(generalPayrollReport, sqlParams);
            List<GeneralPayrollReportsViewModel> report = new List<GeneralPayrollReportsViewModel>();
            foreach (DataRow row in table.Rows)
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

        public List<Headcounter1ViewModel> Headcounter1(int companyId, int departmentId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(headcounter1, sqlParams);
            List<Headcounter1ViewModel> report = new List<Headcounter1ViewModel>();
            foreach (DataRow row in table.Rows)
            {
                report.Add(new Headcounter1ViewModel
                {
                    Departamento = row[0].ToString(),
                    Puesto = row[1].ToString(),
                    CantidadEmpleados = Convert.ToUInt32(row[2])
                });
            }

            return report;
        }

        public List<Headcounter2ViewModel> Headcounter2(int companyId, int departmentId, DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@id_empresa", companyId);
            sqlParams.Add("@id_departamento", departmentId);
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(headcounter2, sqlParams);
            List<Headcounter2ViewModel> report = new List<Headcounter2ViewModel>();
            foreach (DataRow row in table.Rows)
            {
                report.Add(new Headcounter2ViewModel
                {
                    Departamento = row[0].ToString(),
                    CantidadEmpleados = Convert.ToUInt32(row[1])
                });
            }

            return report;
        }

        public List<PayrollReportsViewModel> PayrollReport(int year)
        {
            sqlParams.Start();
            sqlParams.Add("@anio", year);

            DataTable table = mainRepository.ExecuteReader(payrollReport, sqlParams);
            List<PayrollReportsViewModel> report = new List<PayrollReportsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                report.Add(new PayrollReportsViewModel
                {
                    Departamento = row[0].ToString(),
                    Anio = row[1].ToString(),
                    Mes = row[2].ToString(),
                    SueldoBruto = Convert.ToDecimal(row[3]),
                    SueldoNeto = Convert.ToDecimal(row[4])
                });
            }

            return report;
        }
    }
}
