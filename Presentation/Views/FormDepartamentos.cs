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
using Data_Access.Repositorios;
using Data_Access.Entidades;
using System.Data.SqlClient;
using Data_Access.Interfaces;
using CustomMessageBox;

namespace Presentation.Views
{
    public partial class FormDepartamentos : Form
    {
        private IDepartmentsRepository repository;
        private Departments department;
        private int dtgPrevIndex = -1;
        private int departmentId = -1;

        private EntityState departmentState;
        private EntityState DepartmentState
        {
            get
            {
                return departmentState;
            }

            set
            {
                departmentState = value;

                switch (departmentState)
                {
                    case EntityState.Add:
                    {
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                        break;
                    }
                }
            }
        }

        public FormDepartamentos()
        {
            InitializeComponent();
            repository = new DepartmentsRepository();
            department = new Departments();
        }

        private void FormDepartments_Load(object sender, EventArgs e)
        {
            DepartmentState = EntityState.Add;
            ListDepartments();
            dtgDepartaments.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillDepartment();
            ValidationResult result = AddDepartment();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListDepartments();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillDepartment();
            ValidationResult result = UpdateDepartment();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListDepartments();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?",
                "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            FillDepartment();
            ValidationResult result = DeleteDepartment();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListDepartments();
            ClearForm();
        }

        private void dtgDepartaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgPrevIndex || index < 0 || index > dtgDepartaments.RowCount)
            {
                ClearForm();
            }
            else
            {
                FillForm(index);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ListDepartments();
        }

        public ValidationResult AddDepartment()
        {
            if (DepartmentState != EntityState.Add)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(department).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Create(department);
                if (result)
                {
                    return new ValidationResult("El departamento se agregó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public ValidationResult UpdateDepartment()
        {
            if (DepartmentState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(department).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Update(department);
                if (result)
                {
                    return new ValidationResult("El departamento se modificó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public ValidationResult DeleteDepartment()
        {
            if (DepartmentState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                bool result = repository.Delete(department.DepartmentId);
                if (result)
                {
                    return new ValidationResult("El departamento se eliminó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public void ListDepartments()
        {
            try
            {
                dtgDepartaments.DataSource = repository.Read(txtFilter.Text.Trim(), Session.companyId);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString(), "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillDepartment()
        {
            department.DepartmentId = departmentId;
            department.Name = txtName.Text.Trim();
            department.BaseSalary = nudBaseSalary.Value;
            department.CompanyId = Session.companyId;
        }

        public void ClearForm()
        {
            departmentId = -1;
            txtName.Clear();
            nudBaseSalary.Value = decimal.Zero;

            DepartmentState = EntityState.Add;
            dtgPrevIndex = -1;
        }

        public void FillForm(int index)
        {
            if (index < 0 || index > dtgDepartaments.RowCount)
            {
                return;
            }

            var row = dtgDepartaments.Rows[index];
            departmentId = Convert.ToInt32(row.Cells["id"].Value);
            txtName.Text = row.Cells["name"].Value.ToString();
            nudBaseSalary.Value = Convert.ToDecimal(row.Cells["baseSalary"].Value);

            DepartmentState = EntityState.Modify;
            dtgPrevIndex = index;
        }
    }
}