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
        private RepositorioEmpleados repository;
        private RepositorioTelefonos phonesRepository;
        private RepositorioEmpresas companyRepository;
        private Empleados employee;
        private List<Telefonos> phones;
        int dtgPrevIndex = -1;
        int employeeId = -1;

        // Esto podría tronar si entra y aun no tiene ninguna empresa
        DateTime payrollDate; 

        int cbPhonesPrevIndex = -1;
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
                        dtpHiringDate.MinDate = companyRepository.GetCreationDate(Session.companyId, false);
                        dtpHiringDate.Enabled = false;
                        dtpDateOfBirth.MaxDate = repository.GetHiringDate(employeeId, false).AddYears(-18);
                        break;
                    }
                }
            }
        }
        
        public FormEmpleados()
        {
            InitializeComponent();
            repository = new RepositorioEmpleados();
            phonesRepository = new RepositorioTelefonos();
            companyRepository = new RepositorioEmpresas();
            employee = new Empleados();
            phones = new List<Telefonos>();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            try
            {
                RepositorioNominas payrollRepository = new RepositorioNominas();
                payrollDate = payrollRepository.GetDate(Session.companyId, false);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // Error
                {
                    MessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            EmployeeState = EntityState.Add;
            ListEmployees();
            ListBanks();
            ListDepartments();
            ListPositions();
            InitStates();

            dtgEmployees.DoubleBuffered(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillEmployee();
            ValidationResult result = AddEmployee();

            if (result.State == ValidationState.Error)
            {
                MessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListEmployees();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillEmployee();
            ValidationResult result = UpdateEmployee();

            if (result.State == ValidationState.Error)
            {
                MessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListEmployees();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Está seguro que desea realizar esta acción?",
                "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            FillEmployee();
            ValidationResult result = DeleteEmployee();

            if (result.State == ValidationState.Error)
            {
                MessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    return new ValidationResult("La operación se realizó éxitosamente", ValidationState.Success);
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
                    return new ValidationResult("La operación se realizó éxitosamente", ValidationState.Success);
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
                bool result = repository.Delete(employeeId);
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
                List<EmployeesViewModel> employees = repository.ReadAll(string.Empty);
                dtgEmployees.DataSource = employees;
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

            employee.NumeroCuenta = txtAccountNumber.Text;
            employee.CorreoElectronico = txtEmail.Text;
            employee.Contrasena = txtPassword.Text;

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

            employee.Calle = txtStreet.Text;
            employee.Numero = txtNumber.Text;
            employee.Colonia = txtSuburb.Text;
            employee.Ciudad = cbCity.Text;
            employee.Estado = cbState.Text;
            employee.CodigoPostal = txtPostalCode.Text;

            for(int i = 0; i < cbPhones.Items.Count; i++)
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

            nudBaseSalary.Value = 0.0m;
            nudWageLevel.Value = 0.0m;
            nudDailySalary.Value = 0.0m;
            cbDepartments.SelectedIndex = 0;
            cbPositions.SelectedIndex = 0;


            EmployeeState = EntityState.Add;
            dtgPrevIndex = -1;

            txtFilter.Clear();
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
            ComboBoxUtils.FindHiddenValue(bankId, ref cbBank);

            int departmentId = ((PairItem)row.Cells["department"].Value).HiddenValue;
            ComboBoxUtils.FindHiddenValue(departmentId, ref cbDepartments);

            int positionId = ((PairItem)row.Cells["position"].Value).HiddenValue;
            ComboBoxUtils.FindHiddenValue(positionId, ref cbPositions);

            cbPhones.SelectedIndex = -1;
            cbPhones.Items.Clear();
            List<string> phones = phonesRepository.ReadEmployeePhones(employeeId);
            foreach(string phone in phones)
            {
                cbPhones.Items.Add(phone);
            }

            EmployeeState = EntityState.Modify;

            dtpDateOfBirth.Value = Convert.ToDateTime(row.Cells["dateOfBirth"].Value);
            dtpHiringDate.Value = Convert.ToDateTime(row.Cells["hiringDate"].Value);

            dtgPrevIndex = index;
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

        private void ListBanks()
        {
            RepositorioBancos repository = new RepositorioBancos();
            List<Banks> bancos = repository.ReadAll();
            List<PairItem> nombres = new List<PairItem>();
            nombres.Add(new PairItem("Seleccionar", -1));
            foreach (var banco in bancos)
            {
                nombres.Add(new PairItem(banco.Name, banco.BankId));
            }
            cbBank.DataSource = nombres;
        }

        private void ListDepartments()
        {
            DepartmentsRepository repository = new DepartmentsRepository();
            List<DepartmentsViewModel> departments = repository.ReadAll(string.Empty, Session.companyId);
            List<PairItem>  names = new List<PairItem>();
            names.Add(new PairItem("Seleccionar", -1));
            foreach (var department in departments)
            {
                names.Add(new PairItem(department.Name, department.Id));
            }
            cbDepartments.DataSource = names;
        }

        private void ListPositions()
        {
            PositionsRepository repository = new PositionsRepository();
            List<PositionsViewModel> positions = repository.ReadAll(string.Empty, Session.companyId);
            List<PairItem> names = new List<PairItem>();
            names.Add(new PairItem("Seleccionar", -1));
            foreach (var position in positions)
            {
                names.Add(new PairItem(position.Name, position.Id));
            }
            cbPositions.DataSource = names;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtgEmployees.DataSource = repository.ReadAll(txtFilter.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            dtpDateOfBirth.MaxDate = dtpHiringDate.Value.AddYears(-18);
        }
    }
}
