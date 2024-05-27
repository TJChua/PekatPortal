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

namespace PekatPortal.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Approval : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Approval(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private ApprovalTemplate _TemplateName;
        [NoForeignKey]
        public ApprovalTemplate TemplateName
        {
            get { return _TemplateName; }
            set { SetPropertyValue("TemplateName", ref _TemplateName, value); }
        }

        private SalesQuotation _SalesQuotation;
        [NoForeignKey]
        public SalesQuotation SalesQuotation
        {
            get { return _SalesQuotation; }
            set { SetPropertyValue("SalesQuotation", ref _SalesQuotation, value); }
        }

        private SystemUsers _Approver;
        [NoForeignKey]
        public SystemUsers Approver
        {
            get { return _Approver; }
            set { SetPropertyValue("Approver", ref _Approver, value); }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { SetPropertyValue("Status", ref _Status, value); }
        }

        private string _Remark;
        [Size(SizeAttribute.Unlimited)]
        public string Remark
        {
            get { return _Remark; }
            set { SetPropertyValue("Remark", ref _Remark, value); }
        }

        private DateTime? _ApproveDate;
        public DateTime? ApproveDate
        {
            get { return _ApproveDate; }
            set
            {
                SetPropertyValue("ApproveDate", ref _ApproveDate, value);
            }
        }

        private SystemUsers _CreateUser;
        [NoForeignKey]
        public SystemUsers CreateUser
        {
            get { return _CreateUser; }
            set
            {
                SetPropertyValue("CreateUser", ref _CreateUser, value);
            }
        }

        private DateTime? _CreateDate;
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        private string _Level;
        public string Level
        {
            get { return _Level; }
            set { SetPropertyValue("Level", ref _Level, value); }
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