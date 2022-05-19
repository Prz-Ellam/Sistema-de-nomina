using CustomMessageBox;
using Data_Access.Entidades;
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
    public partial class FormAplicarConceptos : Form
    {
        private ApplyPerceptionsRepository applyPerceptionsRepository;
        private ApplyDeductionsRepository applyDeductionsRepository;
        private int entityId = -1;
        private int conceptId = -1;

        private int dtgPerceptionPrevIndex = -1;
        private int dtgDeductionPrevIndex = -1;
        private int dtgDepartmentPrevIndex = -1;
        private int dtgEmployeePrevIndex = -1;

        private DateTime payrollDate;
        private bool isPayrollDate;
        private EntityType entityType;
        private ConceptType conceptType;

        Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public FormAplicarConceptos()
        {
            InitializeComponent();
            applyPerceptionsRepository = new ApplyPerceptionsRepository();
            applyDeductionsRepository = new ApplyDeductionsRepository();
            rbPerceptionsFilterAll.CheckedChanged += new EventHandler(perceptionsRadioButtons_CheckedChange);
            rbPerceptionsFilterApply.CheckedChanged += new EventHandler(perceptionsRadioButtons_CheckedChange);
            rbPerceptionsFilterNotApply.CheckedChanged += new EventHandler(perceptionsRadioButtons_CheckedChange);
            rbDeductionsFilterAll.CheckedChanged += new EventHandler(deductionsRadioButtons_CheckedChange);
            rbDeductionsFilterApply.CheckedChanged += new EventHandler(deductionsRadioButtons_CheckedChange);
            rbDeductionsFilterNotApply.CheckedChanged += new EventHandler(deductionsRadioButtons_CheckedChange);
        }

        private void Concepts_Load(object sender, EventArgs e)
        {
            dictionary.Add("Todas", 1);
            dictionary.Add("Aplicadas", 2);
            dictionary.Add("No aplicadas", 3);

            rbPerceptionsFilterAll.Checked = true;
            rbDeductionsFilterAll.Checked = true;

            btnApply.Enabled = false;
            btnDelete.Enabled = false;

            try
            {
                PayrollsRepository payrollRepository = new PayrollsRepository();
                ICompaniesRepository companyRepository = new CompaniesRepository();
                DateTime creationDate = companyRepository.GetCreationDate(Session.companyId, true);
                payrollDate = payrollRepository.GetDate(Session.companyId, true);
                dtpDate.MinDate = creationDate;
                dtpDate.MaxDate = payrollDate;
                dtpDate.Value = payrollDate;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dtgEmployees.DoubleBuffered(true);
            dtgDepartaments.DoubleBuffered(true);
            dtgPerceptions.DoubleBuffered(true);
            dtgDeductions.DoubleBuffered(true);
        }

        private void dtgEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgEmployeePrevIndex || index == -1)
            {
                ClearForm();
            }
            else
            {
                var row = dtgEmployees.Rows[e.RowIndex];

                entityType = EntityType.Employee;
                entityId = Convert.ToInt32(row.Cells[0].Value);
                txtEntity.Text = row.Cells[1].Value.ToString();

                dtgEmployeePrevIndex = index;
                dtgDepartmentPrevIndex = -1;

                ClearConcept();

                rbPerceptionsFilterAll.Checked = true;
                rbDeductionsFilterAll.Checked = true;

                dtgPerceptions.DataSource = applyPerceptionsRepository.ReadEmployeePerceptions(1, entityId, dtpDate.Value);
                dtgDeductions.DataSource = applyDeductionsRepository.ReadEmployeeDeductions(1, entityId, dtpDate.Value);
            }
        }

        private void dtgDepartaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgDepartmentPrevIndex || index == -1)
            {
                ClearForm();
            }
            else
            {
                var row = dtgDepartaments.Rows[e.RowIndex];

                entityType = EntityType.Department;
                entityId = Convert.ToInt32(row.Cells[0].Value);
                txtEntity.Text = row.Cells[1].Value.ToString();

                dtgEmployeePrevIndex = -1;
                dtgDepartmentPrevIndex = index;

                ClearConcept();

                rbPerceptionsFilterAll.Checked = true;
                rbDeductionsFilterAll.Checked = true;

                dtgPerceptions.DataSource = applyPerceptionsRepository.ReadDepartmentPerceptions(1, entityId, dtpDate.Value);
                dtgDeductions.DataSource = applyDeductionsRepository.ReadDepartmentDeductions(1, entityId, dtpDate.Value);
            }
        }

        private void dtgPerceptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isPayrollDate)
            {
                RJMessageBox.Show("No se pueden cargar conceptos debido a que no es el periodo actual de nómina", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = e.RowIndex;
            if (index == dtgPerceptionPrevIndex || index == -1)
            {
                ClearConcept();
            }
            else
            {
                var row = dtgPerceptions.Rows[e.RowIndex];

                conceptType = ConceptType.Perception;
                conceptId = Convert.ToInt32(row.Cells[1].Value);
                txtConcept.Text = row.Cells[2].Value.ToString();

                dtgPerceptionPrevIndex = index;
                dtgDeductionPrevIndex = -1;

                bool apply = Convert.ToBoolean(row.Cells[0].Value);
                btnApply.Enabled = !apply;
                btnDelete.Enabled = apply;
            }
        }

        private void dtgDeductions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isPayrollDate)
            {
                RJMessageBox.Show("No se pueden cargar conceptos debido a que no es el periodo actual de nómina", "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = e.RowIndex;
            if (index == dtgDeductionPrevIndex || index == -1)
            {
                ClearConcept();
            }
            else
            {
                var row = dtgDeductions.Rows[e.RowIndex];

                conceptType = ConceptType.Deduction;
                conceptId = Convert.ToInt32(row.Cells[1].Value);

                txtConcept.Text = row.Cells[2].Value.ToString();
                dtgPerceptionPrevIndex = -1;
                dtgDeductionPrevIndex = index;

                bool apply = Convert.ToBoolean(row.Cells[0].Value);
                btnApply.Enabled = !apply;
                btnDelete.Enabled = apply;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = false;
                switch (conceptType)
                {
                    case ConceptType.Perception:
                    {
                        switch (entityType)
                        {
                            case EntityType.Employee:
                            {
                                result = applyPerceptionsRepository.ApplyEmployeePerception(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                            case EntityType.Department:
                            {
                                result = applyPerceptionsRepository.ApplyDepartmentPerception(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                        }
                        break;
                    }
                    case ConceptType.Deduction:
                    {
                        switch (entityType)
                        {
                            case EntityType.Employee:
                            {
                                result = applyDeductionsRepository.ApplyEmployeeDeduction(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                            case EntityType.Department:
                            {
                                result = applyDeductionsRepository.ApplyDepartmentDeduction(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                        }
                        break;
                    }
                }

                if (!result)
                {

                }
            }
            catch (SqlException ex)
            {

            }

            string perceptionName = gpPerceptions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            string deductionName = gpDeductions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            int perceptionType = dictionary[perceptionName];
            int deductionType = dictionary[deductionName];

            switch (entityType)
            {
                case EntityType.Employee:
                { 
                    dtgPerceptions.DataSource = applyPerceptionsRepository.ReadEmployeePerceptions(perceptionType, entityId, dtpDate.Value);
                    dtgDeductions.DataSource = applyDeductionsRepository.ReadEmployeeDeductions(deductionType, entityId, dtpDate.Value);
                    break;
                }
                case EntityType.Department:
                {
                    dtgPerceptions.DataSource = applyPerceptionsRepository.ReadDepartmentPerceptions(perceptionType, entityId, dtpDate.Value);
                    dtgDeductions.DataSource = applyDeductionsRepository.ReadDepartmentDeductions(deductionType, entityId, dtpDate.Value);
                    break;
                }
            }

            ListEmployees();
            ListDepartments();
            ClearConcept();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = false;
                switch (conceptType)
                {
                    case ConceptType.Perception:
                    {
                        switch (entityType)
                        {
                            case EntityType.Employee:
                            {
                                result = applyPerceptionsRepository.UndoEmployeePerception(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                            case EntityType.Department:
                            {
                                result = applyPerceptionsRepository.UndoDepartmentPerception(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                        }
                        break;
                    }
                    case ConceptType.Deduction:
                    {
                        switch (entityType)
                        {
                            case EntityType.Employee:
                            {
                                result = applyDeductionsRepository.UndoEmployeeDeduction(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                            case EntityType.Department:
                            {
                                result = applyDeductionsRepository.UndoDepartmentDeduction(
                                    entityId, conceptId, dtpDate.Value);
                                break;
                            }
                        }
                        break;
                    }
                }

                if (!result)
                {

                }
            }
            catch (SqlException ex)
            {

            }
            
            switch (entityType)
            {
                case EntityType.Employee:
                {
                    dtgPerceptions.DataSource = applyPerceptionsRepository.ReadEmployeePerceptions(1, entityId, dtpDate.Value);
                    dtgDeductions.DataSource = applyDeductionsRepository.ReadEmployeeDeductions(1, entityId, dtpDate.Value);
                    break;
                }
                case EntityType.Department:
                {
                    dtgPerceptions.DataSource = applyPerceptionsRepository.ReadDepartmentPerceptions(1, entityId, dtpDate.Value);
                    dtgDeductions.DataSource = applyDeductionsRepository.ReadDepartmentDeductions(1, entityId, dtpDate.Value);
                    break;
                }
            }
            
            ListEmployees();
            ListDepartments();
            ClearConcept();
        }

        private void ClearForm()
        {
            entityId = -1;
            txtEntity.Clear();

            dtgEmployeePrevIndex = -1;
            dtgDepartmentPrevIndex = -1;

            ClearConcept();

            rbPerceptionsFilterAll.Checked = true;
            rbDeductionsFilterAll.Checked = true;

            dtgPerceptions.DataSource = new List<ApplyPerceptionViewModel>();
            dtgDeductions.DataSource = new List<ApplyDeductionsViewModel>();
        }

        private void ClearConcept()
        {
            conceptId = -1;
            txtConcept.Clear();

            dtgPerceptionPrevIndex = -1;
            dtgDeductionPrevIndex = -1;

            btnApply.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void ListEmployees()
        {
            try
            {
                EmployeesRepository employeeRepository = new EmployeesRepository();
                dtgEmployees.DataSource = employeeRepository.ReadEmployeePayrolls(Session.companyId, dtpDate.Value);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    RJMessageBox.Show("No se pudo realizar la operación", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListDepartments()
        {
            try
            {
                DepartmentsRepository departmentRepository = new DepartmentsRepository();
                dtgDepartaments.DataSource = departmentRepository.ReadPayrolls(Session.companyId, dtpDate.Value);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    RJMessageBox.Show("No se pudo realizar la operación", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtgEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dtgEmployees.Rows)
            { 
                if (Convert.ToDecimal(row.Cells[9].Value) <= 0.0m) 
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            DateTime requestDate = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1);
            DateTime actualDate = new DateTime(payrollDate.Year, payrollDate.Month, 1);

            if (requestDate.CompareTo(actualDate) < 0)
            {
                RJMessageBox.Show("No se pueden cargar conceptos debido a que ya fue cerrada la nómina de este periodo",
                    "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isPayrollDate = false;
            }
            else if (requestDate.CompareTo(actualDate) > 0)
            {
                RJMessageBox.Show("No se pueden cargar conceptos antes del periodo de actual nómina",
                    "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isPayrollDate = false;
            }
            else
            {
                isPayrollDate = true;
            }

            ListEmployees();
            ListDepartments();
            ClearForm();
        }


        private void btnStartPayroll_Click(object sender, EventArgs e)
        {
            PayrollsRepository payrollRepository = new PayrollsRepository();
            DateTime requestDate = dtpDate.Value;
            try
            {
                bool result = payrollRepository.StartPayroll(Session.companyId, requestDate);
                if (!result)
                {
                    RJMessageBox.Show("No se pudo iniciar la nómina", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RJMessageBox.Show("La nómina se ha iniciado correctamente", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isPayrollDate = true;
                ListEmployees();
                ListDepartments();
                ClearForm();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void perceptionsRadioButtons_CheckedChange(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (!radioButton.Checked)
            {
                return;
            }

            string name = gpPerceptions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            int perceptionType = dictionary[name];

            dtgPerceptions.DataSource = applyPerceptionsRepository.ReadPerceptions(
                perceptionType, entityId, entityType, dtpDate.Value);
        }

        private void deductionsRadioButtons_CheckedChange(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (!radioButton.Checked)
            {
                return;
            }

            string name = gpDeductions.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            int deductionType = dictionary[name];

            dtgDeductions.DataSource = applyDeductionsRepository.ReadDeductions(
                deductionType, entityId, entityType, dtpDate.Value);
        }
    }
}