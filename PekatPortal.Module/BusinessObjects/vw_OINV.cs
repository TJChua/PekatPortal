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
    [Appearance("DisableNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = ViewItemVisibility.Hide, Context = "Any")]
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
    public class vw_OINV : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_OINV(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Browsable(true)]
        [XafDisplayName("DocEntry")]
        [Key]
        [Appearance("DocEntry", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int DocEntry
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Posting Date")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [Appearance("DocDate", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public DateTime DocDate
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Due Date")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [Appearance("DocDueDate", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public DateTime DocDueDate
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Invoice No.")]
        [Appearance("DocNum", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public int DocNum
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Customer Code")]
        [Appearance("CardCode", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string CardCode
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Customer Name")]
        [Appearance("CardName", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string CardName
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Status")]
        [Appearance("Status", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Status
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Salesperson")]
        [Appearance("Salesperson", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Memo
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Remarks")]
        [Appearance("Remarks", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Comments
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Company")]
        [Appearance("Company", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Company
        {
            get; set;
        }

        [Association("vw_OINV-vw_INV1")]//Save Detail together with Header.
        [XafDisplayName("Invoice Detail")]
        public XPCollection<vw_INV1> vw_INV1
        {
            get { return GetCollection<vw_INV1>("vw_INV1"); }
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