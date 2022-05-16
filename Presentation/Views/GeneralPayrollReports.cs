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
        private RepositorioNominas payrollRepository = new RepositorioNominas();
        public GeneralPayrollReports()
        {
            InitializeComponent();
        }

        private void GeneralPayrollReports_Load(object sender, EventArgs e)
        {
            try
            {
                RepositorioEmpresas companyRepository = new RepositorioEmpresas();
                DateTime creationDate = companyRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId);
                dtpDate.Value = creationDate;
                dtpDate.MinDate = creationDate;

                if (creationDate == payrollDate)
                {
                    dtpDate.MaxDate = payrollDate;
                }
                else
                {
                    dtpDate.MaxDate = payrollDate.AddMonths(-1);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dtgGeneralPayroll.DoubleBuffered(true);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dtpDate.Value;
            try
            {
                dtgGeneralPayroll.DataSource = payrollRepository.GeneralPayrollReport(date);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
