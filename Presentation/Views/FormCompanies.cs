using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Data_Access.Repositories;
using System.IO;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Presentation.Helpers;

namespace Presentation.Views
{
    public partial class FormCompanies : Form
    {
        private CompaniesRepository repository = new CompaniesRepository();
        private Companies company = new Companies();

        private List<States> states;
        public FormCompanies()
        {
            InitializeComponent();
        }

        private void Companies_Load(object sender, EventArgs e)
        {
            InitStates();
            InitCompanyData();
        }

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCities.DataSource = null;
            if (cbStates.SelectedIndex <= 0)
            {
                return;
            }
            cbCities.DataSource = states[cbStates.SelectedIndex - 1].cities;
            cbCities.SelectedIndex = 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            company.BusinessName = txtBusinessName.Text;
            company.Employer_registration = txtEmployerRegistration.Text;
            company.Email = txtEmail.Text;
            company.Start_date = dtpStartDate.Value;
            company.Address = 0;
            company.Rfc = txtRFC.Text;
            repository.Create(company);
        }

        private void InitStates()
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

        private void InitCompanyData()
        {
            Companies company = repository.Read(/*Session.company_id*/ 1);
            txtBusinessName.Text = company.BusinessName;
            txtEmployerRegistration.Text = company.Employer_registration;
            txtRFC.Text = company.Rfc;
            
        }
    }
}
