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
    public class vw_PostedMR : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_PostedMR(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private MaterialRequest _OID;
        [Key]
        [XafDisplayName("OID")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public MaterialRequest OID
        {
            get { return _OID; }
            set { SetPropertyValue("OID", ref _OID, value); }
        }

        private DateTime _DocDate;
        [XafDisplayName("Posting Date")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime DocDate
        {
            get { return _DocDate; }
            set { SetPropertyValue("DocDate", ref _DocDate, value); }
        }

        private string _PrjCode;
        [XafDisplayName("Project")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string PrjCode
        {
            get { return _PrjCode; }
        }

        private string _Remark;
        [XafDisplayName("Remark")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Remark
        {
            get { return _Remark; }
        }

        private string _Reference;
        [XafDisplayName("Status")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Reference
        {
            get { return _Reference; }
        }

        private bool _IsPost;
        [XafDisplayName("Post")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        public bool IsCanceled
        {
            get { return _IsCanceled; }
            set
            {
                SetPropertyValue("IsCanceled", ref _IsCanceled, value);
            }
        }

        private string _Status;
        [XafDisplayName("Status")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Status
        {
            get {
                if (IsCanceled)
                {
                    return "Canceled";
                }
                else
                {
                    if (IsPost)
                        return "Processed";
                    else
                        return "Posted";
                }
            }
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