using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Profile()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        private void Profile_Load(object sender, EventArgs e)
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
    }
}
