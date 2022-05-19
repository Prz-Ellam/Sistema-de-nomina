using Data_Access.Helpers;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Views
{
    public partial class HeadcounterReports : Form
    {
        private ReportsRepository reportsRepository;

        public HeadcounterReports()
        {
            InitializeComponent();
            reportsRepository = new ReportsRepository();
        }

        private void HeadcounterReports_Load(object sender, EventArgs e)
        {
            ListDepartments();
            InitDates();
            dtgHeadcounter1.DoubleBuffered(true);
            dtgHeadcounter2.DoubleBuffered(true);
        }

        private void cbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListHeadcounter1();
            ListHeadcounter2();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            ListHeadcounter1();
            ListHeadcounter2();
        }

        private void ListHeadcounter1()
        {
            int departmentId = -1;
            try
            {
                departmentId = ((PairItem)cbDepartments.SelectedItem).HiddenValue;
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                dtgHeadcounter1.DataSource = reportsRepository.Headcounter1(Session.companyId,
                   departmentId, dtpDate.Value);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListHeadcounter2()
        {
            int departmentId = -1;
            try
            {
                departmentId = ((PairItem)cbDepartments.SelectedItem).HiddenValue;
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                dtgHeadcounter2.DataSource = reportsRepository.Headcounter2(Session.companyId,
                   departmentId, dtpDate.Value);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListDepartments()
        {
            try
            {
                IDepartmentsRepository departmentsRepository = new DepartmentsRepository();
                cbDepartments.DataSource = departmentsRepository.ReadPair(false);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitDates()
        {
            try
            {
                CompaniesRepository companyRepository = new CompaniesRepository();
                PayrollsRepository payrollRepository = new PayrollsRepository();
                DateTime creationDate = companyRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId, true);
                dtpDate.MinDate = creationDate;
                dtpDate.MaxDate = payrollDate;
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