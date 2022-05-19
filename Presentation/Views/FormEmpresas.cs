using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access.Repositorios;
using Data_Access.Entities;
using Presentation.Helpers;
using Data_Access.Entidades;
using Data_Access.ViewModels;
using System.Data.SqlClient;
using CustomMessageBox;

namespace Presentation.Views
{
    public partial class FormEmpresas : Form
    {
        private RepositorioEmpresas repository;
        private RepositorioTelefonos phonesRepository;
        private Companies company;
        private List<States> states;
        int cbPhonesPrevIndex = -1;

        private EntityState companyState;

        private EntityState CompanyState
        {
            get
            {
                return companyState;
            }

            set
            {
                companyState = value;

                switch (companyState)
                {
                    case EntityState.Add:
                    {
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = false;
                        dtpStartDate.Enabled = true;
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = true;
                        dtpStartDate.Enabled = false;
                        break;
                    }
                }
            }
        }

        public FormEmpresas()
        {
            InitializeComponent();
            repository = new RepositorioEmpresas();
            phonesRepository = new RepositorioTelefonos();
            company = new Companies();
        }

        private void Companies_Load(object sender, EventArgs e)
        {
            ListStates();
            ListCompany();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillCompany();
            ValidationResult result = AddCompany();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InitCompanyId(); // Carga en el Session el id de la empresa recien creada
            ListCompany();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillCompany();
            ValidationResult result = UpdateCompany();

            if (result.State == ValidationState.Error)
            {
                RJMessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RJMessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListCompany();
        }

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCities.DataSource = null;
            if (cbStates.SelectedIndex <= 0)
            {
                List<string> cities = new List<string>();
                cities.Add("Seleccionar");
                cbCities.DataSource = cities;
                return;
            }
            cbCities.DataSource = states[cbStates.SelectedIndex - 1].cities;
            cbCities.SelectedIndex = 0;
        }

        private ValidationResult AddCompany()
        {
            if (CompanyState != EntityState.Add)
            {
                return new ValidationResult("Operación incorrecta", ValidationState.Error);
            }

            try
            {
                Tuple<bool, string> feedback = new DataValidation(company).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Create(company);
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

        private ValidationResult UpdateCompany()
        {
            try
            {
                Tuple<bool, string> feedback = new DataValidation(company).Validate();
                if (!feedback.Item1)
                {
                    return new ValidationResult(feedback.Item2, ValidationState.Error);
                }

                bool result = repository.Update(company);
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

        private void ListCompany()
        {
            // Verify regresa -1 si el administrador aun no ha creado su empresa
            if (Session.companyId == -1)
            {
                CompanyState = EntityState.Add;
            }
            else
            {
                InitCompanyData();
                CompanyState = EntityState.Modify;
            }
        }

        private void FillCompany()
        {
            company.CompanyId = Session.companyId;
            company.AdministratorId = Session.id;

            company.BusinessName = txtBusinessName.Text.Trim();
            company.EmployerRegistration = txtEmployerRegistration.Text.Trim();
            company.Rfc = txtRfc.Text.Trim();
            company.StartDate = dtpStartDate.Value;
            company.Email = txtEmail.Text.Trim();

            company.Phones.Clear();
            foreach (var phone in cbPhones.Items)
            {
                company.Phones.Add(phone.ToString().Trim());
            }

            company.Street = txtStreet.Text.Trim();
            company.Number = txtNumber.Text.Trim();
            company.Suburb = txtSuburb.Text.Trim();
            company.City = cbCities.Text.Trim();
            company.State = cbStates.Text.Trim();
            company.PostalCode = txtPostalCode.Text.Trim();
        }

        private void InitCompanyData()
        {
            CompaniesViewModel company = repository.Read(Session.companyId);
            txtBusinessName.Text = company.RazonSocial;
            txtEmployerRegistration.Text = company.RegistroPatronal;
            txtRfc.Text = company.Rfc;
            dtpStartDate.Value = company.FechaInicio;
            txtEmail.Text = company.CorreoElectronico;

            cbPhones.Items.Clear();
            cbPhones.SelectedIndex = -1;
            List<string> phones = phonesRepository.ReadCompanyPhones(Session.companyId);
            foreach (string phone in phones)
            {
                cbPhones.Items.Add(phone);
            }

            txtStreet.Text = company.Calle;
            txtNumber.Text = company.Numero;
            txtSuburb.Text = company.Colonia;
            txtPostalCode.Text = company.CodigoPostal;
            cbStates.SelectedIndex = cbStates.FindString(company.Estado);
            cbCities.SelectedIndex = cbCities.FindString(company.Ciudad);
        }

        private void InitCompanyId()
        {
            if (Session.position == "Administrador")
            {
                RepositorioEmpresas companiesRepository = new RepositorioEmpresas();
                Session.companyId = companiesRepository.Verify(Session.id);
            }
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

        private void ListStates()
        {
            StatesRepository repository = new StatesRepository();
            states = repository.GetAll();
            cbStates.Items.Add("Seleccionar");
            foreach (var state in states)
            {
                cbStates.Items.Add(state.state);
            }
            cbStates.SelectedIndex = 0;
        }
    }
}