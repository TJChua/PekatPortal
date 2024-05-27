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
    public class vw_DLN1 : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_DLN1(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Browsable(true)]
        [XafDisplayName("Key")]
        [Key]
        [Appearance("Key", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Key
        {
            get; set;
        }


        private vw_ODLN _DocEntry;
        [XafDisplayName("Order Detail")]
        [Association("vw_ODLN-vw_DLN1")]
        [Appearance("_vw_ODLN", Enabled = false)]
        [Browsable(true)]
        [Appearance("DocEntry", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public vw_ODLN DocEntry
        {
            get { return _DocEntry; }
            set { SetPropertyValue("DocEntry", ref _DocEntry, value); }
        }

        [Browsable(true)]
        [XafDisplayName("Catalog No.")]
        [Appearance("CatalogNo", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string SuppCatNum
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Item Code")]
        [Appearance("ItemCode", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string ItemCode
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Description")]
        [Appearance("Description", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Dscription
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Item Details")]
        [Appearance("Text", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Text
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Quantity")]
        [Appearance("Quantity", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public double Quantity
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Remaining Quantity")]
        [Appearance("OpenQty", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public double OpenQty
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Unit Price")]
        [Appearance("Price", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public double Price
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Total")]
        [Appearance("LineTotal", Enabled = false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public double LineTotal
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