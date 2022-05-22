
namespace Presentation.Views
{
    partial class FormNominas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGeneratePayroll = new System.Windows.Forms.Button();
            this.btnCSV = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgPayrolls = new System.Windows.Forms.DataGridView();
            this.dtpConsult = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.ofnPayrollCSV = new System.Windows.Forms.SaveFileDialog();
            this.btnDeletePayroll = new System.Windows.Forms.Button();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPayrolls)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MMMM / yyyy";
            this.dtpDate.Enabled = false;
            this.dtpDate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(12, 103);
            this.dtpDate.MaxDate = new System.DateTime(2038, 12, 31, 0, 0, 0, 0);
            this.dtpDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowUpDown = true;
            this.dtpDate.Size = new System.Drawing.Size(200, 29);
            this.dtpDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label1.Location = new System.Drawing.Point(9, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha";
            // 
            // btnGeneratePayroll
            // 
            this.btnGeneratePayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnGeneratePayroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeneratePayroll.FlatAppearance.BorderSize = 0;
            this.btnGeneratePayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePayroll.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnGeneratePayroll.ForeColor = System.Drawing.Color.White;
            this.btnGeneratePayroll.Location = new System.Drawing.Point(12, 161);
            this.btnGeneratePayroll.Name = "btnGeneratePayroll";
            this.btnGeneratePayroll.Size = new System.Drawing.Size(175, 54);
            this.btnGeneratePayroll.TabIndex = 9;
            this.btnGeneratePayroll.Text = "Cerrar nómina";
            this.btnGeneratePayroll.UseVisualStyleBackColor = false;
            this.btnGeneratePayroll.Click += new System.EventHandler(this.btnGeneratePayroll_Click);
            // 
            // btnCSV
            // 
            this.btnCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCSV.FlatAppearance.BorderSize = 0;
            this.btnCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCSV.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnCSV.ForeColor = System.Drawing.Color.White;
            this.btnCSV.Image = global::Presentation.Properties.Resources.CSV_Logo;
            this.btnCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCSV.Location = new System.Drawing.Point(828, 251);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(224, 81);
            this.btnCSV.TabIndex = 10;
            this.btnCSV.Text = "       Generar CSV";
            this.btnCSV.UseVisualStyleBackColor = false;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 31);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nómina";
            // 
            // dtgPayrolls
            // 
            this.dtgPayrolls.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.dtgPayrolls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgPayrolls.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgPayrolls.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgPayrolls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgPayrolls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPayrolls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.empName,
            this.date,
            this.amountemp,
            this.bank,
            this.accountNumber});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(169)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgPayrolls.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgPayrolls.EnableHeadersVisualStyles = false;
            this.dtgPayrolls.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgPayrolls.Location = new System.Drawing.Point(12, 349);
            this.dtgPayrolls.Name = "dtgPayrolls";
            this.dtgPayrolls.ReadOnly = true;
            this.dtgPayrolls.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(169)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgPayrolls.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgPayrolls.Size = new System.Drawing.Size(1040, 309);
            this.dtgPayrolls.TabIndex = 12;
            // 
            // dtpConsult
            // 
            this.dtpConsult.CustomFormat = "MMMM / yyyy";
            this.dtpConsult.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.dtpConsult.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpConsult.Location = new System.Drawing.Point(12, 303);
            this.dtpConsult.MaxDate = new System.DateTime(2038, 12, 31, 0, 0, 0, 0);
            this.dtpConsult.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpConsult.Name = "dtpConsult";
            this.dtpConsult.ShowUpDown = true;
            this.dtpConsult.Size = new System.Drawing.Size(200, 29);
            this.dtpConsult.TabIndex = 13;
            this.dtpConsult.ValueChanged += new System.EventHandler(this.dtpConsult_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label3.Location = new System.Drawing.Point(14, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "Fecha";
            // 
            // ofnPayrollCSV
            // 
            this.ofnPayrollCSV.Filter = "CSV (*.csv)|.csv";
            // 
            // btnDeletePayroll
            // 
            this.btnDeletePayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeletePayroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeletePayroll.FlatAppearance.BorderSize = 0;
            this.btnDeletePayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePayroll.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.btnDeletePayroll.ForeColor = System.Drawing.Color.White;
            this.btnDeletePayroll.Location = new System.Drawing.Point(241, 161);
            this.btnDeletePayroll.Name = "btnDeletePayroll";
            this.btnDeletePayroll.Size = new System.Drawing.Size(175, 54);
            this.btnDeletePayroll.TabIndex = 29;
            this.btnDeletePayroll.Text = "Eliminar nómina";
            this.btnDeletePayroll.UseVisualStyleBackColor = false;
            this.btnDeletePayroll.Click += new System.EventHandler(this.btnDeletePayroll_Click);
            // 
            // number
            // 
            this.number.DataPropertyName = "employeeNumber";
            this.number.HeaderText = "Número de empleado";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 150;
            // 
            // empName
            // 
            this.empName.DataPropertyName = "employeeName";
            this.empName.HeaderText = "Nombre del empleado";
            this.empName.Name = "empName";
            this.empName.ReadOnly = true;
            this.empName.Width = 250;
            // 
            // date
            // 
            this.date.DataPropertyName = "date";
            dataGridViewCellStyle2.Format = "MMMM yyyy";
            this.date.DefaultCellStyle = dataGridViewCellStyle2;
            this.date.HeaderText = "Fecha";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 200;
            // 
            // amountemp
            // 
            this.amountemp.DataPropertyName = "amount";
            dataGridViewCellStyle3.Format = "c";
            this.amountemp.DefaultCellStyle = dataGridViewCellStyle3;
            this.amountemp.HeaderText = "Cantidad";
            this.amountemp.Name = "amountemp";
            this.amountemp.ReadOnly = true;
            this.amountemp.Width = 200;
            // 
            // bank
            // 
            this.bank.DataPropertyName = "bank";
            this.bank.HeaderText = "Banco";
            this.bank.Name = "bank";
            this.bank.ReadOnly = true;
            this.bank.Width = 200;
            // 
            // accountNumber
            // 
            this.accountNumber.DataPropertyName = "accountNumber";
            this.accountNumber.HeaderText = "Número de cuenta";
            this.accountNumber.Name = "accountNumber";
            this.accountNumber.ReadOnly = true;
            this.accountNumber.Width = 200;
            // 
            // FormNominas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1064, 670);
            this.Controls.Add(this.btnDeletePayroll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpConsult);
            this.Controls.Add(this.dtgPayrolls);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.btnGeneratePayroll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNominas";
            this.Text = "Payroll";
            this.Load += new System.EventHandler(this.Payroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPayrolls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGeneratePayroll;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtgPayrolls;
        private System.Windows.Forms.DateTimePicker dtpConsult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog ofnPayrollCSV;
        private System.Windows.Forms.Button btnDeletePayroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn bank;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNumber;
    }
}