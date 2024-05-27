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
    [Appearance("DisableRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = ViewItemVisibility.Hide,Context = "DetailView")]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; View", Visibility = ViewItemVisibility.Hide, Context = "DetailView",Criteria ="(IsPost = 1 OR IsCanceled = 1)")]
    //[Appearance("DisablePost", AppearanceItemType.Action, "True", TargetItems = "btnPost", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDuplicate", AppearanceItemType.Action, "True", TargetItems = "btnDuplicate", Visibility = ViewItemVisibility.Hide, Context = "ListView")]
    [Appearance("DisablePrint", AppearanceItemType.Action, "True", TargetItems = "btnPrint", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class MaterialRequest : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public MaterialRequest(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DocDate = DateTime.Today;
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

        private string _DocNum;
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Document No")]
        [Appearance("DocNum", Enabled = false)]
        public string DocNum
        {
            get
            {
                int Number = 1000000 + Oid;
                _DocNum = Number.ToString();
                return _DocNum;
            }
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

        private string _Reference;
        [XafDisplayName("Reference")]
        [Size(100)]
        public string Reference
        {
            get { return _Reference; }
            set { SetPropertyValue("Reference", ref _Reference, value); }
        }

        private string _RequestedBy;
        [XafDisplayName("Requested By")]
        [Size(100)]
        public string RequestedBy
        {
            get { return _RequestedBy; }
            set { SetPropertyValue("RequestedBy", ref _RequestedBy, value); }
        }


        private DateTime _RequiredDate;
        [XafDisplayName("Required Date")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime RequiredDate
        {
            get { return _RequiredDate; }
            set { SetPropertyValue("RequiredDate", ref _RequiredDate, value); }
        }

        private string _Remark;
        [XafDisplayName("Remark")]
        [Size(254)]
        public string Remark
        {
            get { return _Remark; }
            set { SetPropertyValue("Remark", ref _Remark, value); }
        }


        private bool _IsPost;
        [ImmediatePostData]
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
        [ImmediatePostData]
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

        private bool _IsProcessed;
        [ImmediatePostData]
        [XafDisplayName("Processed")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(70), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("IsProcessed", Enabled = false)]
        public bool IsProcessed
        {
            get { return _IsProcessed; }
            set
            {
                SetPropertyValue("IsProcessed", ref _IsProcessed, value);
            }
        }

        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Status")]
        public string Status
        {
            get
            {
                if (IsCanceled && IsPost)
                {
                    return "Canceled";
                }
                else if (IsCanceled)
                {
                    return "Canceled";
                }
                else
                {
                    if (IsPost && IsProcessed)
                    {
                        return "Processed";
                    }
                    else if (IsPost)
                    {
                        return "Posted";
                    } 
                    else
                    {
                        return "Open";
                    }
                }
            }
        }

        private vw_ProjectList _PrjCode;
        [NoForeignKey]
        //[Association("MaterialRequest-vw_ProjectList"), DevExpress.Xpo.Aggregated]//Save Detail together with Header.
        [XafDisplayName("Project")]
        public vw_ProjectList PrjCode
        {
            get { return _PrjCode; }
            set { SetPropertyValue("Project", ref _PrjCode, value); }
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

        [Association("MaterialRequest-MaterialRequestDetail1"), DevExpress.Xpo.Aggregated]//Save Detail together with Header.
        [XafDisplayName("Item")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<MaterialRequestDetail1> MaterialRequestDetail1
        {
            get { return GetCollection<MaterialRequestDetail1>("MaterialRequestDetail1"); }
        }

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
                    CreateUser = Session.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    CreateDate = DateTime.Now;
                }
                else
                {
                    UpdateUser = Session.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    UpdateDate = DateTime.Now;
                }
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            this.Reload();
        }
        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}