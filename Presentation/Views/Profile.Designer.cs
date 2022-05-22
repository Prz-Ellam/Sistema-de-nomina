
namespace Presentation.Views
{
    partial class Profile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPhones = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSuburb = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNSS = new System.Windows.Forms.TextBox();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.txtCURP = new System.Windows.Forms.TextBox();
            this.txtMotherLastName = new System.Windows.Forms.TextBox();
            this.txtFatherLastName = new System.Windows.Forms.TextBox();
            this.txtNames = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.cbBank = new System.Windows.Forms.ComboBox();
            this.nudDailySalary = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.nudWageLevel = new System.Windows.Forms.NumericUpDown();
            this.nudBaseSalary = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.dtpHiringDate = new System.Windows.Forms.DateTimePicker();
            this.helpInfo = new System.Windows.Forms.ToolTip(this.components);
            this.pbHelp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDailySalary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWageLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSalary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCity
            // 
            this.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCity.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(829, 176);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(200, 27);
            this.cbCity.TabIndex = 95;
            // 
            // cbState
            // 
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(829, 209);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(200, 27);
            this.cbState.TabIndex = 94;
            this.cbState.SelectedIndexChanged += new System.EventHandler(this.cbState_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label1.Location = new System.Drawing.Point(398, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 91;
            this.label1.Text = "Teléfonos";
            // 
            // cbPhones
            // 
            this.cbPhones.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbPhones.FormattingEnabled = true;
            this.cbPhones.Location = new System.Drawing.Point(501, 248);
            this.cbPhones.MaxLength = 10;
            this.cbPhones.Name = "cbPhones";
            this.cbPhones.Size = new System.Drawing.Size(200, 27);
            this.cbPhones.TabIndex = 90;
            this.cbPhones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPhones_KeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label20.Location = new System.Drawing.Point(761, 293);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 19);
            this.label20.TabIndex = 89;
            this.label20.Text = "Puesto";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label19.Location = new System.Drawing.Point(393, 293);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 19);
            this.label19.TabIndex = 88;
            this.label19.Text = "Departamento";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(397, 469);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(304, 39);
            this.btnEdit.TabIndex = 86;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label18.Location = new System.Drawing.Point(58, 251);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 19);
            this.label18.TabIndex = 84;
            this.label18.Text = "Contraseña";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(152, 248);
            this.txtPassword.MaxLength = 30;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 25);
            this.txtPassword.TabIndex = 83;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(152, 211);
            this.txtEmail.MaxLength = 60;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 25);
            this.txtEmail.TabIndex = 82;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label17.Location = new System.Drawing.Point(14, 214);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 19);
            this.label17.TabIndex = 81;
            this.label17.Text = "Correo electrónico";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label16.Location = new System.Drawing.Point(440, 179);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 19);
            this.label16.TabIndex = 80;
            this.label16.Text = "Banco";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNumber.Location = new System.Drawing.Point(501, 211);
            this.txtAccountNumber.MaxLength = 16;
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(200, 25);
            this.txtAccountNumber.TabIndex = 79;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label15.Location = new System.Drawing.Point(362, 214);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 19);
            this.label15.TabIndex = 78;
            this.label15.Text = "Número de cuenta";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label14.Location = new System.Drawing.Point(721, 251);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 19);
            this.label14.TabIndex = 76;
            this.label14.Text = "Código postal";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostalCode.Location = new System.Drawing.Point(829, 248);
            this.txtPostalCode.MaxLength = 5;
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(200, 25);
            this.txtPostalCode.TabIndex = 75;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label13.Location = new System.Drawing.Point(765, 212);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 19);
            this.label13.TabIndex = 74;
            this.label13.Text = "Estado";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label12.Location = new System.Drawing.Point(742, 179);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 19);
            this.label12.TabIndex = 73;
            this.label12.Text = "Municipio";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label11.Location = new System.Drawing.Point(761, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 19);
            this.label11.TabIndex = 72;
            this.label11.Text = "Colonia";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label10.Location = new System.Drawing.Point(755, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 19);
            this.label10.TabIndex = 71;
            this.label10.Text = "Número";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label9.Location = new System.Drawing.Point(777, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 19);
            this.label9.TabIndex = 70;
            this.label9.Text = "Calle";
            // 
            // txtSuburb
            // 
            this.txtSuburb.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuburb.Location = new System.Drawing.Point(829, 135);
            this.txtSuburb.MaxLength = 30;
            this.txtSuburb.Name = "txtSuburb";
            this.txtSuburb.Size = new System.Drawing.Size(200, 25);
            this.txtSuburb.TabIndex = 69;
            // 
            // txtNumber
            // 
            this.txtNumber.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.Location = new System.Drawing.Point(829, 99);
            this.txtNumber.MaxLength = 10;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(200, 25);
            this.txtNumber.TabIndex = 68;
            // 
            // txtStreet
            // 
            this.txtStreet.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStreet.Location = new System.Drawing.Point(829, 68);
            this.txtStreet.MaxLength = 30;
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(200, 25);
            this.txtStreet.TabIndex = 67;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label8.Location = new System.Drawing.Point(1, 179);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 19);
            this.label8.TabIndex = 66;
            this.label8.Text = "Fecha de nacimiento";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(152, 174);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 25);
            this.dtpDateOfBirth.TabIndex = 65;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label7.Location = new System.Drawing.Point(451, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 19);
            this.label7.TabIndex = 64;
            this.label7.Text = "NSS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label6.Location = new System.Drawing.Point(454, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 19);
            this.label6.TabIndex = 63;
            this.label6.Text = "RFC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label5.Location = new System.Drawing.Point(443, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 19);
            this.label5.TabIndex = 62;
            this.label5.Text = "CURP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label4.Location = new System.Drawing.Point(23, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 19);
            this.label4.TabIndex = 61;
            this.label4.Text = "Apellido materno";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label3.Location = new System.Drawing.Point(27, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 19);
            this.label3.TabIndex = 60;
            this.label3.Text = "Apellido paterno";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label2.Location = new System.Drawing.Point(63, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 59;
            this.label2.Text = "Nombre(s)";
            // 
            // txtNSS
            // 
            this.txtNSS.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNSS.Location = new System.Drawing.Point(501, 138);
            this.txtNSS.MaxLength = 11;
            this.txtNSS.Name = "txtNSS";
            this.txtNSS.ReadOnly = true;
            this.txtNSS.Size = new System.Drawing.Size(200, 25);
            this.txtNSS.TabIndex = 58;
            // 
            // txtRFC
            // 
            this.txtRFC.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(501, 104);
            this.txtRFC.MaxLength = 13;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(200, 25);
            this.txtRFC.TabIndex = 57;
            // 
            // txtCURP
            // 
            this.txtCURP.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCURP.Location = new System.Drawing.Point(501, 68);
            this.txtCURP.MaxLength = 18;
            this.txtCURP.Name = "txtCURP";
            this.txtCURP.Size = new System.Drawing.Size(200, 25);
            this.txtCURP.TabIndex = 56;
            // 
            // txtMotherLastName
            // 
            this.txtMotherLastName.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotherLastName.Location = new System.Drawing.Point(152, 138);
            this.txtMotherLastName.MaxLength = 30;
            this.txtMotherLastName.Name = "txtMotherLastName";
            this.txtMotherLastName.ReadOnly = true;
            this.txtMotherLastName.Size = new System.Drawing.Size(200, 25);
            this.txtMotherLastName.TabIndex = 55;
            // 
            // txtFatherLastName
            // 
            this.txtFatherLastName.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatherLastName.Location = new System.Drawing.Point(152, 104);
            this.txtFatherLastName.MaxLength = 30;
            this.txtFatherLastName.Name = "txtFatherLastName";
            this.txtFatherLastName.ReadOnly = true;
            this.txtFatherLastName.Size = new System.Drawing.Size(200, 25);
            this.txtFatherLastName.TabIndex = 54;
            // 
            // txtNames
            // 
            this.txtNames.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNames.Location = new System.Drawing.Point(152, 71);
            this.txtNames.MaxLength = 30;
            this.txtNames.Name = "txtNames";
            this.txtNames.ReadOnly = true;
            this.txtNames.Size = new System.Drawing.Size(200, 25);
            this.txtNames.TabIndex = 53;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(147, 31);
            this.lblTitle.TabIndex = 52;
            this.lblTitle.Text = "Empleados";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.Location = new System.Drawing.Point(501, 290);
            this.txtDepartment.MaxLength = 30;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(200, 25);
            this.txtDepartment.TabIndex = 98;
            // 
            // txtPosition
            // 
            this.txtPosition.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosition.Location = new System.Drawing.Point(829, 290);
            this.txtPosition.MaxLength = 30;
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.ReadOnly = true;
            this.txtPosition.Size = new System.Drawing.Size(200, 25);
            this.txtPosition.TabIndex = 99;
            // 
            // cbBank
            // 
            this.cbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBank.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbBank.FormattingEnabled = true;
            this.cbBank.Location = new System.Drawing.Point(501, 176);
            this.cbBank.Name = "cbBank";
            this.cbBank.Size = new System.Drawing.Size(200, 27);
            this.cbBank.TabIndex = 100;
            // 
            // nudDailySalary
            // 
            this.nudDailySalary.DecimalPlaces = 2;
            this.nudDailySalary.Enabled = false;
            this.nudDailySalary.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.nudDailySalary.Location = new System.Drawing.Point(844, 373);
            this.nudDailySalary.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudDailySalary.Name = "nudDailySalary";
            this.nudDailySalary.ReadOnly = true;
            this.nudDailySalary.Size = new System.Drawing.Size(185, 25);
            this.nudDailySalary.TabIndex = 109;
            this.nudDailySalary.ThousandsSeparator = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label28.Location = new System.Drawing.Point(824, 376);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 20);
            this.label28.TabIndex = 108;
            this.label28.Text = "$";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label27.Location = new System.Drawing.Point(822, 338);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(21, 20);
            this.label27.TabIndex = 107;
            this.label27.Text = "%";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label26.Location = new System.Drawing.Point(497, 338);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(17, 20);
            this.label26.TabIndex = 106;
            this.label26.Text = "$";
            // 
            // nudWageLevel
            // 
            this.nudWageLevel.DecimalPlaces = 2;
            this.nudWageLevel.Enabled = false;
            this.nudWageLevel.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.nudWageLevel.Location = new System.Drawing.Point(844, 335);
            this.nudWageLevel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudWageLevel.Name = "nudWageLevel";
            this.nudWageLevel.ReadOnly = true;
            this.nudWageLevel.Size = new System.Drawing.Size(185, 25);
            this.nudWageLevel.TabIndex = 105;
            this.nudWageLevel.ThousandsSeparator = true;
            // 
            // nudBaseSalary
            // 
            this.nudBaseSalary.DecimalPlaces = 2;
            this.nudBaseSalary.Enabled = false;
            this.nudBaseSalary.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.nudBaseSalary.Location = new System.Drawing.Point(516, 335);
            this.nudBaseSalary.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudBaseSalary.Name = "nudBaseSalary";
            this.nudBaseSalary.ReadOnly = true;
            this.nudBaseSalary.Size = new System.Drawing.Size(185, 25);
            this.nudBaseSalary.TabIndex = 104;
            this.nudBaseSalary.ThousandsSeparator = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label25.Location = new System.Drawing.Point(726, 341);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(85, 19);
            this.label25.TabIndex = 103;
            this.label25.Text = "Nivel salarial";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label24.Location = new System.Drawing.Point(398, 344);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(83, 19);
            this.label24.TabIndex = 102;
            this.label24.Text = "Sueldo base";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label21.Location = new System.Drawing.Point(726, 373);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 19);
            this.label21.TabIndex = 101;
            this.label21.Text = "Sueldo diario";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label22.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label22.Location = new System.Drawing.Point(1, 295);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(146, 19);
            this.label22.TabIndex = 111;
            this.label22.Text = "Fecha de contratación";
            // 
            // dtpHiringDate
            // 
            this.dtpHiringDate.Enabled = false;
            this.dtpHiringDate.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.dtpHiringDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHiringDate.Location = new System.Drawing.Point(152, 290);
            this.dtpHiringDate.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dtpHiringDate.Name = "dtpHiringDate";
            this.dtpHiringDate.Size = new System.Drawing.Size(200, 25);
            this.dtpHiringDate.TabIndex = 110;
            this.dtpHiringDate.ValueChanged += new System.EventHandler(this.dtpHiringDate_ValueChanged);
            // 
            // pbHelp
            // 
            this.pbHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHelp.Image = global::Presentation.Properties.Resources.Help;
            this.pbHelp.Location = new System.Drawing.Point(465, 254);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(16, 16);
            this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHelp.TabIndex = 112;
            this.pbHelp.TabStop = false;
            this.helpInfo.SetToolTip(this.pbHelp, "Ingrese su teléfono y presione Enter para añadirlo, si desea eliminar un teléfono" +
        ", seleccionelo y pulse Delete");
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 670);
            this.Controls.Add(this.pbHelp);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.dtpHiringDate);
            this.Controls.Add(this.nudDailySalary);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.nudWageLevel);
            this.Controls.Add(this.nudBaseSalary);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cbBank);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.cbCity);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPhones);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtAccountNumber);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtPostalCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSuburb);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtStreet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNSS);
            this.Controls.Add(this.txtRFC);
            this.Controls.Add(this.txtCURP);
            this.Controls.Add(this.txtMotherLastName);
            this.Controls.Add(this.txtFatherLastName);
            this.Controls.Add(this.txtNames);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Profile";
            this.Text = "Profile";
            this.Load += new System.EventHandler(this.Profile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDailySalary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWageLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSalary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPhones;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSuburb;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNSS;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.TextBox txtCURP;
        private System.Windows.Forms.TextBox txtMotherLastName;
        private System.Windows.Forms.TextBox txtFatherLastName;
        private System.Windows.Forms.TextBox txtNames;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.ComboBox cbBank;
        private System.Windows.Forms.NumericUpDown nudDailySalary;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown nudWageLevel;
        private System.Windows.Forms.NumericUpDown nudBaseSalary;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DateTimePicker dtpHiringDate;
        private System.Windows.Forms.ToolTip helpInfo;
        private System.Windows.Forms.PictureBox pbHelp;
    }
}