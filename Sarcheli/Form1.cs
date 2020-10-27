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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

                ReportDataSource GetSuitAgents1 = new ReportDataSource();
                LegalCounselTableAdapters.GetSuitAgentsTableAdapter GetSuitAgents = new LegalCounselTableAdapters.GetSuitAgentsTableAdapter();
                GetSuitAgents1.Name = "GetSuitAgents";
                GetSuitAgents1.Value = GetSuitAgents.GetData(Suits[i]);

                List<GetSuitCircumstances> GetSuitCircumstancesList=new List<Sarcheli.GetSuitCircumstances>();

                LegalCounselTableAdapters.GetSuitCircumstancesTableAdapter GetSuitCircumstances = new LegalCounselTableAdapters.GetSuitCircumstancesTableAdapter();
                ReportDataSource GetSuitCircumstances1 = new ReportDataSource();
                GetSuitCircumstances1.Name = "GetSuitCircumstances";
                DataTable Data=new DataTable();
                Data.Columns.Add("suitid");
                Data.Columns.Add("stageid");
                Data.Columns.Add("Circumstance");
                Data.Columns.Add("activeid");
                Data.Columns.Add("priority");
                Data.Columns.Add("TextPriority");

                for (int j = 0; j < 3; j++)
                {

                   var cumstances=GetSuitCircumstances.GetData(1, 1);
                   foreach (var item in cumstances)
                   {
                       Data.Rows.Add(item.ItemArray);
                   }
                }
                GetSuitCircumstances1.Value = Data;
                ReportDataSource GetSuitDebtors1 = new ReportDataSource();                          
                LegalCounselTableAdapters.GetSuitDebtorsTableAdapter GetSuitDebtors = new LegalCounselTableAdapters.GetSuitDebtorsTableAdapter();
                GetSuitDebtors1.Name = "GetSuitDebtors";
                GetSuitDebtors1.Value = GetSuitDebtors.GetData(1);

                ReportDataSource GetSuitOverview1 = new ReportDataSource();
                LegalCounselTableAdapters.GetSuitOverviewTableAdapter GetSuitOverview = new LegalCounselTableAdapters.GetSuitOverviewTableAdapter();
                GetSuitOverview1.Name = "GetSuitOverview";
                GetSuitOverview1.Value = GetSuitOverview.GetData(1);

                ReportDataSource GetSuitRequests1 = new ReportDataSource();
                LegalCounselTableAdapters.GetSuitRequestsTableAdapter GetSuitRequests = new LegalCounselTableAdapters.GetSuitRequestsTableAdapter();
                GetSuitRequests1.Name = "GetSuitRequests";
                DataTable Requests = new DataTable();
                Requests.Columns.Add("article");
                Requests.Columns.Add("activeid");
                Requests.Columns.Add("priority");
                Requests.Columns.Add("TextPriority");
                for (int j = 0; j < 3; j++)
                {

                    var SuitRequest = GetSuitRequests.GetData(1, 1);
                    foreach (var item in SuitRequest)
                    {
                        Requests.Rows.Add(item.ItemArray);
                    }
                }

                GetSuitRequests1.Value = Requests;

                ReportDataSource GetSuitPetitions1 = new ReportDataSource();
                LegalCounselTableAdapters.GetSuitPetitionsTableAdapter GetSuitPetitions = new LegalCounselTableAdapters.GetSuitPetitionsTableAdapter();
                GetSuitPetitions1.Name = "GetSuitPetitions";
                DataTable Petitions = new DataTable();
                Petitions.Columns.Add("article");
                Petitions.Columns.Add("activeid");
                Petitions.Columns.Add("priority");
                Petitions.Columns.Add("TextPriority");
                for (int j = 0; j < 3; j++)
                {

                    var Petition = GetSuitPetitions.GetData(1, 1);
                    foreach (var item in Petition)
                    {
                        Petitions.Rows.Add(item.ItemArray);
                    }
                }

                GetSuitPetitions1.Value = Petitions;

                ReportDataSource GetSuitActivesInfo1 = new ReportDataSource();
                LegalCounselTableAdapters.GetSuitActivesInfoTableAdapter GetSuitActivesInfo = new LegalCounselTableAdapters.GetSuitActivesInfoTableAdapter();
                GetSuitActivesInfo1.Name = "GetSuitActivesInfo";
                GetSuitActivesInfo1.Value = GetSuitActivesInfo.GetData(1);


                ReportDataSource GetSuitAttachments1 = new ReportDataSource();
                LegalCounselTableAdapters.GetSuitAttachmentsTableAdapter GetSuitAttachments = new LegalCounselTableAdapters.GetSuitAttachmentsTableAdapter();
                GetSuitAttachments1.Name = "GetSuitAttachments";
                GetSuitAttachments1.Value = GetSuitAttachments.GetData(1);


                reportViewer1.ProcessingMode = ProcessingMode.Local;

                reportViewer1.LocalReport.ReportPath = @"C:\Users\admin\Downloads\pdfgenerator (2)\pdfgenerator\New folder\CreatePdf\Sarcheli\Sarcheli.rdlc";

                this.reportViewer1.RefreshReport();


                // var rv1 = new LocalReport();
                reportViewer1.LocalReport.DataSources.Add(GetSuitInfo1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitAgents1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitCircumstances1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitDebtors1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitOverview1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitRequests1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitPetitions1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitActivesInfo1);
                reportViewer1.LocalReport.DataSources.Add(GetSuitAttachments1);
                // reportViewer1.LocalReport.ReportPath = string.Format(@"C:\Users\admin\Downloads\pdfgenerator (2)\pdfgenerator\New folder\CreatePdf\Sarcheli\Sarcheli.rdlc");

                rv1.Refresh();
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
                    bytesa = reportViewer1.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.InnerException.ToString());
                }

                using (FileStream fs = new FileStream(@"C:\Users\admin\Desktop\hotels\" + Suits[i].ToString() + "_" + DateTime.Now.ToString("dd.MM.yyyy") + ".PDF", FileMode.Create))
                {
                    fs.Write(bytesa, 0, bytesa.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }
}