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
using Data_Access.Helpers;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormEmpleados : Form
    {
        private RepositorioEmpleados repository = new RepositorioEmpleados();
        private Empleados employee = new Empleados();
        private Domicilios address = new Domicilios();

        int dtgPrevIndex = -1;
        int cbPhonesPrevIndex = -1;
        int employeeId = -1;
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

            List<Bancos> bancos = new RepositorioBancos().ReadAll();
            List<PairItem> nombres = new List<PairItem>();
            foreach(var banco in bancos)
            {
                nombres.Add(new PairItem(banco.Nombre, banco.IdBanco));
            }
            cbBank.DataSource = nombres;


            List<DepartmentsViewModel> departamentos = new RepositorioDepartamentos().ReadAll();
            nombres = new List<PairItem>();
            foreach(var departamento in departamentos)
            {
                nombres.Add(new PairItem(departamento.Name, departamento.Id));
            }
            cbDepartments.DataSource = nombres;


            List<PositionsViewModel> puestos = new RepositorioPuestos().ReadAll();
            nombres = new List<PairItem>();
            foreach (var puesto in puestos)
            {
                nombres.Add(new PairItem(puesto.Name, puesto.Id));
            }
            cbPositions.DataSource = nombres;

            InitStates();


            // Esto es para evitar el molesto flickering que tienen los data grid view
            dtgEmployees.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillEmployee();
            string message = AddEmployee();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListEmployees();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillEmployee();
            string message = UpdateEmployee();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
            ListEmployees();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FillEmployee();
            string message = DeleteEmployee();
            MessageBox.Show(message, "Sistema de nómina dice: ", MessageBoxButtons.OK);
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


        public string AddEmployee()
        {
            if (EmployeeState != EntityState.Add)
            {
                return "Operación incorrecta";
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(employee).Validate();
                if (!feedback.Item1)
                {
                    return feedback.Item2;
                }

                int result = repository.Create(employee, address);
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

        public string UpdateEmployee()
        {
            if (EmployeeState != EntityState.Modify)
            {
                return "Operación incorrecta";
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(employee).Validate();
                if (!feedback.Item1)
                {
                    return feedback.Item2;
                }

                int result =repository.Update(employee, address);
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

        public string DeleteEmployee()
        {
            if (EmployeeState != EntityState.Modify)
            {
                return "Operación incorrecta";
            }

            try
            {
                int result = repository.Delete(employeeId);
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

        public void FillEmployee()
        {
            employee.NumeroEmpleado = employeeId; 
            employee.Nombre = txtNames.Text;
            employee.ApellidoPaterno = txtFatherLastName.Text;
            employee.ApellidoMaterno = txtMotherLastName.Text;
            employee.FechaNacimiento = dtpDateOfBirth.Value;
            employee.Curp = txtCURP.Text;
            employee.Nss = txtNSS.Text;
            employee.Rfc = txtRFC.Text;
            //employee.Address = 1;
            employee.Banco = ((KeyValueItem)cbBank.SelectedItem).HiddenValue;
            employee.NumeroCuenta = Convert.ToInt32(txtAccountNumber.Text);
            employee.CorreoElectronico = txtEmail.Text;
            employee.Contrasena = txtPassword.Text;
            employee.IdDepartamento = ((KeyValueItem)cbDepartments.SelectedItem).HiddenValue;
            employee.IdPuesto = ((KeyValueItem)cbPositions.SelectedItem).HiddenValue;
            employee.FechaContratacion = dtpHiringDate.Value;

            address.Calle = txtStreet.Text;
            address.Numero = txtNumber.Text;
            address.Colonia = txtSuburb.Text;
            address.Ciudad = cbCity.SelectedItem.ToString();
            address.Estado = cbState.SelectedItem.ToString();
            address.CodigoPostal = txtPostalCode.Text;
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

            EmployeeState = EntityState.Add;
            dtgPrevIndex = -1;
        }

        public void FillForm(int index)
        {
            if (index == -1)
            {
                return;
            }

            var row = dtgEmployees.Rows[index];
            employeeId = Convert.ToInt32(row.Cells[0].Value);
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
            cbState.SelectedIndex = cbState.FindString(row.Cells[12].Value.ToString());
            cbCity.SelectedIndex = cbCity.FindString(row.Cells[11].Value.ToString());
            //txtCity.Text = row.Cells[11].Value.ToString();
            //txtState.Text = row.Cells[12].Value.ToString();
            txtPostalCode.Text = row.Cells[13].Value.ToString();
            //txtBank.Text = row.Cells[14].Value.ToString();
            txtAccountNumber.Text = row.Cells[15].Value.ToString();
            txtEmail.Text = row.Cells[16].Value.ToString();
            nudDailySalary.Value = Convert.ToDecimal(row.Cells[20].Value);
            nudBaseSalary.Value = Convert.ToDecimal( row.Cells[21].Value);
            nudWageLevel.Value = Convert.ToDecimal(row.Cells[22].Value);

            int bankId = ((PairItem)row.Cells[14].Value).HiddenValue;
            foreach (var item in cbBank.Items)
            {
                if (((PairItem)item).HiddenValue == bankId)
                {
                    cbBank.SelectedItem = item;
                    break;
                }
            }

            int departmentId = ((PairItem)row.Cells[17].Value).HiddenValue;
            foreach (var item in cbDepartments.Items)
            {
                if( ((PairItem)item).HiddenValue == departmentId)
                {
                    cbDepartments.SelectedItem = item;
                    break;
                }
            }

            int positionId = ((PairItem)row.Cells[18].Value).HiddenValue;
            foreach (var item in cbPositions.Items)
            {
                if (((PairItem)item).HiddenValue == positionId)
                {
                    cbPositions.SelectedItem = item;
                    break;
                }
            }


            EmployeeState = EntityState.Modify;
            dtgPrevIndex = index;
        }

      



        private void InitStates()
        {
            StatesRepository repository = new StatesRepository();
            states = repository.GetAll();

            //cbState.Items.Add("Seleccionar");
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
            cbCity.DataSource = states[cbState.SelectedIndex].cities;
            cbCity.SelectedIndex = 0;
        }


        private void cbPhones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbPhonesPrevIndex != -1)
                {
                    // Si dejo vacio se borra, si escribio un telefono que ya existe, igual se borra para
                    // que quede el que ya estaba
                    if (cbPhones.Text == string.Empty || cbPhones.FindString(cbPhones.Text) != -1)
                    {
                        cbPhones.Items.RemoveAt(cbPhonesPrevIndex);
                    }
                    else
                    {
                        cbPhones.Items[cbPhonesPrevIndex] = cbPhones.Text;
                    }
                    cbPhonesPrevIndex = -1;
                }
                else
                {
                    if (cbPhones.FindString(cbPhones.Text) == -1 && cbPhones.Text != string.Empty)
                    {
                        cbPhones.Items.Add(cbPhones.Text);
                    }
                }
                cbPhones.Text = "";
            }
        }

        private void cbPhones_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPhonesPrevIndex = cbPhones.SelectedIndex;
        }
    }
}
