using Data_Access.Entidades;
using Data_Access.Entities;
using Data_Access.Helpers;
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
    public partial class Profile : Form
    {
        private RepositorioEmpleados employeeRepository = new RepositorioEmpleados();
        private RepositorioTelefonos phonesRepository = new RepositorioTelefonos();
        private Empleados employee = new Empleados();
        private List<Telefonos> phones = new List<Telefonos>();
        private List<States> states;

        public Profile()
        {
            InitializeComponent();
        }

        // TODO: Aun no se hacen pruebas en esto
        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillProfile();
            ValidationResult result = UpdateProfile();

            if (result.State == ValidationState.Error)
            {
                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListProfile();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            // Inicializar bancos, departamentos y puestos
            List<Bancos> bancos = new RepositorioBancos().ReadAll();
            List<PairItem> nombres = new List<PairItem>();
            foreach (var banco in bancos)
            {
                nombres.Add(new PairItem(banco.Nombre, banco.IdBanco));
            }
            cbBank.DataSource = nombres;

            InitStates();
            ListProfile();
        }

        private void ListProfile()
        {
            EmployeesViewModel employee = employeeRepository.GetEmployeeById(Session.id);

            txtNames.Text = employee.Name;
            txtFatherLastName.Text = employee.FatherLastName;
            txtMotherLastName.Text = employee.MotherLastName;

            txtCURP.Text = employee.Curp;
            txtNSS.Text = employee.Nss;
            txtRFC.Text = employee.Rfc;
            
            txtEmail.Text = employee.Email;
            txtPassword.Text = employee.Password;
            txtAccountNumber.Text = employee.AccountNumber;

            cbBank.SelectedIndex = cbBank.FindString(employee.Bank.ToString());

            txtStreet.Text = employee.Street;
            txtNumber.Text = employee.Number;
            txtSuburb.Text = employee.Suburb;
            cbState.SelectedIndex = cbState.FindString(employee.State);
            cbCity.SelectedIndex = cbCity.FindString(employee.City);
            txtPostalCode.Text = employee.PostalCode;

            txtDepartment.Text = employee.Department.ToString();
            txtPosition.Text = employee.Position.ToString();

            nudDailySalary.Value = employee.SueldoDiario;
            nudBaseSalary.Value = employee.BaseSalary;
            nudWageLevel.Value = employee.WageLevel;

            cbPhones.SelectedIndex = -1;
            List<string> phones = phonesRepository.ReadEmployeePhones(employee.EmployeeNumber);
            foreach (string phone in phones)
            {
                cbPhones.Items.Add(phone);
            }


        }

        private void FillProfile()
        {
            employee.NumeroEmpleado = Session.id;
            employee.Nombre = txtNames.Text;
            employee.ApellidoPaterno = txtFatherLastName.Text;
            employee.ApellidoMaterno = txtMotherLastName.Text;

            employee.FechaNacimiento = dtpDateOfBirth.Value;
            employee.Curp = txtCURP.Text;
            employee.Nss = txtNSS.Text;
            employee.Rfc = txtRFC.Text;

            //employee.Address = 1;            
            if (cbBank.Items.Count > 0)
            {
                employee.Banco = ((PairItem)cbBank.SelectedItem).HiddenValue;
            }
            else
            {
                employee.Banco = -1;
            }
            
            employee.NumeroCuenta = txtAccountNumber.Text;
            employee.CorreoElectronico = txtEmail.Text;
            employee.Contrasena = txtPassword.Text;

            /*
            if (cbDepartments.Items.Count > 0)
            {
                employee.IdDepartamento = ((PairItem)cbDepartments.SelectedItem).HiddenValue;
            }
            else
            {
                employee.IdDepartamento = -1;
            }

            if (cbPositions.Items.Count > 0)
            {
                employee.IdPuesto = ((PairItem)cbPositions.SelectedItem).HiddenValue;
            }
            else
            {
                employee.IdPuesto = -1;
            }

            
            */

            employee.FechaContratacion = dtpHiringDate.Value;
            employee.Calle = txtStreet.Text;
            employee.Numero = txtNumber.Text;
            employee.Colonia = txtSuburb.Text;
            employee.Ciudad = cbCity.Text;
            employee.Estado = cbState.Text;
            employee.CodigoPostal = txtPostalCode.Text;

            for (int i = 0; i < cbPhones.Items.Count; i++)
            {
                phones.Add(new Telefonos
                {
                    IdPropietario = Session.id,
                    Nombre = cbPhones.Items[i].ToString()
                });
            }

            employee.Telefonos.Clear();
            for (int i = 0; i < cbPhones.Items.Count; i++)
            {
                employee.Telefonos.Add(cbPhones.Items[i].ToString());
            }

        }

        public ValidationResult UpdateProfile()
        {
            try
            {
                Tuple<bool, string> feedback = new DataValidation(employee).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = employeeRepository.UpdateByEmployee(employee);
                if (result)
                {
                    return new ValidationResult("La operación se realizó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                return new ValidationResult(ex.Message, ValidationState.Error);
            }
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
    }
}
