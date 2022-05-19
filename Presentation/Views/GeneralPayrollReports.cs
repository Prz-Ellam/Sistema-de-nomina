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
using CustomMessageBox;
using Data_Access.Interfaces;
using Data_Access.Repositorios;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class GeneralPayrollReports : Form
    {
        private ReportsRepository reportsRepository;

        public GeneralPayrollReports()
        {
            InitializeComponent();
            reportsRepository = new ReportsRepository();
        }

        private void GeneralPayrollReports_Load(object sender, EventArgs e)
        {
            InitDates();
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
    }
}