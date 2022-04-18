using CsvHelper;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Views
{
    public partial class FormPayroll : Form
    {
        RepositorioNominas payrollRepository = new RepositorioNominas();
        DateTime payrollDate;
        public FormPayroll()
        {
            InitializeComponent();
        }

        private void Payroll_Load(object sender, EventArgs e)
        {
            payrollDate = payrollRepository.GetDate(Session.company_id);

            dtpDate.Value = payrollDate;
            dtpDate.MinDate = payrollDate;

            /*
            Form modal = new ModalPayroll();
            modal.ShowDialog();
            */
        }

        private void btnGeneratePayroll_Click(object sender, EventArgs e)
        {
            DateTime date = dtpDate.Value;

            RepositorioEmpleados employeeRepository = new RepositorioEmpleados();
            List<int> employeesNumber = employeeRepository.GetEmployeesId();
            
            foreach(int employeeNumber in employeesNumber)
            {
                payrollRepository.GeneratePayrolls(date, employeeNumber);
            }
            

            //GenerateCSV();
        }

        private void GenerateCSV()
        {
            ofnPayrollCSV.FileName = Guid.NewGuid().ToString();

            if (ofnPayrollCSV.ShowDialog() == DialogResult.OK)
            {
                var writer = new StreamWriter(ofnPayrollCSV.FileName);
                var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

                List<PayrollViewModel> payrolls = dtgPayrolls.DataSource as List<PayrollViewModel>;
                if (payrolls == null)
                {
                    MessageBox.Show("Error inesperado", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                csvWriter.WriteRecords(payrolls);
                csvWriter.Dispose();
                writer.Close();
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                dtgPayrolls.DataSource = payrollRepository.ReadByDate(dtpConsult.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnCSV.Enabled = true;
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
    }
}
