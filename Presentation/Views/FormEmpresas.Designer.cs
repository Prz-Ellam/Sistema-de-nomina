
namespace Presentation.Views
{
    partial class FormEmpresas
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
            this.txtBusinessName = new System.Windows.Forms.TextBox();
            this.txtEmployerRegistration = new System.Windows.Forms.TextBox();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblCompanies = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblSuburb = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblStreet = new System.Windows.Forms.Label();
            this.txtSuburb = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.lblBusinessName = new System.Windows.Forms.Label();
            this.lblEmployerRegistration = new System.Windows.Forms.Label();
            this.lblRFC = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.cbCities = new System.Windows.Forms.ComboBox();
            this.cbStates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPhones = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBusinessName
            // 
            this.txtBusinessName.BackColor = System.Drawing.Color.White;
            this.txtBusinessName.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtBusinessName.Location = new System.Drawing.Point(20, 100);
            this.txtBusinessName.MaxLength = 60;
            this.txtBusinessName.Name = "txtBusinessName";
            this.txtBusinessName.Size = new System.Drawing.Size(400, 29);
            this.txtBusinessName.TabIndex = 1;
            // 
            // txtEmployerRegistration
            // 
            this.txtEmployerRegistration.BackColor = System.Drawing.Color.White;
            this.txtEmployerRegistration.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtEmployerRegistration.Location = new System.Drawing.Point(20, 170);
            this.txtEmployerRegistration.MaxLength = 60;
            this.txtEmployerRegistration.Name = "txtEmployerRegistration";
            this.txtEmployerRegistration.Size = new System.Drawing.Size(400, 29);
            this.txtEmployerRegistration.TabIndex = 2;
            // 
            // txtRfc
            // 
            this.txtRfc.BackColor = System.Drawing.Color.White;
            this.txtRfc.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtRfc.Location = new System.Drawing.Point(20, 240);
            this.txtRfc.MaxLength = 12;
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Size = new System.Drawing.Size(400, 29);
            this.txtRfc.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtEmail.Location = new System.Drawing.Point(20, 380);
            this.txtEmail.MaxLength = 60;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 29);
            this.txtEmail.TabIndex = 5;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(20, 310);
            this.dtpStartDate.MaxDate = new System.DateTime(2038, 1, 17, 0, 0, 0, 0);
            this.dtpStartDate.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(400, 29);
            this.dtpStartDate.TabIndex = 4;
            // 
            // lblCompanies
            // 
            this.lblCompanies.AutoSize = true;
            this.lblCompanies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCompanies.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCompanies.Location = new System.Drawing.Point(20, 20);
            this.lblCompanies.Name = "lblCompanies";
            this.lblCompanies.Size = new System.Drawing.Size(130, 31);
            this.lblCompanies.TabIndex = 5;
            this.lblCompanies.Text = "Empresas";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(520, 530);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(400, 42);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblPostalCode
            // 
            this.lblPostalCode.AutoSize = true;
            this.lblPostalCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPostalCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPostalCode.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblPostalCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPostalCode.Location = new System.Drawing.Point(520, 420);
            this.lblPostalCode.Name = "lblPostalCode";
            this.lblPostalCode.Size = new System.Drawing.Size(116, 21);
            this.lblPostalCode.TabIndex = 38;
            this.lblPostalCode.Text = "Código postal";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.BackColor = System.Drawing.Color.White;
            this.txtPostalCode.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtPostalCode.Location = new System.Drawing.Point(520, 450);
            this.txtPostalCode.MaxLength = 5;
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(400, 29);
            this.txtPostalCode.TabIndex = 12;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblState.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblState.Location = new System.Drawing.Point(520, 280);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(61, 21);
            this.lblState.TabIndex = 36;
            this.lblState.Text = "Estado";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCity.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblCity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCity.Location = new System.Drawing.Point(520, 350);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(86, 21);
            this.lblCity.TabIndex = 35;
            this.lblCity.Text = "Municipio";
            // 
            // lblSuburb
            // 
            this.lblSuburb.AutoSize = true;
            this.lblSuburb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSuburb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSuburb.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblSuburb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblSuburb.Location = new System.Drawing.Point(520, 210);
            this.lblSuburb.Name = "lblSuburb";
            this.lblSuburb.Size = new System.Drawing.Size(68, 21);
            this.lblSuburb.TabIndex = 34;
            this.lblSuburb.Text = "Colonia";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNumber.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblNumber.Location = new System.Drawing.Point(520, 140);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(73, 21);
            this.lblNumber.TabIndex = 33;
            this.lblNumber.Text = "Número";
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStreet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStreet.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblStreet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblStreet.Location = new System.Drawing.Point(520, 70);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(47, 21);
            this.lblStreet.TabIndex = 32;
            this.lblStreet.Text = "Calle";
            // 
            // txtSuburb
            // 
            this.txtSuburb.BackColor = System.Drawing.Color.White;
            this.txtSuburb.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtSuburb.Location = new System.Drawing.Point(520, 240);
            this.txtSuburb.MaxLength = 30;
            this.txtSuburb.Name = "txtSuburb";
            this.txtSuburb.Size = new System.Drawing.Size(400, 29);
            this.txtSuburb.TabIndex = 9;
            // 
            // txtNumber
            // 
            this.txtNumber.BackColor = System.Drawing.Color.White;
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumber.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtNumber.Location = new System.Drawing.Point(520, 170);
            this.txtNumber.MaxLength = 5;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(400, 29);
            this.txtNumber.TabIndex = 8;
            // 
            // txtStreet
            // 
            this.txtStreet.BackColor = System.Drawing.Color.White;
            this.txtStreet.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txtStreet.Location = new System.Drawing.Point(520, 100);
            this.txtStreet.MaxLength = 30;
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(400, 29);
            this.txtStreet.TabIndex = 7;
            // 
            // lblBusinessName
            // 
            this.lblBusinessName.AutoSize = true;
            this.lblBusinessName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBusinessName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBusinessName.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblBusinessName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblBusinessName.Location = new System.Drawing.Point(20, 70);
            this.lblBusinessName.Name = "lblBusinessName";
            this.lblBusinessName.Size = new System.Drawing.Size(104, 21);
            this.lblBusinessName.TabIndex = 39;
            this.lblBusinessName.Text = "Razón social";
            // 
            // lblEmployerRegistration
            // 
            this.lblEmployerRegistration.AutoSize = true;
            this.lblEmployerRegistration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEmployerRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmployerRegistration.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblEmployerRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblEmployerRegistration.Location = new System.Drawing.Point(20, 140);
            this.lblEmployerRegistration.Name = "lblEmployerRegistration";
            this.lblEmployerRegistration.Size = new System.Drawing.Size(141, 21);
            this.lblEmployerRegistration.TabIndex = 40;
            this.lblEmployerRegistration.Text = "Registro patronal";
            // 
            // lblRFC
            // 
            this.lblRFC.AutoSize = true;
            this.lblRFC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRFC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRFC.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblRFC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblRFC.Location = new System.Drawing.Point(20, 210);
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(278, 21);
            this.lblRFC.TabIndex = 41;
            this.lblRFC.Text = "Registro Federal de Contribuyentes";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStartDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblStartDate.Location = new System.Drawing.Point(20, 280);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(245, 21);
            this.lblStartDate.TabIndex = 42;
            this.lblStartDate.Text = "Fecha de inicio de operaciones";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblEmail.Location = new System.Drawing.Point(20, 350);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(151, 21);
            this.lblEmail.TabIndex = 43;
            this.lblEmail.Text = "Correo electrónico";
            // 
            // cbCities
            // 
            this.cbCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCities.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.cbCities.FormattingEnabled = true;
            this.cbCities.Location = new System.Drawing.Point(520, 380);
            this.cbCities.Name = "cbCities";
            this.cbCities.Size = new System.Drawing.Size(400, 29);
            this.cbCities.TabIndex = 10;
            // 
            // cbStates
            // 
            this.cbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStates.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.cbStates.FormattingEnabled = true;
            this.cbStates.Location = new System.Drawing.Point(520, 310);
            this.cbStates.Name = "cbStates";
            this.cbStates.Size = new System.Drawing.Size(400, 29);
            this.cbStates.TabIndex = 11;
            this.cbStates.SelectedIndexChanged += new System.EventHandler(this.cbStates_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label1.Location = new System.Drawing.Point(20, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 21);
            this.label1.TabIndex = 46;
            this.label1.Text = "Teléfonos";
            // 
            // cbPhones
            // 
            this.cbPhones.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.cbPhones.FormattingEnabled = true;
            this.cbPhones.Location = new System.Drawing.Point(20, 450);
            this.cbPhones.Name = "cbPhones";
            this.cbPhones.Size = new System.Drawing.Size(400, 29);
            this.cbPhones.TabIndex = 6;
            this.cbPhones.SelectedIndexChanged += new System.EventHandler(this.cbPhones_SelectedIndexChanged);
            this.cbPhones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPhones_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 530);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(400, 42);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormEmpresas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1064, 670);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbPhones);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbStates);
            this.Controls.Add(this.cbCities);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblRFC);
            this.Controls.Add(this.lblEmployerRegistration);
            this.Controls.Add(this.lblBusinessName);
            this.Controls.Add(this.lblPostalCode);
            this.Controls.Add(this.txtPostalCode);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblSuburb);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblStreet);
            this.Controls.Add(this.txtSuburb);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtStreet);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblCompanies);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtRfc);
            this.Controls.Add(this.txtEmployerRegistration);
            this.Controls.Add(this.txtBusinessName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEmpresas";
            this.Text = "Companies";
            this.Load += new System.EventHandler(this.Companies_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBusinessName;
        private System.Windows.Forms.TextBox txtEmployerRegistration;
        private System.Windows.Forms.TextBox txtRfc;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblCompanies;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblPostalCode;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblSuburb;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.TextBox txtSuburb;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label lblBusinessName;
        private System.Windows.Forms.Label lblEmployerRegistration;
        private System.Windows.Forms.Label lblRFC;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.ComboBox cbCities;
        private System.Windows.Forms.ComboBox cbStates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPhones;
        private System.Windows.Forms.Button btnAdd;
    }
}