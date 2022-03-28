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

using Presentation.Helpers;

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

            switch (Session.position)
            {
                case "Administrador":
                {
                    // Habilitar o deshabilitar opciones de acuerdo al tipo de usuario
                    break;
                }
                case "Empleado":
                {
                    // Habilitar o deshabilitar opciones de acuerdo al tipo de usuario
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
            OpenFormChild(new FormCompanies());
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormDepartments());
        }

        private void btnPositions_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormPositions());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormEmployees());
        }

        private void btnConcepts_Click(object sender, EventArgs e)
        {
            OpenFormChild(new ConceptsCatalog());
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            OpenFormChild(new FormPayroll());
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

        private void fadeIn_Tick(object sender, EventArgs e)
        {
            Opacity = Opacity + 0.2f % 1;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnConceptsApply_Click(object sender, EventArgs e)
        {
            OpenFormChild(new Concepts());
        }
    }
}
