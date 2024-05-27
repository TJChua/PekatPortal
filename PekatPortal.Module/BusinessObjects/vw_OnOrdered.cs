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

namespace PekatPortal.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class vw_OnOrdered : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_OnOrdered(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private vw_ItemWarehouse _vw_ItemWarehouse;
        [XafDisplayName("Item Information")]
        [Association("vw_ItemWarehouse-vw_OnOrdered")]
        [Appearance("vw_ItemWarehouse", Enabled = false)]
        public vw_ItemWarehouse vw_ItemWarehouse
        {
            get { return _vw_ItemWarehouse; }
            set { SetPropertyValue("vw_ItemWarehouse", ref _vw_ItemWarehouse, value); }
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("Key")]
        [Appearance("Key", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string Key
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
        [XafDisplayName("Item Code")]
        [Appearance("ItemCode", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ItemCode
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Document Type")]
        [Appearance("Type", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Type
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Document No.")]
        [Appearance("DocNum", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string DocNum
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Posting Date")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [Appearance("DocDate", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DocDate
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Expected Delivery Date")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [Appearance("DocDueDate", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DocDueDate
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Customer Name")]
        [Appearance("CardName", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string CardName
        {
            get; set;
        }

        //[Browsable(true)]
        [XafDisplayName("Committed Quantity")]
        [Appearance("OpenQty", Enabled = false)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public double OpenQty
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Company")]
        [Appearance("Company", Enabled = false)]
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