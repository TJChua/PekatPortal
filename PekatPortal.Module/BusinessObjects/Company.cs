using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo.Metadata;
using System.Drawing;
using DevExpress.ExpressApp.Editors;

namespace PekatPortal.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).

    [Appearance("DisableDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableReset", AppearanceItemType.Action, "True", TargetItems = "ResetViewSettings", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableExport", AppearanceItemType.Action, "True", TargetItems = "Export", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDuplicate", AppearanceItemType.Action, "True", TargetItems = "btnDuplicate", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrint", AppearanceItemType.Action, "True", TargetItems = "btnPrint", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableConfirm", AppearanceItemType.Action, "True", TargetItems = "btnConfirm", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableView", AppearanceItemType.Action, "True", TargetItems = "btnView", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableRevision", AppearanceItemType.Action, "True", TargetItems = "btnRevision", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrintRevision", AppearanceItemType.Action, "True", TargetItems = "btnPrintRvs", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePostRevision", AppearanceItemType.Action, "True", TargetItems = "btnPostRev", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableStatement", AppearanceItemType.Action, "True", TargetItems = "btnStatement", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class Company : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Company(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _CompanyName;
        [XafDisplayName("Company Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]

        public string CompanyName
        {
            get { return _CompanyName; }
            set { SetPropertyValue("CompanyName", ref _CompanyName, value); }
        }

        private string _B1UserName;
        [XafDisplayName("User Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1UserName
        {
            get { return _B1UserName; }
            set { SetPropertyValue("B1UserName", ref _B1UserName, value); }
        }

        private string _B1Password;
        [XafDisplayName("Password")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1Password
        {
            get { return _B1Password; }
            set { SetPropertyValue("B1Password", ref _B1Password, value); }
        }

        private string _B1Server;
        [XafDisplayName("Server Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1Server
        {
            get { return _B1Server; }
            set { SetPropertyValue("B1Server", ref _B1Server, value); }
        }

        private string _B1CompanyDB;
        [XafDisplayName("Company DB")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1CompanyDB
        {
            get { return _B1CompanyDB; }
            set { SetPropertyValue("B1CompanyDB", ref _B1CompanyDB, value); }
        }

        private string _B1License;
        [XafDisplayName("License Server")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1License
        {
            get { return _B1License; }
            set { SetPropertyValue("B1License", ref _B1License, value); }
        }

        private string _B1DbServerType;
        [XafDisplayName("Server Type")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1DbServerType
        {
            get { return _B1DbServerType; }
            set { SetPropertyValue("B1DbServerType", ref _B1DbServerType, value); }
        }

        private string _B1DbUserName;
        [XafDisplayName("DB User Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1DbUserName
        {
            get { return _B1DbUserName; }
            set { SetPropertyValue("B1DbUserName", ref _B1DbUserName, value); }
        }

        private string _B1DbPassword;
        [XafDisplayName("DB Password")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string B1DbPassword
        {
            get { return _B1DbPassword; }
            set { SetPropertyValue("B1DbPassword", ref _B1DbPassword, value); }
        }

        private string _SMTPHost;
        [XafDisplayName("SMTP Host")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string SMTPHost
        {
            get { return _SMTPHost; }
            set { SetPropertyValue("SMTPHost", ref _SMTPHost, value); }
        }

        private string _Port;
        [XafDisplayName("Port")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string Port
        {
            get { return _Port; }
            set { SetPropertyValue("Port", ref _Port, value); }
        }

        private string _Email;
        [XafDisplayName("Sender Email Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string Email
        {
            get { return _Email; }
            set { SetPropertyValue("Email", ref _Email, value); }
        }

        private string _EmailUser;
        [XafDisplayName("Email ID")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string EmailUser
        {
            get { return _EmailUser; }
            set { SetPropertyValue("EmailUser", ref _EmailUser, value); }
        }

        private string _EmailPass;
        [XafDisplayName("Email Password")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string EmailPass
        {
            get { return _EmailPass; }
            set { SetPropertyValue("EmailPass", ref _EmailPass, value); }
        }
     

        [Association("Company-SystemUsers")]
        public XPCollection<SystemUsers> SystemUser
        {
            get { return GetCollection<SystemUsers>("SystemUser"); }
        }

        [Association("Company-DocumentNumber")]
        public XPCollection<DocumentNumber> DocumentNumber
        {
            get { return GetCollection<DocumentNumber>("DocumentNumber"); }
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}