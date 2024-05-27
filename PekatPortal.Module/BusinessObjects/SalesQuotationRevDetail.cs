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
    public class SalesQuotationRevDetail : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SalesQuotationRevDetail(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string _ItemCode;
        [XafDisplayName("Item Code")]
        [Size(50)]
        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                SetPropertyValue("ItemCode", ref _ItemCode, value);
            }
        }
        #region MyRegion


        private string _ItemName;
        [XafDisplayName("Item Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string ItemName
        {
            get { return _ItemName; }
            set { SetPropertyValue("ItemName", ref _ItemName, value); }
        }

        private string _SuppCatNum;
        [XafDisplayName("Catalog No.")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [Size(100)]
        public string SuppCatNum
        {
            get { return _SuppCatNum; }
            set { SetPropertyValue("SuppCatNum", ref _SuppCatNum, value); }
        }

        private string _ItemDetail;
        [XafDisplayName("Item Detail")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.Unlimited)]
        public string ItemDetail
        {
            get { return _ItemDetail; }
            set { SetPropertyValue("ItemDetail", ref _ItemDetail, value); }
        }

        private double _Quantity;
        [XafDisplayName("Quantity")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public double Quantity
        {
            get { return _Quantity; }
            set { SetPropertyValue("Quantity", ref _Quantity, value); }
        }

        private string _UOM;
        [XafDisplayName("UOM")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        public string UOM
        {
            get { return _UOM; }
            set { SetPropertyValue("UOM", ref _UOM, value); }
        }

        private string _Brand;
        [XafDisplayName("Brand")]
        [Appearance("Brand", Enabled = false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(100)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Brand
        {
            get { return _Brand; }
            set { SetPropertyValue("Brand", ref _Brand, value); }
        }

        #endregion
        private string _Warehouse;
        [XafDisplayName("Warehouse")]
        [Size(100)]
        public string Warehouse
        {
            get { return _Warehouse; }
            set { SetPropertyValue("Warehouse", ref _Warehouse, value); }
        }

        private double _UnitPrice;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public double UnitPrice
        {
            get { return _UnitPrice; }
            set { SetPropertyValue("UnitPrice", ref _UnitPrice, value); }
        }

        private double _UnitPriceAfDisc;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public double UnitPriceAfDisc
        {
            get { return _UnitPriceAfDisc; }
            set { SetPropertyValue("UnitPriceAfDisc", ref _UnitPrice, value); }
        }

        private double _Discount;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [XafDisplayName("Discount")]
        public double Discount
        {
            get { return _Discount; }
            set
            {
                SetPropertyValue("Discount", ref _Discount, value);
            }
        }

        private double _DiscountAmt;
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [XafDisplayName("Discount Amount")]
        public double DiscountAmt
        {
            get
            {
                return _DiscountAmt;
            }
            set
            {
                SetPropertyValue("DiscountAmt", ref _DiscountAmt, value);
            }
        }

        private string _TaxCode;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { SetPropertyValue("TaxCode", ref _TaxCode, value); }
        }

        private double _LineTotal;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public double LineTotal
        {
            get
            {
                return _LineTotal;
            }
            set { SetPropertyValue("LineTotal", ref _LineTotal, value); }
        }

        private SalesQuotationRev _SalesQuotationRev;
        [XafDisplayName("SalesQuotationRevDetail")]
        [Association("SalesQuotationRev-SalesQuotationRevDetail")]
        [Appearance("SalesQuotationRev", Enabled = false)]
        public SalesQuotationRev SalesQuotationRev
        {
            get { return _SalesQuotationRev; }
            set { SetPropertyValue("SalesQuotationRev", ref _SalesQuotationRev, value); }
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