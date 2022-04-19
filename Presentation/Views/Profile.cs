using Data_Access.Entidades;
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
        private Empleados employee = new Empleados();
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
            txtAccountNumber.Text = employee.AccountNumber;

            txtStreet.Text = employee.Street;
            txtNumber.Text = employee.Number;
            txtSuburb.Text = employee.Suburb;
            txtPostalCode.Text = employee.PostalCode;
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

            // Estos PairItem pueden provocar excepcion si no hay registros, asi que hay que validar
            // que los haya
            /*
            if (cbBank.Items.Count > 0)
            {
                employee.Banco = ((PairItem)cbBank.SelectedItem).HiddenValue;
            }
            else
            {
                employee.Banco = -1;
            }
            */

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

            employee.FechaContratacion = dtpHiringDate.Value;
            */

            employee.Calle = txtStreet.Text;
            employee.Numero = txtNumber.Text;
            employee.Colonia = txtSuburb.Text;
            //employee.Ciudad = cbCity.Text;
            //employee.Estado = cbState.Text;
            employee.CodigoPostal = txtPostalCode.Text;
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

                bool result = employeeRepository.Update(employee);
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

    }
}
