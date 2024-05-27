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
    [Appearance("DisableSave", AppearanceItemType.Action, "True", TargetItems = "Save", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableNew", AppearanceItemType.Action, "True", TargetItems = "btnNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePost", AppearanceItemType.Action, "True", TargetItems = "btnPost", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDuplicate", AppearanceItemType.Action, "True", TargetItems = "btnDuplicate", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrint", AppearanceItemType.Action, "True", TargetItems = "btnPrint", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableConfirm", AppearanceItemType.Action, "True", TargetItems = "btnConfirm", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableView", AppearanceItemType.Action, "True", TargetItems = "btnView", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableStatement", AppearanceItemType.Action, "True", TargetItems = "btnStatement", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("DisablePrintRevision", AppearanceItemType.Action, "True", TargetItems = "btnPrintRvs", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("DisablePostRevision", AppearanceItemType.Action, "True", TargetItems = "btnPostRev", Visibility = ViewItemVisibility.Hide, Context = "Any")]

    public class SalesQuotationRev : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SalesQuotationRev(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string _DocNum;
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(2)]
        [XafDisplayName("Document No")]
        [Appearance("DocNum", Enabled = false)]
        public string DocNum
        {
            get
            {
                return _DocNum;
            }
            set
            {
                SetPropertyValue("DocNum", ref _DocNum, value);
            }
        }

        private DateTime _DocDate;
        [XafDisplayName("Posting Date")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(3)]
        public DateTime DocDate
        {
            get { return _DocDate; }
            set { SetPropertyValue("DocDate", ref _DocDate, value); }
        }

        private DateTime _DocDueDate;
        [XafDisplayName("Validity Date")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(4)]
        public DateTime DocDueDate
        {
            get { return _DocDueDate; }
            set { SetPropertyValue("DocDueDate", ref _DocDueDate, value); }
        }

        private string _DocEntry;
        [XafDisplayName("Customer Code")]
        [Size(50)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Index(5)]
        public string DocEntry
        {
            get { return _DocEntry; }
            set
            {
                SetPropertyValue("DocEntry", ref _DocEntry, value);
            }
        }

        private string _CardCode;
        [XafDisplayName("Customer Code")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(6)]
        public string CardCode
        {
            get { return _CardCode; }
            set { SetPropertyValue("CardCode", ref _CardCode, value); }
        }

        private string _CardName;
        [XafDisplayName("Customer Name")]
        [Size(100)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(6)]
        public string CardName
        {
            get { return _CardName; }
            set { SetPropertyValue("CardName", ref _CardName, value); }
        }

        private string _Currency;
        [XafDisplayName("Currency")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("Currency", Enabled = false)]
        public string Currency
        {
            get { return _Currency; }
            set { SetPropertyValue("Currency", ref _Currency, value); }
        }

        private double _CurrRate;
        [XafDisplayName("Currency Rate")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("CurrRate", Enabled = false)]
        public double CurrRate
        {
            get { return _CurrRate; }
            set { SetPropertyValue("CurrRate", ref _CurrRate, value); }
        }

        private string _ContactPerson;
        [XafDisplayName("ContactPerson")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set
            {
                SetPropertyValue("ContactPerson", ref _ContactPerson, value);
            }
        }

        private string _Name;
        [XafDisplayName("Name")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }

        private string _Tel1;
        [XafDisplayName("Phone")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Tel1
        {
            get { return _Tel1; }
            set { SetPropertyValue("Tel1", ref _Tel1, value); }
        }

        private string _Email;
        [XafDisplayName("Email")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Email
        {
            get { return _Email; }
            set { SetPropertyValue("Email", ref _Email, value); }
        }

        private string _Reference;
        [XafDisplayName("Reference")]
        [Size(100)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(7)]
        public string Reference
        {
            get { return _Reference; }
            set { SetPropertyValue("Reference", ref _Reference, value); }
        }

        private string _RevisionNo;
        [XafDisplayName("Version No.")]
        [Appearance("RevisionNo", Enabled = false)]
        [Size(100)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(1)]
        public string RevisionNo
        {
            get { return _RevisionNo; }
            set { SetPropertyValue("RevisionNo", ref _RevisionNo, value); }
        }

        private string _Remark;
        [XafDisplayName("Remark")]
        [Size(254)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(8)]
        public string Remark
        {
            get { return _Remark; }
            set { SetPropertyValue("Remark", ref _Remark, value); }
        }

        private string _Header;
        [XafDisplayName("Quotation Title")]
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(9)]
        public string Header
        {
            get { return _Header; }
            set { SetPropertyValue("Header", ref _Header, value); }
        }

        private string _Price;
        [XafDisplayName("Price")]
        [Size(30)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(10)]
        public string Price
        {
            get { return _Price; }
            set { SetPropertyValue("Price", ref _Price, value); }
        }

        private string _Delivery;
        [XafDisplayName("Delivery")]
        [Size(50)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(11)]
        public string Delivery
        {
            get { return _Delivery; }
            set { SetPropertyValue("Delivery", ref _Delivery, value); }
        }

        private string _Validity;
        [XafDisplayName("Validity")]
        [Size(30)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(12)]
        public string Validity
        {
            get { return _Validity; }
            set { SetPropertyValue("Validity", ref _Validity, value); }
        }

        private string _PaymentTerm;
        [XafDisplayName("PaymentTerm")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Index(13)]
        public string PaymentTerm
        {
            get { return _PaymentTerm; }
            set
            {
                SetPropertyValue("PaymentTerm", ref _PaymentTerm, value);
            }
        }

        private string _PaymentTermName;
        [XafDisplayName("PaymentTermName")]
        [Index(14)]
        [Size(30)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string PaymentTermName
        {
            get { return _PaymentTermName; }
            set { SetPropertyValue("PaymentTermName", ref _PaymentTermName, value); }
        }

        private string _Warranty;
        [XafDisplayName("Warranty")]
        [Size(100)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(15)]
        public string Warranty
        {
            get { return _Warranty; }
            set { SetPropertyValue("Warranty", ref _Warranty, value); }
        }


        private string _PredefinedText;
        [XafDisplayName("Terms & Condition")]
        [Size(254)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Index(16)]
        public string PredefinedText
        {
            get { return _PredefinedText; }
            set
            {
                SetPropertyValue("PredefinedText", ref _PredefinedText, value);
            }
        }


        private string _Footer;
        [XafDisplayName("Terms & Condition")]
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(17)]
        public string Footer
        {
            get { return _Footer; }
            set { SetPropertyValue("Footer", ref _Footer, value); }
        }


        private bool _IsPost;
        [XafDisplayName("Post")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(18), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("IsPost", Enabled = false)]
        public bool IsPost
        {
            get { return _IsPost; }
            set
            {
                SetPropertyValue("IsPost", ref _IsPost, value);
            }
        }

        private bool _IsCanceled;
        [XafDisplayName("Canceled")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(19), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("IsCanceled", Enabled = false)]
        public bool IsCanceled
        {
            get { return _IsCanceled; }
            set
            {
                SetPropertyValue("IsCanceled", ref _IsCanceled, value);
            }
        }

        private StatusEnum _Status;
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Status")]
        public StatusEnum Status
        {
            get { return _Status; }
            set { SetPropertyValue("Status", ref _Status, value); }
        }


        private string _BillToDef;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BillToDef
        {
            get { return _BillToDef; }
            set
            {
                SetPropertyValue("BillToDef", ref _BillToDef, value);
            }
        }

        private string _BillToAdress;
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(20)]
        public string BillToAdress
        {
            get { return _BillToAdress; }
            set { SetPropertyValue("BillToAdress", ref _BillToAdress, value); }

        }


        private string _BillToAdress1;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(21)]
        public string BillToAdress1
        {
            get { return _BillToAdress1; }
            set { SetPropertyValue("BillToAdress1", ref _BillToAdress1, value); }

        }

        private string _BillToAdress2;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(22)]
        public string BillToAdress2
        {
            get { return _BillToAdress2; }
            set { SetPropertyValue("BillToAdress2", ref _BillToAdress2, value); }

        }

        private string _BillToAdress3;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(24)]
        public string BillToAdress3
        {
            get { return _BillToAdress3; }
            set { SetPropertyValue("BillToAdress3", ref _BillToAdress3, value); }

        }

        private string _BillToAdress4;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(25)]
        public string BillToAdress4
        {
            get { return _BillToAdress4; }
            set { SetPropertyValue("BillToAdress4", ref _BillToAdress4, value); }

        }

        private string _ShipToDef;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(26)]
        public string ShipToDef
        {
            get { return _ShipToDef; }
            set
            {
                SetPropertyValue("ShipToDef", ref _ShipToDef, value);
            }

        }

        private string _ShipToAdress;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(27)]
        public string ShipToAdress
        {
            get { return _ShipToAdress; }
            set { SetPropertyValue("ShipToAdress", ref _ShipToAdress, value); }

        }

        private string _ShipToAdress1;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(28)]
        public string ShipToAdress1
        {
            get { return _ShipToAdress1; }
            set { SetPropertyValue("ShipToAdress1", ref _ShipToAdress1, value); }

        }

        private string _ShipToAdress2;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(29)]
        public string ShipToAdress2
        {
            get { return _ShipToAdress2; }
            set { SetPropertyValue("ShipToAdress2", ref _ShipToAdress2, value); }

        }

        private string _ShipToAdress3;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(30)]
        public string ShipToAdress3
        {
            get { return _ShipToAdress3; }
            set { SetPropertyValue("ShipToAdress3", ref _ShipToAdress3, value); }

        }

        private string _ShipToAdress4;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Index(31)]
        public string ShipToAdress4
        {
            get { return _ShipToAdress4; }
            set { SetPropertyValue("ShipToAdress4", ref _ShipToAdress4, value); }

        }

        private string _SlpCode;
        //[Association("MaterialRequest-vw_ProjectList"), DevExpress.Xpo.Aggregated]//Save Detail together with Header.
        [XafDisplayName("Salesperson")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SlpCode
        {
            get { return _SlpCode; }
            set { SetPropertyValue("SlpCode", ref _SlpCode, value); }
        }

        private string _SlpName;
        //[Association("MaterialRequest-vw_ProjectList"), DevExpress.Xpo.Aggregated]//Save Detail together with Header.
        [XafDisplayName("Salesperson")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SlpName
        {
            get { return _SlpName; }
            set { SetPropertyValue("SlpName", ref _SlpName, value); }
        }


        private double _TotalBeforeDiscount;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("TotalBeforeDiscount", Enabled = false)]
        public double TotalBeforeDiscount
        {
            get
            {
                return _TotalBeforeDiscount;
            }
            set { SetPropertyValue("TotalBeforeDiscount", ref _TotalBeforeDiscount, value); }
        }

        private double _Discount;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [XafDisplayName("Discount %")]
        public double Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                SetPropertyValue("Discount", ref _Discount, value);
            }
        }

        private double _DiscountAmt;
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

        private double _TaxAmount;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("TaxAmount", Enabled = false)]
        [XafDisplayName("Tax Amount")]
        public double TaxAmount
        {
            get
            {
                return _TaxAmount;
            }
            set { SetPropertyValue("TaxAmount", ref _TaxAmount, value); }
        }

        private double _GrandTotal;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("GrandTotal", Enabled = false)]
        public double GrandTotal
        {
            get
            {
                return _GrandTotal;
            }
            set { SetPropertyValue("GrandTotal", ref _GrandTotal, value); }
        }


        private SystemUsers _CreateUser;
        [XafDisplayName("Create User")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public SystemUsers CreateUser
        {
            get { return _CreateUser; }
            set
            {
                SetPropertyValue("CreateUser", ref _CreateUser, value);
            }
        }

        private DateTime? _CreateDate;
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        private SystemUsers _UpdateUser;
        [XafDisplayName("Update User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public SystemUsers UpdateUser
        {
            get { return _UpdateUser; }
            set
            {
                SetPropertyValue("UpdateUser", ref _UpdateUser, value);
            }
        }

        private DateTime? _UpdateDate;
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                SetPropertyValue("UpdateDate", ref _UpdateDate, value);
            }
        }


        private int _Company;
        [XafDisplayName("Company")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int Company
        {
            get { return _Company; }
            set
            {
                SetPropertyValue("Company", ref _Company, value);
            }
        }

        [Association("SalesQuotationRev-SalesQuotationRevDetail")]//Save Detail together with Header.
        [XafDisplayName("Item")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<SalesQuotationRevDetail> SalesQuotationRevDetail
        {
            get { return GetCollection<SalesQuotationRevDetail>("SalesQuotationRevDetail"); }
        }

        private SalesQuotation _SalesQuotation;
        [XafDisplayName("Revision")]
        [Association("SalesQuotation-SalesQuotationRev")]
        [Appearance("SalesQuotation", Enabled = false)]
        public SalesQuotation SalesQuotation
        {
            get { return _SalesQuotation; }
            set { SetPropertyValue("SalesQuotation", ref _SalesQuotation, value); }
        }

        //private SalesQuotation _SalesQuotationRev;
        //[NoForeignKey]
        //[Appearance("SalesQuotationRev", Enabled = false)]
        //public SalesQuotation SalesQuotationRevs
        //{
        //    get { return _SalesQuotationRev; }
        //    set { SetPropertyValue("SalesQuotationRevs", ref _SalesQuotationRev, value); }
        //}

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                    && (Session.ObjectLayer is SimpleObjectLayer)
                        )
            {

                if (Session.IsNewObject(this))
                {
                    SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                    CreateUser = Session.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    CreateDate = DateTime.Now;
                }
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            this.Reload();
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
