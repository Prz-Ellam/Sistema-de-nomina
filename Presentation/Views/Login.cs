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
                MessageBox.Show("Todos los campos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Users user = repository.Login(email, password);

            if (user == null)
            {
                MessageBox.Show("Sus credenciales no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ShowMenu(user);
            }

        }

        private void ShowMenu(Users user)
        {
            Session.position = user.Position;
            Session.email = user.Email;
            Session.id = user.Id;
            if (Session.position == "Administrador")
                Session.company_id = new CompaniesRepository().Verify(Session.id);

            Layout menu = new Layout();
            menu.Show();
            this.Hide();
        }

        private void fadeIn_Tick(object sender, EventArgs e)
        {
            Opacity = Opacity + 0.2f % 1;
        }

        private void pbClosed_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
