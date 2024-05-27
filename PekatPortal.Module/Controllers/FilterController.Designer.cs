namespace PekatPortal.Module.Controllers
{
    partial class FilterController
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
            this.Revision = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.Confirm = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // Revision
            // 
            this.Revision.AcceptButtonCaption = null;
            this.Revision.CancelButtonCaption = null;
            this.Revision.Caption = "Revision";
            this.Revision.ConfirmationMessage = null;
            this.Revision.Id = "btnRevision";
            this.Revision.ToolTip = null;
            this.Revision.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.Revision_CustomizePopupWindowParams);
            this.Revision.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.Revision_Execute);
            // 
            // Confirm
            // 
            this.Confirm.AcceptButtonCaption = null;
            this.Confirm.CancelButtonCaption = null;
            this.Confirm.Caption = "Confirm";
            this.Confirm.ConfirmationMessage = null;
            this.Confirm.Id = "btnConfirm";
            this.Confirm.ToolTip = null;
            this.Confirm.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.Confirm_CustomizePopupWindowParams);
            this.Confirm.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.Confirm_Execute);
            // 
            // FilterController
            // 
            this.Actions.Add(this.Revision);
            this.Actions.Add(this.Confirm);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction Revision;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction Confirm;
    }
}
