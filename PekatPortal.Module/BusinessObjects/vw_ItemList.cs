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
    [DefaultProperty("ItemCode")]
    //[NonPersistent]
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

    [XafDisplayName("Inventory Master")]
    public class vw_ItemList : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_ItemList(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("Item Code")]
        [Appearance("ItemCode",Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string ItemCode
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Catalog No.")]
        [Appearance("SuppCatNum", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string SuppCatNum
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Item Name")]
        [Appearance("ItemName", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string ItemName
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Item Group")]
        [Appearance("ItemGroup", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ItemGroup
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("UOM")]
        [Appearance("Sales UOM", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string UOM
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("DfltWH")]
        [Appearance("Default Warehouse", Enabled = false)]
        [VisibleInDetailView(false),VisibleInListView(false),VisibleInLookupListView(false)]
        public string DfltWH
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("TaxCode")]
        [Appearance("Sales Tax Code", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string TaxCode
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("BrandName")]
        [Appearance("Brand Name", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BrandName
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("ItemDetail")]
        [Appearance("ItemDetail", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ItemDetail
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("On Hand")]
        [Appearance("OnHand", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public double OnHand
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Committed")]
        [Appearance("Committed", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public double Committed
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Available")]
        [Appearance("Available", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public double Available
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Ordered")]
        [Appearance("Ordered", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public double Ordered
        {
            get; set;
        }


        [Association("vw_ItemList-vw_ItemWarehouse")]//Save Detail together with Header.
        [XafDisplayName("Warehouse")]
        public XPCollection<vw_ItemWarehouse> vw_ItemWarehouse
        {
            get { return GetCollection<vw_ItemWarehouse>("vw_ItemWarehouse"); }
        }

        [Browsable(true)]
        [XafDisplayName("Company")]
        [Appearance("Company", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Company
        {
            get; set;
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
        [XafDisplayName("Locked")]
        [Appearance("Locked", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Locked
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Category")]
        [Appearance("Category", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Category
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