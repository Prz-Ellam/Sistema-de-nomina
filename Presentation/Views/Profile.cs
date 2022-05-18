using CustomMessageBox;
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
        private EmployeesRepository employeeRepository;
        private RepositorioTelefonos phonesRepository;
        private Employees employee;
        private List<States> states;

        public Profile()
        {
            InitializeComponent();
            employeeRepository = new EmployeesRepository();
            phonesRepository = new RepositorioTelefonos();
            employee = new Employees();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            RepositorioEmpresas companiesRepository = new RepositorioEmpresas();
            dtpHiringDate.MinDate = companiesRepository.GetCreationDate(Session.companyId, false);
            dtpDateOfBirth.MaxDate = employeeRepository.GetHiringDate(Session.id, false).AddYears(-18);
            
            ListBanks();
            ListStates();
            ListProfile();
        }

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

        private void ListProfile()
        {
            EmployeesViewModel employee = employeeRepository.GetEmployeeById(Session.id);
            txtNames.Text = employee.Name;
            txtFatherLastName.Text = employee.FatherLastName;
            txtMotherLastName.Text = employee.MotherLastName;
            dtpDateOfBirth.Value = employee.DateOfBirth;
            txtEmail.Text = employee.Email;
            txtPassword.Text = employee.Password;
            dtpHiringDate.Value = employee.HiringDate;
            txtCURP.Text = employee.Curp;
            txtRFC.Text = employee.Rfc;
            txtNSS.Text = employee.Nss;
            cbBank.SelectedItem = ComboBoxUtils.FindHiddenValue(employee.Bank.HiddenValue, ref cbBank);
            txtAccountNumber.Text = employee.AccountNumber;

            cbPhones.SelectedIndex = -1;
            cbPhones.Items.Clear();
            List<string> phones = phonesRepository.ReadEmployeePhones(employee.EmployeeNumber);
            foreach (string phone in phones)
            {
                cbPhones.Items.Add(phone);
            }

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
        }

        private void FillProfile()
        {
            employee.NumeroEmpleado = Session.id;
            employee.Nombre = txtNames.Text;
            employee.ApellidoPaterno = txtFatherLastName.Text;
            employee.ApellidoMaterno = txtMotherLastName.Text;
            employee.FechaNacimiento = dtpDateOfBirth.Value;
            employee.CorreoElectronico = txtEmail.Text;
            employee.Contrasena = txtPassword.Text;
            employee.Curp = txtCURP.Text;
            employee.Rfc = txtRFC.Text;
            employee.Nss = txtNSS.Text;
            employee.NumeroCuenta = txtAccountNumber.Text;
        
            if (cbBank.Items.Count > 0)
            {
                employee.Banco = ((PairItem)cbBank.SelectedItem).HiddenValue;
            }
            else
            {
                employee.Banco = -1;
            }

            employee.Telefonos.Clear();
            for (int i = 0; i < cbPhones.Items.Count; i++)
            {
                employee.Telefonos.Add(cbPhones.Items[i].ToString());
            }

            employee.Calle = txtStreet.Text;
            employee.Numero = txtNumber.Text;
            employee.Colonia = txtSuburb.Text;
            employee.Estado = cbState.Text;
            employee.Ciudad = cbCity.Text;
            employee.CodigoPostal = txtPostalCode.Text;
        }

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCity.DataSource = null;
            if (cbState.SelectedIndex <= 0)
            {
                List<string> cities = new List<string>();
                cities.Add("Seleccionar");
                cbCity.DataSource = cities;
                return;
            }
            cbCity.DataSource = states[cbState.SelectedIndex - 1].cities;
            cbCity.SelectedIndex = 0;
        }

        private void ListBanks()
        {
            try
            {
                BanksRepository repository = new BanksRepository();
                cbBank.DataSource = repository.ReadPair();
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString(), "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListStates()
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

        private void dtpHiringDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDateOfBirth.MaxDate = dtpHiringDate.Value.AddYears(-18);
        }
    }
}
