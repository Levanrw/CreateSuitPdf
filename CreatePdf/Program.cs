using CreatePdf.DataSet1TableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Xml;

namespace CreatePdf
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                //C:\Users\gaga\Desktop\Analitics\_Documentation\Cartu\piradoba\transfercontractnumber

                string Path = AppDomain.CurrentDomain.BaseDirectory;
                string GlobalPath = "";

                XmlDocument doc = new XmlDocument();
                doc.Load(Path + @"\config.xml");

                XmlNode node = doc.DocumentElement.SelectSingleNode("/customConfig/ProgramMode");
                XmlNode CurrentUrlIs = node.NextSibling;

                int ModeType = Convert.ToInt32(node.InnerText);  //index


                XmlNodeList OrgListt = doc.DocumentElement.SelectNodes("/customConfig/organisation/Item");

                XmlNode OneObj = OrgListt.Item(ModeType);
                int Type = Convert.ToInt32(OneObj.SelectSingleNode("Type").InnerText);
                string TemplateName = OneObj.SelectSingleNode("TemplateName").InnerText;
                string DirectoryName = OneObj.SelectSingleNode("DirectoryName").InnerText;
                string FileName = OneObj.SelectSingleNode("FileName").InnerText;
                string FilePath = "";
                string additionalPath = "";

                GlobalPath = (CurrentUrlIs.InnerText == "1") ? Path + @"\PDF\" : DirectoryName;





                GetInfoTableAdapter Info = new GetInfoTableAdapter();

                List<CreatePdf.DataSet1.GetInfoRow> List = Info.GetData(Type).ToList<CreatePdf.DataSet1.GetInfoRow>();
                int Lenght = List.Count;
                for (int i = 0; i < List.Count; i++)
                {


                    if (List[i].Urgent == 1)
                    {

                        additionalPath = (CurrentUrlIs.InnerText == "1") ? "" : List[i].personalnumber + @"\" + List[i].transfercontractnumber;

                        FilePath = GlobalPath + additionalPath + @"\" + FileName + ".pdf";
                    }
                    else
                    {
                        FilePath = @"C:\Users\gaga\Desktop\Analitics\_Documentation\otherdocuments\" + Type.ToString() + @"\" + List[i].personalnumber + "_" + List[i].transfercontractnumber + "_" + FileName + ".pdf";
                    }

                    string ReportTeplatePath = Path + TemplateName + ".rdlc";


                 
                   CreatePdf(FilePath, ReportTeplatePath, List[i],i, Lenght);
                  

                }

                Console.WriteLine("Finish");
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }





            Console.ReadKey();


        }





        static void CreatePdf(string FilePath,string ReportTeplatePath, DataSet1.GetInfoRow Source,int indicator,int length) {

            try
            {
                Console.WriteLine("Step: "+ length.ToString() +"/"+ indicator.ToString() +" Filename: "+FilePath);

                //DataSet1.Edoc_SP_GetReportAqtByIDTableAdapter dataset1AD = new DataSet1.Edoc_SP_GetReportAqtByIDTableAdapter();

                ReportDataSource ds1 = new ReportDataSource();

                DataTable datatable = new DataTable();
                datatable.Columns.Add("activeid");
                datatable.Columns.Add("personalnumber");
                datatable.Columns.Add("TrueTransferDate");
                datatable.Columns.Add("initialreturndate");
                datatable.Columns.Add("loanduration");
                datatable.Columns.Add("truetransferinitialdebt");
                datatable.Columns.Add("truetransferpercentamount");
                datatable.Columns.Add("TrueTransferLateFee");
                datatable.Columns.Add("TotalDebt");
                datatable.Columns.Add("transfercontractnumber");
                datatable.Columns.Add("issuedate");
                datatable.Columns.Add("initialbase");
                datatable.Columns.Add("initialpercent");
                datatable.Columns.Add("totalamount");
                datatable.Columns.Add("firstname");
                datatable.Columns.Add("lastname");
                datatable.Columns.Add("name");
                datatable.Columns.Add("Birthday");
                datatable.Columns.Add("address");
                datatable.Columns.Add("loanphone");
                datatable.Columns.Add("accountnumber");
                datatable.Columns.Add("email");
                datatable.Columns.Add("Urgent");
                datatable.Columns.Add("Typeid");

                DataRow myRow = datatable.NewRow();
                myRow["activeid"] = Source.activeid;
                myRow["personalnumber"] = Source.personalnumber;
                myRow["TrueTransferDate"] = Source.TrueTransferDate.ToString("dd/MM/yyyy");
                myRow["initialreturndate"] = Source.initialreturndate.ToString("dd/MM/yyyy");
                myRow["loanduration"] = Source.loanduration;
                myRow["truetransferinitialdebt"] = Source.truetransferinitialdebt;
                myRow["truetransferpercentamount"] = Source.truetransferpercentamount;
                myRow["TrueTransferLateFee"] = Source.TrueTransferLateFee;
                myRow["TotalDebt"] = Source.TotalDebt;
                myRow["transfercontractnumber"] = Source.transfercontractnumber;
                myRow["issuedate"] = Source.issuedate.ToString("dd/MM/yyyy");
                myRow["initialbase"] = Source.initialbase;
                myRow["initialpercent"] = Source.initialpercent;
                myRow["totalamount"] = Source.totalamount;
                myRow["firstname"] = Source.firstname;
                myRow["lastname"] = Source.lastname;
                myRow["name"] = Source.name;
                myRow["Birthday"] = Source.Birthday.ToString("dd/MM/yyyy");
                myRow["address"] = Source.address;
                myRow["loanphone"] = Source.loanphone;
                myRow["accountnumber"] = Source.accountnumber;
                myRow["email"] = Source.email;
                myRow["Urgent"] = Source.Urgent;
                myRow["Typeid"] = Source.Typeid;

                datatable.Rows.Add(myRow);


                ds1.Name = "DataSet1";

                ds1.Value = datatable;


                var rv1 = new LocalReport();
                rv1.DataSources.Add(ds1);
                rv1.ReportPath = string.Format(ReportTeplatePath);

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
                    bytesa = rv1.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.InnerException.ToString());
                }

                using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                {
                    fs.Write(bytesa, 0, bytesa.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
            
        }
        
    }


    public class Organisation
    {
        public int type { get; set; }

        public string templateName { get; set; }

        public string DirectoryName { get; set; }
        
    }


}
