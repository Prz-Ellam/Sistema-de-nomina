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
        private RepositorioPercepcionesAplicadas applyPerceptionsRepository = new RepositorioPercepcionesAplicadas();
        private RepositorioDeduccionesAplicadas applyDeductionsRepository = new RepositorioDeduccionesAplicadas();
        private int departmentId = -1;
        private int employeeId = -1;
        private int perceptionId = -1;
        private int deductionId = -1;

        private int perceptionRadioId = -1;
        private int deductionRadioId = -1;

        private int dtgPerceptionPrevIndex = -1;
        private int dtgDeductionPrevIndex = -1;
        private int dtgDepartmentPrevIndex = -1;
        private int dtgEmployeePrevIndex = -1;

        public FormAplicarConceptos()
        {
            InitializeComponent();
        }

        private void Concepts_Load(object sender, EventArgs e)
        {
            rbPerceptionsFilterAll.Checked = true;
            perceptionRadioId = 1;
            rbDeductionsFilterAll.Checked = true;
            deductionRadioId = 1;

            List<DepartmentsViewModel> departamentos = new RepositorioDepartamentos().ReadAll(Session.company_id);
            dtgDepartaments.DataSource = departamentos;

            List<EmployeePayrollsViewModel> employees = new RepositorioEmpleados().ReadEmployeePayrolls(dtpDate.Value);
            dtgEmployees.DataSource = employees;

            
            /*
            List<DeductionViewModel> deductions = new RepositorioDeducciones().ReadAll();
            dtgDeductions.DataSource = deductions;
            */
            dtgEmployees.DoubleBuffered(true);
            dtgPerceptions.DoubleBuffered(true);
            dtgDeductions.DoubleBuffered(true);

        }

        private void dtgEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgEmployeePrevIndex || index == -1)
            {
                employeeId = -1;
                txtEntity.Text = string.Empty;
                dtgEmployeePrevIndex = -1;
                dtgDepartmentPrevIndex = -1;
            }
            else
            {
                var row = dtgEmployees.Rows[e.RowIndex];
                employeeId = Convert.ToInt32(row.Cells[0].Value);
                txtEntity.Text = row.Cells[1].Value.ToString();
                dtgEmployeePrevIndex = index;
                dtgDepartmentPrevIndex = -1;

                dtgPerceptions.DataSource = new RepositorioPercepcionesAplicadas().ReadApplyPerceptions(1, employeeId, dtpDate.Value);
                dtgDeductions.DataSource = new RepositorioDeduccionesAplicadas().ReadApplyDeductions(1, employeeId, dtpDate.Value);
            }
        }

        private void dtgDepartaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgDepartmentPrevIndex || index == -1)
            {
                departmentId = -1;
                txtEntity.Text = string.Empty;
                dtgEmployeePrevIndex = -1;
                dtgDepartmentPrevIndex = -1;
            }
            else
            {
                var row = dtgDepartaments.Rows[e.RowIndex];
                departmentId = Convert.ToInt32(row.Cells[0].Value);
                txtEntity.Text = row.Cells[1].Value.ToString();
                dtgEmployeePrevIndex = -1;
                dtgDepartmentPrevIndex = index;
            }
        }

        private void dtgPerceptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgPerceptionPrevIndex || index == -1)
            {
                perceptionId = -1;
                deductionId = -1;
                txtConcept.Text = string.Empty;
                dtgPerceptionPrevIndex = -1;
                dtgDeductionPrevIndex = -1;
            }
            else
            {
                var row = dtgPerceptions.Rows[e.RowIndex];
                perceptionId = Convert.ToInt32(row.Cells[1].Value);
                deductionId = -1;
                txtConcept.Text = row.Cells[2].Value.ToString();
                dtgPerceptionPrevIndex = index;
                dtgDeductionPrevIndex = -1;
            }
        }

        private void dtgDeductions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgDeductionPrevIndex || index == -1)
            {
                perceptionId = -1;
                deductionId = -1;
                txtConcept.Text = string.Empty;
                dtgPerceptionPrevIndex = -1;
                dtgDeductionPrevIndex = -1;
            }
            else
            {
                var row = dtgDeductions.Rows[e.RowIndex];
                perceptionId = -1;
                deductionId = Convert.ToInt32(row.Cells[1].Value);
                txtConcept.Text = row.Cells[2].Value.ToString();
                dtgPerceptionPrevIndex = -1;
                dtgDeductionPrevIndex = index;
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (perceptionId != -1)
            {
                bool result = applyPerceptionsRepository.ApplyEmployeePerception(employeeId, perceptionId, dtpDate.Value);
            }
            else if (deductionId != -1)
            {
                bool result = applyDeductionsRepository.ApplyEmployeeDeduction(employeeId, deductionId, dtpDate.Value);
            }
            else
            {
                // ?
                return;
            }

            List<EmployeePayrollsViewModel> employees = new RepositorioEmpleados().ReadEmployeePayrolls(dtpDate.Value);
            dtgEmployees.DataSource = employees;
            dtgPerceptions.DataSource = applyPerceptionsRepository.ReadApplyPerceptions(perceptionRadioId, employeeId, dtpDate.Value);
            dtgDeductions.DataSource = applyDeductionsRepository.ReadApplyDeductions(deductionRadioId, employeeId, dtpDate.Value);
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (perceptionId != -1)
            {
                bool result = applyPerceptionsRepository.UndoEmployeePerception(employeeId, perceptionId, dtpDate.Value);
            }
            else if (deductionId != -1)
            {
                bool result = applyDeductionsRepository.UndoEmployeeDeduction(employeeId, deductionId, dtpDate.Value);
            }
            else
            {
                // ?
                return;
            }

            List<EmployeePayrollsViewModel> employees = new RepositorioEmpleados().ReadEmployeePayrolls(dtpDate.Value);
            dtgEmployees.DataSource = employees;
            dtgPerceptions.DataSource = applyPerceptionsRepository.ReadApplyPerceptions(perceptionRadioId, employeeId, dtpDate.Value);
            dtgDeductions.DataSource = applyDeductionsRepository.ReadApplyDeductions(deductionRadioId, employeeId, dtpDate.Value);
            ClearForm();
        }

        private void ClearForm()
        {
            txtEntity.Clear();
            txtConcept.Clear();
            rbPerceptionsFilterAll.Checked = true;
            rbDeductionsFilterAll.Checked = true;
        }

        private void rbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            dtgEmployees.Enabled = rbEmployee.Checked;
            txtEntity.Text = string.Empty;
        }

        private void rbDepartment_CheckedChanged(object sender, EventArgs e)
        {
            dtgDepartaments.Enabled = rbDepartment.Checked;
            txtEntity.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void rbPerceptionsFilterAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPerceptionsFilterAll.Checked)
            {
                perceptionRadioId = 1;
                dtgPerceptions.DataSource = applyPerceptionsRepository.ReadApplyPerceptions(perceptionRadioId, employeeId, dtpDate.Value);
            }
        }

        private void rbPerceptionsFilterApply_CheckedChanged(object sender, EventArgs e)
        { 
            if (rbPerceptionsFilterApply.Checked)
            {
                perceptionRadioId = 2;
                dtgPerceptions.DataSource = applyPerceptionsRepository.ReadApplyPerceptions(perceptionRadioId, employeeId, dtpDate.Value);
            }
        }

        private void rbPerceptionsFilterNotApply_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPerceptionsFilterNotApply.Checked)
            {
                perceptionRadioId = 3;
                dtgPerceptions.DataSource = applyPerceptionsRepository.ReadApplyPerceptions(perceptionRadioId, employeeId, dtpDate.Value);
            }
        }

        private void rbDeductionsFilterAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeductionsFilterAll.Checked)
            {
                deductionRadioId = 1;
                dtgDeductions.DataSource = applyDeductionsRepository.ReadApplyDeductions(deductionRadioId, employeeId, dtpDate.Value);
            }
        }

        private void rbDeductionsFilterApply_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeductionsFilterApply.Checked)
            {
                deductionRadioId = 2;
                dtgDeductions.DataSource = applyDeductionsRepository.ReadApplyDeductions(deductionRadioId, employeeId, dtpDate.Value);
            }
        }

        private void rbDeductionsFilterNotApply_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeductionsFilterNotApply.Checked)
            {
                deductionRadioId = 3;
                dtgDeductions.DataSource = applyDeductionsRepository.ReadApplyDeductions(deductionRadioId, employeeId, dtpDate.Value);
            }
        }

        private void dtgEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dtgEmployees.Rows)
            { 
                if (Convert.ToDecimal(row.Cells[9].Value) < 0.0m) 
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 53, 69);
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}
