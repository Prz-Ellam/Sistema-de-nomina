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
using Data_Access.Entities;
using Data_Access.Interfaces;
using Data_Access.Repositories;
using Data_Access.ViewModels;

namespace Presentation.Views
{
    public partial class FormDepartments : Form
    {
        private DepartmentsRepository repository = new DepartmentsRepository();
        private Departments department = new Departments();
        int dtgPrevIndex = -1;
        int entityID = -1;

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
                        entityID = -1;
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

        public FormDepartments()
        {
            InitializeComponent();
            Session.company_id = 1; // <- Temporal
        }

        private void FormDepartments_Load(object sender, EventArgs e)
        {
            DepartmentState = EntityState.Add;
            //ListDepartments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillDepartment();
            AddDepartment();
            ListDepartments();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillDepartment();
            UpdateDepartment();
            ListDepartments();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillDepartment();
            DeleteDepartment();
            ListDepartments();
            ClearForm();
        }

        private void dtgDepartaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgPrevIndex || index == -1)
            {
                ClearForm();
                DepartmentState = EntityState.Add;
                dtgPrevIndex = -1;
            }
            else
            {
                FillForm(index);
                DepartmentState = EntityState.Modify;
                dtgPrevIndex = index;
            }

        }

        private void lblName_Click(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void lblBaseSalary_Click(object sender, EventArgs e)
        {
            nudBaseSalary.Focus();
        }

        private void lblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }






        public void AddDepartment()
        {
            if (departmentState == EntityState.Add)
            {
                Tuple<bool, string> feedback = new DataValidation(department).Validate();
                if (!feedback.Item1)
                {
                    MessageBox.Show(feedback.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                repository.Create(department);
                MessageBox.Show("La operación se realizó exitosamente");
            }
        }

        public void UpdateDepartment()
        {
            if (departmentState == EntityState.Modify)
            {
                Tuple<bool, string> feedback = new DataValidation(department).Validate();
                if (!feedback.Item1)
                {
                    MessageBox.Show(feedback.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                repository.Update(department);
                MessageBox.Show("La operación se realizó exitosamente");
            }
        }

        public void DeleteDepartment()
        {
            if (departmentState == EntityState.Modify)
            {
                repository.Delete(entityID);
                MessageBox.Show("La operación se realizó exitosamente");
            }
        }

        public void ListDepartments()
        {
            try
            {
                dtgDepartaments.DataSource = repository.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void FillDepartment()
        {
            department.Id = entityID;
            department.Name = txtName.Text;
            department.BaseSalary = nudBaseSalary.Value;
            department.Company_id = Session.company_id;
        }

        public void ClearForm()
        {
            txtName.Text = string.Empty;
            nudBaseSalary.Value = 0.0m;
            txtFilter.Text = string.Empty;
            DepartmentState = EntityState.Add;
        }

        public void FillForm(int rowIndex)
        {
            if (rowIndex == -1)
            {
                return;
            }

            var row = dtgDepartaments.Rows[rowIndex];
            entityID = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            nudBaseSalary.Value = Convert.ToDecimal(row.Cells[2].Value);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtgDepartaments.DataSource = repository.ReadLike(txtFilter.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
