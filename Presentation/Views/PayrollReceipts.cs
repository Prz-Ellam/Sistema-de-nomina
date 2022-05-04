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

                bool result = PDFReceipt.GeneratePDFReceipt(report, per, ded,  ofnPayroll.FileName);

            }
        }
    }
}
