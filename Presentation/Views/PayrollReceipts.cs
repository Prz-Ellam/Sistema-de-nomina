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

namespace Presentation.Views
{
    public partial class PayrollReceipts : Form
    {
        private RepositorioNominas repository = new RepositorioNominas();

        public PayrollReceipts()
        {
            InitializeComponent();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ofnPayroll.FileName = string.Format("{0}-{1} {2}", dtpDate.Value.Year, dtpDate.Value.Month, DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"));

            if (ofnPayroll.ShowDialog() == DialogResult.OK)
            {
                var report = repository.GetPayrollReceipt(Session.id, dtpDate.Value);

                var per = new RepositorioPercepcionesAplicadas().ReadPayrollPerceptions(report.IdNomina);
                var ded = new RepositorioDeduccionesAplicadas().ReadPayrollDeductions(report.IdNomina);

                bool result = PDFReceipt.GeneratePDFReceipt(report, per, ded, ofnPayroll.FileName);

                if (result)
                {
                    System.Diagnostics.Process.Start(ofnPayroll.FileName);
                }

            }
        }

        private void PayrollReceipts_Load(object sender, EventArgs e)
        {

        }

        private void GenerateReceipt()
        {
            var report = repository.GetPayrollReceipt(Session.id, dtpDate.Value);

            if (report == null)
            {
                return;
            }

            var applyPerceptions = new RepositorioPercepcionesAplicadas();
            var applyDeductions = new RepositorioDeduccionesAplicadas();

            var per = applyPerceptions.ReadPayrollPerceptions(report.IdNomina);
            var ded = applyDeductions.ReadPayrollDeductions(report.IdNomina);

            var stream = PDFReceipt.ReadPDFReceipt(report, per, ded);
            PdfDocument document = PdfDocument.Load(stream);
            pdfViewer.Document = document;
            
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            GenerateReceipt();
        }
    }
}
