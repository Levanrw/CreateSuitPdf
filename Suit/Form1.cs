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

namespace Suit
{
    public partial class SuitForm : Form
    {
        public SuitForm()
        {
            InitializeComponent();
        }

        private void SuitForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet1 ds = new DataSet1();

                List<int> Suits = new List<int>();

               


                DataSet1TableAdapters.getSimplifiedProccesDataTableAdapter GetSuitInfoAll = new DataSet1TableAdapters.getSimplifiedProccesDataTableAdapter();
                var SuitIds = GetSuitInfoAll.GetData(null);

                foreach (var item in SuitIds)
                {
                    Suits.Add(item.ID);
                }
                for (int i = 0; i < Suits.Count; i++)
                {
                    ReportDataSource GetSuitInfo1 = new ReportDataSource();
                    DataSet1TableAdapters.getSimplifiedProccesDataTableAdapter GetSuitInfo = new DataSet1TableAdapters.getSimplifiedProccesDataTableAdapter();
                    GetSuitInfo1.Name = "getSimplifiedProccesData";
                    GetSuitInfo1.Value = GetSuitInfo.GetData(Suits[i]);

                     
                     ReportViewer _reportViewer1 = new ReportViewer();
                    _reportViewer1.ProcessingMode = ProcessingMode.Local;
                    var DesktopPath =  Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string _Path = Directory.GetCurrentDirectory();

                   // _Path = _Path.Replace(@"bin\Debug", "Suit.rdlc");
                    //chemi
                   // _reportViewer1.LocalReport.ReportPath =  @"C:\Users\admin\Desktop\LevanProjects\CreatePdf\Suit\Suit.rdlc";
                    //gio   C:\Users\PAB\Desktop\suit
                    _reportViewer1.LocalReport.ReportPath = @"C:\Users\PAB\Desktop\suit\Suit.rdlc";

                    _reportViewer1.RefreshReport();
                    _reportViewer1.Clear();

                    _reportViewer1.LocalReport.DataSources.Add(GetSuitInfo1);
                    GetSuitInfo1 = null;

                    string deviceInfo =
                                       "<DeviceInfo> " +
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


                        // C:\Users\admin\Desktop\LevanProjects\CreatePdf\Sarcheli

                        _reportViewer1.LocalReport.Refresh();
                        bytesa = _reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                    }

                    catch (Exception ex)
                     {
                        throw new Exception(ex.InnerException.ToString());
                    }

                    using (FileStream fs = new FileStream(DesktopPath +@"\SuitPDF\"+ Suits[i] + ".PDF", FileMode.Create))
                    //using (FileStream fs = new FileStream(@"C:\Users\admin\Desktop\hotels\" + Suits[i].ToString() + "_" + DateTime.Now.ToString("dd.MM.yyyy") + ".PDF", FileMode.Create))
                    {
                        fs.Write(bytesa, 0, bytesa.Length);
                        fs.Close();
                        fs.Dispose();
                        bytesa = null;
                        _reportViewer1.Refresh();
                        _reportViewer1.Clear();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
