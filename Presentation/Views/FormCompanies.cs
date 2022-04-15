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
    public partial class FormCompanies : Form
    {
        private CompaniesRepository repository = new CompaniesRepository();
        private RepositorioDomicilios addressesRepository = new RepositorioDomicilios();
        private Empresas company = new Empresas();
        private Domicilios address = new Domicilios();

        private List<States> states;
        public FormCompanies()
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            /*
            company.BusinessName = txtBusinessName.Text;
            company.Employer_registration = txtEmployerRegistration.Text;
            company.Email = txtEmail.Text;
            company.Start_date = dtpStartDate.Value;
            company.Address = 0;
            company.Rfc = txtRFC.Text;
            repository.Create(company);
            */
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
            try
            {
                FillEntity();
                AddEntity();
                ListCompany();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillEntity()
        {
            company.RazonSocial = txtBusinessName.Text;
            company.CorreoElectronico = txtEmail.Text;
            company.Rfc = txtRFC.Text;
            company.RegistroPatronal = txtEmployerRegistration.Text;
            company.FechaInicio = dtpStartDate.Value;
            company.IdAdministrador = Session.id;
            
            address.Calle = txtStreet.Text;
            address.Numero = txtNumber.Text;
            address.Colonia = txtStreet.Text;
            address.Ciudad = cbCities.Text;
            address.Estado = cbStates.Text;
            address.CodigoPostal = txtPostalCode.Text;
            
        }

        public void AddEntity()
        {
            Tuple<bool, string> feedback = new DataValidation(address).Validate();
            if (!feedback.Item1)
            {
                MessageBox.Show(feedback.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idDomicilio = addressesRepository.Create(address);
            company.Domicilio = idDomicilio;
            
            feedback = new DataValidation(company).Validate();
            if (!feedback.Item1)
            {
                MessageBox.Show(feedback.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int rowsAffected = repository.Create(company);
            if (rowsAffected < 1)
            {
                MessageBox.Show("No se pudo realizar la operación");
            }
            
        }

        private void ListCompany()
        {
            // Verify regresa -1 si el administrador aun no ha creado su empresa
            if (Session.company_id == -1)
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
            }
            else
            {
                InitCompanyData();
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
            }
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
        }
    }
}
