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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using System.Web;
using PekatPortal.Module.BusinessObjects;
using CrystalDecisions.Shared;
using DevExpress.ExpressApp.Web;
using System.IO;
using System.Configuration;
using PekatPortal.Module.Controllers;

namespace PekatPortal.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GenerateReport : ViewController
    {
        public GenerateReport()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this.Print.Active.SetItemValue("Enabled", false);
            this.PrintNoSign.Active.SetItemValue("Enabled", false);
            this.PrintRevision.Active.SetItemValue("Enabled", false);
            this.PrintRevisionNoSign.Active.SetItemValue("Enabled", false);
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View.Id == "SalesQuotation_DetailView")
            {
                this.Print.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                this.PrintNoSign.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                //this.PrintRevision.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                //this.PrintRevisionNoSign.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
            }
            else if (View.Id == "SalesQuotation_SalesQuotationRev_ListView")
            {
                this.PrintRevision.Active.SetItemValue("Enabled", ((DetailView)View.ObjectSpace.Owner).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                this.PrintRevisionNoSign.Active.SetItemValue("Enabled", ((DetailView)View.ObjectSpace.Owner).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
            }
            //    //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            //    BusinessObjects.SalesQuotation SO = View.CurrentObject as BusinessObjects.SalesQuotation;
            //    if (SO.Status != StatusEnum.Draft)
            //    {
            //        this.Print.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
            //    }
            //}
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
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

        private void Print_Execute(object sender, SimpleActionExecuteEventArgs e)
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
                if (View.ObjectTypeInfo.Type == typeof(SalesQuotation))
                {
                    if (View.Id == "SalesQuotation_DetailView")
                    {
                        SalesQuotation SQ = (SalesQuotation)e.CurrentObject;
                        ReportDocument doc = new ReportDocument();

                        doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\SalesQuotation.rpt"));
                        strServer = ConfigurationSettings.AppSettings.Get("SQLServer").ToString();//"DESKTOP-9A3TPHL\\FT14";
                        strDatabase = ConfigurationSettings.AppSettings.Get("SQLDB").ToString();
                        strUserID = ConfigurationSettings.AppSettings.Get("SQLUser").ToString();
                        strPwd = ConfigurationSettings.AppSettings.Get("SQLPassword").ToString();

                        doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                        doc.SetParameterValue("DocKey@", SQ.Oid);
                        //doc.SetParameterValue("DateTo", DateTime.Today.ToString("yyyy-MM-dd"));
                        //doc.SetParameterValue("AgeBy", "TAXDATE");
                        //doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                        //doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                        //doc.SetParameterValue("GroupBy", "SlpName");
                        //doc.SetParameterValue("InFC", false);
                        //doc.SetParameterValue("Summary", false);
                        //doc.SetParameterValue("PagePerGroup", false);
                        //doc.SetParameterValue("HideArrow", true);

                        //filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "Quotation-.pdf";//" + (100000 + SQ.Oid).ToString() + "

                        filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "Quotation-" + SQ.DocNum.ToString() + ".pdf";

                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                        doc.Close();
                        doc.Dispose();

                        WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != "")
                    showMsg("Layout Generation", ex.InnerException.Message, InformationType.Error);
                else
                    showMsg("Layout Generation", ex.Message, InformationType.Error); 
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

            //To View PDF
            //return @"var newWindow = window.open();
            //newWindow.document.write('<iframe src=""DownloadFile.aspx?filename=" + fileInfo.Name + @""" frameborder =""0"" allowfullscreen style=""width: 100%;height: 100%""></iframe>');
            //";

        }

        private void PrintRevision_Execute(object sender, SimpleActionExecuteEventArgs e)
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
                if (View.ObjectTypeInfo.Type == typeof(SalesQuotationRev))
                {
                    if (View.Id == "SalesQuotation_SalesQuotationRev_ListView")
                    {
                        if (View.SelectedObjects.Count == 1)
                        {

                            SalesQuotationRev SQ = (SalesQuotationRev)View.CurrentObject;
                            ReportDocument doc = new ReportDocument();

                            doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\SalesQuotationRev.rpt"));
                            strServer = ConfigurationSettings.AppSettings.Get("SQLServer").ToString();//"DESKTOP-9A3TPHL\\FT14";
                            strDatabase = ConfigurationSettings.AppSettings.Get("SQLDB").ToString();
                            strUserID = ConfigurationSettings.AppSettings.Get("SQLUser").ToString();
                            strPwd = ConfigurationSettings.AppSettings.Get("SQLPassword").ToString();

                            doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                            doc.SetParameterValue("DocKey@", SQ.Oid);
                            //doc.SetParameterValue("DateTo", DateTime.Today.ToString("yyyy-MM-dd"));
                            //doc.SetParameterValue("AgeBy", "TAXDATE");
                            //doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                            //doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                            //doc.SetParameterValue("GroupBy", "SlpName");
                            //doc.SetParameterValue("InFC", false);
                            //doc.SetParameterValue("Summary", false);
                            //doc.SetParameterValue("PagePerGroup", false);
                            //doc.SetParameterValue("HideArrow", true);

                            //filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "Quotation-.pdf";//" + (100000 + SQ.Oid).ToString() + "

                            filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "RevQuotation-" + SQ.DocNum.ToString() + ".pdf";

                            doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                            doc.Close();
                            doc.Dispose();

                            WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));
                        }
                        else
                        {
                            showMsg("Layout Generation", "Please Select 1 Sales Quotation Version", InformationType.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showMsg("Layout Generation", ex.Message, InformationType.Error);
            }
        }

        private void PrintNoSign_Execute(object sender, SimpleActionExecuteEventArgs e)
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
                if (View.ObjectTypeInfo.Type == typeof(SalesQuotation))
                {
                    if (View.Id == "SalesQuotation_DetailView")
                    {
                        SalesQuotation SQ = (SalesQuotation)e.CurrentObject;
                        ReportDocument doc = new ReportDocument();

                        doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\SalesQuotationNoSign.rpt"));
                        strServer = ConfigurationSettings.AppSettings.Get("SQLServer").ToString();//"DESKTOP-9A3TPHL\\FT14";
                        strDatabase = ConfigurationSettings.AppSettings.Get("SQLDB").ToString();
                        strUserID = ConfigurationSettings.AppSettings.Get("SQLUser").ToString();
                        strPwd = ConfigurationSettings.AppSettings.Get("SQLPassword").ToString();

                        doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                        doc.SetParameterValue("DocKey@", SQ.Oid);
                        //doc.SetParameterValue("DateTo", DateTime.Today.ToString("yyyy-MM-dd"));
                        //doc.SetParameterValue("AgeBy", "TAXDATE");
                        //doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                        //doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                        //doc.SetParameterValue("GroupBy", "SlpName");
                        //doc.SetParameterValue("InFC", false);
                        //doc.SetParameterValue("Summary", false);
                        //doc.SetParameterValue("PagePerGroup", false);
                        //doc.SetParameterValue("HideArrow", true);

                        //filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "Quotation-.pdf";//" + (100000 + SQ.Oid).ToString() + "

                        filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "QuotationNoSign-" + SQ.DocNum.ToString() + ".pdf";

                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                        doc.Close();
                        doc.Dispose();

                        WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));
                    }
                }
            }
            catch (Exception ex)
            {
                showMsg("Layout Genration", ex.Message, InformationType.Error);
            }
        }

        private void PrintRevisionNoSign_Execute(object sender, SimpleActionExecuteEventArgs e)
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
                if (View.ObjectTypeInfo.Type == typeof(SalesQuotationRev))
                {
                    if (View.Id == "SalesQuotation_SalesQuotationRev_ListView")
                    {
                        if (View.SelectedObjects.Count == 1)
                        {

                            SalesQuotationRev SQ = (SalesQuotationRev)View.CurrentObject;
                            ReportDocument doc = new ReportDocument();

                            doc.Load(HttpContext.Current.Server.MapPath("~\\Report\\SalesQuotationRevNoSign.rpt"));
                            strServer = ConfigurationSettings.AppSettings.Get("SQLServer").ToString();//"DESKTOP-9A3TPHL\\FT14";
                            strDatabase = ConfigurationSettings.AppSettings.Get("SQLDB").ToString();
                            strUserID = ConfigurationSettings.AppSettings.Get("SQLUser").ToString();
                            strPwd = ConfigurationSettings.AppSettings.Get("SQLPassword").ToString();

                            doc.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);
                            doc.SetParameterValue("DocKey@", SQ.Oid);
                            //doc.SetParameterValue("DateTo", DateTime.Today.ToString("yyyy-MM-dd"));
                            //doc.SetParameterValue("AgeBy", "TAXDATE");
                            //doc.SetParameterValue("CardCodeFrom@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                            //doc.SetParameterValue("CardCodeTo@FROM OCRD WHERE CardType = 'C'", SO.CardCode.CardCode);
                            //doc.SetParameterValue("GroupBy", "SlpName");
                            //doc.SetParameterValue("InFC", false);
                            //doc.SetParameterValue("Summary", false);
                            //doc.SetParameterValue("PagePerGroup", false);
                            //doc.SetParameterValue("HideArrow", true);

                            //filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "Quotation-.pdf";//" + (100000 + SQ.Oid).ToString() + "

                            filename = HttpContext.Current.Server.MapPath("~\\Report\\") + "RevQuotationNoSign-" + SQ.DocNum.ToString() + ".pdf";

                            doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                            doc.Close();
                            doc.Dispose();

                            WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", GetScript(filename));
                        }
                        else
                        {
                            showMsg("Layout Generation", "Please Select 1 Sales Quotation Version", InformationType.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                showMsg("Layout Generation", ex.Message, InformationType.Error);
            }
        }
    }
}
