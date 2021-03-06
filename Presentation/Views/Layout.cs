using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Views
{
    public partial class Layout : Form
    {
        public Layout()
        {
            InitializeComponent();
        }

        private void Layout_Load(object sender, EventArgs e)
        {
            lblPositionLogged.Text = Session.position;
            lblEmailLogged.Text = Session.email;

            // Habilitar o deshabilitar opciones de acuerdo al tipo de usuario
            switch (Session.position)
            {
                case "Administrador":
                {
                    btnProfile.Visible = false;
                    btnEmployeeReceipts.Visible = false;
                    break;
                }
                case "Empleado":
                {
                    btnCompanies.Visible = false;
                    btnDepartments.Visible = false;
                    btnPositions.Visible = false;
                    btnEmployees.Visible = false;
                    btnConcepts.Visible = false;
                    btnConceptsApply.Visible = false;
                    btnPayroll.Visible = false;
                    btnReports.Visible = false;

                    panelPositions.Visible = false;
                    panelEmployees.Visible = false;
                    panelConcepts.Visible = false;
                    panelApplyConcepts.Visible = false;
                    panelPayroll.Visible = false;
                    panelReports.Visible = false;
                    panelSubmenuReports.Visible = false;
                    break;
                }
                default:
                    break;
            }
        }

        private void OpenFormChild(object son)
        {
            if (panelStorage.Controls.Count != 0)
            {
                panelStorage.Controls.RemoveAt(0);
            }
            Form form = son as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            panelStorage.Controls.Add(form);
            panelStorage.Tag = form;
            form.Show();
        }

        private void btnCompanies_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormEmpresas());
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormDepartamentos());
        }

        private void btnPositions_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormPuestos());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormEmpleados());
        }

        private void btnConcepts_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormCatalogoConceptos());
        }

        private void btnConceptsApply_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormAplicarConceptos());
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormNominas());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            panelSubmenuReports.Visible = !panelSubmenuReports.Visible;
        }

        private void btnGeneralPayrollReport_Click(object sender, EventArgs e)
        {
            OpenFormChild(new GeneralPayrollReports());
        }

        private void btnHeadcounterReport_Click(object sender, EventArgs e)
        {
            OpenFormChild(new HeadcounterReports());
        }

        private void btnPayrollReports_Click(object sender, EventArgs e)
        {
            OpenFormChild(new PayrollReports());
        }

        private void btnEmployeeReceipts_Click(object sender, EventArgs e)
        {
            OpenFormChild(new PayrollReceipts());
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            OpenFormChild(new Profile());
        }

        private void fadeIn_Tick(object sender, EventArgs e)
        {
            Opacity = Opacity + 0.2f % 1;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.LogOut();
            Form form = new Login();
            form.Show();
            this.Close();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
