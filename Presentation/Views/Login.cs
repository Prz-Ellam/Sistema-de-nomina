using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Presentation.Helpers;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Data_Access.Repositorios;

namespace Presentation.Views
{
    public partial class Login : Form
    {
        UsersRepository repository = new UsersRepository();
        Users user;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == string.Empty)
            {
                lblEmailError.Text = "El correo electrónico no puede estar vacío";
                lblEmailError.Visible = true;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            lblEmailError.Text = string.Empty;
            lblEmailError.Visible = false;
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }

        private void lblEmail_MouseHover(object sender, EventArgs e)
        {
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (email == string.Empty || password == string.Empty)
            {
                MessageBox.Show("Fail");
                return;
            }

            user = new Users();
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;
            char position = (chkAdmin.Checked) ? 'A' : 'E';

            repository.Login(position, user.Email, user.Password);
        }
    }
}
