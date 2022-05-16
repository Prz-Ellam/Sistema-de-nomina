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
        private RepositorioNominas payrollRepository;

        public PayrollReports()
        {
            InitializeComponent();
            payrollRepository = new RepositorioNominas();
        }

        private void PayrollReports_Load(object sender, EventArgs e)
        {
            InitDates();
            ListReport();

            dtgPayrollReport.DoubleBuffered(true);
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            ListReport();
        }

        private void ListReport()
        {
            try
            {
                dtgPayrollReport.DataSource = payrollRepository.PayrollReport(Convert.ToInt32(nudYear.Value));
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
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId);
                nudYear.Value = creationDate.Year;
                nudYear.Minimum = creationDate.Year;
                nudYear.Maximum = payrollDate.Year;
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
