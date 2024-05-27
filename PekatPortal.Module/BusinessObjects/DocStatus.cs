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
using DevExpress.ExpressApp.Editors;

namespace PekatPortal.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("DisableSave&Close", AppearanceItemType.Action, "True", TargetItems = "SaveAndClose", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "Edit", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisableSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDuplicate", AppearanceItemType.Action, "True", TargetItems = "btnDuplicate", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisablePrint", AppearanceItemType.Action, "True", TargetItems = "btnPrint", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisablePrintNoSign", AppearanceItemType.Action, "True", TargetItems = "btnPrintNoSign", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisableConfirm", AppearanceItemType.Action, "True", TargetItems = "btnConfirm", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisableView", AppearanceItemType.Action, "True", TargetItems = "btnView", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableRevision", AppearanceItemType.Action, "True", TargetItems = "btnRevision", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisablePrintRevision", AppearanceItemType.Action, "True", TargetItems = "btnPrintRvs", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePostRevision", AppearanceItemType.Action, "True", TargetItems = "btnPostRev", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrintRevisionNoSign", AppearanceItemType.Action, "True", TargetItems = "btnPrintRevisionNoSign", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableStatement", AppearanceItemType.Action, "True", TargetItems = "btnStatement", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class DocStatus : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public DocStatus(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private SystemUsers _CreateUser;
        [NoForeignKey]
        public SystemUsers CreateUser
        {
            get { return _CreateUser; }
            set
            {
                SetPropertyValue("CreateUser", ref _CreateUser, value);
            }
        }

        private DateTime? _CreateDate;
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy HH:mm}")]
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                SetPropertyValue("Status", ref _Status, value);
            }
        }

        private string _Remark;
        [Size(SizeAttribute.Unlimited)]
        public string Remark
        {
            get { return _Remark; }
            set
            {
                SetPropertyValue("Remark", ref _Remark, value);
            }
        }

        private string _TemplateName;
        public string TemplateName
        {
            get { return _TemplateName; }
            set
            {
                SetPropertyValue("TemplateName", ref _TemplateName, value);
            }
        }

        private SalesQuotation _SalesQuotation;
        [Association("SalesQuotation-DocStatus")]
        public SalesQuotation SalesQuotation
        {
            get { return _SalesQuotation; }
            set { SetPropertyValue("SalesQuotation", ref _SalesQuotation, value); }
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