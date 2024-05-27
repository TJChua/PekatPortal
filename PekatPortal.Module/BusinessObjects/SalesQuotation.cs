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
    //[Appearance("DisableSave", AppearanceItemType.Action, "True", TargetItems = "Save", Visibility = ViewItemVisibility.Hide, Context = "Any")]
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
    [RuleCriteria("SlpCode", DefaultContexts.Save, "SlpCode.SlpCode <> -1", "Please Select Sales Employee [-No Sales Employee- not allowed]")]
    [RuleCriteria("DocDueDate", DefaultContexts.Save, "DocDueDate > DocDate", "Posting Date cannot be later than Validity Date")]
    [RuleCriteria("CurrRate", DefaultContexts.Save, "CurrRate >= 0", "Exchange Rate Missing !")]

    public class SalesQuotation : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SalesQuotation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DocDate = DateTime.Today;
            DocDueDate = DateTime.Today.AddDays(30);
            Status = StatusEnum.Open;
            PredefinedText = Session.FindObject<vw_PredefinedText>(CriteriaOperator.Parse("TextCode=?", "TNC"));
            SlpCode = Session.FindObject<vw_SalesPerson>(CriteriaOperator.Parse("SlpCode=?", "-1"));
            RevisionNo = "1";
            Price = "Ex-KL";
            //if (Session.GetObjectByKey<SystemUsers>(SecuritySystem.c);)
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _DocNum;
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [ImmediatePostData]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public DateTime DocDueDate
        {
            get { return _DocDueDate; }
            set
            {
                SetPropertyValue("DocDueDate", ref _DocDueDate, value);
                if (!IsLoading)
                {
                    SetPropertyValue("Validity", ref _Validity, (DocDueDate.Date - DocDate.Date).TotalDays.ToString() + " Days");
                }
            }
        }

        private vw_CustomerList _DocEntry;
        [XafDisplayName("Customer Code")]
        [ImmediatePostData]
        [NoForeignKey]
        [Size(50)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [DataSourceCriteria("frozenFor = 'N'")]
        public vw_CustomerList DocEntry
        {
            get { return _DocEntry; }
            set
            {
                SetPropertyValue("DocEntry", ref _DocEntry, value);
                if (!IsLoading & value != null)
                {
                    SetPropertyValue("CardCode", ref _CardCode, value.CardCode);
                    SetPropertyValue("CardName", ref _CardName, value.CardName);
                    if (value.BillToDef != null && value.BillToDef.Trim() != "")
                    {
                        // Start TJC
                        //SetPropertyValue("BillToAdress", ref _BillToAdress, value.BillToDef == null ? "" : Session.FindObject<vw_BillToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.BillToDef, value.DocEntry)).Address);
                        //SetPropertyValue("BillToAdress1", ref _BillToAdress1, value.BillToDef == null ? "" : Session.FindObject<vw_BillToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.BillToDef, value.DocEntry)).Street);
                        //SetPropertyValue("BillToAdress2", ref _BillToAdress2, value.BillToDef == null ? "" : Session.FindObject<vw_BillToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.BillToDef, value.DocEntry)).StreetNo);
                        //SetPropertyValue("BillToAdress3", ref _BillToAdress3, value.BillToDef == null ? "" : Session.FindObject<vw_BillToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.BillToDef, value.DocEntry)).Block);
                        //SetPropertyValue("BillToAdress4", ref _BillToAdress4, value.BillToDef == null ? "" : Session.FindObject<vw_BillToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.BillToDef, value.DocEntry)).City);

                        vw_BillToAddress billtoaddress = Session.FindObject<vw_BillToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.BillToDef, value.DocEntry));

                        if (billtoaddress != null)
                        {
                            BillToAdress = billtoaddress.Address;
                            BillToAdress1 = billtoaddress.Street;
                            BillToAdress2 = billtoaddress.StreetNo;
                            BillToAdress3 = billtoaddress.Block;
                            BillToAdress4 = billtoaddress.City;
                        }
                        // End TJC

                    }
                    if (value.ShipToDef != null && value.ShipToDef.Trim() != "")
                    {
                        // Start TJC
                        //SetPropertyValue("ShipToAdress", ref _ShipToAdress, value.ShipToDef == null ? "" : Session.FindObject<vw_ShipToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.ShipToDef, value.DocEntry)).Address);
                        //SetPropertyValue("ShipToAdress1", ref _ShipToAdress1, value.ShipToDef == null ? "" : Session.FindObject<vw_ShipToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.ShipToDef, value.DocEntry)).Street);
                        //SetPropertyValue("ShipToAdress2", ref _ShipToAdress2, value.ShipToDef == null ? "" : Session.FindObject<vw_ShipToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.ShipToDef, value.DocEntry)).StreetNo);
                        //SetPropertyValue("ShipToAdress3", ref _ShipToAdress3, value.ShipToDef == null ? "" : Session.FindObject<vw_ShipToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.ShipToDef, value.DocEntry)).Block);
                        //SetPropertyValue("ShipToAdress4", ref _ShipToAdress4, value.ShipToDef == null ? "" : Session.FindObject<vw_ShipToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.ShipToDef, value.DocEntry)).City);

                        vw_ShipToAddress shiptoaddress = Session.FindObject<vw_ShipToAddress>(CriteriaOperator.Parse("Address=? AND DocEntry=?", value.ShipToDef, value.DocEntry));

                        if (shiptoaddress != null)
                        {
                            ShipToAdress = shiptoaddress.Address;
                            ShipToAdress1 = shiptoaddress.Street;
                            ShipToAdress2 = shiptoaddress.StreetNo;
                            ShipToAdress3 = shiptoaddress.Block;
                            ShipToAdress4 = shiptoaddress.City;
                        }
                        // End TJC
                    }
                    //SetPropertyValue("PaymentTerm", ref _PaymentTerm, Session.FindObject<vw_PaymentTerm>(CriteriaOperator.Parse("GroupNum=?", value.GroupNum)));
                    SetPropertyValue("PaymentTermName", ref _PaymentTermName, Session.FindObject<vw_PaymentTerm>(CriteriaOperator.Parse("GroupNum=?", value.GroupNum)).PymntGroup);
                    if (value.CntctPrsn != null && value.CntctPrsn.Trim() != "")
                    {
                        // Start TJC
                        //SetPropertyValue("ContactPerson", ref _ContactPerson, Session.FindObject<vw_ContactPerson>(CriteriaOperator.Parse("DocEntry=? AND Name =?", value.DocEntry, value.CntctPrsn)));
                        //SetPropertyValue("Name", ref _Name, value.CntctPrsn == null ? "" : Session.FindObject<vw_ContactPerson>(CriteriaOperator.Parse("DocEntry=? AND Name =?", value.DocEntry, value.CntctPrsn)).Name);
                        //SetPropertyValue("Tel1", ref _Tel1, value.CntctPrsn == null ? "" : Session.FindObject<vw_ContactPerson>(CriteriaOperator.Parse("DocEntry=? AND Name =?", value.DocEntry, value.CntctPrsn)).Tel1);
                        //SetPropertyValue("Email", ref _Email, value.CntctPrsn == null ? "" : Session.FindObject<vw_ContactPerson>(CriteriaOperator.Parse("DocEntry=? AND Name =?", value.DocEntry, value.CntctPrsn)).Email);

                        ContactPerson = Session.FindObject<vw_ContactPerson>(CriteriaOperator.Parse("DocEntry=? AND Name =?", value.DocEntry, value.CntctPrsn));

                        //if (ContactPerson != null)
                        //{
                        //    Name = ContactPerson.Name;
                        //    Tel1 = ContactPerson.Tel1;
                        //    Email = ContactPerson.Email;
                        //}

                        // End TJC
                    }
                    SetPropertyValue("CompanyPhone", ref _CompanyPhone, value.CompanyPhone);
                    SetPropertyValue("CompanyFax", ref _CompanyFax, value.CompanyFax);

                    // Start ver TJC
                    //SetPropertyValue("SlpCode", ref _SlpCode, Session.FindObject<vw_SalesPerson>(CriteriaOperator.Parse("SlpCode=?", value.SlpCode)));
                    //SetPropertyValue("Approver", ref _Approver, Session.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode.SlpCode=?", value.SlpCode)).Supervisor);
                    //SetPropertyValue("ApproverPost", ref _ApproverPost, Session.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode.SlpCode=?", value.SlpCode)).SDesignation);
                    SlpCode = Session.FindObject<vw_SalesPerson>(CriteriaOperator.Parse("SlpCode=?", value.SlpCode));
                    // End ver TJC
                    SetPropertyValue("Currency", ref _Currency, value.Currency);
                    if (value.Currency == "MYR")
                    {
                        SetPropertyValue("CurrRate", ref _CurrRate, 1.000000);
                    }
                    else
                    {
                        vw_ExchangeRate rate = Session.FindObject<vw_ExchangeRate>(CriteriaOperator.Parse("RateDate=? AND Currency = ?", DocDate, value.Currency));
                        if (rate == null)
                        {
                            SetPropertyValue("CurrRate", ref _CurrRate, 0.000000);
                        }
                        else
                        {
                            SetPropertyValue("CurrRate", ref _CurrRate, rate.Rate);
                        }

                    }
                }
            }

        }

        private string _CardCode;
        [XafDisplayName("Customer Code")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CardCode
        {
            get { return _CardCode; }
            set { SetPropertyValue("CardCode", ref _CardCode, value); }
        }

        private string _CardName;
        [XafDisplayName("Customer Name")]
        [Size(100)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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

        private vw_ContactPerson _ContactPerson;
        [XafDisplayName("ContactPerson")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [DataSourceCriteria("'@this.DocEntry' = DocEntry")]
        [NoForeignKey]
        [ImmediatePostData]
        public vw_ContactPerson ContactPerson
        {
            get { return _ContactPerson; }
            set
            {
                //SetPropertyValue("Reference", ref _ContactPerson, value);
                if (!IsLoading & value != null)
                {
                    SetPropertyValue("Name", ref _Name, value.Name);
                    SetPropertyValue("Phone", ref _Tel1, value.Tel1);
                    SetPropertyValue("Email", ref _Email, value.Email);
                }
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
        public string Reference
        {
            get { return _Reference; }
            set { SetPropertyValue("Reference", ref _Reference, value); }
        }

        private string _Remark;
        [XafDisplayName("Remark")]
        [Size(254)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Remark
        {
            get { return _Remark; }
            set { SetPropertyValue("Remark", ref _Remark, value); }
        }

        private string _Header;
        [XafDisplayName("Quotation Title")]
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Header
        {
            get { return _Header; }
            set { SetPropertyValue("Header", ref _Header, value); }
        }

        private string _Price;
        [XafDisplayName("Price")]
        [Size(50)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Price
        {
            get { return _Price; }
            set { SetPropertyValue("Price", ref _Price, value); }
        }

        private vw_DeliveryTerm _DeliveryCode;
        [XafDisplayName("DeliveryCode")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [NoForeignKey]
        [ImmediatePostData]
        public vw_DeliveryTerm DeliveryCode
        {
            get { return _DeliveryCode; }
            set
            {
                SetPropertyValue("DeliveryCode", ref _DeliveryCode, value);
                if (!IsLoading && value != null)
                {
                    SetPropertyValue("Delivery", ref _Delivery, value.U_Description);
                }
            }
        }

        private string _Delivery;
        [XafDisplayName("Delivery")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Delivery
        {
            get { return _Delivery; }
            set { SetPropertyValue("Delivery", ref _Delivery, value); }
        }

        private string _Validity;
        [XafDisplayName("Validity")]
        [Size(50)]
        [Appearance("Validity", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Validity
        {
            get { return _Validity; }
            set { SetPropertyValue("Validity", ref _Validity, value); }
        }

        //private vw_PaymentTerm _PaymentTerm;
        //[XafDisplayName("PaymentTerm")]
        //[NoForeignKey]
        //[ImmediatePostData]
        //[VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[Appearance("PaymentTerm", Enabled = false)]
        //public vw_PaymentTerm PaymentTerm
        //{
        //    get { return _PaymentTerm; }
        //    set
        //    {
        //        SetPropertyValue("PaymentTerm", ref _PaymentTerm, value);
        //        if (!IsLoading && value != null)
        //        {
        //            SetPropertyValue("PaymentTermName", ref _PaymentTermName, value.PymntGroup);
        //        }
        //    }
        //}

        private string _PaymentTermName;
        [XafDisplayName("PaymentTermName")]
        [Size(50)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("PaymentTermName", Enabled = false)]
        public string PaymentTermName
        {
            get { return _PaymentTermName; }
            set { SetPropertyValue("PaymentTermName", ref _PaymentTermName, value); }
        }

        private vw_WarrantyTerm _WarrantyCode;
        [XafDisplayName("WarrantyCode")]
        [Size(50)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [NoForeignKey]
        [ImmediatePostData]
        public vw_WarrantyTerm WarrantyCode
        {
            get { return _WarrantyCode; }
            set
            {
                SetPropertyValue("WarrantyCode", ref _WarrantyCode, value);
                if (!IsLoading && value != null)
                {
                    SetPropertyValue("Warranty", ref _Warranty, value.U_Description);
                }
            }
        }


        private string _Warranty;
        [XafDisplayName("Warranty")]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Warranty
        {
            get { return _Warranty; }
            set { SetPropertyValue("Warranty", ref _Warranty, value); }
        }


        private vw_PredefinedText _PredefinedText;
        [XafDisplayName("Terms & Condition")]
        [Size(254)]
        [ImmediatePostData]
        [NoForeignKey]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public vw_PredefinedText PredefinedText
        {
            get { return _PredefinedText; }
            set
            {
                SetPropertyValue("PredefinedText", ref _PredefinedText, value);

                if (!IsLoading && value != null)
                {
                    SetPropertyValue("Footer", ref _Footer, value.Text);
                }
            }
        }


        private string _Footer;
        [XafDisplayName("Terms & Condition")]
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Footer
        {
            get { return _Footer; }
            set { SetPropertyValue("Footer", ref _Footer, value); }
        }


        private bool _IsPost;
        [XafDisplayName("Post")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(70), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        [Index(70), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Status")]
        public StatusEnum Status
        {
            get { return _Status; }
            set { SetPropertyValue("Status", ref _Status, value); }
        }


        //private vw_BillToAddress _BillToDef;
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DataSourceCriteria("'@this.DocEntry' = DocEntry")]
        //[NoForeignKey]
        //[ImmediatePostData]
        //public vw_BillToAddress BillToDef
        //{
        //    get { return _BillToDef; }
        //    set
        //    {
        //        SetPropertyValue("BillToDef", ref _BillToDef, value);

        //        if (!IsLoading & value != null)
        //        {
        //            SetPropertyValue("BillToAddress", ref _BillToAdress, value.Address);
        //            SetPropertyValue("BillToAddress1", ref _BillToAdress1, value.Street);
        //            SetPropertyValue("BillToAddress2", ref _BillToAdress2, value.StreetNo);
        //            SetPropertyValue("BillToAddress3", ref _BillToAdress3, value.Block);
        //            SetPropertyValue("BillToAddress4", ref _BillToAdress4, value.City);
        //        }
        //    }
        //}

        private string _BillToAdress;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BillToAdress
        {
            get { return _BillToAdress; }
            set { SetPropertyValue("BillToAdress", ref _BillToAdress, value); }

        }


        private string _BillToAdress1;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BillToAdress1
        {
            get { return _BillToAdress1; }
            set { SetPropertyValue("BillToAdress1", ref _BillToAdress1, value); }

        }

        private string _BillToAdress2;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BillToAdress2
        {
            get { return _BillToAdress2; }
            set { SetPropertyValue("BillToAdress2", ref _BillToAdress2, value); }

        }

        private string _BillToAdress3;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BillToAdress3
        {
            get { return _BillToAdress3; }
            set { SetPropertyValue("BillToAdress3", ref _BillToAdress3, value); }

        }

        private string _BillToAdress4;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string BillToAdress4
        {
            get { return _BillToAdress4; }
            set { SetPropertyValue("BillToAdress4", ref _BillToAdress4, value); }

        }

        //private vw_ShipToAddress _ShipToDef;
        //[DataSourceCriteria("'@this.DocEntry' = DocEntry")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[ImmediatePostData]
        //[NoForeignKey]
        //public vw_ShipToAddress ShipToDef
        //{
        //    get { return _ShipToDef; }
        //    set
        //    {
        //        SetPropertyValue("ShipToDef", ref _ShipToDef, value);
        //        if (!IsLoading & value != null)
        //        {
        //            SetPropertyValue("ShipToAddress", ref _ShipToAdress, value.Address);
        //            SetPropertyValue("ShipToAddress1", ref _ShipToAdress1, value.Street);
        //            SetPropertyValue("ShipToAddress2", ref _ShipToAdress2, value.StreetNo);
        //            SetPropertyValue("ShipToAddress3", ref _ShipToAdress3, value.Block);
        //            SetPropertyValue("ShipToAddress4", ref _ShipToAdress4, value.City);
        //        }
        //    }

        //}

        private string _ShipToAdress;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ShipToAdress
        {
            get { return _ShipToAdress; }
            set { SetPropertyValue("ShipToAdress", ref _ShipToAdress, value); }

        }

        private string _ShipToAdress1;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ShipToAdress1
        {
            get { return _ShipToAdress1; }
            set { SetPropertyValue("ShipToAdress1", ref _ShipToAdress1, value); }

        }

        private string _ShipToAdress2;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ShipToAdress2
        {
            get { return _ShipToAdress2; }
            set { SetPropertyValue("ShipToAdress2", ref _ShipToAdress2, value); }

        }

        private string _ShipToAdress3;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ShipToAdress3
        {
            get { return _ShipToAdress3; }
            set { SetPropertyValue("ShipToAdress3", ref _ShipToAdress3, value); }

        }

        private string _ShipToAdress4;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ShipToAdress4
        {
            get { return _ShipToAdress4; }
            set { SetPropertyValue("ShipToAdress4", ref _ShipToAdress4, value); }
        }

        private string _Approver;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Approver
        {
            get { return _Approver; }
            set { SetPropertyValue("Approver", ref _Approver, value); }

        }

        private string _ApproverPost;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ApproverPost
        {
            get { return _ApproverPost; }
            set { SetPropertyValue("ApproverPost", ref _ApproverPost, value); }

        }

        private vw_SalesPerson _SlpCode;
        [NoForeignKey]
        [ImmediatePostData]
        //[Association("MaterialRequest-vw_ProjectList"), DevExpress.Xpo.Aggregated]//Save Detail together with Header.
        [XafDisplayName("Salesperson")]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public vw_SalesPerson SlpCode
        {
            get { return _SlpCode; }
            set
            {
                SetPropertyValue("SlpCode", ref _SlpCode, value);
                if (!IsLoading)
                {
                    SetPropertyValue("Approver", ref _Approver, Session.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=?", value.SlpCode)).Supervisor);
                    SetPropertyValue("ApproverPost", ref _ApproverPost, Session.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=?", value.SlpCode)).SDesignation);
                }
            }
        }

        private double _TotalBeforeDiscount;
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("TotalBeforeDiscount", Enabled = false)]
        public double TotalBeforeDiscount
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    double temp = 0;
                    if (SalesQuotationDetail != null)
                    {
                        temp += SalesQuotationDetail.Sum(p => p.LineTotal);
                    }
                    return temp;
                }
                else
                {
                    return _TotalBeforeDiscount;
                }
            }
            set { SetPropertyValue("TotalBeforeDiscount", ref _TotalBeforeDiscount, value); }
        }

        private double _Discount;
        [ImmediatePostData(true)]
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
                if (!IsLoading)
                {
                    if (value != 0)
                    {
                        SetPropertyValue("DiscountAmt", ref _DiscountAmt, value * TotalBeforeDiscount / 100);
                    }
                    else
                    {
                        SetPropertyValue("DiscountAmt", ref _DiscountAmt, 0);
                    }

                }
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
                if (Session.IsObjectsSaving != true)
                {
                    double temp = 0;

                    if (SalesQuotationDetail != null)
                    {
                        temp += SalesQuotationDetail.Sum(p => p.LineTotal);
                        //ZNS0001
                        //ZNS0003
                    }
                    temp = temp * Discount / 100;
                    return temp;
                    //return _DiscountAmt;
                }
                else
                {
                    return _DiscountAmt;
                }
            }
            set
            {
                SetPropertyValue("DiscountAmt", ref _DiscountAmt, value);
                if (!IsLoading)
                {
                    if (value != 0)
                    {
                        SetPropertyValue("Discount", ref _Discount, value / TotalBeforeDiscount * 100);
                    }
                    else
                    {
                        SetPropertyValue("Discount", ref _Discount, 0);
                    }
                }
            }
        }

        private double _TaxAmount;
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("TaxAmount", Enabled = false)]
        [XafDisplayName("Tax Amount")]
        public double TaxAmount
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    double temp = 0;
                    if (SalesQuotationDetail != null)
                    {
                        temp += SalesQuotationDetail.Sum(p => p.LineTotal * p.TaxCode.Rate / 100);
                    }
                    return temp;
                }
                else
                {
                    return _TaxAmount;
                }
            }
            set { SetPropertyValue("TaxAmount", ref _TaxAmount, value); }
        }

        private double _GrandTotal;
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [Appearance("GrandTotal", Enabled = false)]
        public double GrandTotal
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    double _grandtotal = 0;
                    _grandtotal = TotalBeforeDiscount - DiscountAmt + TaxAmount;
                    return _grandtotal;
                }
                else
                {
                    return _GrandTotal;
                }
            }
            set { SetPropertyValue("GrandTotal", ref _GrandTotal, value); }
        }


        private string _CreateUser;
        [XafDisplayName("Create User")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CreateUser
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

        private string _UpdateUser;
        [XafDisplayName("Update User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string UpdateUser
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

        private Company _Company;
        [XafDisplayName("Company")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public Company Company
        {
            get { return _Company; }
            set
            {
                SetPropertyValue("Company", ref _Company, value);
            }
        }


        [Association("SalesQuotation-SalesQuotationDetail")]//Save Detail together with Header.
        [XafDisplayName("Item")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<SalesQuotationDetail> SalesQuotationDetail
        {
            get { return GetCollection<SalesQuotationDetail>("SalesQuotationDetail"); }
        }

        [Association("SalesQuotation-SalesQuotationRev")]//Save Detail together with Header.
        [XafDisplayName("Revision")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<SalesQuotationRev> SalesQuotationRev
        {
            get { return GetCollection<SalesQuotationRev>("SalesQuotationRev"); }
        }

        [Association("SalesQuotation-DocStatus")]//Save Detail together with Header.
        [XafDisplayName("Approval Status")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<DocStatus> DocStatus
        {
            get { return GetCollection<DocStatus>("DocStatus"); }
        }

        private SystemUsers _Escalate;
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Escalate")]
        public SystemUsers Escalate
        {
            get { return _Escalate; }
            set { SetPropertyValue("Escalate", ref _Escalate, value); }
        }

        private string _RevisionNo;
        [XafDisplayName("Version No.")]
        [Appearance("RevisionNo", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Index(1)]
        public string RevisionNo
        {
            get { return _RevisionNo; }
            set { SetPropertyValue("RevisionNo", ref _RevisionNo, value); }
        }

        private string _PostRevision;
        [XafDisplayName("Posted Version No.")]
        [Appearance("PostRevision", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string PostRevision
        {
            get { return _PostRevision; }
            set { SetPropertyValue("PostRevision", ref _PostRevision, value); }
        }

        private string _IsRejected;
        [XafDisplayName("Is Rejected")]
        [Appearance("IsRejected", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string IsRejected
        {
            get { return _IsRejected; }
            set { SetPropertyValue("IsRejected", ref _IsRejected, value); }
        }

        private string _IsApproved;
        [XafDisplayName("Is Approved")]
        [Appearance("IsApproved", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string IsApproved
        {
            get { return _IsApproved; }
            set { SetPropertyValue("IsApproved", ref _IsApproved, value); }
        }

        private string _CompanyPhone;
        [XafDisplayName("Phone No.")]
        [Appearance("CompanyPhone", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CompanyPhone
        {
            get { return _CompanyPhone; }
            set { SetPropertyValue("CompanyPhone", ref _CompanyPhone, value); }
        }

        private string _CompanyFax;
        [XafDisplayName("Fax No.")]
        [Appearance("CompanyFax", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CompanyFax
        {
            get { return _CompanyFax; }
            set { SetPropertyValue("CompanyFax", ref _CompanyFax, value); }
        }

        [Association("SalesQuotation-SalesQuotationAttachments")]
        [FileTypeFilter("DocumentFiles", 1, "*.pdf")]
        [XafDisplayName("Attachment")]
        public XPCollection<Attachment> SalesQuotationAttachments
        {
            get { return GetCollection<Attachment>("SalesQuotationAttachments"); }
        }

        private string _CurrentAppLevel;
        [XafDisplayName("CurrentAppLevel")]
        [Appearance("CurrentAppLevel", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CurrentAppLevel
        {
            get { return _CurrentAppLevel; }
            set { SetPropertyValue("CurrentAppLevel", ref _CurrentAppLevel, value); }
        }

        private string _CurrentAppTmp;
        [XafDisplayName("CurrentAppTmp")]
        [Appearance("CurrentAppTmp", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CurrentAppTmp
        {
            get { return _CurrentAppTmp; }
            set { SetPropertyValue("CurrentAppTmp", ref _CurrentAppTmp, value); }
        }

        private string _CurrentAppTmpName;
        [XafDisplayName("CurrentAppTmpName")]
        [Appearance("CurrentAppTmpName", Enabled = false)]
        [Size(100)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CurrentAppTmpName
        {
            get { return _CurrentAppTmpName; }
            set { SetPropertyValue("CurrentAppTmpName", ref _CurrentAppTmpName, value); }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            SystemUsers salesperson = Session.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=?", SlpCode.SlpCode));
            if (!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                    && (Session.ObjectLayer is SimpleObjectLayer)
                        )
            {

                if (Session.IsNewObject(this))
                {
                    CreateUser = Session.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId).FullName.ToString();
                    CreateDate = DateTime.Now;
                    Company = Session.FindObject<Company>(CriteriaOperator.Parse("Oid", user.Company.Oid));
                    CurrentAppLevel = "0";
                    CurrentAppTmp = "0";
                    CurrentAppTmpName = "";

                    if (Status == StatusEnum.Draft || Status == StatusEnum.Open)
                    {
                        if (user.SalesPersonCode.SlpCode != SlpCode.SlpCode && salesperson.RoutetoDraft == "Y") Status = StatusEnum.Draft;
                        else Status = StatusEnum.Open;
                    }
                    DocNum = Session.FindObject<DocumentNumber>(CriteriaOperator.Parse("Company", user.Company.Oid)).SQNextNo.ToString();
                    if (salesperson.Escalate != null) Escalate = salesperson.Escalate;
                    DocumentNumber cmnpy = Session.FindObject<DocumentNumber>(CriteriaOperator.Parse("Company", user.Company.Oid));
                    cmnpy.SQNextNo = cmnpy.SQNextNo + 1;
                    cmnpy.Save();
                }
                else
                {
                    if (Status == StatusEnum.Draft || Status == StatusEnum.Open || (Status == StatusEnum.Rejected && IsRejected == "N") || 
                        (Status == StatusEnum.Approved && IsApproved == "N"))
                    {
                        if (user.SalesPersonCode.SlpCode != SlpCode.SlpCode && salesperson.RoutetoDraft == "Y") Status = StatusEnum.Draft;
                        else Status = StatusEnum.Open;
                    }
                    if (IsRejected == "Y" || IsApproved == "Y")
                    {
                        IsRejected = "N";
                        IsApproved = "N";
                        CurrentAppLevel = "0";
                        CurrentAppTmp = "0" ;
                    }
                    //if (Status == StatusEnum.Rejected) ? Update reject document or approval reject 
                    //Status = StatusEnum.Open;
                    UpdateUser = Session.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId).FullName.ToString();
                    UpdateDate = DateTime.Now;
                }
            }
           
            
        }
        protected override void OnSaved()
        {
            base.OnSaved();
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

public enum StatusEnum
{
    Draft = 0,
    Open = 1,
    Canceled = 2,
    Approval = 3,
    Approved = 4,
    Posted = 5,
    Rejected = 6
}