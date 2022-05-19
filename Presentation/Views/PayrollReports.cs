using Data_Access.Interfaces;
using Data_Access.Repositorios;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                RepositorioNominas payrollRepository = new RepositorioNominas();
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
                    MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}