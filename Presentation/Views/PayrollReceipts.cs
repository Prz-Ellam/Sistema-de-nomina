﻿using Data_Access.Repositorios;
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
        private RepositorioNominas repository;

        public PayrollReceipts()
        {
            InitializeComponent();
            repository = new RepositorioNominas();
        }

        private void PayrollReceipts_Load(object sender, EventArgs e)
        {
            RepositorioEmpresas companyRepository = new RepositorioEmpresas();
            DateTime creationDate = companyRepository.GetCreationDate(Session.company_id, true);
            DateTime payrollDate = repository.GetDate(Session.company_id);
            dtpDate.Value = creationDate;
            dtpDate.MinDate = creationDate;
            dtpDate.MaxDate = payrollDate.AddMonths(-1);
        }
        private void btnPDF_Click(object sender, EventArgs e)
        {
            ofnPayroll.FileName = $"{dtpDate.Value.Year}-{dtpDate.Value.Month} {DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}";

            if (ofnPayroll.ShowDialog() == DialogResult.OK)
            {
                var report = repository.GetPayrollReceipt(Session.id, dtpDate.Value);

                if (report == null)
                {
                    MessageBox.Show("No se ha generado el recibo de nómina de este periodo",
                        "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var applyPerceptions = new RepositorioPercepcionesAplicadas();
                var applyDeductions = new RepositorioDeduccionesAplicadas();

                var per = applyPerceptions.ReadPayrollPerceptions(report.IdNomina);
                var ded = applyDeductions.ReadPayrollDeductions(report.IdNomina);

                bool result = PDFReceipt.GeneratePDFReceipt(report, per, ded, ofnPayroll.FileName);

                if (result)
                {
                    System.Diagnostics.Process.Start(ofnPayroll.FileName);
                }

            }
        }

        private void GenerateReceipt()
        {
            var report = repository.GetPayrollReceipt(Session.id, dtpDate.Value);

            if (report == null)
            {
                MessageBox.Show("No se ha generado el recibo de nómina de este periodo",
                    "Sistema de nómina dice: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
