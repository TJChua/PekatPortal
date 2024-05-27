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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PekatPortal.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("FullName")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("DisableDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
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
    [Appearance("DisableQuotationLimit", AppearanceItemType.ViewItem, "True", TargetItems = "QuotationLimit", Enabled = false, Criteria = "IsAdmin = false", Context = "Any")]
    public class SystemUsers : PermissionPolicyUser
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SystemUsers(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        //public string UserEmail { get; set; }
        //[RuleRequiredField(DefaultContexts.Save)]
        [NoForeignKey]
        public vw_SalesPerson SalesPersonCode { get; set; }

        private Company _Company;
        [Association("Company-SystemUsers")]
        public Company Company
        {
            get { return _Company; }
            set { SetPropertyValue("Company", ref _Company, value); }
        }

        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set { SetPropertyValue("FullName", ref _FullName, value); }
        }

        private string _Designation;
        public string Designation
        {
            get { return _Designation; }
            set { SetPropertyValue("Designation", ref _Designation, value); }
        }


        private string _Supervisor;
        public string Supervisor
        {
            get { return _Supervisor; }
            set { SetPropertyValue("Supervisor", ref _Supervisor, value); }
        }

        private string _SDesignation;
        public string SDesignation
        {
            get { return _SDesignation; }
            set { SetPropertyValue("SDesignation", ref _SDesignation, value); }
        }

        private string _RoutetoDraft;
        public string RoutetoDraft
        {
            get { return _RoutetoDraft; }
            set { SetPropertyValue("RoutetoDraft", ref _RoutetoDraft, value); }
        }

        private string _AllowAmend;
        public string AllowAmend
        {
            get { return _AllowAmend; }
            set { SetPropertyValue("AllowAmend", ref _AllowAmend, value); }
        }

        private string _ContactNo;
        public string ContactNo
        {
            get { return _ContactNo; }
            set { SetPropertyValue("ContactNo", ref _ContactNo, value); }
        }

        private SystemUsers _Escalate;
        [NoForeignKey]
        [DataSourceCriteria("Designation = 'Sales Manager'")]
        public SystemUsers Escalate
        {
            get { return _Escalate; }
            set { SetPropertyValue("Escalate", ref _Escalate, value); }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetPropertyValue("Email", ref _Email, value); }
        }

        private double _QuotationLimit;
        public double QuotationLimit
        {
            get { return _QuotationLimit; }
            set { SetPropertyValue("QuotationLimit", ref _QuotationLimit, value); }
        }

        [Browsable(false), NonPersistent]
        public bool IsAdmin { get; set; }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            IsAdmin = false;

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            if (user != null)
            {
                if (user.Roles.Where(p => p.Name == "Administrators").Count() > 0)
                {
                    this.IsAdmin = true;
                }
            }
        }
        //[NoForeignKey]
        //public vw_vendors Vendor { get; set; }

        //[Browsable(false)]
        //public string CurrDept { get; set; }

        //[Browsable(false)]
        //public string CurrDocType { get; set; }

        //[Association("UserDepartment")]
        //[XafDisplayName("Department")]
        //public XPCollection<Departments> Department
        //{
        //    get { return GetCollection<Departments>("Department"); }
        //}

        //[Browsable(false)]
        //[Association("ApprovalTriggers")]
        //[XafDisplayName("Trigger User")]
        //public XPCollection<Approvals> TriggerApproval
        //{
        //    get { return GetCollection<Approvals>("TriggerApproval"); }
        //}
        //[Browsable(false)]
        //[Association("ApprovalUsers")]
        //[XafDisplayName("Approve User")]
        //public XPCollection<Approvals> UserApproval
        //{
        //    get { return GetCollection<Approvals>("UserApproval"); }
        //}
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