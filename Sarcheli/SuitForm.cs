using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sarcheli
{
    public partial class SuitForm : Form
    {
        public SuitForm()
        {
            InitializeComponent();
        }

        private void SuitForm_Load(object sender, EventArgs e)
        {
            try {  
                  ReportViewer _reportViewer1 = new ReportViewer();
                        _reportViewer1.ProcessingMode = ProcessingMode.Local;
                string deviceInfo =
                                   "<DeviceInfo>" +
                                   "  <OutputFormat>EMF</OutputFormat>" +
                                   "  <PageWidth>8.27in</PageWidth>" +
                                   "  <PageHeight>11.69in</PageHeight>" +
                                   "  <MarginTop>0in</MarginTop>" +
                                   "  <MarginLeft>0in</MarginLeft>" +
                                   "  <MarginRight>0in</MarginRight>" +
                                   "  <MarginBottom>0in</MarginBottom>" +
                                   "</DeviceInfo>";
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytesa;
                    try
                    {


                        _reportViewer1.LocalReport.ReportPath = @"C:\Users\admin\Desktop\LevanProjects\CreatePdf\Sarcheli\Sarcheli.rdlc";
                                                                 // C:\Users\admin\Desktop\LevanProjects\CreatePdf\Sarcheli

                        _reportViewer1.LocalReport.Refresh();
                        bytesa = _reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                    }

                    catch (Exception ex)
                    {
                        throw new Exception(ex.InnerException.ToString());
                    }

                   // using (FileStream fs = new FileStream(@"C:\Users\PAB\Desktop\Levan\sarcheli\PDF\" + Suits[i].ToString() + "_" + DateTime.Now.ToString("dd.MM.yyyy") + ".PDF", FileMode.Create))
                    //using (FileStream fs = new FileStream(@"C:\Users\admin\Desktop\hotels\" + Suits[i].ToString() + "_" + DateTime.Now.ToString("dd.MM.yyyy") + ".PDF", FileMode.Create))

                    {
                        //fs.Write(bytesa, 0, bytesa.Length);
                        //fs.Close();
                        //fs.Dispose();
                        bytesa = null;
                        _reportViewer1.Refresh();
                        _reportViewer1.Clear();
                       
                    }
                }

                
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
