namespace PekatPortal.Module.Controllers
{
    partial class SAPUtilities
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
            this.Close = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.Post = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.Duplicate = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.History = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.PostRev = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.Approve = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.Reject = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // Close
            // 
            this.Close.AcceptButtonCaption = "Yes";
            this.Close.CancelButtonCaption = "No";
            this.Close.Caption = "Cancel";
            this.Close.ConfirmationMessage = null;
            this.Close.Id = "btnClose";
            this.Close.ToolTip = null;
            this.Close.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.Close_CustomizePopupWindowParams);
            this.Close.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.Close_Execute);
            // 
            // Post
            // 
            this.Post.AcceptButtonCaption = "Yes";
            this.Post.CancelButtonCaption = "No";
            this.Post.Caption = "Post";
            this.Post.Category = "ObjectsCreation";
            this.Post.ConfirmationMessage = null;
            this.Post.Id = "btnPost";
            this.Post.ToolTip = null;
            this.Post.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.Post_CustomizePopupWindowParams);
            this.Post.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.Post_Execute);
            // 
            // Duplicate
            // 
            this.Duplicate.Caption = "Duplicate";
            this.Duplicate.ConfirmationMessage = null;
            this.Duplicate.Id = "btnDuplicate";
            this.Duplicate.ToolTip = null;
            this.Duplicate.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Duplicate_Execute);
            // 
            // History
            // 
            this.History.AcceptButtonCaption = null;
            this.History.CancelButtonCaption = null;
            this.History.Caption = "History";
            this.History.Category = "My Category";
            this.History.ConfirmationMessage = null;
            this.History.Id = "btnHistory";
            this.History.ToolTip = null;
            this.History.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.History_CustomizePopupWindowParams);
            this.History.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.History_Execute);
            // 
            // PostRev
            // 
            this.PostRev.AcceptButtonCaption = "Yes";
            this.PostRev.CancelButtonCaption = "No";
            this.PostRev.Caption = "Post Revision";
            this.PostRev.ConfirmationMessage = null;
            this.PostRev.Id = "btnPostRev";
            this.PostRev.ToolTip = null;
            this.PostRev.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.PostRev_CustomizePopupWindowParams);
            this.PostRev.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PostRev_Execute);
            // 
            // Approve
            // 
            this.Approve.AcceptButtonCaption = "Yes";
            this.Approve.CancelButtonCaption = "No";
            this.Approve.Caption = "Approve";
            this.Approve.ConfirmationMessage = null;
            this.Approve.Id = "btnApprove";
            this.Approve.ToolTip = null;
            this.Approve.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.Approve_CustomizePopupWindowParams);
            this.Approve.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.Approve_Execute);
            // 
            // Reject
            // 
            this.Reject.AcceptButtonCaption = "Yes";
            this.Reject.CancelButtonCaption = "No";
            this.Reject.Caption = "Reject";
            this.Reject.ConfirmationMessage = null;
            this.Reject.Id = "btnReject";
            this.Reject.ToolTip = null;
            this.Reject.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.Reject_CustomizePopupWindowParams);
            this.Reject.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.Reject_Execute);
            // 
            // SAPUtilities
            // 
            this.Actions.Add(this.Close);
            this.Actions.Add(this.Post);
            this.Actions.Add(this.Duplicate);
            this.Actions.Add(this.History);
            this.Actions.Add(this.PostRev);
            this.Actions.Add(this.Approve);
            this.Actions.Add(this.Reject);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction Close;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction Post;
        private DevExpress.ExpressApp.Actions.SimpleAction Duplicate;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction History;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PostRev;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction Approve;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction Reject;
    }
}
