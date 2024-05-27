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
    public class EmailHistory : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public EmailHistory(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        
        private DateTime? _SendDate;
        [Index(0), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime? SendDate
        {
            get { return _SendDate; }
            set
            {
                SetPropertyValue("SendDate", ref _SendDate, value);
            }
        }

        private string _Sender;
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Accepted")]
        public string Sender
        {
            get { return _Sender; }
            set
            {
                SetPropertyValue("Sender", ref _Sender, value);
            }
        }

        private string _Receipient;
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Accepted")]
        public string Receipient
        {
            get { return _Receipient; }
            set
            {
                SetPropertyValue("Receipient", ref _Receipient, value);
            }
        }

        private string _DocNum;
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Accepted")]
        public string DocNum
        {
            get { return _DocNum; }
            set
            {
                SetPropertyValue("DocNum", ref _DocNum, value);
            }
        }

        private string _DocType;
        [Index(4), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Accepted")]
        public string DocType
        {
            get { return _DocType; }
            set
            {
                SetPropertyValue("DocType", ref _DocType, value);
            }
        }

        private string _MailStatus;
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Accepted")]
        public string MailStatus
        {
            get { return _MailStatus; }
            set
            {
                SetPropertyValue("MailStatus", ref _MailStatus, value);
            }
        }

        private string _ErrorMsg;
        [Index(6), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Accepted")]
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set
            {
                SetPropertyValue("ErrorMsg", ref _ErrorMsg, value);
            }
        }

        private EmailContent _EmailContent;
        [XafDisplayName("Email Content")]
        [Association("EmailContent-EmailHistory")]
        //[Appearance("SalesQuotation", Enabled = false)]
        public EmailContent EmailContent
        {
            get { return _EmailContent; }
            set { SetPropertyValue("EmailContent", ref _EmailContent, value); }
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