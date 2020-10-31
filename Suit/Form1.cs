using iTextSharp.text.pdf;
using Microsoft.Reporting.WinForms;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

                DataSet1TableAdapters.getSimplifiedProccesDocumentationTableAdapter ProccesDocumentation = new DataSet1TableAdapters.getSimplifiedProccesDocumentationTableAdapter();

                ProccesDocumentation.GetData();
                ConcatenatePDF();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ConcatenatePDF()
        {
            try
            {
                List<string> FileName = new List<string>();
                using (var db = new LegalCounselEntities())
                {
                    var ActiveIds = db.SimpleSuitDocumentationLists.Select(m => m.ActiveID).Distinct();
                    foreach (int ActiveId in ActiveIds)
                    {
                        var FIleNames = from st in db.SimpleSuitDocumentationLists
                                        where st.ActiveID == ActiveId
                                        select st.FileName;

                        foreach (var _FileName in FIleNames)
                        {
                            FileName.Add(_FileName);

                        }
                        MergePDF(FileName, ActiveId);
                        FileName.Clear();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        private static void MergePDF(List<string> FileName, int ActiveId)
        {
            try
            {
                //string[] fileArray = new string[2];
                //fileArray[0] = File1;
                //fileArray[1] = File2;

                PdfReader reader = null;
               // PdfReader.unethicalreading = true;
                iTextSharp.text.Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage;
                string outputPdfPath = @"C:\Users\PAB\Desktop\shanava\result\" + ActiveId.ToString() + ".pdf";

                sourceDocument = new iTextSharp.text.Document();
                pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                //output file Open  
                sourceDocument.Open();


                //files list wise Loop  
                for (int f = 0; f < FileName.Count; f++)
                {
                    int pages = TotalPageCount(FileName[f]);
                    if (pages > 0)
                    {
                        reader = new PdfReader(FileName[f]);
                        if (pages < 2)
                        {
                            PdfReader.unethicalreading = true;
                        }
                        //Add pages in new file  
                        for (int i = 1; i <= pages; i++)
                        {
                            importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                            pdfCopyProvider.AddPage(importedPage);
                        }

                        reader.Close();
                    }
                }
                //save the output file  
                sourceDocument.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private static int TotalPageCount(string file)
        {
            try
            {
                if (file == @"C:\Users\PAB\Desktop\analitics\_Analytics\21132.pdf")
                {
                }
                using (StreamReader sr = new StreamReader(System.IO.File.OpenRead(file)))
                {
                     string line= "";
                    if (sr != null)
                    {
                        using (FileStream pdfStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            //Create a new instance of PDF document syntax analyzer.
                            PdfDocumentAnalyzer analyzer = new PdfDocumentAnalyzer(pdfStream);
                            //Analyze the syntax and return the results.
                            SyntaxAnalyzerResult analyzerResult = analyzer.AnalyzeSyntax();
                            if (analyzerResult.IsCorrupted == false && analyzerResult.Errors==null)
                            {

                                string ppath = file;
                                string text = System.IO.File.ReadAllText(ppath);

                                PdfReader pdfReader = new PdfReader(ppath);
                                int numberOfPages = pdfReader.NumberOfPages;



                                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                                MatchCollection matches = regex.Matches(sr.ReadToEnd());
                                return numberOfPages;//matches.Count;
                            }
                            else
                                return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
