using CsvHelper;
using CustomMessageBox;
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
            DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?\nAl cerrarse la nómina, está no podrá volver a ser editada",
               "Sistema de nómina dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            DateTime requestDate = dtpDate.Value;
            try
            {
                bool result = payrollRepository.GeneratePayrolls(requestDate, Session.companyId);
                if (result)
                {
                    RJMessageBox.Show("La nómina se ha generado éxitosamente", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    RJMessageBox.Show("No se pudo generar la nómina", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    if (ex.Message.Contains("chk_nomina_sueldo"))
                    {
                        RJMessageBox.Show("La nómina no puede ser cerrada debido a que un registro es inválido, favor de revisar y realizar correcciones las necesarias",
                            "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    RJMessageBox.Show("No se pudo realizar la operación", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            InitDates();
            dtpConsult.Value = requestDate;
            GenerateCSV();
        }

        private void btnDeletePayroll_Click(object sender, EventArgs e)
        {
            DialogResult res = RJMessageBox.Show("¿Está seguro que desea realizar esta acción?",
               "Sistema de nómina dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.No)
            {
                return;
            }

            try
            {
                bool result = payrollRepository.DeletePayroll(Session.companyId, dtpDate.Value);
                if (result)
                {
                    RJMessageBox.Show("La nómina fue eliminada éxitosamente", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    RJMessageBox.Show("No se pudo eliminar la nómina", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    RJMessageBox.Show("No se pudo realizar la operación", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }

        private void dtpConsult_ValueChanged(object sender, EventArgs e)
        {
            ListPayrolls();
        }

        private void GenerateCSV()
        {
            ofnPayrollCSV.FileName = Guid.NewGuid().ToString();

            if (ofnPayrollCSV.ShowDialog() == DialogResult.OK)
            {
                var writer = new StreamWriter(ofnPayrollCSV.FileName);
                var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

                IEnumerable<PayrollViewModel> payrolls = dtgPayrolls.DataSource as IEnumerable<PayrollViewModel>;
                if (payrolls == null)
                {
                    RJMessageBox.Show("No se pudo generar el reporte", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                csvWriter.WriteRecords(payrolls);
                csvWriter.Dispose();
                writer.Close();

                RJMessageBox.Show("El reporte se generó éxitosamente", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(ofnPayrollCSV.FileName);
            }
        }

        private void ListPayrolls()
        {
            try
            {
                dtgPayrolls.DataSource = payrollRepository.ReadByDate(dtpConsult.Value);
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitDates()
        {
            try
            {
                // Obtiene la fecha de creacion de la empresa como la fecha minima y la nomina mas
                // reciente como la fecha maxima, solo si coinciden la creacion de la empresa con la nomina
                // reciente se omite el restar un mes
                CompaniesRepository companiesRepository = new CompaniesRepository();
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
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListPayrolls();
                }
            }
        }
    }
}