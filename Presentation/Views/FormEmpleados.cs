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
using CustomMessageBox;
using Data_Access.Entidades;
using Data_Access.Entities;
using Data_Access.Helpers;
using Data_Access.Interfaces;
using Data_Access.Repositorios;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormEmpleados : Form
    {
        private IEmployeesRepository repository;
        private IPhonesRepository phonesRepository;
        private CompaniesRepository companyRepository;
        private Employees employee;
        int dtgPrevIndex = -1;
        int employeeId = -1;

        // Esto podría tronar si entra y aun no tiene ninguna empresa
        DateTime payrollDate; 

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

                        dtpHiringDate.MinDate = payrollDate;
                        dtpHiringDate.MaxDate = new DateTime(payrollDate.Year, payrollDate.Month, DateTime.DaysInMonth(payrollDate.Year, payrollDate.Month));
                        dtpHiringDate.Value = payrollDate;
                        dtpHiringDate.Enabled = true;
                        dtpDateOfBirth.MaxDate = payrollDate.AddDays(-1).AddYears(-18);
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;

                        PayrollsRepository payrollsRepository = new PayrollsRepository();
                        DateTime payrollDate = payrollsRepository.GetDate(Session.companyId, false);
                        DateTime hiringDate = repository.GetHiringDate(employeeId, false);

                        dtpHiringDate.MinDate = companyRepository.GetCreationDate(Session.companyId, false);

                        if (payrollsRepository.GetDate(Session.companyId, true) !=
                            repository.GetHiringDate(employeeId, true))
                        {
                            dtpHiringDate.Enabled = false;
                        }
                        else
                        {
                            dtpHiringDate.Enabled = true;
                        }

                        dtpDateOfBirth.MaxDate = hiringDate.AddYears(-18).AddDays(-1);
                        break;
                    }
                }
            }
        }
        
        public FormEmpleados()
        {
            InitializeComponent();
            repository = new EmployeesRepository();
            phonesRepository = new PhonesRepository();
            companyRepository = new CompaniesRepository();
            employee = new Employees();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            WinAPI.SendMessage(txtNames.Handle, WinAPI.EM_SETCUEBANNER, 0, "John");
            WinAPI.SendMessage(txtFatherLastName.Handle, WinAPI.EM_SETCUEBANNER, 0, "Doe");
            WinAPI.SendMessage(txtEmail.Handle, WinAPI.EM_SETCUEBANNER, 0, "ejemplo@correo.com");
            WinAPI.SendMessage(txtAccountNumber.Handle, WinAPI.EM_SETCUEBANNER, 0, "0000000000000000");

            try
            {
                PayrollsRepository payrollRepository = new PayrollsRepository();
                payrollDate = payrollRepository.GetDate(Session.companyId, false);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // Error
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            EmployeeState = EntityState.Add;
            ListEmployees();
            ListBanks();
            ListDepartments();
            ListPositions();
            ListStates();

            dtgEmployees.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult res = RJMessageBox.Show("Al generarse la primera nómina del empleado, la fecha de ingresó no podrá volver a ser modificada, ¿Está seguro que desea continuar?",
                "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            FillEmployee();
            ValidationResult result = AddEmployee();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListEmployees();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillEmployee();
            ValidationResult result = UpdateEmployee();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListEmployees();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?",
                "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            FillEmployee();
            ValidationResult result = DeleteEmployee();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListEmployees();
            ClearForm();
        }

        private void dtgEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index == dtgPrevIndex || index == -1)
            {
                ClearForm();
            }
            else
            {
                FillForm(index);
            }
        }

        public ValidationResult AddEmployee()
        {
            if (EmployeeState != EntityState.Add)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(employee).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Create(employee);
                if (result)
                {
                    return new ValidationResult("El empleado se agregó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // Unique Constraint
                {
                    string uniqueName = GetUniqueName(ex.Message);
                    switch (uniqueName)
                    {
                        case "unique_curp":
                        {
                            return new ValidationResult("El CURP que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_rfc_empleado":
                        {
                            return new ValidationResult("El RFC que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_nss":
                        {
                            return new ValidationResult("El NSS que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_numero_cuenta":
                        {
                            return new ValidationResult("El Número de cuenta que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_correo_electronico":
                        {
                            return new ValidationResult("El correo electrónico que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        default:
                        {
                            return new ValidationResult(ex.Message, ValidationState.Error);
                        }
                    }
                }
                else if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public ValidationResult UpdateEmployee()
        {
            if (EmployeeState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(employee).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Update(employee);
                if (result)
                {
                    return new ValidationResult("El empleado se modificó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // Unique Constraint
                {
                    string uniqueName = GetUniqueName(ex.Message);
                    switch (uniqueName)
                    {
                        case "unique_curp":
                        {
                            return new ValidationResult("El CURP que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_rfc_empleado":
                        {
                            return new ValidationResult("El RFC que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_nss":
                        {
                            return new ValidationResult("El NSS que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_numero_cuenta":
                        {
                            return new ValidationResult("El Número de cuenta que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        case "unique_correo_electronico":
                        {
                            return new ValidationResult("El correo electrónico que ingresó ya está siendo utilizado por otro usuario", ValidationState.Error);
                        }
                        default:
                        {
                            return new ValidationResult(ex.Message, ValidationState.Error);
                        }
                    }
                }
                else if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        public ValidationResult DeleteEmployee()
        {
            if (EmployeeState != EntityState.Modify)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                bool result = repository.Delete(employee.NumeroEmpleado);
                if (result)
                {
                    return new ValidationResult("El empleado se eliminó éxitosamente", ValidationState.Success);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    return new ValidationResult(ex.Message, ValidationState.Error);
                }
                else
                {
                    return new ValidationResult("No se pudo realizar la operación", ValidationState.Error);
                }
            }
        }

        private void ListEmployees()
        {
            try
            {
                dtgEmployees.DataSource = repository.Read(txtFilter.Text.Trim(), Session.companyId);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString(), "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillEmployee()
        {
            employee.NumeroEmpleado = employeeId; 
            employee.Nombre = txtNames.Text.Trim();
            employee.ApellidoPaterno = txtFatherLastName.Text.Trim();
            employee.ApellidoMaterno = txtMotherLastName.Text.Trim();

            employee.FechaNacimiento = dtpDateOfBirth.Value;
            employee.Curp = txtCURP.Text.Trim();
            employee.Nss = txtNSS.Text.Trim();
            employee.Rfc = txtRFC.Text.Trim();

            // Estos PairItem pueden provocar excepcion si no hay registros, asi que hay que validar
            // que los haya
            if (cbBank.Items.Count > 0)
            {
                employee.Banco = ((PairItem)cbBank.SelectedItem).HiddenValue;
            }
            else
            {
                employee.Banco = -1;
            }

            employee.NumeroCuenta = txtAccountNumber.Text.Trim();
            employee.CorreoElectronico = txtEmail.Text.Trim();
            employee.Contrasena = txtPassword.Text.Trim();

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

            employee.Calle = txtStreet.Text.Trim();
            employee.Numero = txtNumber.Text.Trim();
            employee.Colonia = txtSuburb.Text.Trim();
            employee.Ciudad = cbCity.Text.Trim();
            employee.Estado = cbState.Text.Trim();
            employee.CodigoPostal = txtPostalCode.Text.Trim();

            employee.Telefonos.Clear();
            for (int i = 0; i < cbPhones.Items.Count; i++)
            {
                employee.Telefonos.Add(cbPhones.Items[i].ToString().Trim());
            }

        }

        public void ClearForm()
        {
            employeeId = -1;

            txtNames.Clear();
            txtFatherLastName.Clear();
            txtMotherLastName.Clear();
            txtCURP.Clear();
            txtNSS.Clear();
            txtRFC.Clear();
            dtpDateOfBirth.MaxDate = payrollDate.AddDays(-1).AddYears(-18);
            dtpDateOfBirth.Value = payrollDate.AddDays(-1).AddYears(-18);
            txtEmail.Clear();
            txtPassword.Clear();
            txtStreet.Clear();
            txtNumber.Clear();
            txtSuburb.Clear();
            cbCity.SelectedIndex = -1;
            cbState.SelectedIndex = 0;
            txtPostalCode.Clear();
            cbBank.SelectedIndex = 0;
            txtAccountNumber.Clear();
            cbPhones.Items.Clear();

            nudBaseSalary.Value = decimal.Zero;
            nudWageLevel.Value = decimal.Zero;
            nudDailySalary.Value = decimal.Zero;
            cbDepartments.SelectedIndex = 0;
            cbPositions.SelectedIndex = 0;

            EmployeeState = EntityState.Add;
            dtgPrevIndex = -1;
        }

        public void FillForm(int index)
        {
            if (index < 0 || index > dtgEmployees.RowCount)
            {
                return;
            }

            var row = dtgEmployees.Rows[index];
            employeeId = Convert.ToInt32(row.Cells["employeeNumber"].Value);
            txtNames.Text = row.Cells["name"].Value.ToString();
            txtFatherLastName.Text = row.Cells["fatherLastName"].Value.ToString();
            txtMotherLastName.Text = row.Cells["motherLastName"].Value.ToString();
            txtCURP.Text = row.Cells["curp"].Value.ToString();
            txtNSS.Text = row.Cells["nss"].Value.ToString();
            txtRFC.Text = row.Cells["rfc"].Value.ToString();
            txtStreet.Text = row.Cells["street"].Value.ToString();
            txtNumber.Text = row.Cells["number"].Value.ToString();
            txtSuburb.Text = row.Cells["suburb"].Value.ToString();
            cbState.SelectedIndex = cbState.FindString(row.Cells["state"].Value.ToString());
            cbCity.SelectedIndex = cbCity.FindString(row.Cells["city"].Value.ToString());
            txtPostalCode.Text = row.Cells["postalCode"].Value.ToString();
            txtAccountNumber.Text = row.Cells["accountNumber"].Value.ToString();
            txtEmail.Text = row.Cells["email"].Value.ToString();
            txtPassword.Text = row.Cells["password"].Value.ToString();
            nudDailySalary.Value = Convert.ToDecimal(row.Cells["sueldoDiario"].Value);
            nudBaseSalary.Value = Convert.ToDecimal( row.Cells["baseSalary"].Value);
            nudWageLevel.Value = Convert.ToDecimal(row.Cells["wageLevel"].Value);

            int bankId = ((PairItem)row.Cells["bank"].Value).HiddenValue;
            cbBank.SelectedItem = ComboBoxUtils.FindHiddenValue(bankId, ref cbBank);

            int departmentId = ((PairItem)row.Cells["department"].Value).HiddenValue;
            cbDepartments.SelectedItem = ComboBoxUtils.FindHiddenValue(departmentId, ref cbDepartments);

            int positionId = ((PairItem)row.Cells["position"].Value).HiddenValue;
            cbPositions.SelectedItem = ComboBoxUtils.FindHiddenValue(positionId, ref cbPositions);

            cbPhones.SelectedIndex = -1;
            cbPhones.Items.Clear();
            List<string> phones = phonesRepository.ReadEmployeePhones(employeeId).ToList();
            foreach(string phone in phones)
            {
                cbPhones.Items.Add(phone);
            }

            EmployeeState = EntityState.Modify;

            dtpDateOfBirth.Value = Convert.ToDateTime(row.Cells["dateOfBirth"].Value);
            dtpHiringDate.Value = Convert.ToDateTime(row.Cells["hiringDate"].Value);

            dtgPrevIndex = index;
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


        private void cbPhones_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            switch (e.KeyCode)
            {
                case Keys.Enter:
                {
                    if (comboBox.Items.Count > 9)
                    {
                        RJMessageBox.Show("Solo se aceptan máximo 10 teléfonos", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (comboBox.Text.Length != 10)
                    {
                        RJMessageBox.Show("Los teléfonos deben contener 10 dígitos", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (comboBox.FindString(comboBox.Text) == -1 && comboBox.Text != string.Empty &&
                        comboBox.SelectedItem == null)
                    {
                        comboBox.Items.Add(comboBox.Text);
                    }
                    comboBox.Text = string.Empty;
                    comboBox.SelectedIndex = -1;
                    break;
                }
                case Keys.Delete:
                {
                    if (comboBox.SelectedItem != null && comboBox.DroppedDown == false)
                    {
                        comboBox.Items.Remove(comboBox.SelectedItem);
                    }
                    comboBox.SelectedIndex = -1;
                    break;
                }
            }
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

        private void ListDepartments()
        {
            try
            {
                IDepartmentsRepository repository = new DepartmentsRepository();
                cbDepartments.DataSource = repository.ReadPair(true);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString(), "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListPositions()
        {
            try
            {
                IPositionsRepository repository = new PositionsRepository();
                cbPositions.DataSource = repository.ReadPair();
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.ToString(), "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ListEmployees();
        }

        private string GetUniqueName(string message)
        {
            int startIndex = message.IndexOf("'");
            if (startIndex <= 1)
            {
                return string.Empty;
            }
            int endIndex = message.Substring(++startIndex).IndexOf("'");
            return message.Substring(startIndex, endIndex);
        }

        private void dtpHiringDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDateOfBirth.MaxDate = dtpHiringDate.Value.AddYears(-18).AddDays(-1);
        }
    }
}