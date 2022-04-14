using Data_Access.Repositorios;
using Data_Access.ViewModels;
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
    public partial class FormAplicarConceptos : Form
    {
        public FormAplicarConceptos()
        {
            InitializeComponent();
        }

        private void Concepts_Load(object sender, EventArgs e)
        {
            List<DepartmentsViewModel> departamentos = new RepositorioDepartamentos().Leer();
            dtgDepartaments.DataSource = departamentos;

            List<EmployeesViewModel> employees = new RepositorioEmpleados().ReadAll();
            dtgEmployees.DataSource = employees;

            dtgEmployees.DoubleBuffered(true);

        }

        private void dtgEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dtgEmployees.Rows[e.RowIndex];
            txtEntity.Text = row.Cells[1].Value.ToString();
        }

        private void dtgDepartaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dtgDepartaments.Rows[e.RowIndex];
            txtEntity.Text = row.Cells[1].Value.ToString();
        }
    }
}
