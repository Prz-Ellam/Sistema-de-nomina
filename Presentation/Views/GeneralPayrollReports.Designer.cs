
namespace Presentation.Views
{
    partial class GeneralPayrollReports
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
            this.dtgGeneralPayroll = new System.Windows.Forms.DataGridView();
            this.lblGeneralPayrollReport = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnCSV = new System.Windows.Forms.Button();
            this.ofnPayrollCSV = new System.Windows.Forms.SaveFileDialog();
            this.departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGeneralPayroll)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgGeneralPayroll
            // 
            this.dtgGeneralPayroll.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.dtgGeneralPayroll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgGeneralPayroll.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgGeneralPayroll.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgGeneralPayroll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgGeneralPayroll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgGeneralPayroll.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.departamento,
            this.puesto,
            this.nombre,
            this.date,
            this.edad,
            this.salario});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(169)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgGeneralPayroll.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgGeneralPayroll.EnableHeadersVisualStyles = false;
            this.dtgGeneralPayroll.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgGeneralPayroll.Location = new System.Drawing.Point(20, 170);
            this.dtgGeneralPayroll.Name = "dtgGeneralPayroll";
            this.dtgGeneralPayroll.ReadOnly = true;
            this.dtgGeneralPayroll.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(169)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgGeneralPayroll.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgGeneralPayroll.Size = new System.Drawing.Size(1017, 377);
            this.dtgGeneralPayroll.TabIndex = 0;
            // 
            // lblGeneralPayrollReport
            // 
            this.lblGeneralPayrollReport.AutoSize = true;
            this.lblGeneralPayrollReport.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneralPayrollReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblGeneralPayrollReport.Location = new System.Drawing.Point(20, 20);
            this.lblGeneralPayrollReport.Name = "lblGeneralPayrollReport";
            this.lblGeneralPayrollReport.Size = new System.Drawing.Size(346, 31);
            this.lblGeneralPayrollReport.TabIndex = 1;
            this.lblGeneralPayrollReport.Text = "Reporte general de nómina";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDate.Location = new System.Drawing.Point(20, 90);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 21);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "Fecha";
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.dtpDate.CustomFormat = "MMMM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(20, 120);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowUpDown = true;
            this.dtpDate.Size = new System.Drawing.Size(200, 29);
            this.dtpDate.TabIndex = 6;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
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
            this.btnCSV.Location = new System.Drawing.Point(813, 565);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(224, 81);
            this.btnCSV.TabIndex = 11;
            this.btnCSV.Text = "       Generar CSV";
            this.btnCSV.UseVisualStyleBackColor = false;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // ofnPayrollCSV
            // 
            this.ofnPayrollCSV.Filter = "CSV (*.csv)|.csv";
            // 
            // departamento
            // 
            this.departamento.DataPropertyName = "department";
            this.departamento.HeaderText = "Departamento";
            this.departamento.Name = "departamento";
            this.departamento.ReadOnly = true;
            this.departamento.Width = 150;
            // 
            // puesto
            // 
            this.puesto.DataPropertyName = "position";
            this.puesto.HeaderText = "Puesto";
            this.puesto.Name = "puesto";
            this.puesto.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "employeeName";
            this.nombre.HeaderText = "Nombre del empleado";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 225;
            // 
            // date
            // 
            this.date.DataPropertyName = "startDate";
            this.date.HeaderText = "Fecha de ingreso";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 200;
            // 
            // edad
            // 
            this.edad.DataPropertyName = "age";
            this.edad.HeaderText = "Edad";
            this.edad.Name = "edad";
            this.edad.ReadOnly = true;
            // 
            // salario
            // 
            this.salario.DataPropertyName = "baseSalary";
            dataGridViewCellStyle2.Format = "c";
            this.salario.DefaultCellStyle = dataGridViewCellStyle2;
            this.salario.HeaderText = "Salario diario";
            this.salario.Name = "salario";
            this.salario.ReadOnly = true;
            this.salario.Width = 200;
            // 
            // GeneralPayrollReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1064, 670);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblGeneralPayrollReport);
            this.Controls.Add(this.dtgGeneralPayroll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GeneralPayrollReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneralPayrollReports";
            this.Load += new System.EventHandler(this.GeneralPayrollReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgGeneralPayroll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgGeneralPayroll;
        private System.Windows.Forms.Label lblGeneralPayrollReport;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.SaveFileDialog ofnPayrollCSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn puesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn edad;
        private System.Windows.Forms.DataGridViewTextBoxColumn salario;
    }
}