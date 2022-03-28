using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access.Entities;
using Data_Access.Repositories;
using Data_Access.ViewModels;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormEmployees : Form
    {
        private EmployeesRepository repository = new EmployeesRepository();
        private Employees employee = new Employees();
        int dtgPrevIndex = -1;
        int entityID = -1;

        private EntityState positionState;
        private EntityState PositionState
        {
            get
            {
                return positionState;
            }

            set
            {
                positionState = value;

                switch (positionState)
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

        public FormEmployees()
        {
            InitializeComponent();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            PositionState = EntityState.Add;
            FillDataGridView();

            // Esto es para evitar el molesto flickering que tienen los data grid view
            dtgEmployees.DoubleBuffered(true);
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void AddEntity()
        {
            if (positionState == EntityState.Add)
            {
                int rowsAffected = repository.Create(employee);
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Algo fallo");
                }
            }
        }

        public void EditEntity()
        {
            if (positionState == EntityState.Modify)
            {
                repository.Update(employee);
            }
        }

        public void DeleteEntity()
        {
            if (positionState == EntityState.Modify)
            {
                repository.Delete(entityID);
            }
        }

        public void FillEntity()
        {
            employee.Name = txtNames.Text;
            employee.FatherLastName = txtFatherLastName.Text;
            employee.MotherLastName = txtMotherLastName.Text;
            employee.DateOfBirth = dtpDateOfBirth.Value;
            employee.Curp = txtCURP.Text;
            employee.Nss = txtNSS.Text;
            employee.Rfc = txtRFC.Text;
            employee.Address = 1;
            employee.Bank = 1;
            employee.AccountNumber = Convert.ToInt32(txtAccountNumber.Text);
            employee.Email = txtEmail.Text;
            employee.Password = txtPassword.Text;
            employee.DepartmentId = 0;
            employee.PositionId = 0;
        }

        public void FillForm(int index)
        {
            var row = dtgEmployees.Rows[index];
            entityID = Convert.ToInt32(row.Cells[0].Value);
            txtNames.Text = row.Cells[1].Value.ToString();
            txtFatherLastName.Text = row.Cells[2].Value.ToString();
            txtMotherLastName.Text = row.Cells[3].Value.ToString();
            dtpDateOfBirth.Value = Convert.ToDateTime(row.Cells[4].Value);
            txtCURP.Text = row.Cells[5].Value.ToString();
            txtNSS.Text = row.Cells[6].Value.ToString();
            txtRFC.Text = row.Cells[7].Value.ToString();
            txtStreet.Text = row.Cells[8].Value.ToString();
            txtNumber.Text = row.Cells[9].Value.ToString();
            txtSuburb.Text = row.Cells[10].Value.ToString();
            //txtCity.Text = row.Cells[11].Value.ToString();
            //txtState.Text = row.Cells[12].Value.ToString();
            txtPostalCode.Text = row.Cells[13].Value.ToString();
            //txtBank.Text = row.Cells[14].Value.ToString();
            txtAccountNumber.Text = row.Cells[15].Value.ToString();
            txtEmail.Text = row.Cells[16].Value.ToString();
        }

        public void FillDataGridView()
        {
            List<EmployeesViewModel> employees = repository.ReadAll();
            dtgEmployees.DataSource = employees;
        }

        public void ClearForm()
        {
            txtNames.Clear();
            txtFatherLastName.Clear();
            txtMotherLastName.Clear();
            txtCURP.Clear();
            txtNSS.Clear();
            txtRFC.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            txtEmail.Clear();
            txtPassword.Clear();
            txtStreet.Clear();
            txtNumber.Clear();
            txtSuburb.Clear();
            //txtCity.Clear();
            //txtState.Clear();
            txtPostalCode.Clear();
            //txtBank.Clear();
            txtAccountNumber.Clear();
            cbPhones.Items.Clear();
        }

        private void dtgEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index == dtgPrevIndex || index == -1)
            {
                ClearForm();
                PositionState = EntityState.Add;
                dtgPrevIndex = -1;
            }
            else
            {
                FillForm(index);
                PositionState = EntityState.Modify;
                dtgPrevIndex = index;
            }
        }
    }
}
