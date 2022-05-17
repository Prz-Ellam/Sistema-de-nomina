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
using Data_Access.Repositorios;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class GeneralPayrollReports : Form
    {
        private RepositorioNominas payrollRepository;

        public GeneralPayrollReports()
        {
            InitializeComponent();
            payrollRepository = new RepositorioNominas();
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
                dtgGeneralPayroll.DataSource = payrollRepository.GeneralPayrollReport(dtpDate.Value);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitDates()
        {
            try
            {
                RepositorioEmpresas companyRepository = new RepositorioEmpresas();
                DateTime creationDate = companyRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId, true);
                // El Value se adapta al MinDate o MaxDate antes de ponerlo
                dtpDate.MinDate = creationDate;
                dtpDate.MaxDate = (creationDate == payrollDate) ? payrollDate : payrollDate.AddMonths(-1);
                dtpDate.Value = creationDate;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
