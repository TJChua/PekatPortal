using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PekatPortal.Module.BusinessObjects;
using CrystalDecisions.Shared;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.ExpressApp.Web;
using System.IO;
using System.Configuration;

namespace PekatPortal.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AgingController : ViewController
    {
        public AgingController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            ReportList master = os.GetObject<ReportList>(View.CurrentObject as ReportList);
            //CollectionSource cs = new CollectionSource(objectSpace, typeof(ReportList));
            // cs.Criteria["NotesFilter"] = new InOperator("Oid", master.Notes);
            //e.View = Application.CreateListView("INote_ListView", cs, false);
            //ReportList reportList = ObjectSpace.
            //IObjectSpace os = Application.CreateObjectSpace();
            //
            //
            if (master.ActReportName == "SOReceived(ByDate).rpt" || master.ActReportName == "TotalBilling.rpt" || master.ActReportName == "OutstandingSO.rpt")
            {
                e.View = Application.CreateDetailView(os, os.CreateObject<SOReport>());
            }
            else
            {
                e.View = Application.CreateDetailView(os, os.CreateObject<Reports>());
            }
            ((DetailView)e.View).ViewEditMode = ViewEditMode.Edit;
        }

        private void Generate_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string strServer;
            string strDatabase;
            string strUserID;
            string strPwd;
            string filename;

            IObjectSpace os = Application.CreateObjectSpace();
            //vwAdmSettings Settings = os.FindObject<vwAdmSettings>(CriteriaOperator.Parse("Oid=1"));

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            try
            {
                if (View.ObjectTypeInfo.Type == typeof(Reports))
                {
                    if (View.Id == "Reports_DetailView")
                    {
                        ReportList Report = (ReportList)e.SelectedObjects;
                        ReportDocument doc = new ReportDocument();

                        Reports rpt = (Reports)e.CurrentObject;

                        doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\") + Report.ActReportName);
                        strServer = "DESKTOP-9A3TPHL\\FT14";
                        strDatabase = "TWE_LIVE";
                        strUserID = "sa";
                        strPwd = "sa";

                        doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                        doc.SetParameterValue("StatementDate", rpt.DocDate);
                        doc.SetParameterValue("AgeBy", "DUE");
                        doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE CardType = 'C'", rpt.CardCodeFrom.CardCode);
                        doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE CardType = 'C'", rpt.CardCodeTo.CardCode);
                        doc.SetParameterValue("Schema@", "1200");
                        //doc.SetParameterValue("InFC", false);
                        //doc.SetParameterValue("Summary", false);
                        //doc.SetParameterValue("PagePerGroup", false);
                        //doc.SetParameterValue("HideArrow", true);

                        // filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "Quotation" + (100000 + SQ.Oid).ToString() + ".pdf";

                        //doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                        doc.Close();
                        doc.Dispose();

                        //WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));
                    }
                }
            }
            catch (Exception ex)
            {
                showMsg("Layout Genration", ex.Message, InformationType.Error);
            }
        }

        public void showMsg(string caption, string msg, InformationType msgtype)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 3000;
            //options.Message = string.Format("{0} task(s) have been successfully updated!", e.SelectedObjects.Count);
            options.Message = string.Format("{0}", msg);
            options.Type = msgtype;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = caption;
            options.Win.Type = WinMessageType.Flyout;
            Application.ShowViewStrategy.ShowMessage(options);

        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            string strServer;
            string strDatabase;
            string strUserID;
            string strPwd;
            string filename = "";

            IObjectSpace os = Application.CreateObjectSpace();
            //vwAdmSettings Settings = os.FindObject<vwAdmSettings>(CriteriaOperator.Parse("Oid=1"));

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            try
            {
                //ReportList Report = (ReportList)e.SelectedObjects;
                ReportDocument doc = new ReportDocument();



                foreach (ReportList selectedObject in e.SelectedObjects)
                {
                    doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\") + selectedObject.ActReportName);
                    if (selectedObject.ReportPath == "SQL")
                    {
                        strServer = ConfigurationSettings.AppSettings.Get("SQLServer").ToString();
                        strDatabase = ConfigurationSettings.AppSettings.Get("SQLDB").ToString();
                        strUserID = ConfigurationSettings.AppSettings.Get("SQLUser").ToString();
                        strPwd = ConfigurationSettings.AppSettings.Get("SQLPassword").ToString();
                        doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                    }
                    else
                    {
                        //    NameValuePair2
                        //string Connection = "DRIVER ={HDBODBC32}; SERVERNODE = " + user.Company.B1Server + "; DATABASE = " + user.Company.B1CompanyDB + "";
                        string strConnection = "DRIVER = {HDBODBC32}; UID =" + user.Company.B1DbUserName; //string de conexion contruido de las variables
                        strConnection += "; PWD =" + user.Company.B1DbPassword + "; SERVERNODE =" + user.Company.B1Server;
                        strConnection += "; DATABASE =" + user.Company.B1CompanyDB + ";";
                        NameValuePairs2 logonProps2 = doc.DataSourceConnections[0].LogonProperties;
                        logonProps2.Set("Provider", "HDBODBC32");
                        logonProps2.Set("Server Type", "HDBODBC32");
                        logonProps2.Set("Connection String", strConnection);
                        logonProps2.Set("Locale Identifier", "1033");
                        doc.DataSourceConnections[0].SetLogonProperties(logonProps2);

                        //doc.DataSourceConnections[0].SetConnection(user.Company.B1Server, user.Company.B1CompanyDB, user.Company.B1DbUserName, user.Company.B1DbPassword);
                        strServer = user.Company.B1Server;
                        strDatabase = user.Company.B1CompanyDB;
                        strUserID = user.Company.B1DbUserName;
                        strPwd = user.Company.B1DbPassword;
                        doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                    }


                    if (selectedObject.ActReportName == "StatementOfAccount.rpt")
                    {

                        Reports rpt = (Reports)e.PopupWindowViewCurrentObject;
                        doc.SetParameterValue("StatementDate", rpt.DocDate);
                        doc.SetParameterValue("AgeBy", "DUE");
                        doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE \"CardType\" = 'C'", rpt.CardCodeFrom.CardCode);
                        doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE \"CardType\" = 'C'", rpt.CardCodeTo.CardCode);
                        doc.SetParameterValue("Schema@", "1200");
                        filename = HttpContext.Current.Server.MapPath("~\\Report\\") + selectedObject.ReportName + " - " + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".pdf";
                    }
                    else if (selectedObject.ActReportName == "SOReceived(ByDate).rpt" || selectedObject.ActReportName == "TotalBilling.rpt" || selectedObject.ActReportName == "OutstandingSO.rpt")
                    {
                        SOReport SOrpt = (SOReport)e.PopupWindowViewCurrentObject;
                        doc.SetParameterValue("Schema@", user.Company.B1CompanyDB);
                        doc.SetParameterValue("DateFrom", SOrpt.DateFrom);
                        doc.SetParameterValue("DateTo", SOrpt.DateTo);
                        filename = HttpContext.Current.Server.MapPath("~\\Report\\") + selectedObject.ReportName + " - " + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".pdf";
                    }

                    doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                    doc.Close();
                    doc.Dispose();
                }
                WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));

            }
            catch (Exception ex)
            {
                showMsg("Layout Genration", ex.Message, InformationType.Error);
            }
        }

        protected string GetScript(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            //To Download PDF
            return @"var mainDocument = window.parent.document;
            var iframe = mainDocument.getElementById('reportout');
            if (iframe != null) {
              mainDocument.body.removeChild(iframe);
            }
            iframe = mainDocument.createElement('iframe');
            iframe.setAttribute('id', 'reportout');
            iframe.style.width = 0 + 'px';
            iframe.style.height = 0 + 'px';
            iframe.style.display = 'none';
            mainDocument.body.appendChild(iframe);
            mainDocument.getElementById('reportout').contentWindow.location = 'DownloadFile.aspx?filename=" + fileInfo.Name + "';";
            //return @"var newWindow = window.open();
            //newWindow.document.write('<iframe src=""DownloadFile.aspx?filename=" + fileInfo.Name + @""" frameborder = ""0"" allowfullscreen style =""width: 100%;height: 100%""></iframe>";
        }

        private void Statement_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            ReportList master = os.GetObject<ReportList>(View.CurrentObject as ReportList);

            e.View = Application.CreateDetailView(os, os.CreateObject<Statement>());
            ((DetailView)e.View).ViewEditMode = ViewEditMode.Edit;
        }

        private void Statement_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            vw_CustomerList selectedObject = (vw_CustomerList)e.CurrentObject;
            string strServer;
            string strDatabase;
            string strUserID;
            string strPwd;
            string filename = "";

            IObjectSpace os = Application.CreateObjectSpace();
            //vwAdmSettings Settings = os.FindObject<vwAdmSettings>(CriteriaOperator.Parse("Oid=1"));

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            try
            {
                //ReportList Report = (ReportList)e.SelectedObjects;
                ReportDocument doc = new ReportDocument();
                doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\StatementOfAccount.rpt"));

                //    NameValuePair2
                //string Connection = "DRIVER ={HDBODBC32}; SERVERNODE = " + user.Company.B1Server + "; DATABASE = " + user.Company.B1CompanyDB + "";
                string strConnection = "DRIVER = {HDBODBC32}; UID =" + user.Company.B1DbUserName; //string de conexion contruido de las variables
                strConnection += "; PWD =" + user.Company.B1DbPassword + "; SERVERNODE =" + user.Company.B1Server;
                strConnection += "; DATABASE =" + user.Company.B1CompanyDB + ";";
                NameValuePairs2 logonProps2 = doc.DataSourceConnections[0].LogonProperties;
                logonProps2.Set("Provider", "HDBODBC32");
                logonProps2.Set("Server Type", "HDBODBC32");
                logonProps2.Set("Connection String", strConnection);
                logonProps2.Set("Locale Identifier", "1033");
                doc.DataSourceConnections[0].SetLogonProperties(logonProps2);

                //doc.DataSourceConnections[0].SetConnection(user.Company.B1Server, user.Company.B1CompanyDB, user.Company.B1DbUserName, user.Company.B1DbPassword);
                strServer = user.Company.B1Server;
                strDatabase = user.Company.B1CompanyDB;
                strUserID = user.Company.B1DbUserName;
                strPwd = user.Company.B1DbPassword;
                doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);

                Statement rpt = (Statement)e.PopupWindowViewCurrentObject;
                doc.SetParameterValue("StatementDate", rpt.DocDate);
                doc.SetParameterValue("AgeBy", "DUE");
                doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE \"CardType\" = 'C'", selectedObject.CardCode);
                doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE \"CardType\" = 'C'", selectedObject.CardCode);
                doc.SetParameterValue("Schema@", "1200");
                filename = HttpContext.Current.Server.MapPath("~\\Report\\Statement") + "Statement-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".pdf";


                doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                doc.Close();
                doc.Dispose();

                WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));

            }
            catch (Exception ex)
            {
                showMsg("Report Genration", ex.Message, InformationType.Error);
            }

        }
    }
}
