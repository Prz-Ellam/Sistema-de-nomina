using Data_Access.Repositorios;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;
using Data_Access.Interfaces;
using CustomMessageBox;
using System.Data.SqlClient;

namespace Presentation.Views
{
    public partial class PayrollReceipts : Form
    {
        private PayrollsRepository repository;

        public PayrollReceipts()
        {
            InitializeComponent();
            repository = new PayrollsRepository();
        }

        private void PayrollReceipts_Load(object sender, EventArgs e)
        {
            try
            {
                IEmployeesRepository employeeRepository = new EmployeesRepository();
                DateTime hiringDate = employeeRepository.GetHiringDate(Session.id, true);
                DateTime payrollDate = repository.GetDate(Session.companyId, true);
                dtpDate.MinDate = hiringDate;
                dtpDate.MaxDate = (hiringDate == payrollDate) ? payrollDate : payrollDate.AddMonths(-1);
                dtpDate.Value = hiringDate;
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                var report = repository.GetPayrollReceipt(Session.id, dtpDate.Value);
                if (report == null)
                {
                    RJMessageBox.Show("No se ha generado el recibo de nómina de este periodo",
                        "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var applyPerceptions = new ApplyPerceptionsRepository();
                var applyDeductions = new ApplyDeductionsRepository();

                var perceptions = applyPerceptions.ReadPayrollPerceptions(report.IdNomina);
                var deductions = applyDeductions.ReadPayrollDeductions(report.IdNomina);

                var stream = PDFReceipt.ReadPDFReceipt(report, perceptions, deductions);
                PdfDocument document = PdfDocument.Load(stream);
                pdfViewer.Document = document;
            }
            catch (SqlException ex)
            {
                RJMessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ofnPayroll.FileName = $"{dtpDate.Value.Year}-{dtpDate.Value.Month} {DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}";

            if (ofnPayroll.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var report = repository.GetPayrollReceipt(Session.id, dtpDate.Value);
                    if (report == null)
                    {
                        RJMessageBox.Show("No se ha generado el recibo de nómina de este periodo",
                            "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var applyPerceptions = new ApplyPerceptionsRepository();
                    var applyDeductions = new ApplyDeductionsRepository();

                    var perceptions = applyPerceptions.ReadPayrollPerceptions(report.IdNomina);
                    var deductions = applyDeductions.ReadPayrollDeductions(report.IdNomina);

                    bool result = PDFReceipt.GeneratePDFReceipt(report, perceptions, deductions, ofnPayroll.FileName);

                    if (result)
                    {
                        RJMessageBox.Show("El recibo se generó éxitosamente", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Process.Start(ofnPayroll.FileName);
                    }
                    else
                    {
                        RJMessageBox.Show("No se pudo generar el recibo", "Sistema de nómina dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    RJMessageBox.Show(ex.Message, "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}