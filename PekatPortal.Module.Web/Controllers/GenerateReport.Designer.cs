namespace PekatPortal.Module.Web.Controllers
{
    partial class GenerateReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Print = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.PrintRevision = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.PrintNoSign = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.PrintRevisionNoSign = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // Print
            // 
            this.Print.Caption = "Print";
            this.Print.ConfirmationMessage = null;
            this.Print.Id = "btnPrint";
            this.Print.ToolTip = null;
            this.Print.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Print_Execute);
            // 
            // PrintRevision
            // 
            this.PrintRevision.Caption = "Print Revision";
            this.PrintRevision.ConfirmationMessage = null;
            this.PrintRevision.Id = "btnPrintRvs";
            this.PrintRevision.ToolTip = null;
            this.PrintRevision.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PrintRevision_Execute);
            // 
            // PrintNoSign
            // 
            this.PrintNoSign.Caption = "Print No Sign";
            this.PrintNoSign.ConfirmationMessage = null;
            this.PrintNoSign.Id = "btnPrintNoSign";
            this.PrintNoSign.ToolTip = null;
            this.PrintNoSign.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PrintNoSign_Execute);
            // 
            // PrintRevisionNoSign
            // 
            this.PrintRevisionNoSign.Caption = "Print Revision No Sign";
            this.PrintRevisionNoSign.ConfirmationMessage = null;
            this.PrintRevisionNoSign.Id = "btnPrintRevisionNoSign";
            this.PrintRevisionNoSign.ToolTip = null;
            this.PrintRevisionNoSign.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PrintRevisionNoSign_Execute);
            // 
            // GenerateReport
            // 
            this.Actions.Add(this.Print);
            this.Actions.Add(this.PrintRevision);
            this.Actions.Add(this.PrintNoSign);
            this.Actions.Add(this.PrintRevisionNoSign);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction Print;
        private DevExpress.ExpressApp.Actions.SimpleAction PrintRevision;
        private DevExpress.ExpressApp.Actions.SimpleAction PrintNoSign;
        private DevExpress.ExpressApp.Actions.SimpleAction PrintRevisionNoSign;
    }
}
