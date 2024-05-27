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
    [Appearance("DisableStatement", AppearanceItemType.Action, "True", TargetItems = "btnStatement", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableView", AppearanceItemType.Action, "True", TargetItems = "btnView", Visibility = ViewItemVisibility.Hide, Context = "Any")]

    public class vw_ItemWarehouse : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_ItemWarehouse(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        private vw_ItemList _vw_ItemList;
        [XafDisplayName("Item Code")]
        [Association("vw_ItemList-vw_ItemWarehouse")]
        [Appearance("vw_ItemList", Enabled = false)]
        public vw_ItemList vw_ItemList
        {
            get { return _vw_ItemList; }
            set { SetPropertyValue("Item Code", ref _vw_ItemList, value); }
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("Key")]
        [Appearance("Key", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Key
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Item Name")]
        [Appearance("ItemName", Enabled = false)]
        [VisibleInDetailView(true),VisibleInListView(false),VisibleInLookupListView(false)]
        public string ItemName
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("UOM")]
        [Appearance("UOM", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string InvntryUom
        {
            get; set;
        }


        //[Browsable(true)]
        [XafDisplayName("Warehouse Code")]
        [Appearance("WhsCode", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string WhsCode
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Warehouse Name")]
        [Appearance("WhsName", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string WhsName
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("On Hand")]
        [Appearance("OnHand", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public double OnHand
        {
            get; set;
        }

        //[Browsable(true)] 
        [XafDisplayName("Committed")]
        [Appearance("IsCommitted", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public double IsCommited
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Available")]
        [Appearance("Available", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public double Available
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Ordered")]
        [Appearance("OnOrder", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public double OnOrder
        {
            get; set;
        }

        [Association("vw_ItemWarehouse-vw_Committed")]//Save Detail together with Header.
        [XafDisplayName("Committed")]
        public XPCollection<vw_Committed> vw_Committed
        {
            get { return GetCollection<vw_Committed>("vw_Committed"); }
        }

        [Association("vw_ItemWarehouse-vw_OnOrdered")]//Save Detail together with Header.
        [XafDisplayName("Ordered")]
        public XPCollection<vw_OnOrdered> vw_OnOrdered
        {
            get { return GetCollection<vw_OnOrdered>("vw_OnOrdered"); }
        }

        [Browsable(true)]
        [XafDisplayName("Company")]
        [Appearance("Company", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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