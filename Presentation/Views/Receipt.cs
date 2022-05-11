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
    public partial class Receipt : Form
    {
        public Receipt()
        {
            InitializeComponent();
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //string path = @"C:\1\C# Threading Handbook.pdf";
            //System.Diagnostics.Process.Start("IExplore.exe", path);
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            //string path = @"C:\FE-MAD-01-JAVM-EJ2022.pdf";
            // System.Diagnostics.Process.Start("IExplore.exe", path);
            // browser.Navigate( path);
            //browser.DocumentText = "<html>hello world<h1>H</h1></html>";
            pdfViewer.src = @"C:\Users\Perez\Desktop\Sexto Semestre\2. Modelos de Administracion de Datos\FE-MAD-01-JAVM-EJ2022.pdf";
        }
    }
}
