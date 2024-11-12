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
    [Appearance("DisableLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDuplicate", AppearanceItemType.Action, "True", TargetItems = "btnDuplicate", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrint", AppearanceItemType.Action, "True", TargetItems = "btnPrint", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableConfirm", AppearanceItemType.Action, "True", TargetItems = "btnConfirm", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableView", AppearanceItemType.Action, "True", TargetItems = "btnView", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableRevision", AppearanceItemType.Action, "True", TargetItems = "btnRevision", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("DisableHistory", AppearanceItemType.Action, "True", TargetItems = "btnHistory", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrintRevision", AppearanceItemType.Action, "True", TargetItems = "btnPrintRvs", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePostRevision", AppearanceItemType.Action, "True", TargetItems = "btnPostRev", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableStatement", AppearanceItemType.Action, "True", TargetItems = "btnStatement", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableCatalogNo", AppearanceItemType = "ViewItem",
         TargetItems = "SuppCatNum", Criteria = "ItemCode.Locked = 'Y'", Context = "DetailView",
             Enabled = false)]

    public class SalesQuotationDetail : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SalesQuotationDetail(Session session)
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

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        private vw_ItemList _ItemCode;
        [XafDisplayName("Item Code")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [NoForeignKey]
        [Size(50)]
        [DataSourceCriteria("frozenFor = 'N'")]
        public vw_ItemList ItemCode
        {
            get { return _ItemCode; }
            set
            {
                SetPropertyValue("ItemCode", ref _ItemCode, value);
                if (!IsLoading & value != null)
                {
                    //SetPropertyValue("ItemCode", ref _ItemCode, Session.FindObject<vw_ItemList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)));
                    SetPropertyValue("ItemName", ref _ItemName, value.ItemName);
                    SetPropertyValue("UOM", ref _UOM, value.UOM);
                    SetPropertyValue("Brand", ref _Brand, value.BrandName);
                    SetPropertyValue("Availabe", ref _Available, value.Available);
                    SetPropertyValue("SuppCatNum", ref _SuppCatNum, value.SuppCatNum);
                    SetPropertyValue("ItemDetail", ref _ItemDetail, value.ItemDetail);
                    SetPropertyValue("ItemCategory", ref _ItemCategory, value.Category);
                    if (value.DfltWH != "") SetPropertyValue("Warehouse", ref _Warehouse, Session.FindObject<vw_WarehouseList>(CriteriaOperator.Parse("WhsCode=?", value.DfltWH)));
                    if (value.TaxCode != "") SetPropertyValue("TaxCode", ref _TaxCode, Session.FindObject<vw_OutputTax>(CriteriaOperator.Parse("Code=?", value.TaxCode)));
                    if (value.ItemCode != "FreeText")
                    {
                        // Start ver TJC
                        vw_PriceList pricelist = Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode));
                        // End ver TJC

                        if (Currency == "MYR")
                        {
                            // Start ver TJC
                            //SetPropertyValue("UnitPrice", ref _UnitPrice, Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)).Price);
                            //SetPropertyValue("UnitPriceAfDisc", ref _UnitPriceAfDisc, Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)).Price);
                            //SetPropertyValue("OriUnitPrice", ref _OriUnitPrice, Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)).Price);

                            if (pricelist != null)
                            {
                                UnitPrice = pricelist.Price;
                                UnitPriceAfDisc = pricelist.Price;
                                OriUnitPrice = pricelist.Price;
                            }
                            // End ver TJC
                        }
                        else
                        {
                            // Start ver TJC
                            //SetPropertyValue("UnitPrice", ref _UnitPrice, Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)).Price / CurrRate);
                            //SetPropertyValue("UnitPriceAfDisc", ref _UnitPriceAfDisc, Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)).Price / CurrRate);
                            //SetPropertyValue("OriUnitPrice", ref _OriUnitPrice, Session.FindObject<vw_PriceList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)).Price / CurrRate);

                            if (pricelist != null)
                            {
                                UnitPrice = pricelist.Price / CurrRate;
                                UnitPriceAfDisc = pricelist.Price / CurrRate;
                                OriUnitPrice = pricelist.Price / CurrRate;
                            }
                            // End ver TJC
                        }
                    }
                    SetPropertyValue("Discount", ref _Discount, 0);
                    SetPropertyValue("DiscountAmt", ref _DiscountAmt, 0);
                }
            }
        }
        #region MyRegion


        private string _ItemName;
        [XafDisplayName("Item Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string ItemDetail
        {
            get { return _ItemDetail; }
            set { SetPropertyValue("ItemDetail", ref _ItemDetail, value); }
        }

        private double _Quantity;
        [XafDisplayName("Quantity")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ImmediatePostData]
        public double Quantity
        {
            get { return _Quantity; }
            set {
                SetPropertyValue("Quantity", ref _Quantity, value);
                if (value != 0)
                {
                    SetPropertyValue("Discount", ref _Discount, Math.Round(((Quantity * UnitPrice) - LineTotal) / (Quantity * UnitPrice) * 100,6));
                }
                else
                {
                    SetPropertyValue("Discount", ref _Discount, Math.Round(((Quantity * UnitPrice) - LineTotal) / (Quantity * UnitPrice) * 100,6));
                }
            }
        }

        private string _UOM;
        [XafDisplayName("UOM")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [Size(100)]
        public string UOM
        {
            get { return _UOM; }
            set { SetPropertyValue("UOM", ref _UOM, value); }
        }

        private string _Brand;
        [XafDisplayName("Brand")]
        [Appearance("Brand", Enabled = false)]
        [Size(100)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Brand
        {
            get { return _Brand; }
            set { SetPropertyValue("Brand", ref _Brand, value); }
        }

        private string _ItemCategory;
        public string ItemCategory
        {
            get { return _ItemCategory; }
            set { SetPropertyValue("ItemCategory", ref _ItemCategory, value); }
        }

        #endregion
        private vw_WarehouseList _Warehouse;
        [XafDisplayName("Warehouse")]
        [RuleRequiredField(DefaultContexts.Save)]
        [NoForeignKey]
        [Size(100)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public vw_WarehouseList Warehouse
        {
            get { return _Warehouse; }
            set { SetPropertyValue("Warehouse", ref _Warehouse, value); }
        }

        private double _Available;
        [XafDisplayName("Available")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("Available", Enabled = false)]
        public double Available
        {
            get { return _Available; }
            set { SetPropertyValue("Available", ref _Available, value); }
        }
        
        private double _UnitPrice;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("DisableUnitPrice",Enabled = false, Criteria = "OriUnitPrice > 0")]
        public double UnitPrice
        {
            get { return _UnitPrice; }
            set
            {
                SetPropertyValue("UnitPrice", ref _UnitPrice, value);
            }
        }

        private double _UnitPriceAfDisc;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        public double UnitPriceAfDisc
        {
            get { return _UnitPriceAfDisc; }
            set
            {
                SetPropertyValue("UnitPriceAfDisc", ref _UnitPriceAfDisc, value);
                if (!IsLoading)
                {
                    if (value != 0)
                    {
                        SetPropertyValue("Discount", ref _Discount, Math.Round(((Quantity * UnitPrice) - LineTotal) / (Quantity * UnitPrice) * 100,6));
                    }
                    else
                    {
                        SetPropertyValue("Discount", ref _Discount, Math.Round(((Quantity * UnitPrice) - LineTotal) / (Quantity * UnitPrice) * 100,6));
                    }

                }
            }
        }

        private double _OriUnitPrice;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        
        public double OriUnitPrice
        {
            get { return _OriUnitPrice; }
            set
            {
                SetPropertyValue("OriUnitPrice", ref _OriUnitPrice, value);
            }
        }

        private double _Discount;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [ImmediatePostData]
        [XafDisplayName("Discount")]
        [Appearance("DisableDiscount", Enabled = false)]
        public double Discount
        {
            get { return _Discount; }
            set
            {
                SetPropertyValue("Discount", ref _Discount, value);
                //if (!IsLoading)
                //{
                //    if (value != 0)
                //    {
                //        double disc = Math.Round((UnitPrice - UnitPriceAfDisc) / UnitPrice * 100,2);
                //        if (value == disc) SetPropertyValue("DiscountAmt", ref _DiscountAmt, 0);
                //        else SetPropertyValue("DiscountAmt", ref _DiscountAmt, (value - disc) * (Quantity * UnitPriceAfDisc) / 100);
                //    }
                //    else
                //    {
                //        SetPropertyValue("DiscountAmt", ref _DiscountAmt, 0);
                //    }
                    
                //}

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

                if (!IsLoading)
                {
                    if (value != 0)
                    {
                        SetPropertyValue("Discount", ref _Discount, Math.Round(((Quantity * UnitPrice) - LineTotal) / (Quantity * UnitPrice) * 100,6));
                    }
                    else
                    {
                        SetPropertyValue("Discount", ref _Discount, Math.Round(((Quantity * UnitPrice) - LineTotal) / (Quantity * UnitPrice) * 100,6));
                    }
                }

            }
        }

        private vw_OutputTax _TaxCode;
        [NoForeignKey]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public vw_OutputTax TaxCode
        {
            get { return _TaxCode; }
            set { SetPropertyValue("TaxCode", ref _TaxCode, value); }
        }

        private double _LineTotal;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("DisableLineTotal", Enabled = false)]
        public double LineTotal
        {
            get
            {
                _LineTotal = (Quantity * _UnitPriceAfDisc) - DiscountAmt;
                return _LineTotal;
            }
            set { SetPropertyValue("LineTotal", ref _LineTotal, value); }
        }

        private string _Currency;
        [XafDisplayName("Currency")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Currency
        {
            get { return _Currency; }
            set { SetPropertyValue("Currency", ref _Currency, value); }
        }

        private double _CurrRate;
        [XafDisplayName("Currency Rate")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public double CurrRate
        {
            get { return _CurrRate; }
            set { SetPropertyValue("CurrRate", ref _CurrRate, value); }
        }

        private SalesQuotation _SalesQuotation;
        [XafDisplayName("SalesQuotationDetail")]
        [Association("SalesQuotation-SalesQuotationDetail")]
        //[Appearance("SalesQuotation", Enabled = false)]
        public SalesQuotation SalesQuotation
        {
            get { return _SalesQuotation; }
            set { SetPropertyValue("SalesQuotation", ref _SalesQuotation, value); }
        }
    }
}