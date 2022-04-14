using CsvHelper;
using Data_Access.ViewModels;
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
        public FormPayroll()
        {
            InitializeComponent();
        }

        private void Payroll_Load(object sender, EventArgs e)
        {
            /*
            Form modal = new ModalPayroll();
            modal.ShowDialog();
            */
        }

        private void btnGeneratePayroll_Click(object sender, EventArgs e)
        {
            //GenerateCSV();
        }

        private void GenerateCSV()
        {
            ofnPayrollCSV.FileName = Guid.NewGuid().ToString();

            if (ofnPayrollCSV.ShowDialog() == DialogResult.OK)
            {
                var writer = new StreamWriter(ofnPayrollCSV.FileName);
                var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

                List<PayrollViewModel> payrolls = new List<PayrollViewModel>();
                payrolls.Add(new PayrollViewModel { 
                    EmployeeNumber = 1,
                    EmployeeName = "Eduardo",
                    Date = DateTime.Now,
                    Amount = 5.0m,
                    Bank = "Santander",
                    AccountNumber = "11"
                });

                csvWriter.WriteRecords(payrolls);
                csvWriter.Dispose();
                writer.Close();
            }
        }
    }
}
