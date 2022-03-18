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
        }

        private void FormDepartments_Load(object sender, EventArgs e)
        {
            DepartmentState = EntityState.Add;
            FillDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillEntity();
            AddEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            FillDataGridView();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillEntity();
            EditEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            FillDataGridView();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillEntity();
            DeleteEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            FillDataGridView();
            ClearForm();
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

        void AddEntity()
        {
            if (departmentState == EntityState.Add)
            {
                int rowsAffected = repository.Create(department);
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Algo fallo");
                }
            }
        }

        void EditEntity()
        {
            if (departmentState == EntityState.Modify)
            {
                repository.Update(department);
            }
        }

        void DeleteEntity()
        {
            if (departmentState == EntityState.Modify)
            {
                repository.Delete(entityID);
            }
        }





        // El Formulario llena la entidad
        void FillEntity()
        {
            department.Id = entityID;
            department.Name = txtName.Text;
            department.BaseSalary = nudBaseSalary.Value;
            department.Company_id = /* Session.company_id */1;
        }

        // El Data Grid View llena el formulario
        void FillForm(int index)
        {
            var row = dtgDepartaments.Rows[index];
            entityID = Convert.ToInt32(row.Cells[0].Value);
            txtName.Text = row.Cells[1].Value.ToString();
            nudBaseSalary.Value = Convert.ToDecimal(row.Cells[2].Value);
        }

        // La capa de persistencia llena el Data Grid View
        void FillDataGridView()
        {
            List<DepartmentsViewModel> departments = repository.ReadAll();
            dtgDepartaments.DataSource = departments;
        }

        void ClearForm()
        {
            txtName.Clear();
            nudBaseSalary.Value = 0.0m;
            DepartmentState = EntityState.Add;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
