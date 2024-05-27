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
    //[DefaultClassOptions]
    [DefaultProperty("DocEntry")]
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
    [Appearance("DisableStatement", AppearanceItemType.Action, "True", TargetItems = "btnStatement", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    //[XafDisplayName("Customer Master")]
    public class vw_CustomerList : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_CustomerList(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}
        [Browsable(true)]
        [XafDisplayName("DocEntry")]
        [Key]
        [Size(100)]
        [Appearance("DocEntry", Enabled = false)]
        public string DocEntry
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Customer Code")]
        [Appearance("CardCode", Enabled = false)]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        public string CardCode
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Customer Name")]
        [Appearance("CardName", Enabled = false)]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        public string CardName
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Salesperson Code")]
        //[Appearance("PayTermName", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string SlpCode
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Salesperson Name")]
        //[Appearance("PayTermName", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(true)]
        public string SlpName
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Bill To")]
        [Appearance("BillToDef", Enabled = false)]
        [VisibleInLookupListView(false),VisibleInListView(false), VisibleInDetailView(false)]
        public string BillToDef
        {
            get; set;
        }


        [Browsable(true)]
        [XafDisplayName("Ship To")]
        [Appearance("ShipToDef", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string ShipToDef
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("GroupNum")]
        [Appearance("GroupNum", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string GroupNum
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Payment Term")]
        [Appearance("PymntGroup", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(true)]
        public string PymntGroup
        {
            get; set;
        }


        [Browsable(true)]
        [XafDisplayName("Company")]
        [Appearance("Company", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string Company
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Credit Limit")]
        [Appearance("CreditLine", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(true), VisibleInDetailView(true)]
        public double CreditLine
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Available Credit")]
        [Appearance("AvailableCredit", Enabled = false)]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        public double AvailableCredit
        {
            get; set;
        }

        [Association("vw_CustomerList-vw_ContactPerson")]//Save Detail together with Header.
        [XafDisplayName("Contact Person")]
        public XPCollection<vw_ContactPerson> vw_ContactPerson
        {
            get { return GetCollection<vw_ContactPerson>("vw_ContactPerson"); }
        }

        [Association("vw_CustomerList-vw_BillTo")]//Save Detail together with Header.
        [XafDisplayName("Bill To Address")]
        public XPCollection<vw_BillToAddress> vw_BillTo
        {
            get { return GetCollection<vw_BillToAddress>("vw_BillTo"); }
        }

        [Association("vw_CustomerList-vw_ShipTo")]//Save Detail together with Header.
        [XafDisplayName("Ship To Address")]
        public XPCollection<vw_ShipToAddress> vw_ShipTo
        {
            get { return GetCollection<vw_ShipToAddress>("vw_ShipTo"); }
        }

        [Browsable(true)]
        [XafDisplayName("Inactive")]
        [Appearance("Inactive", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string frozenFor
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Currency")]
        [Appearance("Currency", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(true), VisibleInDetailView(true)]
        public string Currency
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Contact Person")]
        [Appearance("CntctPrsn", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string CntctPrsn
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Company Phone")]
        [Appearance("CompanyPhone", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(true)]
        public string CompanyPhone
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Company Fax")]
        [Appearance("CompanyFax", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(true)]
        public string CompanyFax
        {
            get; set;
        }


        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}