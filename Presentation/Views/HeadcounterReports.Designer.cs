
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgHeadcounter1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgHeadcounter1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHeadcounter1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.departamento,
            this.puesto,
            this.count});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgHeadcounter1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgHeadcounter1.EnableHeadersVisualStyles = false;
            this.dtgHeadcounter1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgHeadcounter1.Location = new System.Drawing.Point(40, 145);
            this.dtgHeadcounter1.Name = "dtgHeadcounter1";
            this.dtgHeadcounter1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgHeadcounter1.Size = new System.Drawing.Size(644, 227);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgHeadcounter2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgHeadcounter2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHeadcounter2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.depa2,
            this.cantu});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgHeadcounter2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgHeadcounter2.EnableHeadersVisualStyles = false;
            this.dtgHeadcounter2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dtgHeadcounter2.Location = new System.Drawing.Point(40, 420);
            this.dtgHeadcounter2.Name = "dtgHeadcounter2";
            this.dtgHeadcounter2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgHeadcounter2.Size = new System.Drawing.Size(545, 216);
            this.dtgHeadcounter2.TabIndex = 2;
            // 
            // cbDepartments
            // 
            this.cbDepartments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepartments.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.cbDepartments.FormattingEnabled = true;
            this.cbDepartments.Location = new System.Drawing.Point(40, 100);
            this.cbDepartments.Name = "cbDepartments";
            this.cbDepartments.Size = new System.Drawing.Size(400, 29);
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
            this.lblState.Location = new System.Drawing.Point(41, 76);
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
            this.dtpDate.Location = new System.Drawing.Point(473, 97);
            this.dtpDate.Name = "dtpDate";
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
            this.label3.Location = new System.Drawing.Point(469, 73);
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
            this.departamento.Width = 200;
            // 
            // puesto
            // 
            this.puesto.DataPropertyName = "puesto";
            this.puesto.HeaderText = "Puesto";
            this.puesto.Name = "puesto";
            this.puesto.Width = 200;
            // 
            // count
            // 
            this.count.DataPropertyName = "cantidadEmpleados";
            this.count.HeaderText = "Cantidad de empleados";
            this.count.Name = "count";
            this.count.Width = 200;
            // 
            // depa2
            // 
            this.depa2.DataPropertyName = "departamento";
            this.depa2.HeaderText = "Departamento";
            this.depa2.Name = "depa2";
            this.depa2.Width = 300;
            // 
            // cantu
            // 
            this.cantu.DataPropertyName = "cantidadEmpleados";
            this.cantu.HeaderText = "Cantidad de empleados";
            this.cantu.Name = "cantu";
            this.cantu.Width = 200;
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