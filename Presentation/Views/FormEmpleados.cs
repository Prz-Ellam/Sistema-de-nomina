using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access.Entidades;
using Data_Access.Entities;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormEmpleados : Form
    {
        private RepositorioEmpleados repository = new RepositorioEmpleados();
        private Empleados employee = new Empleados();
        private Domicilios domicilio = new Domicilios();
        int dtgPrevIndex = -1;
        int entityID = -1;

        private EntityState employeeState;
        private EntityState EmployeeState
        {
            get
            {
                return employeeState;
            }

            set
            {
                employeeState = value;

                switch (employeeState)
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

        public FormEmpleados()
        {
            InitializeComponent();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            employeeState = EntityState.Add;
            FillDataGridView();

            List<DepartmentsViewModel> departamentos = new RepositorioDepartamentos().Leer();
            List<ComboBoxItem> nombres = new List<ComboBoxItem>();
            foreach(var departamento in departamentos)
            {
                nombres.Add(new ComboBoxItem(departamento.Name, departamento.Id));
            }
            cbDepartments.DataSource = nombres;


            List<PositionsViewModel> puestos = new RepositorioPuestos().ReadAll();
            nombres = new List<ComboBoxItem>();
            foreach (var puesto in puestos)
            {
                nombres.Add(new ComboBoxItem(puesto.Name, puesto.Id));
            }
            cbPositions.DataSource = nombres;


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
            if (EmployeeState == EntityState.Add)
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
            if (EmployeeState == EntityState.Modify)
            {
                repository.Update(employee);
            }
        }

        public void DeleteEntity()
        {
            if (EmployeeState == EntityState.Modify)
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

            domicilio.Calle = txtStreet.Text;
            domicilio.Numero = txtNumber.Text;
            domicilio.Colonia = txtSuburb.Text;
            domicilio.Ciudad = cbCity.SelectedItem.ToString();
            domicilio.Estado = cbState.SelectedItem.ToString();
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

        private void ListEmployees()
        {
            try
            {
                dtgEmployees.DataSource = repository.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                EmployeeState = EntityState.Add;
                dtgPrevIndex = -1;
            }
            else
            {
                FillForm(index);
                EmployeeState = EntityState.Modify;
                dtgPrevIndex = index;
            }
        }
    }
}
