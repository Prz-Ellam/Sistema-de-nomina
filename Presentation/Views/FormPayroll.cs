using CsvHelper;
using Data_Access.Repositorios;
using Data_Access.ViewModels;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            dtpConsult.Value = payrollDate;

            dtgPayrolls.DoubleBuffered(true);
        }

        private void btnGeneratePayroll_Click(object sender, EventArgs e)
        {
            if (dtpDate.Value.Month != payrollDate.Month || dtpDate.Value.Year != payrollDate.Year)
            {
                MessageBox.Show("No se puede generar la nómina fuera del periodo actual de nómina",
               "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            bool payrollStatus = payrollRepository.IsPayrollProcess(Session.company_id);

            if (!payrollStatus)
            {
                MessageBox.Show("No se puede realizar esta acción, no hay una nómina en proceso", 
                    "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DialogResult res = MessageBox.Show("¿Está seguro que desea realizar esta acción?\nAl cerrarse la nómina, está no podrá volver a ser editada",
               "Sistema de nómina dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }


            DateTime date = dtpDate.Value;

            //RepositorioEmpleados employeeRepository = new RepositorioEmpleados();
            //List<int> employeesNumber = employeeRepository.GetEmployeesId();

            //if (employeesNumber.Count < 1)
            //{
            //    MessageBox.Show("No hay empleados actualmente");
            //}

            try
            {

                //foreach (int employeeNumber in employeesNumber)
                //{
                    payrollRepository.GeneratePayrolls(date, Session.company_id);
                //}

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
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
