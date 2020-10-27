using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ServiceModel.Web;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
//using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace GenerateCourtReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Billing_CreateFile();
        }
        public void Billing_CreateFile()
        {
            string FileName = "";
            ReportDataSource ds1 = new ReportDataSource();
            DataSet1TableAdapters.GetSuitInfoTableAdapter GetSuitInfo = new DataSet1TableAdapters.GetSuitInfoTableAdapter();
            ds1.Name = "DataSet1";

            ds1.Value = GetSuitInfo.GetData(1021);
            //ReportDataSource ds2 = new ReportDataSource();
            //LocationDSAccessTableAdapters.Edoc_AditionalChargeReportTableAdapter dataset2AD = new LocationDSAccessTableAdapters.Edoc_AditionalChargeReportTableAdapter();
            //ds2.Name = "Details1";
            //ds2.Value = dataset2AD.GetData(HeaderID);


           // return BillingGetPdfByteArray(ds1, ds2);
             BillingGetPdfByteArray(ds1);
        }


        //public static byte[] BillingGetPdfByteArray(ReportDataSource ds1, ReportDataSource ds2)
        public static byte[] BillingGetPdfByteArray(ReportDataSource ds1)
        {
            var rv1 = new LocalReport();
            rv1.DataSources.Add(ds1);
            //rv1.DataSources.Add(ds2);
            rv1.ReportPath = @"C:\Users\admin\Downloads\pdfgenerator (2)\pdfgenerator\New folder\CreatePdf\GenerateCourtReport\bin\Debug\sasamartlo.rdlc";

            rv1.Refresh();
            string deviceInfo =
                           "<DeviceInfo>" +
                           "  <OutputFormat>EMF</OutputFormat>" +
                           "  <PageWidth>8.3in</PageWidth>" +
                           "  <PageHeight>11.7in</PageHeight>" +
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
                bytesa = rv1.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

                using (FileStream fs = new FileStream(@"C:\Users\admin\Desktop\hotels\hh.pdf", FileMode.Create))
                {
                    fs.Write(bytesa, 0, bytesa.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

            return bytesa;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
