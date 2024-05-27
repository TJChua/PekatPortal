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
    public class ApprovalTemplate : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ApprovalTemplate(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _TemplateName;
        public string TemplateName
        {
            get { return _TemplateName; }
            set { SetPropertyValue("TemplateName", ref _TemplateName, value); }
        }

        private string _Priority;
        public string Priority
        {
            get { return _Priority; }
            set { SetPropertyValue("Priority", ref _Priority, value); }
        }

        private string _StoredProcedure;
        public string StoredProcedure
        {
            get { return _StoredProcedure; }
            set { SetPropertyValue("StoredProcedure", ref _StoredProcedure, value); }
        }

        private string _Level;
        public string Level
        {
            get { return _Level; }
            set { SetPropertyValue("Level", ref _Level, value); }
        }

        [Association("ApprovalTemplate-Approver")]//Save Detail together with Header.
        [XafDisplayName("Approver")]
        public XPCollection<Approver> Approver
        {
            get { return GetCollection<Approver>("Approver"); }
        }

        [Association("ApprovalTemplate-Originator")]//Save Detail together with Header.
        [XafDisplayName("Originator")]
        public XPCollection<Originator> Originator
        {
            get { return GetCollection<Originator>("Originator"); }
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