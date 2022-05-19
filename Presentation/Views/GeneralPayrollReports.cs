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

        private void InitDates()
        {
            try
            {
                RepositorioEmpresas companyRepository = new RepositorioEmpresas();
                RepositorioNominas payrollRepository = new RepositorioNominas();
                DateTime creationDate = companyRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId, true);
                // El Value se adapta al MinDate o MaxDate antes de ponerlo
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

    }
}
