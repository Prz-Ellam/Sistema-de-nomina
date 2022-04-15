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
        private RepositorioDomicilios addressesRepository = new RepositorioDomicilios();
        private Empleados employee = new Empleados();
        private Domicilios address = new Domicilios();
        int dtgPrevIndex = -1;
        int entityID = -1;
        private List<States> states;

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
            EmployeeState = EntityState.Add;
            ListEmployees();





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

            InitStates();


            // Esto es para evitar el molesto flickering que tienen los data grid view
            dtgEmployees.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FillEntity();
                AddEntity();
                MessageBox.Show("La operación se realizó exitosamente");
                ListEmployees();
                ClearForm();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillEntity();
            EditEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            ListEmployees();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillEntity();
            DeleteEntity();
            MessageBox.Show("La operación se realizó exitosamente");
            ListEmployees();
            ClearForm();
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


        public void AddEntity()
        {
            if (EmployeeState == EntityState.Add)
            {
                int id = addressesRepository.Create(address);

                employee.Domicilio = id;

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
            employee.Nombre = txtNames.Text;
            employee.ApellidoPaterno = txtFatherLastName.Text;
            employee.ApellidoMaterno = txtMotherLastName.Text;
            employee.FechaNacimiento = dtpDateOfBirth.Value;
            employee.Curp = txtCURP.Text;
            employee.Nss = txtNSS.Text;
            employee.Rfc = txtRFC.Text;
            //employee.Address = 1;
            employee.Banco = 1;
            employee.NumeroCuenta = Convert.ToInt32(txtAccountNumber.Text);
            employee.CorreoElectronico = txtEmail.Text;
            employee.Contrasena = txtPassword.Text;
            employee.IdDepartamento = ((ComboBoxItem)cbDepartments.SelectedItem).HiddenValue;
            employee.IdPuesto = ((ComboBoxItem)cbPositions.SelectedItem).HiddenValue;
            employee.FechaContratacion = dtpHiringDate.Value;

            address.Calle = txtStreet.Text;
            address.Numero = txtNumber.Text;
            address.Colonia = txtSuburb.Text;
            address.Ciudad = cbCity.SelectedItem.ToString();
            address.Estado = cbState.SelectedItem.ToString();
            address.CodigoPostal = txtPostalCode.Text;
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
            txtBaseSalary.Text = row.Cells[20].Value.ToString();
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
            cbCity.SelectedIndex = -1;
            cbState.SelectedIndex = 0;
            txtPostalCode.Clear();
            cbBank.SelectedIndex = -1;
            txtAccountNumber.Clear();
            cbPhones.Items.Clear();
        }



        private void InitStates()
        {
            StatesRepository repository = new StatesRepository();
            states = repository.GetAll();

            cbState.Items.Add("Seleccionar");
            foreach (var state in states)
            {
                cbState.Items.Add(state.state);
            }

            cbState.SelectedIndex = 0;
        }

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCity.DataSource = null;
            if (cbState.SelectedIndex <= 0)
            {
                return;
            }
            cbCity.DataSource = states[cbState.SelectedIndex - 1].cities;
            cbCity.SelectedIndex = 0;
        }
    }
}
