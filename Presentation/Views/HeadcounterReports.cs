using Data_Access.Helpers;
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
        private RepositorioNominas payrollRepository;

        public HeadcounterReports()
        {
            InitializeComponent();
            payrollRepository = new RepositorioNominas();
        }

        private void HeadcounterReports_Load(object sender, EventArgs e)
        {
            ListDepartments();

            try
            {
                RepositorioEmpresas companyRepository = new RepositorioEmpresas();
                DateTime creationDate = companyRepository.GetCreationDate(Session.company_id, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.company_id);
                dtpDate.Value = creationDate;
                dtpDate.MinDate = creationDate;
                dtpDate.MaxDate = payrollDate.AddMonths(-1);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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
            PairItem item;
            try
            {
                item = (PairItem)cbDepartments.SelectedItem;
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                dtgHeadcounter1.DataSource = payrollRepository.Headcounter1(Session.company_id,
                   item.HiddenValue, dtpDate.Value);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListHeadcounter2()
        {
            PairItem item;
            try
            {
                item = (PairItem)cbDepartments.SelectedItem;
            }
            catch (Exception ex)
            {
                return;
            }

            try
            {
                dtgHeadcounter2.DataSource = payrollRepository.Headcounter2(Session.company_id,
                   item.HiddenValue, dtpDate.Value);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListDepartments()
        {
            RepositorioDepartamentos departmentsRepository = new RepositorioDepartamentos();
            List<DepartmentsViewModel> departments = departmentsRepository.ReadAll(string.Empty, Session.company_id);
            List<PairItem> departmentsName = new List<PairItem>();
            departmentsName.Add(new PairItem("Todos", -1));
            foreach (var department in departments)
            {
                departmentsName.Add(new PairItem(department.Name, department.Id));
            }
            cbDepartments.DataSource = departmentsName;
        }

    }
}
