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
using Data_Access.Repositorios;
using System.Data.SqlClient;
using CustomMessageBox;
using Data_Access.Interfaces;

namespace Presentation.Views
{
    public partial class Login : Form
    {
        IUsersRepository repository;

        public Login()
        {
            InitializeComponent();
            repository = new UsersRepository();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            WinAPI.SendMessage(txtEmail.Handle, WinAPI.EM_SETCUEBANNER, 0, "ejemplo@correo.com");

            this.MouseDown += Moveable;
            panel.MouseDown += Moveable;
            titleBar.MouseDown += Moveable;
            pbLogo.MouseDown += Moveable;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (email == string.Empty || password == string.Empty)
            {
                RJMessageBox.Show("Todos los campos son obligatorios", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Users user = repository.Login(email, password);

                if (user == null)
                {
                    RJMessageBox.Show("Sus credenciales no coinciden", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ShowMenu(user);
                }
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMenu(Users user)
        {
            Session.position = user.Position;
            Session.email = user.Email;
            Session.id = user.Id;
            Session.companyId = user.CompanyId;

            Layout menu = new Layout();
            menu.Show();
            this.Hide();
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void fadeIn_Tick(object sender, EventArgs e)
        {
            Opacity = Opacity + 0.15f % 1;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            lineEmail.BackColor = Color.FromArgb(0, 123, 255);
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            lineEmail.BackColor = Color.FromArgb(47, 47, 47);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            linePassword.BackColor = Color.FromArgb(0, 123, 255);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            linePassword.BackColor = Color.FromArgb(47, 47, 47);
        }

        private void Moveable(object sender, MouseEventArgs e)
        {
            WinAPI.ReleaseCapture();
            WinAPI.SendMessage(this.Handle, WinAPI.WM_SYSCOMMAND, 0xf012, 0);
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}