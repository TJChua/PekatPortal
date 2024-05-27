using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using PekatPortal.Module.BusinessObjects;
using DevExpress.ExpressApp.Web.Controls;
using System.Web.UI.WebControls;

namespace PekatPortal.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
            
        }
        protected void Application_Start(Object sender, EventArgs e) {
            XafPopupWindowControl.DefaultHeight = Unit.Percentage(90);
            XafPopupWindowControl.DefaultWidth = Unit.Percentage(90);
            XafPopupWindowControl.PopupTemplateType = PopupTemplateType.ByDefault;
            XafPopupWindowControl.ShowPopupMode = ShowPopupMode.Centered;
            
            
            SecurityAdapterHelper.Enable();
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(Object sender, EventArgs e) {
            Tracing.Initialize();
            WebApplication.SetInstance(Session, new PekatPortalAspNetApplication());
            WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath =
        "DropDownButton.ascx";
            DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            WebApplication.Instance.SwitchToNewStyle();//Comment to go into the fucking mode.
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            //    WebApplication.Instance.CreateCustomLogonWindowObjectSpace +=
            //application_CreateCustomLogonWindowObjectSpace;
            // WebApplication.Instance.Setup();
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }

        private void Instance_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e)
        {

            //e.FormattingCulture.NumberFormat.CurrencySymbol = "RM ";
            e.FormattingCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        }

        //    private static void application_CreateCustomLogonWindowObjectSpace(object sender,
        //CreateCustomLogonWindowObjectSpaceEventArgs e)
        //    {
        //        e.ObjectSpace = ((XafApplication)sender).CreateObjectSpace(typeof(CustomLogonParameters));
        //        if (e.ObjectSpace is NonPersistentObjectSpace)
        //        {
        //            IObjectSpace objectSpaceCompany = ((XafApplication)sender).CreateObjectSpace(typeof(Company));
        //            ((NonPersistentObjectSpace)e.ObjectSpace).AdditionalObjectSpaces.Add(objectSpaceCompany);
        //        }
        // }

        protected void Application_BeginRequest(Object sender, EventArgs e) {
        }
        protected void Application_EndRequest(Object sender, EventArgs e) {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
        }
        protected void Application_Error(Object sender, EventArgs e) {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e) {
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e) {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
