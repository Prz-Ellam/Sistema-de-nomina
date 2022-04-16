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
using Data_Access.ViewModels;
using Data_Access.Entidades;
using System.Data.SqlClient;

namespace Presentation.Views
{
    public partial class FormDepartamentos : Form
    {
        private RepositorioDepartamentos repository = new RepositorioDepartamentos();
        private Departamentos department = new Departamentos();
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

        public FormDepartamentos()
        {
            InitializeComponent();
        }

        private void FormDepartments_Load(object sender, EventArgs e)
        {
            DepartmentState = EntityState.Add;
            ListDepartments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FillDepartment();
                string message = AddDepartment();
                MessageBox.Show(message);
                ListDepartments();
                ClearForm();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                FillDepartment();
                string message = UpdateDepartment();
                MessageBox.Show(message);
                ListDepartments();
                ClearForm();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                FillDepartment();
                string message = DeleteDepartment();
                MessageBox.Show(message);
                ListDepartments();
                ClearForm();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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






        public string AddDepartment()
        {
            if (departmentState == EntityState.Add)
            {
                try
                {
                    Tuple<bool, string> feedback = new DataValidation(department).Validate();
                    if (!feedback.Item1)
                    {
                        return feedback.Item2;
                    }

                    int result = repository.Agregar(department);

                    if (result > 0)
                    {
                        return "La operación se realizó éxitosamente";
                    }
                    else
                    {
                        return "No se pudo realizar la operación";
                    }
                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

            return "";
        }

        public string UpdateDepartment()
        {
            if (departmentState == EntityState.Modify)
            {
                try
                {
                    Tuple<bool, string> feedback = new DataValidation(department).Validate();
                    if (!feedback.Item1)
                    {
                        return feedback.Item2;
                    }

                    int result = repository.Actualizar(department);
                    if (result > 0)
                    {
                        return "La operación se realizó éxitosamente";
                    }
                    else
                    {
                        return "No se pudo realizar la operación";
                    }
                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

            return "";
        }

        public string DeleteDepartment()
        {
            if (departmentState == EntityState.Modify)
            {
                try
                {
                    int result = repository.Eliminar(entityID);

                    if (result > 0)
                    {
                        return "La operación se realizó éxitosamente";
                    }
                    else
                    {
                        return "No se pudo realizar la operación";
                    }
                }
                catch (SqlException ex)
                {
                    return ex.Message;
                }
            }

            return "";
        }

        public void ListDepartments()
        {
            try
            {
                dtgDepartaments.DataSource = repository.Leer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void FillDepartment()
        {
            department.IdDepartamento = entityID;
            department.Nombre = txtName.Text;
            department.SueldoBase = nudBaseSalary.Value;
            department.IdEmpresa = Session.company_id;
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
                dtgDepartaments.DataSource = repository.Filtrar(txtFilter.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
