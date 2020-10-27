using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sarcheli
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {

                List<int> Suits = new List<int>();

                ReportViewer rv1 = new ReportViewer();


                LegalCounselTableAdapters.GetSuitInfoTableAdapter GetSuitInfoAll = new LegalCounselTableAdapters.GetSuitInfoTableAdapter();
                var SuitIds = GetSuitInfoAll.GetData(null);

                foreach (var item in SuitIds)
                {
                    Suits.Add(item.ID);
                }

                for (int i = 0; i < Suits.Count; i++)
                {
                    ReportDataSource GetSuitInfo1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitInfoTableAdapter GetSuitInfo = new LegalCounselTableAdapters.GetSuitInfoTableAdapter();
                    GetSuitInfo1.Name = "GetSuitInfo";
                    GetSuitInfo1.Value = GetSuitInfo.GetData(Suits[i]);

                    int StageCount = Convert.ToInt32(GetSuitInfo.GetData(Suits[i])[0].StageCount);

                    ReportDataSource GetSuitAgents1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitAgentsTableAdapter GetSuitAgents = new LegalCounselTableAdapters.GetSuitAgentsTableAdapter();
                    GetSuitAgents1.Name = "GetSuitAgents";
                    GetSuitAgents1.Value = GetSuitAgents.GetData(Suits[i]);

                    List<GetSuitCircumstances> GetSuitCircumstancesList = new List<Sarcheli.GetSuitCircumstances>();

                    LegalCounselTableAdapters.GetSuitCircumstancesTableAdapter GetSuitCircumstances = new LegalCounselTableAdapters.GetSuitCircumstancesTableAdapter();
                    ReportDataSource GetSuitCircumstances1 = new ReportDataSource();
                    GetSuitCircumstances1.Name = "GetSuitCircumstances";
                    DataTable Data = new DataTable();
                    Data.Columns.Add("suitid");
                    Data.Columns.Add("stageid");
                    Data.Columns.Add("Circumstance");
                    Data.Columns.Add("activeid");
                    Data.Columns.Add("priority");
                    Data.Columns.Add("TextPriority");
                    for (int j = 1; j <= StageCount; j++)
                    {

                        var cumstances = GetSuitCircumstances.GetData(Suits[i], j);
                        foreach (var item in cumstances)
                        {
                            Data.Rows.Add(item.ItemArray);
                        }
                    }
                    GetSuitCircumstances1.Value = Data;
                    ReportDataSource GetSuitDebtors1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitDebtorsTableAdapter GetSuitDebtors = new LegalCounselTableAdapters.GetSuitDebtorsTableAdapter();
                    GetSuitDebtors1.Name = "GetSuitDebtors";
                    GetSuitDebtors1.Value = GetSuitDebtors.GetData(Suits[i]);

                    ReportDataSource GetSuitOverview1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitOverviewTableAdapter GetSuitOverview = new LegalCounselTableAdapters.GetSuitOverviewTableAdapter();
                    GetSuitOverview1.Name = "GetSuitOverview";
                    GetSuitOverview1.Value = GetSuitOverview.GetData(Suits[i]);

                    ReportDataSource GetSuitRequests1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitRequestsTableAdapter GetSuitRequests = new LegalCounselTableAdapters.GetSuitRequestsTableAdapter();
                    GetSuitRequests1.Name = "GetSuitRequests";
                    DataTable Requests = new DataTable();
                    Requests.Columns.Add("article");
                    Requests.Columns.Add("activeid");
                    Requests.Columns.Add("priority");
                    Requests.Columns.Add("TextPriority");
                    for (int j = 1; j <= 3; j++)
                    {

                        var SuitRequest = GetSuitRequests.GetData(Suits[i], j);
                        foreach (var item in SuitRequest)
                        {
                            Requests.Rows.Add(item.ItemArray);
                        }
                    }

                    GetSuitRequests1.Value = Requests;
                    int StageCountPetitions = Convert.ToInt32(GetSuitInfo.GetData(Suits[i])[0].StageCountPetitions);

                    ReportDataSource GetSuitPetitions1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitPetitionsTableAdapter GetSuitPetitions = new LegalCounselTableAdapters.GetSuitPetitionsTableAdapter();
                    GetSuitPetitions1.Name = "GetSuitPetitions";
                    DataTable Petitions = new DataTable();
                    Petitions.Columns.Add("article");
                    Petitions.Columns.Add("activeid");
                    Petitions.Columns.Add("priority");
                    Petitions.Columns.Add("TextPriority");
                    for (int j = 1; j <= StageCountPetitions; j++)
                    {

                        var Petition = GetSuitPetitions.GetData(Suits[i], j);
                        foreach (var item in Petition)
                        {
                            Petitions.Rows.Add(item.ItemArray);
                        }
                    }

                    GetSuitPetitions1.Value = Petitions;

                    ReportDataSource GetSuitActivesInfo1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitActivesInfoTableAdapter GetSuitActivesInfo = new LegalCounselTableAdapters.GetSuitActivesInfoTableAdapter();
                    GetSuitActivesInfo1.Name = "GetSuitActivesInfo";
                    GetSuitActivesInfo1.Value = GetSuitActivesInfo.GetData(Suits[i]);


                    ReportDataSource GetSuitAttachments1 = new ReportDataSource();
                    LegalCounselTableAdapters.GetSuitAttachmentsTableAdapter GetSuitAttachments = new LegalCounselTableAdapters.GetSuitAttachmentsTableAdapter();
                    GetSuitAttachments1.Name = "GetSuitAttachments";
                    GetSuitAttachments1.Value = GetSuitAttachments.GetData(Suits[i]);

                    ReportViewer _reportViewer1 = new ReportViewer();
                    _reportViewer1.ProcessingMode = ProcessingMode.Local;

                    _reportViewer1.LocalReport.ReportPath = @"C:\Users\PAB\Desktop\Levan\pdfgenerator (2)\pdfgenerator (2)\pdfgenerator\New folder\CreatePdf\Sarcheli\Sarcheli2.rdlc";

                    // _reportViewer1.LocalReport.ReportPath = @"C:\Users\admin\Desktop\pdfgenerator (2)\pdfgenerator (2)\pdfgenerator\New folder\CreatePdf\Sarcheli\Sarcheli2.rdlc";

                    _reportViewer1.RefreshReport();
                    _reportViewer1.Clear();

                    // var rv1 = new LocalReport();
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitInfo1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitAgents1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitCircumstances1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitDebtors1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitOverview1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitRequests1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitPetitions1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitActivesInfo1);
                    _reportViewer1.LocalReport.DataSources.Add(GetSuitAttachments1);
                    GetSuitInfo1 = null;
                    GetSuitAgents1 = null;
                    GetSuitCircumstances1 = null;
                    GetSuitDebtors1 = null;
                    GetSuitOverview1 = null;
                    GetSuitRequests1 = null;
                    GetSuitPetitions1 = null;
                    GetSuitActivesInfo1 = null;
                    GetSuitAttachments1 = null;
                    // _reportViewer1.LocalReport.ReportPath = string.Format(@"C:\Users\admin\Downloads\pdfgenerator (2)\pdfgenerator\New folder\CreatePdf\Sarcheli\Sarcheli.rdlc");

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
                        _reportViewer1.LocalReport.Refresh();
                        bytesa = _reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                    }

                    catch (Exception ex)
                    {
                        throw new Exception(ex.InnerException.ToString());
                    }

                    using (FileStream fs = new FileStream(@"C:\Users\PAB\Desktop\Levan\sarcheli\PDF\" + Suits[i].ToString() + "_" + DateTime.Now.ToString("dd.MM.yyyy") + ".PDF", FileMode.Create))
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

