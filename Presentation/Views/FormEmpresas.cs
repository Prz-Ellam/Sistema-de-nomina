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
using System.IO;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Presentation.Helpers;
using Data_Access.Entidades;
using Data_Access.ViewModels;
using System.Data.SqlClient;

namespace Presentation.Views
{
    public partial class FormEmpresas : Form
    {
        private CompaniesRepository repository = new CompaniesRepository();
        private Empresas company = new Empresas();
        private List<States> states;

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
                        //btnDelete.Enabled = false;
                        break;
                    }
                    case EntityState.Modify:
                    {
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = true;
                        //btnDelete.Enabled = true;
                        break;
                    }
                }
            }
        }

        public FormEmpresas()
        {
            InitializeComponent();
        }

        private void Companies_Load(object sender, EventArgs e)
        {
            InitStates();
            ListCompany();
        }

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCities.DataSource = null;
            if (cbStates.SelectedIndex <= 0)
            {
                return;
            }
            cbCities.DataSource = states[cbStates.SelectedIndex].cities;
            cbCities.SelectedIndex = 0;
        }

        private void InitStates()
        {
            StatesRepository repository = new StatesRepository();
            states = repository.GetAll();

            foreach (var state in states)
            {
                cbStates.Items.Add(state.state);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillCompany();
            ValidationResult result = AddCompany();

            if (result.State == ValidationState.Error)
            {
                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InitCompanyId(); // Carga en el Session el id de la empresa recien creada
            ListCompany();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FillCompany();
            ValidationResult result = UpdateCompany();

            if (result.State == ValidationState.Error)
            {
                MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(result.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListCompany();
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
                return new ValidationResult(ex.Message, ValidationState.Error);
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
                return new ValidationResult(ex.Message, ValidationState.Error);
            }
        }

        private void ListCompany()
        {
            // Verify regresa -1 si el administrador aun no ha creado su empresa
            if (Session.company_id == -1)
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
            company.IdEmpresa = Session.company_id;
            company.RazonSocial = txtBusinessName.Text;
            company.CorreoElectronico = txtEmail.Text;
            company.Rfc = txtRFC.Text;
            company.RegistroPatronal = txtEmployerRegistration.Text;
            company.FechaInicio = dtpStartDate.Value;
            company.IdAdministrador = Session.id;

            company.Calle = txtStreet.Text;
            company.Numero = txtNumber.Text;
            company.Colonia = txtStreet.Text;
            company.Ciudad = cbCities.Text;
            company.Estado = cbStates.Text;
            company.CodigoPostal = txtPostalCode.Text;
        }

        private void InitCompanyData()
        {
            CompaniesViewModel company = repository.Read(Session.company_id);
            txtBusinessName.Text = company.RazonSocial;
            txtEmployerRegistration.Text = company.RegistroPatronal;
            txtRFC.Text = company.Rfc;
            txtEmail.Text = company.CorreoElectronico;
            dtpStartDate.Value = company.FechaInicio;
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
                Session.company_id = new CompaniesRepository().Verify(Session.id);
        }
    }
}
