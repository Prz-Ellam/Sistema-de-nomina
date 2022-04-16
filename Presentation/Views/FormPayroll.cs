using CsvHelper;
using Data_Access.Repositorios;
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
        RepositorioNominas payrollRepository = new RepositorioNominas();
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

                List<PayrollViewModel> payrolls = new List<PayrollViewModel>();
                payrolls.Add(new PayrollViewModel { 
                    NumeroEmpleado = 1,
                    NombreEmpleado = "Eduardo",
                    Fecha = DateTime.Now,
                    Cantidad = 5.0m,
                    Banco = "Santander",
                    NumeroCuenta = "11"
                });

                csvWriter.WriteRecords(payrolls);
                csvWriter.Dispose();
                writer.Close();
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            List<PayrollViewModel> payrolls = payrollRepository.ReadByDate(dtpConsult.Value);
            dtgPayrolls.DataSource = payrolls;
        }
    }
}
