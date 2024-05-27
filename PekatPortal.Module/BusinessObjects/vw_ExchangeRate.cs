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
    public class vw_ExchangeRate : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vw_ExchangeRate(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("Key")]
        [Appearance("Key", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string Key
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("RateDate")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [Appearance("RateDate", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public DateTime RateDate
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Currency")]
        [Appearance("Currency", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public string Currency
        {
            get; set;
        }

        [Browsable(true)]
        [XafDisplayName("Rate")]
        [Appearance("Rate", Enabled = false)]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        public double Rate
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