using Data_Access.Entidades;
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
        private int departmentId = -1;
        private int employeeId = -1;
        private int perceptionId = -1;
        private int deductionId = -1;

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

            List<PerceptionViewModel> perceptions = new RepositorioPercepciones().Leer();
            dtgPerceptions.DataSource = perceptions;

            List<DeductionViewModel> deductions = new RepositorioDeducciones().ReadAll();
            dtgDeductions.DataSource = deductions;

            dtgEmployees.DoubleBuffered(true);
            dtgPerceptions.DoubleBuffered(true);
            dtgDeductions.DoubleBuffered(true);

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

        private void dtgPerceptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dtgPerceptions.Rows[e.RowIndex];
            txtConcept.Text = row.Cells[1].Value.ToString();
        }

        private void dtgDeductions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dtgDeductions.Rows[e.RowIndex];
            txtConcept.Text = row.Cells[1].Value.ToString();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

        }
    }
}
