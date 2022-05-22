using Data_Access.ViewModels;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class PDFReceipt
    {
        public static bool GeneratePDFReceipt(PayrollReceiptViewModel receipt, 
            List<PayrollPerceptionViewModel> perceptions,
            List<PayrollDeductionViewModel> deductions,
            string path)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Razon social
            gfx.DrawString(receipt.NombreEmpresa, new XFont("Arial", 20), XBrushes.Black,
                new XRect(20, 20, page.Width, page.Height), XStringFormats.TopLeft);

            // RFC
            gfx.DrawString("Reg. Fed. Cont. " + receipt.RfcEmpresa, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 60, page.Width, page.Height), XStringFormats.TopLeft);

            // Registro Patronal
            gfx.DrawString("Reg. Patronal " + receipt.RegistroPatronal, new XFont("Arial", 10), XBrushes.Black,
                new XRect(0, 60, page.Width, page.Height), XStringFormats.TopCenter);

            // Fecha de alta a la empresa
            gfx.DrawString("Fecha Alta " + receipt.FechaIngreso.ToString("dd/MM/yyyy"), new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 60, page.Width, page.Height), XStringFormats.TopRight);

            // Domicilio
            gfx.DrawString(receipt.DomicilioFiscalParte1, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 80, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString(receipt.DomicilioFiscalParte2, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 100, page.Width, page.Height), XStringFormats.TopLeft);

            DateTime date = new DateTime(receipt.Periodo.Year, receipt.Periodo.Month, 1).AddMonths(1);

            gfx.DrawString("Fecha de pago " + date.ToString(), new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 100, page.Width, page.Height), XStringFormats.TopRight);


            // Nombre del empleado 
            string employee = string.Format("No. Empleado {0}  {1}",
                receipt.NumeroEmpleado.ToString(), receipt.NombreEmpleado);

            gfx.DrawString(employee, new XFont("Arial", 11, XFontStyle.Bold), XBrushes.Black,
                new XRect(20, 140, page.Width, page.Height), XStringFormats.TopLeft);

            // Periodo
            string period = "Periodo " + receipt.Periodo.ToString("dd/MM/yyyy") + " - " + receipt.FinalPeriod.ToString("dd/MM/yyyy");
            gfx.DrawString(period, new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 140, page.Width, page.Height), XStringFormats.TopRight);

            // RFC del empleado
            gfx.DrawString("RFC " + receipt.RfcEmpleado, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 160, page.Width, page.Height), XStringFormats.TopLeft);

            // Numero del seguro social
            gfx.DrawString("NSS " + receipt.NssEmpleado, new XFont("Arial", 10), XBrushes.Black,
                new XRect(0, 160, page.Width, page.Height), XStringFormats.TopCenter);

            // CURP
            gfx.DrawString("CURP " + receipt.CurpEmpleado, new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 160, page.Width, page.Height), XStringFormats.TopRight);

            // Departamento
            gfx.DrawString("Departamento " + receipt.Departamento, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 180, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Dias pagados: " + Convert.ToInt32(receipt.SueldoBruto / receipt.SueldoDiario), new XFont("Arial", 10), XBrushes.Black,
                new XRect(0, 180, page.Width, page.Height), XStringFormats.TopCenter);

            // Puesto
            gfx.DrawString("Puesto " + receipt.Puesto, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 200, page.Width, page.Height), XStringFormats.TopLeft);



            gfx.DrawString("Percepciones", new XFont("Arial", 12), XBrushes.Black,
               new XRect(0, 240, page.Width / 2, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Clave", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 0.2, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Concepto", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 0.6, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Importe", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 2.4, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            int y1 = 280;
            decimal total = 0.0m;
            foreach (var perception in perceptions)
            {
                gfx.DrawString(perception.IdPercepcion.ToString(), new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 0.2, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(perception.Concepto, new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 0.6, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(perception.Importe.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 2.4, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);

                y1 += 20;
                total += perception.Importe;
            }

            gfx.DrawString(total.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 2.4, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);



            gfx.DrawString("Deducciones", new XFont("Arial", 12), XBrushes.Black,
              new XRect(page.Width / 2, 240, page.Width / 2, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Clave", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 3.2, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Concepto", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 3.6, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Importe", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 5.4, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);


            int y2 = 280;
            total = 0.0m;
            foreach (var deduction in deductions)
            {
                gfx.DrawString(deduction.IdDeduccion.ToString(), new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 3.2, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(deduction.Concepto, new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 3.6, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(deduction.Importe.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 5.4, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);

                y2 += 20;
                total += deduction.Importe;
            }

            gfx.DrawString(total.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 5.4, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);


            int y = Math.Max(y1, y2) + 60;

            gfx.DrawString("Sueldo diario " + receipt.SueldoDiario.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, y, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Sueldo bruto " + receipt.SueldoBruto.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, y + 20, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Total " + receipt.SueldoNeto.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, y + 40, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Total con letra " + NumericUtils.GetNumberString(receipt.SueldoNeto) +
                $" PESOS ({Convert.ToInt32((receipt.SueldoNeto - Math.Truncate(receipt.SueldoNeto)) * 100).ToString("00")}/100) M.N.",
                new XFont("Arial", 10), XBrushes.Black, new XRect(20, y + 60, page.Width, page.Height),
                XStringFormats.TopLeft);

            document.Save(path);
            return true;
        }

        public static MemoryStream ReadPDFReceipt(PayrollReceiptViewModel receipt,
            List<PayrollPerceptionViewModel> perceptions,
            List<PayrollDeductionViewModel> deductions)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Razon social
            gfx.DrawString(receipt.NombreEmpresa, new XFont("Arial", 20), XBrushes.Black,
                new XRect(20, 20, page.Width, page.Height), XStringFormats.TopLeft);

            // RFC
            gfx.DrawString("Reg. Fed. Cont. " + receipt.RfcEmpresa, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 60, page.Width, page.Height), XStringFormats.TopLeft);

            // Registro Patronal
            gfx.DrawString("Reg. Patronal " + receipt.RegistroPatronal, new XFont("Arial", 10), XBrushes.Black,
                new XRect(0, 60, page.Width, page.Height), XStringFormats.TopCenter);

            // Fecha de alta a la empresa
            gfx.DrawString("Fecha Alta " + receipt.FechaIngreso.ToString("dd/MM/yyyy"), new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 60, page.Width, page.Height), XStringFormats.TopRight);

            // Domicilio
            gfx.DrawString(receipt.DomicilioFiscalParte1, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 80, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString(receipt.DomicilioFiscalParte2, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 100, page.Width, page.Height), XStringFormats.TopLeft);

            DateTime date = new DateTime(receipt.Periodo.Year, receipt.Periodo.Month, 1).AddMonths(1);

            gfx.DrawString("Fecha de pago " + date.ToString(), new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 100, page.Width, page.Height), XStringFormats.TopRight);


            // Nombre del empleado 
            string employee = string.Format("No. Empleado {0}  {1}",
                receipt.NumeroEmpleado.ToString(), receipt.NombreEmpleado);

            gfx.DrawString(employee, new XFont("Arial", 11, XFontStyle.Bold), XBrushes.Black,
                new XRect(20, 140, page.Width, page.Height), XStringFormats.TopLeft);
            
            // Periodo
            string period = "Periodo " + receipt.Periodo.ToString("dd/MM/yyyy") + " - " + receipt.FinalPeriod.ToString("dd/MM/yyyy");
            gfx.DrawString(period, new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 140, page.Width, page.Height), XStringFormats.TopRight);

            // RFC del empleado
            gfx.DrawString("RFC " + receipt.RfcEmpleado, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 160, page.Width, page.Height), XStringFormats.TopLeft);
            
            // Numero del seguro social
            gfx.DrawString("NSS " + receipt.NssEmpleado, new XFont("Arial", 10), XBrushes.Black,
                new XRect(0, 160, page.Width, page.Height), XStringFormats.TopCenter);
             
            // CURP
            gfx.DrawString("CURP " + receipt.CurpEmpleado, new XFont("Arial", 10), XBrushes.Black,
                new XRect(-20, 160, page.Width, page.Height), XStringFormats.TopRight);

            // Departamento
            gfx.DrawString("Departamento " + receipt.Departamento, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 180, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Dias pagados: " + Convert.ToInt32(receipt.SueldoBruto / receipt.SueldoDiario), new XFont("Arial", 10), XBrushes.Black,
                new XRect(0, 180, page.Width, page.Height), XStringFormats.TopCenter);

            // Puesto
            gfx.DrawString("Puesto " + receipt.Puesto, new XFont("Arial", 10), XBrushes.Black,
                new XRect(20, 200, page.Width, page.Height), XStringFormats.TopLeft);



            gfx.DrawString("Percepciones", new XFont("Arial", 12), XBrushes.Black,
               new XRect(0, 240, page.Width / 2, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Clave", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 0.2, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Concepto", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 0.6, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Importe", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 2.4, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            int y1 = 280;
            decimal total = 0.0m;
            foreach (var perception in perceptions)
            {
                gfx.DrawString(perception.IdPercepcion.ToString(), new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 0.2, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(perception.Concepto, new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 0.6, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(perception.Importe.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 2.4, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);

                y1 += 20;
                total += perception.Importe;
            }

            gfx.DrawString(total.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 2.4, y1, page.Width / 6, page.Height), XStringFormats.TopLeft);



            gfx.DrawString("Deducciones", new XFont("Arial", 12), XBrushes.Black,
              new XRect(page.Width / 2, 240, page.Width / 2, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Clave", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 3.2, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Concepto", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 3.6, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Importe", new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 5.4, 260, page.Width / 6, page.Height), XStringFormats.TopLeft);


            int y2 = 280;
            total = 0.0m;
            foreach (var deduction in deductions)
            {
                gfx.DrawString(deduction.IdDeduccion.ToString(), new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 3.2, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(deduction.Concepto, new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 3.6, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);

                gfx.DrawString(deduction.Importe.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
                   new XRect(page.Width / 6 * 5.4, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);

                y2 += 20;
                total += deduction.Importe;
            }

            gfx.DrawString(total.ToString("c"), new XFont("Arial", 10), XBrushes.Black,
               new XRect(page.Width / 6 * 5.4, y2, page.Width / 6, page.Height), XStringFormats.TopLeft);


            int y = Math.Max(y1, y2) + 60;

            gfx.DrawString("Sueldo diario " + receipt.SueldoDiario.ToString("c"), new XFont("Arial", 10), XBrushes.Black, 
                new XRect(20, y, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Sueldo bruto " + receipt.SueldoBruto.ToString("c"), new XFont("Arial", 10), XBrushes.Black, 
                new XRect(20, y + 20, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Total " + receipt.SueldoNeto.ToString("c"), new XFont("Arial", 10), XBrushes.Black, 
                new XRect(20, y + 40, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Total con letra " + NumericUtils.GetNumberString(receipt.SueldoNeto) +
                $" PESOS ({Convert.ToInt32((receipt.SueldoNeto - Math.Truncate(receipt.SueldoNeto)) * 100).ToString("00")}/100) M.N.",
                new XFont("Arial", 10), XBrushes.Black, new XRect(20, y + 60, page.Width, page.Height),
                XStringFormats.TopLeft);

            var ms = new MemoryStream();
            document.Save(ms);
            return ms;
            
        }
    }
}
