
namespace Presentation.Views
{
    partial class HeadcounterReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgHeadcounter1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgHeadcounter2 = new System.Windows.Forms.DataGridView();
            this.cbDepartments = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.depa2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHeadcounter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHeadcounter2)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgHeadcounter1
            // 
            this.dtgHeadcounter1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.dtgHeadcounter1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgHeadcounter1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgHeadcounter1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgHeadcounter1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHeadcounter1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.departamento,
            this.puesto,
            this.count});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgHeadcounter1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtgHeadcounter1.EnableHeadersVisualStyles = false;
            this.dtgHeadcounter1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgHeadcounter1.Location = new System.Drawing.Point(20, 170);
            this.dtgHeadcounter1.Name = "dtgHeadcounter1";
            this.dtgHeadcounter1.ReadOnly = true;
            this.dtgHeadcounter1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgHeadcounter1.Size = new System.Drawing.Size(793, 227);
            this.dtgHeadcounter1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reporte de headcounter";
            // 
            // dtgHeadcounter2
            // 
            this.dtgHeadcounter2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.dtgHeadcounter2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgHeadcounter2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgHeadcounter2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgHeadcounter2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHeadcounter2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.depa2,
            this.cantu});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgHeadcounter2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtgHeadcounter2.EnableHeadersVisualStyles = false;
            this.dtgHeadcounter2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgHeadcounter2.Location = new System.Drawing.Point(20, 420);
            this.dtgHeadcounter2.Name = "dtgHeadcounter2";
            this.dtgHeadcounter2.ReadOnly = true;
            this.dtgHeadcounter2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgHeadcounter2.Size = new System.Drawing.Size(793, 216);
            this.dtgHeadcounter2.TabIndex = 2;
            // 
            // cbDepartments
            // 
            this.cbDepartments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartments.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.cbDepartments.FormattingEnabled = true;
            this.cbDepartments.Location = new System.Drawing.Point(20, 120);
            this.cbDepartments.Name = "cbDepartments";
            this.cbDepartments.Size = new System.Drawing.Size(569, 29);
            this.cbDepartments.TabIndex = 47;
            this.cbDepartments.SelectedIndexChanged += new System.EventHandler(this.cbDepartments_SelectedIndexChanged);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblState.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblState.Location = new System.Drawing.Point(20, 90);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(121, 21);
            this.lblState.TabIndex = 46;
            this.lblState.Text = "Departamento";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MMMM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(613, 120);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowUpDown = true;
            this.dtpDate.Size = new System.Drawing.Size(200, 29);
            this.dtpDate.TabIndex = 49;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label3.Location = new System.Drawing.Point(609, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 21);
            this.label3.TabIndex = 48;
            this.label3.Text = "Fecha";
            // 
            // departamento
            // 
            this.departamento.DataPropertyName = "departamento";
            this.departamento.HeaderText = "Departamento";
            this.departamento.Name = "departamento";
            this.departamento.ReadOnly = true;
            this.departamento.Width = 250;
            // 
            // puesto
            // 
            this.puesto.DataPropertyName = "puesto";
            this.puesto.HeaderText = "Puesto";
            this.puesto.Name = "puesto";
            this.puesto.ReadOnly = true;
            this.puesto.Width = 250;
            // 
            // count
            // 
            this.count.DataPropertyName = "cantidadEmpleados";
            this.count.HeaderText = "Cantidad de empleados";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Width = 250;
            // 
            // depa2
            // 
            this.depa2.DataPropertyName = "departamento";
            this.depa2.HeaderText = "Departamento";
            this.depa2.Name = "depa2";
            this.depa2.ReadOnly = true;
            this.depa2.Width = 400;
            // 
            // cantu
            // 
            this.cantu.DataPropertyName = "cantidadEmpleados";
            this.cantu.HeaderText = "Cantidad de empleados";
            this.cantu.Name = "cantu";
            this.cantu.ReadOnly = true;
            this.cantu.Width = 350;
            // 
            // HeadcounterReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1064, 670);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDepartments);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.dtgHeadcounter2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgHeadcounter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HeadcounterReports";
            this.Text = "HeadcounterReports";
            this.Load += new System.EventHandler(this.HeadcounterReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHeadcounter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHeadcounter2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgHeadcounter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgHeadcounter2;
        private System.Windows.Forms.ComboBox cbDepartments;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn puesto;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn depa2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantu;
    }
}