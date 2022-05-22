using CsvHelper;
using CustomMessageBox;
using Data_Access.Interfaces;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Views
{
    public partial class PayrollReports : Form
    {
        private ReportsRepository reportsRepository;

        public PayrollReports()
        {
            InitializeComponent();
            reportsRepository = new ReportsRepository();
        }

        private void PayrollReports_Load(object sender, EventArgs e)
        {
            InitDates();
            ListReport();
            dtgPayrollReport.DoubleBuffered(true);
        }

        private void dtpYear_ValueChanged(object sender, EventArgs e)
        {
            ListReport();
        }

        private void InitDates()
        {
            try
            {
                ICompaniesRepository companiesRepository = new CompaniesRepository();
                PayrollsRepository payrollRepository = new PayrollsRepository();
                DateTime creationDate = companiesRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId, true);
                dtpYear.MinDate = creationDate;
                dtpYear.MaxDate = (creationDate == payrollDate) ? payrollDate : payrollDate.AddMonths(-1);
                dtpYear.Value = creationDate;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListReport()
        {
            try
            {
                dtgPayrollReport.DataSource = reportsRepository.PayrollReport(dtpYear.Value.Year);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            ofnPayrollCSV.FileName = $"Reporte-Nomina-{dtpYear.Value.ToString("yyyy")}";

            if (ofnPayrollCSV.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(ofnPayrollCSV.FileName))
                {
                    using (var csvWriter = new CsvWriter(writer, CultureInfo.GetCultureInfo("es-MX")))
                    {
                        IEnumerable<PayrollReportsViewModel> payrolls = dtgPayrollReport.DataSource as IEnumerable<PayrollReportsViewModel>;
                        if (payrolls == null)
                        {
                            RJMessageBox.Show("No se pudo generar el reporte", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        csvWriter.WriteRecords(payrolls);
                        csvWriter.Dispose();
                        writer.Close();
                    }
                }

                RJMessageBox.Show("El reporte se generó éxitosamente", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(ofnPayrollCSV.FileName);
            }
        }
    }
}