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
using CsvHelper;
using CustomMessageBox;
using Data_Access.Interfaces;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class GeneralPayrollReports : Form
    {
        private IReportsRepository reportsRepository;

        public GeneralPayrollReports()
        {
            InitializeComponent();
            reportsRepository = new ReportsRepository();
        }

        private void GeneralPayrollReports_Load(object sender, EventArgs e)
        {
            InitDates();
            ListReport();
            dtgGeneralPayroll.DoubleBuffered(true);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            ListReport();
        }

        private void InitDates()
        {
            try
            {
                ICompaniesRepository companiesRepository = new CompaniesRepository();
                PayrollsRepository payrollsRepository = new PayrollsRepository();
                DateTime creationDate = companiesRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollsRepository.GetDate(Session.companyId, true);
                dtpDate.MinDate = creationDate;
                dtpDate.MaxDate = payrollDate;
                dtpDate.Value = creationDate;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListReport()
        {
            try
            {
                dtgGeneralPayroll.DataSource = reportsRepository.GeneralPayrollReport(dtpDate.Value);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString(), "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            ofnPayrollCSV.FileName = $"Reporte-General-Nomina-{dtpDate.Value.ToString("MMMM-yyyy")}";

            if (ofnPayrollCSV.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(ofnPayrollCSV.FileName))
                {
                    using (var csvWriter = new CsvWriter(writer, CultureInfo.GetCultureInfo("es-MX")))
                    {
                        IEnumerable<GeneralPayrollReportsViewModel> payrolls = dtgGeneralPayroll.DataSource as IEnumerable<GeneralPayrollReportsViewModel>;
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