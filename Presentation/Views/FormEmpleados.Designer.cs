
namespace Presentation.Views
{
    partial class FormEmpleados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtNames = new System.Windows.Forms.TextBox();
            this.txtFatherLastName = new System.Windows.Forms.TextBox();
            this.txtMotherLastName = new System.Windows.Forms.TextBox();
            this.txtCURP = new System.Windows.Forms.TextBox();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.txtNSS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtSuburb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cbPhones = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgEmployees = new System.Windows.Forms.DataGridView();
            this.cbDepartments = new System.Windows.Forms.ComboBox();
            this.cbPositions = new System.Windows.Forms.ComboBox();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.cbBank = new System.Windows.Forms.ComboBox();
            this.dtpHiringDate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtBaseSalary = new System.Windows.Forms.TextBox();
            this.employeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fatherLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motherLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.street = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suburb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.city = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postalCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hiringDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sueldoDiario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(147, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Empleados";
            // 
            // txtNames
            // 
            this.txtNames.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNames.Location = new System.Drawing.Point(160, 64);
            this.txtNames.Name = "txtNames";
            this.txtNames.Size = new System.Drawing.Size(200, 25);
            this.txtNames.TabIndex = 1;
            // 
            // txtFatherLastName
            // 
            this.txtFatherLastName.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatherLastName.Location = new System.Drawing.Point(160, 98);
            this.txtFatherLastName.Name = "txtFatherLastName";
            this.txtFatherLastName.Size = new System.Drawing.Size(200, 25);
            this.txtFatherLastName.TabIndex = 2;
            // 
            // txtMotherLastName
            // 
            this.txtMotherLastName.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotherLastName.Location = new System.Drawing.Point(160, 134);
            this.txtMotherLastName.Name = "txtMotherLastName";
            this.txtMotherLastName.Size = new System.Drawing.Size(200, 25);
            this.txtMotherLastName.TabIndex = 3;
            // 
            // txtCURP
            // 
            this.txtCURP.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCURP.Location = new System.Drawing.Point(509, 64);
            this.txtCURP.Name = "txtCURP";
            this.txtCURP.Size = new System.Drawing.Size(200, 25);
            this.txtCURP.TabIndex = 4;
            // 
            // txtRFC
            // 
            this.txtRFC.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(509, 100);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(200, 25);
            this.txtRFC.TabIndex = 5;
            // 
            // txtNSS
            // 
            this.txtNSS.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNSS.Location = new System.Drawing.Point(509, 136);
            this.txtNSS.Name = "txtNSS";
            this.txtNSS.Size = new System.Drawing.Size(200, 25);
            this.txtNSS.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label2.Location = new System.Drawing.Point(20, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nombre(s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label3.Location = new System.Drawing.Point(20, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Apellido paterno";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label4.Location = new System.Drawing.Point(22, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Apellido materno";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label5.Location = new System.Drawing.Point(459, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "CURP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label6.Location = new System.Drawing.Point(459, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "RFC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label7.Location = new System.Drawing.Point(459, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "NSS";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(160, 173);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 25);
            this.dtpDateOfBirth.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label8.Location = new System.Drawing.Point(20, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 19);
            this.label8.TabIndex = 14;
            this.label8.Text = "Fecha de nacimiento";
            // 
            // txtStreet
            // 
            this.txtStreet.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStreet.Location = new System.Drawing.Point(837, 70);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(200, 25);
            this.txtStreet.TabIndex = 15;
            // 
            // txtNumber
            // 
            this.txtNumber.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.Location = new System.Drawing.Point(837, 103);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(200, 25);
            this.txtNumber.TabIndex = 16;
            // 
            // txtSuburb
            // 
            this.txtSuburb.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuburb.Location = new System.Drawing.Point(837, 140);
            this.txtSuburb.Name = "txtSuburb";
            this.txtSuburb.Size = new System.Drawing.Size(200, 25);
            this.txtSuburb.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label9.Location = new System.Drawing.Point(785, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Calle";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label10.Location = new System.Drawing.Point(763, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 19);
            this.label10.TabIndex = 21;
            this.label10.Text = "Número";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label11.Location = new System.Drawing.Point(768, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 19);
            this.label11.TabIndex = 22;
            this.label11.Text = "Colonia";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label12.Location = new System.Drawing.Point(751, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 19);
            this.label12.TabIndex = 23;
            this.label12.Text = "Municipio";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label13.Location = new System.Drawing.Point(773, 218);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 19);
            this.label13.TabIndex = 24;
            this.label13.Text = "Estado";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostalCode.Location = new System.Drawing.Point(837, 254);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(200, 25);
            this.txtPostalCode.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label14.Location = new System.Drawing.Point(729, 257);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 19);
            this.label14.TabIndex = 26;
            this.label14.Text = "Código postal";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label15.Location = new System.Drawing.Point(370, 217);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 19);
            this.label15.TabIndex = 28;
            this.label15.Text = "Número de cuenta";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNumber.Location = new System.Drawing.Point(509, 214);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(200, 25);
            this.txtAccountNumber.TabIndex = 29;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label16.Location = new System.Drawing.Point(448, 183);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 19);
            this.label16.TabIndex = 30;
            this.label16.Text = "Banco";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label17.Location = new System.Drawing.Point(22, 213);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 19);
            this.label17.TabIndex = 31;
            this.label17.Text = "Correo electrónico";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(160, 213);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 25);
            this.txtEmail.TabIndex = 32;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(160, 247);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 25);
            this.txtPassword.TabIndex = 33;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label18.Location = new System.Drawing.Point(22, 247);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 19);
            this.label18.TabIndex = 34;
            this.label18.Text = "Contraseña";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(345, 360);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(124, 39);
            this.btnDelete.TabIndex = 42;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(177, 360);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(124, 39);
            this.btnEdit.TabIndex = 41;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(26, 358);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(124, 41);
            this.btnAdd.TabIndex = 40;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label19.Location = new System.Drawing.Point(401, 295);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 19);
            this.label19.TabIndex = 43;
            this.label19.Text = "Departamento";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label20.Location = new System.Drawing.Point(769, 300);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 19);
            this.label20.TabIndex = 44;
            this.label20.Text = "Puesto";
            // 
            // cbPhones
            // 
            this.cbPhones.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbPhones.FormattingEnabled = true;
            this.cbPhones.Location = new System.Drawing.Point(509, 248);
            this.cbPhones.Name = "cbPhones";
            this.cbPhones.Size = new System.Drawing.Size(200, 27);
            this.cbPhones.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label1.Location = new System.Drawing.Point(427, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 46;
            this.label1.Text = "Teléfonos";
            // 
            // dtgEmployees
            // 
            this.dtgEmployees.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.dtgEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgEmployees.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.employeeNumber,
            this.empName,
            this.fatherLastName,
            this.motherLastName,
            this.dateOfBirth,
            this.curp,
            this.nss,
            this.rfc,
            this.street,
            this.number,
            this.suburb,
            this.city,
            this.state,
            this.postalCode,
            this.bank,
            this.accountNumber,
            this.email,
            this.department,
            this.position,
            this.hiringDate,
            this.sueldoDiario});
            this.dtgEmployees.EnableHeadersVisualStyles = false;
            this.dtgEmployees.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgEmployees.Location = new System.Drawing.Point(12, 435);
            this.dtgEmployees.Name = "dtgEmployees";
            this.dtgEmployees.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgEmployees.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.dtgEmployees.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgEmployees.Size = new System.Drawing.Size(1040, 223);
            this.dtgEmployees.TabIndex = 47;
            this.dtgEmployees.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEmployees_CellDoubleClick);
            // 
            // cbDepartments
            // 
            this.cbDepartments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartments.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbDepartments.FormattingEnabled = true;
            this.cbDepartments.Location = new System.Drawing.Point(509, 292);
            this.cbDepartments.Name = "cbDepartments";
            this.cbDepartments.Size = new System.Drawing.Size(200, 27);
            this.cbDepartments.TabIndex = 48;
            // 
            // cbPositions
            // 
            this.cbPositions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPositions.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbPositions.FormattingEnabled = true;
            this.cbPositions.Location = new System.Drawing.Point(837, 297);
            this.cbPositions.Name = "cbPositions";
            this.cbPositions.Size = new System.Drawing.Size(200, 27);
            this.cbPositions.TabIndex = 49;
            // 
            // cbState
            // 
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(837, 216);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(200, 27);
            this.cbState.TabIndex = 50;
            this.cbState.SelectedIndexChanged += new System.EventHandler(this.cbState_SelectedIndexChanged);
            // 
            // cbCity
            // 
            this.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCity.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(837, 180);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(200, 27);
            this.cbCity.TabIndex = 51;
            // 
            // cbBank
            // 
            this.cbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBank.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.cbBank.FormattingEnabled = true;
            this.cbBank.Location = new System.Drawing.Point(509, 175);
            this.cbBank.Name = "cbBank";
            this.cbBank.Size = new System.Drawing.Size(200, 27);
            this.cbBank.TabIndex = 52;
            // 
            // dtpHiringDate
            // 
            this.dtpHiringDate.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.dtpHiringDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHiringDate.Location = new System.Drawing.Point(160, 292);
            this.dtpHiringDate.Name = "dtpHiringDate";
            this.dtpHiringDate.Size = new System.Drawing.Size(200, 25);
            this.dtpHiringDate.TabIndex = 53;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label21.Location = new System.Drawing.Point(12, 295);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(146, 19);
            this.label21.TabIndex = 54;
            this.label21.Text = "Fecha de contratación";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(789, 395);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 25);
            this.textBox1.TabIndex = 56;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label22.Location = new System.Drawing.Point(729, 398);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 19);
            this.label22.TabIndex = 55;
            this.label22.Text = "Filtrar";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label23.Location = new System.Drawing.Point(734, 347);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(89, 19);
            this.label23.TabIndex = 58;
            this.label23.Text = "Sueldo diario";
            // 
            // txtBaseSalary
            // 
            this.txtBaseSalary.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseSalary.Location = new System.Drawing.Point(837, 344);
            this.txtBaseSalary.Name = "txtBaseSalary";
            this.txtBaseSalary.ReadOnly = true;
            this.txtBaseSalary.Size = new System.Drawing.Size(200, 25);
            this.txtBaseSalary.TabIndex = 57;
            // 
            // employeeNumber
            // 
            this.employeeNumber.DataPropertyName = "employeeNumber";
            this.employeeNumber.HeaderText = "Número de empleado";
            this.employeeNumber.Name = "employeeNumber";
            this.employeeNumber.ReadOnly = true;
            this.employeeNumber.Width = 120;
            // 
            // empName
            // 
            this.empName.DataPropertyName = "name";
            this.empName.HeaderText = "Nombre";
            this.empName.Name = "empName";
            this.empName.ReadOnly = true;
            this.empName.Width = 120;
            // 
            // fatherLastName
            // 
            this.fatherLastName.DataPropertyName = "fatherLastName";
            this.fatherLastName.HeaderText = "Apellido paterno";
            this.fatherLastName.Name = "fatherLastName";
            this.fatherLastName.ReadOnly = true;
            this.fatherLastName.Width = 120;
            // 
            // motherLastName
            // 
            this.motherLastName.DataPropertyName = "motherLastName";
            this.motherLastName.HeaderText = "Apellido materno";
            this.motherLastName.Name = "motherLastName";
            this.motherLastName.ReadOnly = true;
            this.motherLastName.Width = 120;
            // 
            // dateOfBirth
            // 
            this.dateOfBirth.DataPropertyName = "dateOfBirth";
            this.dateOfBirth.HeaderText = "Fecha de nacimiento";
            this.dateOfBirth.Name = "dateOfBirth";
            this.dateOfBirth.ReadOnly = true;
            this.dateOfBirth.Width = 120;
            // 
            // curp
            // 
            this.curp.DataPropertyName = "curp";
            this.curp.HeaderText = "CURP";
            this.curp.Name = "curp";
            this.curp.ReadOnly = true;
            this.curp.Width = 120;
            // 
            // nss
            // 
            this.nss.DataPropertyName = "nss";
            this.nss.HeaderText = "NSS";
            this.nss.Name = "nss";
            this.nss.ReadOnly = true;
            this.nss.Width = 120;
            // 
            // rfc
            // 
            this.rfc.DataPropertyName = "rfc";
            this.rfc.HeaderText = "RFC";
            this.rfc.Name = "rfc";
            this.rfc.ReadOnly = true;
            this.rfc.Width = 120;
            // 
            // street
            // 
            this.street.DataPropertyName = "street";
            this.street.HeaderText = "Calle";
            this.street.Name = "street";
            this.street.ReadOnly = true;
            this.street.Width = 120;
            // 
            // number
            // 
            this.number.DataPropertyName = "number";
            this.number.HeaderText = "Numero";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 120;
            // 
            // suburb
            // 
            this.suburb.DataPropertyName = "suburb";
            this.suburb.HeaderText = "Colonia";
            this.suburb.Name = "suburb";
            this.suburb.ReadOnly = true;
            this.suburb.Width = 120;
            // 
            // city
            // 
            this.city.DataPropertyName = "city";
            this.city.HeaderText = "Municipio";
            this.city.Name = "city";
            this.city.ReadOnly = true;
            this.city.Width = 120;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "Estado";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 120;
            // 
            // postalCode
            // 
            this.postalCode.DataPropertyName = "postalCode";
            this.postalCode.HeaderText = "Codigo postal";
            this.postalCode.Name = "postalCode";
            this.postalCode.ReadOnly = true;
            this.postalCode.Width = 120;
            // 
            // bank
            // 
            this.bank.DataPropertyName = "bank";
            this.bank.HeaderText = "Banco";
            this.bank.Name = "bank";
            this.bank.ReadOnly = true;
            this.bank.Width = 120;
            // 
            // accountNumber
            // 
            this.accountNumber.DataPropertyName = "accountNumber";
            this.accountNumber.HeaderText = "Numero de cuenta";
            this.accountNumber.Name = "accountNumber";
            this.accountNumber.ReadOnly = true;
            this.accountNumber.Width = 120;
            // 
            // email
            // 
            this.email.DataPropertyName = "email";
            this.email.HeaderText = "Correo electronico";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            this.email.Width = 120;
            // 
            // department
            // 
            this.department.DataPropertyName = "department";
            this.department.HeaderText = "Departamento";
            this.department.Name = "department";
            this.department.ReadOnly = true;
            this.department.Width = 120;
            // 
            // position
            // 
            this.position.DataPropertyName = "position";
            this.position.HeaderText = "Puesto";
            this.position.Name = "position";
            this.position.ReadOnly = true;
            this.position.Width = 120;
            // 
            // hiringDate
            // 
            this.hiringDate.DataPropertyName = "hiringDate";
            this.hiringDate.HeaderText = "Fecha de contratacion";
            this.hiringDate.Name = "hiringDate";
            this.hiringDate.ReadOnly = true;
            this.hiringDate.Width = 120;
            // 
            // sueldoDiario
            // 
            this.sueldoDiario.DataPropertyName = "sueldoDiario";
            dataGridViewCellStyle2.Format = "c";
            this.sueldoDiario.DefaultCellStyle = dataGridViewCellStyle2;
            this.sueldoDiario.HeaderText = "Sueldo diario";
            this.sueldoDiario.Name = "sueldoDiario";
            this.sueldoDiario.ReadOnly = true;
            // 
            // FormEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1064, 670);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtBaseSalary);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dtpHiringDate);
            this.Controls.Add(this.cbBank);
            this.Controls.Add(this.cbCity);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.cbPositions);
            this.Controls.Add(this.cbDepartments);
            this.Controls.Add(this.dtgEmployees);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPhones);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
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
            this.Name = "FormEmpleados";
            this.Text = "Employees";
            this.Load += new System.EventHandler(this.Employees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtNames;
        private System.Windows.Forms.TextBox txtFatherLastName;
        private System.Windows.Forms.TextBox txtMotherLastName;
        private System.Windows.Forms.TextBox txtCURP;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.TextBox txtNSS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtSuburb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbPhones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgEmployees;
        private System.Windows.Forms.ComboBox cbDepartments;
        private System.Windows.Forms.ComboBox cbPositions;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.ComboBox cbBank;
        private System.Windows.Forms.DateTimePicker dtpHiringDate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtBaseSalary;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fatherLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn motherLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfBirth;
        private System.Windows.Forms.DataGridViewTextBoxColumn curp;
        private System.Windows.Forms.DataGridViewTextBoxColumn nss;
        private System.Windows.Forms.DataGridViewTextBoxColumn rfc;
        private System.Windows.Forms.DataGridViewTextBoxColumn street;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn suburb;
        private System.Windows.Forms.DataGridViewTextBoxColumn city;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn postalCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn bank;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn department;
        private System.Windows.Forms.DataGridViewTextBoxColumn position;
        private System.Windows.Forms.DataGridViewTextBoxColumn hiringDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn sueldoDiario;
    }
}