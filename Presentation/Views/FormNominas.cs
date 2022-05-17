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
    public partial class FormNominas : Form
    {
        RepositorioNominas payrollRepository;

        public FormNominas()
        {
            InitializeComponent();
            payrollRepository = new RepositorioNominas();
        }

        private void Payroll_Load(object sender, EventArgs e)
        {
            InitDates();

            dtgPayrolls.DoubleBuffered(true);
        }

        private void btnGeneratePayroll_Click(object sender, EventArgs e)
        {
            //if (dtpDate.Value != payrollDate)
            //{
            //    MessageBox.Show("No se puede generar la nómina fuera del periodo actual de nómina",
            //   "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            try
            {
                bool payrollStatus = payrollRepository.IsPayrollProcess(Session.companyId);
                if (!payrollStatus)
                {
                    MessageBox.Show("No se puede realizar esta acción, no hay una nómina en proceso",
                        "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    MessageBox.Show(ex.Message, "Sistema de nómina dice:", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            DialogResult res = MessageBox.Show("¿Está seguro que desea realizar esta acción?\nAl cerrarse la nómina, está no podrá volver a ser editada",
               "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            try
            {
                payrollRepository.GeneratePayrolls(dtpDate.Value, Session.companyId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    if (ex.Message.Contains("chk_nomina_sueldo"))
                    {
                        MessageBox.Show("La nómina no puede ser cerrada debido a que un registro es inválido, favor de revisar y realizar correcciones las necesarias",
                            "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo realizar la operación", "Sistema de nómina dice: ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            ListPayrolls(dtpDate.Value);
            InitDates();
            //dtpConsult.Value = dtpDate.Value;
            GenerateCSV();
        }

        private void btnDeletePayroll_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Está seguro que desea realizar esta acción?",
               "Sistema de nómina dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            DateTime requestDate = dtpDate.Value;
            try
            {
                bool result = payrollRepository.DeletePayroll(Session.companyId, requestDate);
                if (result)
                {
                    MessageBox.Show("La nómina fue eliminada éxitosamente", "Sistema de nómina dice:",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la nómina", "Sistema de nómina dice:",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            ListPayrolls(dtpConsult.Value);
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            GenerateCSV();
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

        private void ListPayrolls(DateTime request)
        {
            try
            {
                dtgPayrolls.DataSource = payrollRepository.ReadByDate(request);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (dtgPayrolls.RowCount > 0)
            {
                btnCSV.Enabled = true;
            }
        }

        private void InitDates()
        {
            try
            {
                // Obtiene la fecha de creacion de la empresa como la fecha minima y la nomina mas
                // reciente como la fecha maxima, solo si coinciden la creacion de la empresa con la nomina
                // reciente se omite el restar un mes
                RepositorioEmpresas companiesRepository = new RepositorioEmpresas();
                DateTime creationDate = companiesRepository.GetCreationDate(Session.companyId, true);
                DateTime payrollDate = payrollRepository.GetDate(Session.companyId, true);
                dtpDate.Value = payrollDate;
                dtpConsult.MinDate = creationDate;
                dtpConsult.MaxDate = (creationDate == payrollDate) ? payrollDate : payrollDate.AddMonths(-1);
                dtpConsult.Value = creationDate;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    MessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}