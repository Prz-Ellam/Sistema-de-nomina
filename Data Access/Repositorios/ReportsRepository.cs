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
    public class ReportsRepository : IReportsRepository
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

        public IEnumerable<GeneralPayrollReportsViewModel> GeneralPayrollReport(DateTime date)
        {
            sqlParams.Start();
            sqlParams.Add("@fecha", date);

            DataTable table = mainRepository.ExecuteReader(generalPayrollReport, sqlParams);
            List<GeneralPayrollReportsViewModel> report = new List<GeneralPayrollReportsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                report.Add(new GeneralPayrollReportsViewModel
                {
                    Department = row["Departamento"].ToString(),
                    Position = row["Puesto"].ToString(),
                    EmployeeName = row["Nombre del empleado"].ToString(),
                    StartDate = Convert.ToDateTime(row["Fecha de ingreso"]),
                    Age = Convert.ToUInt32(row["Edad"]),
                    BaseSalary = Convert.ToDecimal(row["Sueldo diario"])
                });
            }

            return report;
        }

        public IEnumerable<Headcounter1ViewModel> Headcounter1(int companyId, int departmentId, DateTime date)
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

        public IEnumerable<Headcounter2ViewModel> Headcounter2(int companyId, int departmentId, DateTime date)
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

        public IEnumerable<PayrollReportsViewModel> PayrollReport(int year)
        {
            sqlParams.Start();
            sqlParams.Add("@anio", year);

            DataTable table = mainRepository.ExecuteReader(payrollReport, sqlParams);
            List<PayrollReportsViewModel> report = new List<PayrollReportsViewModel>();
            foreach (DataRow row in table.Rows)
            {
                report.Add(new PayrollReportsViewModel
                {
                    Department = row["Departamento"].ToString(),
                    Year = row["Año"].ToString(),
                    Month = row["Mes"].ToString(),
                    GrossSalary = Convert.ToDecimal(row["Sueldo bruto"]),
                    NetSalary = Convert.ToDecimal(row["Sueldo neto"])
                });
            }

            return report;
        }
    }
}