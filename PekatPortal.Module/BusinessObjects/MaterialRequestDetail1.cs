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
    [RuleCriteria("Quantity",DefaultContexts.Save,"Quantity > 0","Quantity must be greater than zero")]
    [Appearance("DisableLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDuplicate", AppearanceItemType.Action, "True", TargetItems = "Duplicate", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisablePrint", AppearanceItemType.Action, "True", TargetItems = "Print", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class MaterialRequestDetail1 : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public MaterialRequestDetail1(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private vw_ItemList _ItemCode;
        [XafDisplayName("Item Code")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [NoForeignKey]
        [Size(50)]
        public vw_ItemList ItemCode
        {
            get { return _ItemCode; }
            set {
                SetPropertyValue("ItemCode", ref _ItemCode, value);
                if (!IsLoading & value != null)
                {
                    //SetPropertyValue("ItemCode", ref _ItemCode, Session.FindObject<vw_ItemList>(CriteriaOperator.Parse("ItemCode=?", value.ItemCode)));
                    SetPropertyValue("ItemName", ref _ItemName, value.ItemName);
                    SetPropertyValue("UOM", ref _UOM, value.UOM);
                    SetPropertyValue("Warehouse", ref _Warehouse, Session.FindObject<vw_WarehouseList>(CriteriaOperator.Parse("WhsCode=?",value.DfltWH)));
                }
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

        private double _Quantity;
        [XafDisplayName("Quantity")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        #endregion
        private vw_WarehouseList _Warehouse;
        [XafDisplayName("Warehouse")]
        [RuleRequiredField(DefaultContexts.Save)]
        [NoForeignKey]
        [Size(100)]
        public vw_WarehouseList Warehouse
        {
            get { return _Warehouse; }
            set { SetPropertyValue("Warehouse", ref _Warehouse, value); }
        }

        private MaterialRequest _MaterialRequest;
        [XafDisplayName("MaterialRequest")]
        [Association("MaterialRequest-MaterialRequestDetail1")]
        [Appearance("MaterialRequest",Enabled = false)]
        public MaterialRequest MaterialRequest
        {
            get { return _MaterialRequest; }
            set { SetPropertyValue("MaterialRequest", ref _MaterialRequest, value); }
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