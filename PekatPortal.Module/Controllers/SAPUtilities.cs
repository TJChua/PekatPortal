using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PekatPortal.Module.BusinessObjects;
using static PekatPortal.Module.BusinessObjects.ConfirmationMsg;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.IO;
using System.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System.Net.Mail;
using System.Net;

//using SAPbobsCOM;

namespace PekatPortal.Module.Controllers
{
    [DomainComponent]
    [Appearance("DisableNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "Edit", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("DisableDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.

    public partial class SAPUtilities : ViewController
    {
        private NewObjectViewController controller;
        public SAPUtilities()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //TargetObjectType = typeof(MaterialRequest);
            //TargetObjectType = typeof(SalesQuotation);
        }

        //protected override void OnFrameAssigned()
        //{
        //    base.OnFrameAssigned();
        //    MyDetailsController target = Frame.GetController<MyDetailsController>();
        //    if (target != null && target.MyDetailsAction != null)
        //        target.MyDetailsAction.Caption = SecuritySystem.CurrentUserName;
        //}


        protected override void OnActivated()
        {
            base.OnActivated();
            this.Post.Active.SetItemValue("Enabled", false);
            this.Close.Active.SetItemValue("Enabled", false);
            this.PostRev.Active.SetItemValue("Enabled", false);
            this.Duplicate.Active.SetItemValue("Enabled", false);
            this.PostRev.Active.SetItemValue("Enabled", false);
            this.Approve.Active.SetItemValue("Enabled", false);
            this.Reject.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            if (View is ListView)
            {
                if (View.ObjectTypeInfo.Type == typeof(MaterialRequest))
                {
                    if (View.Id == "MaterialRequest_ListView")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("IsPost=? AND IsCanceled=?", false, false);
                        //View.
                    }
                    else if (View.Id == "MaterialRequest_ListView_Posted")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("IsPost=?", true);
                    }
                    else if (View.Id == "MaterialRequest_ListView_Canceled")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("IsCanceled=?", true);
                        //Frame.Controllers.
                    }
                }

                if (View.ObjectTypeInfo.Type == typeof(SalesQuotation))
                {
                    if (View.Id == "SalesQuotation_ListView_Draft")
                    {
                        // OR Escalate
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=? AND (CreateUser =? OR SlpCode =? OR Escalate =?)", 0, user.Oid.ToString(), user.SalesPersonCode.SlpCode, user.Oid);
                        //View.
                    }
                    if (View.Id == "SalesQuotation_ListView")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=?", 1);
                        //View.
                    }
                    else if (View.Id == "SalesQuotation_ListView_Posted")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=?", 5);
                    }
                    else if (View.Id == "SalesQuotation_ListView_Canceled")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=?", 2);
                        //Frame.Controllers.
                    }
                    else if (View.Id == "SalesQuotation_ListView_Approval")
                    {
                        //((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=? AND [Approval.Approver.Oid]=?", 3, user.Oid);
                        CriteriaOperator criteria = null;
                        criteria = CriteriaOperator.Parse("[<Approval>][^.Oid = SalesQuotation]");
                        criteria = GroupOperator.And(criteria, new BinaryOperator("Status", 3));
                        criteria = GroupOperator.And(criteria, CriteriaOperator.Parse("[<Approval>][Status = 'Pending']"));
                        criteria = GroupOperator.And(criteria, CriteriaOperator.Parse("([<Approval>][Approver = '" + user.Oid + "'] OR SlpCode = ? OR CreateUser = ? OR UpdateUser =?)", user.SalesPersonCode.SlpCode, user.Oid.ToString(), user.Oid.ToString()));
                        //criteria = GroupOperator.Or(criteria, new BinaryOperator("SlpCode", ) + ")");
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = criteria;
                    }
                    else if (View.Id == "SalesQuotation_ListView_Approved")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=?", 4);
                        //Frame.Controllers.
                    }
                    else if (View.Id == "SalesQuotation_ListView_Rejected")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status=?", 6);
                        //Frame.Controllers.
                    }
                }
            }
            //PopupWindowShowAction History = new PopupWindowShowAction(this, "History", PredefinedCategory.View);
            //this.ViewControlsCreated += new EventHandler(Sync_ViewControlsCreated);
            controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                controller.ObjectCreated += controller_ObjectCreated;
            }

            this.History.CustomizePopupWindowParams += action_CustomizePopupWindowParams;

            ObjectSpace.Committed += new EventHandler(ObjectSpace_Committed);
            // Perform various tasks depending on the target View.
        }

        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            ((IObjectSpace)sender).Refresh();

            //if (View.ObjectTypeInfo.Type == typeof(SalesQuotation))
            //{

            //   sendEmail("SQ", ((SalesQuotation)View.CurrentObject).Oid.ToString(), "Draft");
            //}
            //
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            if (View.Id == "SalesQuotation_ListView" || View.Id == "SalesQuotation_ListView_Canceled" || View.Id == "SalesQuotation_ListView_Draft" ||
                View.Id == "SalesQuotation_ListView_Posted" || View.Id == "vw_ItemList_ListView" || View.Id == "vw_CustomerList_ListView" ||
                View.Id == "vw_ORDR_ListView" || View.Id == "vw_ODLN_ListView" || View.Id == "vw_OINV_ListView" || View.Id == "SalesQuotation_ListView_Approval"
                || View.Id == "SalesQuotation_ListView_Approved" || View.Id == "SalesQuotation_ListView_Rejected" || View.Id == "vw_CustomerList_LookupListView" 
                || View.Id == "vw_ItemList_LookupListView" || View.Id == "vw_SalesPerson_LookupListView")
            {
                ASPxGridListEditor listEditor = ((ListView)View).Editor as ASPxGridListEditor;
                if (listEditor != null)
                {
                    listEditor.Grid.EnablePagingGestures = AutoBoolean.False;
                }
            }

            if (View.Id == "SalesQuotation_DetailView")
            {
                BusinessObjects.SalesQuotation SO = View.CurrentObject as BusinessObjects.SalesQuotation;
                if (SO.Status == StatusEnum.Approved)
                {
                    this.Post.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                    this.Close.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                    this.Approve.Active.SetItemValue("Enabled", false);
                    this.Reject.Active.SetItemValue("Enabled", false);
                    if (((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View)
                    {
                        if (SO.SlpCode.SlpCode == user.SalesPersonCode.SlpCode || user.AllowAmend == "Y")
                            View.AllowEdit["Forced"] = true;
                        else
                            View.AllowEdit["Forced"] = false;
                    }
                }
                else if (SO.Status == StatusEnum.Approval)
                {
                    View.AllowEdit["Forced"] = false;
                    foreach (Approver approver in View.ObjectSpace.CreateCollection(typeof(Approver), CriteriaOperator.Parse("ApprovalTemplate=?", int.Parse(SO.CurrentAppTmp))))
                    {
                        SystemUsers sysUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                        if (sysUser.Oid == approver.User.Oid)
                        {
                            this.Approve.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                            this.Reject.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                            break;
                        }
                        else
                        {
                            this.Approve.Active.SetItemValue("Enabled", false);
                            this.Reject.Active.SetItemValue("Enabled", false);
                        }
                    }
                }
                else if (SO.Status == StatusEnum.Rejected)
                {
                    this.Approve.Active.SetItemValue("Enabled", false);
                    this.Reject.Active.SetItemValue("Enabled", false);
                }
                else if (SO.Status == StatusEnum.Open)
                {
                    this.Close.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);

                    if (((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View)
                    {
                        if (SO.SlpCode.SlpCode == user.SalesPersonCode.SlpCode || user.AllowAmend == "Y")
                            View.AllowEdit["Forced"] = true;
                        else
                            View.AllowEdit["Forced"] = false;
                    }
                }
                else
                {
                    if (((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View)
                    {
                        if (SO.Status == StatusEnum.Draft && (user.AllowAmend == "Y" || SO.SlpCode.SlpCode == user.SalesPersonCode.SlpCode || (SO.Escalate == null || SO.Escalate.Oid == user.Oid)))
                            View.AllowEdit["Forced"] = true;
                        else
                            View.AllowEdit["Forced"] = false;
                    }
                }
                this.Duplicate.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
            }
            else if (View.Id == "SalesQuotation_SalesQuotationRev_ListView")
            {
                if (((ListView)View).CollectionSource is PropertyCollectionSource)
                {
                    PropertyCollectionSource collectionSource =
                        (PropertyCollectionSource)((ListView)View).CollectionSource;
                    //var parent = collectionSource.MasterObject;
                    SalesQuotation SQ = (SalesQuotation)collectionSource.MasterObject;
                    if (SQ.Status == StatusEnum.Open)
                    {
                        this.PostRev.Active.SetItemValue("Enabled", ((DetailView)View.ObjectSpace.Owner).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                    }
                }
            }
            else if (View.Id == "MaterialRequest_DetailView")
            {
                BusinessObjects.MaterialRequest MR = View.CurrentObject as BusinessObjects.MaterialRequest;
                if (!MR.IsPost && !MR.IsCanceled)
                {
                    this.Post.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                    this.Close.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                    if (MR.CreateUser.Oid != user.Oid)
                        View.AllowEdit["Forced"] = false;
                }
                else
                {
                    View.AllowEdit["Forced"] = false;
                }
            }

        }



        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            //this.ViewControlsCreated -= new EventHandler(Sync_ViewControlsCreated);
            //Application.ListViewCreating -= Application_ListViewCreating;
            if (controller != null)
            {
                controller.ObjectCreated -= controller_ObjectCreated;
            }
            ObjectSpace.Committed -= new EventHandler(ObjectSpace_Committed);
            base.OnDeactivated();
        }

        public bool ConnectSAP()
        {
            IObjectSpace os = Application.CreateObjectSpace();
            Company companyInfo = os.FindObject<Company>(new BinaryOperator("Oid", "1"));

            if (GeneralSettings.oCompany == null)
            {
                GeneralSettings.oCompany = new SAPbobsCOM.Company();
            }

            if (GeneralSettings.oCompany != null && !GeneralSettings.oCompany.Connected)
            {
                GeneralSettings.oCompany.DbServerType = (SAPbobsCOM.BoDataServerTypes)int.Parse(companyInfo.B1DbServerType);
                GeneralSettings.oCompany.Server = companyInfo.B1Server;
                GeneralSettings.oCompany.CompanyDB = companyInfo.B1CompanyDB;
                //GeneralSettings.oCompany.CompanyDB = oTargetDoc.Company.DBName;
                GeneralSettings.oCompany.LicenseServer = companyInfo.B1License;
                GeneralSettings.oCompany.DbUserName = companyInfo.B1DbUserName;
                GeneralSettings.oCompany.DbPassword = companyInfo.B1DbPassword;
                GeneralSettings.oCompany.UserName = companyInfo.B1UserName;
                GeneralSettings.oCompany.Password = companyInfo.B1Password;
                if (GeneralSettings.oCompany.Connect() != 0)
                {
                    showMsg("Failed", GeneralSettings.oCompany.GetLastErrorDescription(), InformationType.Error);
                }
            }
            return GeneralSettings.oCompany.Connected;
        }

        public void showMsg(string caption, string msg, InformationType msgtype)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 3000;
            //options.Message = string.Format("{0} task(s) have been successfully updated!", e.SelectedObjects.Count);
            options.Message = string.Format("{0}", msg);
            options.Type = msgtype;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = caption;
            options.Win.Type = WinMessageType.Flyout;
            Application.ShowViewStrategy.ShowMessage(options);

        }

        private void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            NestedFrame nestedFrame = Frame as NestedFrame;

            if (nestedFrame != null)
            {
                SalesQuotationDetail qut1 = e.CreatedObject as SalesQuotationDetail;
                if (qut1 != null)
                {
                    SalesQuotation oqut = ((NestedFrame)Frame).ViewItem.CurrentObject as SalesQuotation;

                    if (oqut != null)
                    {
                        if (oqut.CurrRate == 0)
                        {
                            showMsg("Missing Exchange Rate", "Exchange Rate Missing for Currency" + oqut.Currency, InformationType.Error);
                            nestedFrame.View.Close();
                            return;
                        }
                        qut1.Currency = oqut.Currency;
                        qut1.CurrRate = oqut.CurrRate;
                        //qut1.SalesQuotation.Oid = oqut.Oid;
                    }
                }
            }
        }

        private void PostGoodIssue(MaterialRequest oTargetDoc)
        {
            try
            {
                if (!GeneralSettings.oCompany.InTransaction) GeneralSettings.oCompany.StartTransaction();

                SAPbobsCOM.Documents oDoc = (SAPbobsCOM.Documents)GeneralSettings.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                oDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oInventoryGenExit;

                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                oDoc.DocDate = oTargetDoc.DocDate;
                oDoc.TaxDate = oTargetDoc.DocDate;
                oDoc.Reference2 = oTargetDoc.DocNum;
                oDoc.Comments = oTargetDoc.Remark;

                foreach (MaterialRequestDetail1 dtl in oTargetDoc.MaterialRequestDetail1)
                {
                    oDoc.Lines.ItemCode = dtl.ItemCode.ItemCode;
                    oDoc.Lines.Quantity = dtl.Quantity;
                    oDoc.Lines.WarehouseCode = dtl.Warehouse.WhsCode;
                    if (oTargetDoc.PrjCode != null) oDoc.Lines.ProjectCode = oTargetDoc.PrjCode.PrjCode;
                    oDoc.Lines.Add();
                }

                int rc = oDoc.Add();
                if (rc != 0)
                {
                    string temp = GeneralSettings.oCompany.GetLastErrorDescription();
                    if (GeneralSettings.oCompany.InTransaction)
                    {
                        GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    }
                    showMsg("Failed", temp, InformationType.Error);
                }
                else
                {
                    oTargetDoc.IsPost = true;
                    if (GeneralSettings.oCompany.InTransaction)
                        GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    showMsg("Success", "Posting Successful", InformationType.Success);
                }
            }
            catch (Exception ex)
            {
                oTargetDoc.IsPost = false;
                showMsg("Error", ex.Message, InformationType.Error);
                GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
        }

        private void postSalesQuotation(SalesQuotation oTargetDoc)
        {
            try
            {

                if (!GeneralSettings.oCompany.InTransaction) GeneralSettings.oCompany.StartTransaction();

                SAPbobsCOM.Documents oDoc = (SAPbobsCOM.Documents)GeneralSettings.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                oDoc.DocDate = oTargetDoc.DocDate;
                oDoc.DocDueDate = oTargetDoc.DocDueDate;
                oDoc.TaxDate = oTargetDoc.DocDate;
                oDoc.CardCode = oTargetDoc.DocEntry.CardCode;
                oDoc.CardName = oTargetDoc.CardName;
                oDoc.NumAtCard = oTargetDoc.Reference;
                oDoc.Comments = oTargetDoc.Remark;
                oDoc.SalesPersonCode = int.Parse(oTargetDoc.SlpCode.SlpCode);
                oDoc.OpeningRemarks = oTargetDoc.Header;
                oDoc.ClosingRemarks = oTargetDoc.Footer;
                oDoc.DocCurrency = oTargetDoc.Currency;
                oDoc.DocRate = oTargetDoc.CurrRate;
                if (oTargetDoc.DocNum != null) oDoc.UserFields.Fields.Item("U_PortalNo").Value = oTargetDoc.DocNum + "-" + oTargetDoc.RevisionNo;
                if (oTargetDoc.Price != null) oDoc.UserFields.Fields.Item("U_Price").Value = oTargetDoc.Price;
                if (oTargetDoc.PaymentTermName != null) oDoc.UserFields.Fields.Item("U_PayTerm").Value = oTargetDoc.PaymentTermName;
                if (oTargetDoc.Delivery != null) oDoc.UserFields.Fields.Item("U_Delivery").Value = oTargetDoc.Delivery;
                if (oTargetDoc.Validity != null) oDoc.UserFields.Fields.Item("U_Validity").Value = oTargetDoc.Validity;
                if (oTargetDoc.Warranty != null) oDoc.UserFields.Fields.Item("U_Warranty").Value = oTargetDoc.Warranty;
                if (oTargetDoc.Name != null) oDoc.UserFields.Fields.Item("U_Attention").Value = oTargetDoc.Name;
                if (oTargetDoc.Tel1 != null) oDoc.UserFields.Fields.Item("U_PhoneNo").Value = oTargetDoc.Tel1;
                if (oTargetDoc.Email != null) oDoc.UserFields.Fields.Item("U_Email").Value = oTargetDoc.Email;

                foreach (SalesQuotationDetail dtl in oTargetDoc.SalesQuotationDetail)
                {
                    if (dtl.ItemCode.ItemCode == "FreeText")
                    {
                        continue;
                    }
                    oDoc.Lines.ItemCode = dtl.ItemCode.ItemCode;
                    if (dtl.SuppCatNum != null) oDoc.Lines.UserFields.Fields.Item("U_CatalogNo").Value = dtl.SuppCatNum;
                    oDoc.Lines.UserFields.Fields.Item("U_ItemName").Value = dtl.ItemName;
                    oDoc.Lines.Quantity = dtl.Quantity;
                    oDoc.Lines.WarehouseCode = dtl.Warehouse.WhsCode;
                    if (dtl.ItemDetail != null) oDoc.Lines.ItemDetails = dtl.ItemDetail;
                    if (oTargetDoc.SlpCode != null) oDoc.Lines.SalesPersonCode = int.Parse(oTargetDoc.SlpCode.SlpCode);
                    oDoc.Lines.VatGroup = dtl.TaxCode.Code;
                    oDoc.Lines.UnitPrice = dtl.UnitPriceAfDisc;
                    oDoc.Lines.LineTotal = dtl.LineTotal;
                    if (dtl.UOM != null) oDoc.Lines.MeasureUnit = dtl.UOM;
                    oDoc.Lines.Add();
                }
                //oDoc.DiscountPercent = oTargetDoc.Discount;
                oDoc.DocTotal = oTargetDoc.GrandTotal;
                int rc = oDoc.Add();
                if (rc != 0)
                {
                    string temp = GeneralSettings.oCompany.GetLastErrorDescription();
                    if (GeneralSettings.oCompany.InTransaction)
                    {
                        GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    }
                    showMsg("Failed", temp, InformationType.Error);
                }
                else
                {
                    oTargetDoc.IsPost = true;
                    oTargetDoc.Status = StatusEnum.Posted;
                    oTargetDoc.UpdateUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId).FullName.ToString();
                    oTargetDoc.UpdateDate = DateTime.Now;
                    oTargetDoc.PostRevision = oTargetDoc.RevisionNo;
                    if (GeneralSettings.oCompany.InTransaction)
                        GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    showMsg("Success", "Posting Successful", InformationType.Success);
                }
            }
            catch (Exception ex)
            {
                oTargetDoc.IsPost = false;
                showMsg("Error", ex.Message, InformationType.Error);
                GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
        }

        private void postSalesQuotationRev(SalesQuotationRev oTargetDoc)
        {
            try
            {

                if (!GeneralSettings.oCompany.InTransaction) GeneralSettings.oCompany.StartTransaction();

                SAPbobsCOM.Documents oDoc = (SAPbobsCOM.Documents)GeneralSettings.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                oDoc.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                oDoc.DocDate = oTargetDoc.DocDate;
                oDoc.DocDueDate = oTargetDoc.DocDueDate;
                oDoc.TaxDate = oTargetDoc.DocDate;
                oDoc.CardCode = oTargetDoc.CardCode;
                oDoc.CardName = oTargetDoc.CardName;
                oDoc.NumAtCard = oTargetDoc.Reference;
                oDoc.Comments = oTargetDoc.Remark;
                oDoc.SalesPersonCode = int.Parse(oTargetDoc.SlpCode);
                oDoc.OpeningRemarks = oTargetDoc.Header;
                oDoc.ClosingRemarks = oTargetDoc.Footer;
                oDoc.DocCurrency = oTargetDoc.Currency;
                oDoc.DocRate = oTargetDoc.CurrRate;
                if (oTargetDoc.DocNum != null) oDoc.UserFields.Fields.Item("U_PortalNo").Value = oTargetDoc.DocNum + "-" + oTargetDoc.RevisionNo;
                if (oTargetDoc.Price != null) oDoc.UserFields.Fields.Item("U_Price").Value = oTargetDoc.Price;
                if (oTargetDoc.PaymentTermName != null) oDoc.UserFields.Fields.Item("U_PayTerm").Value = oTargetDoc.PaymentTermName;
                if (oTargetDoc.Delivery != null) oDoc.UserFields.Fields.Item("U_Delivery").Value = oTargetDoc.Delivery;
                if (oTargetDoc.Validity != null) oDoc.UserFields.Fields.Item("U_Validity").Value = oTargetDoc.Validity;
                if (oTargetDoc.Warranty != null) oDoc.UserFields.Fields.Item("U_Warranty").Value = oTargetDoc.Warranty;
                if (oTargetDoc.Name != null) oDoc.UserFields.Fields.Item("U_Attention").Value = oTargetDoc.Name;
                if (oTargetDoc.Tel1 != null) oDoc.UserFields.Fields.Item("U_PhoneNo").Value = oTargetDoc.Tel1;
                if (oTargetDoc.Email != null) oDoc.UserFields.Fields.Item("U_Email").Value = oTargetDoc.Email;

                foreach (SalesQuotationRevDetail dtl in oTargetDoc.SalesQuotationRevDetail)
                {
                    if (dtl.ItemCode == "FreeText")
                    {
                        continue;
                    }
                    oDoc.Lines.ItemCode = dtl.ItemCode;
                    if (dtl.SuppCatNum != null) oDoc.Lines.UserFields.Fields.Item("U_CatalogNo").Value = dtl.SuppCatNum;
                    oDoc.Lines.UserFields.Fields.Item("U_ItemName").Value = dtl.ItemName;
                    oDoc.Lines.Quantity = dtl.Quantity;
                    oDoc.Lines.WarehouseCode = dtl.Warehouse;
                    if (dtl.ItemDetail != null) oDoc.Lines.ItemDetails = dtl.ItemDetail;
                    if (oTargetDoc.SlpCode != null) oDoc.Lines.SalesPersonCode = int.Parse(oTargetDoc.SlpCode);
                    oDoc.Lines.VatGroup = dtl.TaxCode;
                    oDoc.Lines.UnitPrice = dtl.UnitPriceAfDisc;
                    oDoc.Lines.LineTotal = dtl.LineTotal;
                    if (dtl.UOM != null) oDoc.Lines.MeasureUnit = dtl.UOM;
                    oDoc.Lines.Add();
                }
                oDoc.DocTotal = oTargetDoc.GrandTotal;
                //oDoc.DiscountPercent = oTargetDoc.Discount;

                int rc = oDoc.Add();
                if (rc != 0)
                {
                    string temp = GeneralSettings.oCompany.GetLastErrorDescription();
                    if (GeneralSettings.oCompany.InTransaction)
                    {
                        GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    }
                    showMsg("Failed", temp, InformationType.Error);
                }
                else
                {
                    SalesQuotation selectedObject = ObjectSpace.FindObject<SalesQuotation>(CriteriaOperator.Parse("Oid=?", oTargetDoc.SalesQuotation));
                    selectedObject.IsPost = true;
                    selectedObject.Status = StatusEnum.Posted;
                    selectedObject.UpdateUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId).FullName.ToString();
                    selectedObject.UpdateDate = DateTime.Now;
                    selectedObject.PostRevision = oTargetDoc.RevisionNo;
                    if (GeneralSettings.oCompany.InTransaction)
                        GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    showMsg("Success", "Posting Successful", InformationType.Success);
                }
            }
            catch (Exception ex)
            {
                oTargetDoc.IsPost = false;
                showMsg("Error", ex.Message, InformationType.Error);
                GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
        }

        private void Close_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Cancel document is irreversible, proceed?";
            e.View = dv;
        }

        private void Close_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ConfirmationMsg p = (ConfirmationMsg)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            if (View.Id == "MaterialRequest_DetailView")
            {
                MaterialRequest selectedObject = (MaterialRequest)e.CurrentObject;
                selectedObject.IsCanceled = true;
            }
            else if (View.Id == "SalesQuotation_DetailView")
            {
                SalesQuotation selectedObject = (SalesQuotation)e.CurrentObject;
                selectedObject.IsCanceled = true;
                selectedObject.Status = StatusEnum.Canceled;
            }
            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
            showMsg("Success", "Canceled Successful", InformationType.Success);
        }

        private void Post_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Document cannot be amend after post, proceed?";
            e.View = dv;
        }

        private void Post_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            bool Connect;
            //showMsg("Failed", "No document was selected - " + View.Id, InformationType.Error);
            if (e.SelectedObjects.Count >= 1)
            {
                Connect = ConnectSAP();
                if (Connect)
                {
                    if (View.Id == "MaterialRequest_DetailView")
                    {
                        MaterialRequest selectedMR = (MaterialRequest)View.CurrentObject;
                        PostGoodIssue(selectedMR);
                    }
                    else if (View.Id == "SalesQuotation_DetailView")
                    {
                        SalesQuotation selectedSO = (SalesQuotation)View.CurrentObject;
                        if (selectedSO.Status == StatusEnum.Posted)
                        {
                            showMsg("Posting Error", "Sales Quotation already posted.", InformationType.Error);
                            return;
                        }
                        postSalesQuotation(selectedSO);

                        EmailHistory emailHistory = ObjectSpace.CreateObject<EmailHistory>();

                        EmailContent emailContent = ObjectSpace.FindObject<EmailContent>(CriteriaOperator.Parse("EmailType=?", "Posted"));

                        string receipient = "";
                        string errmsg = "";
                        Company company = ObjectSpace.FindObject<Company>(1);
                        //EmailHistory emailHistory = new 
                        //SystemUsers systemUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate));

                        MailMessage mailMsg = new MailMessage();

                        mailMsg.From = new MailAddress(company.EmailUser, company.Email);
                        emailHistory.Sender = company.Email;
                        emailHistory.DocNum = selectedSO.DocNum;
                        emailHistory.DocType = "SQ";
                        emailHistory.EmailContent = emailContent;


                        //ApprovalTemplate appTemplate = ObjectSpace.FindObject<ApprovalTemplate>(CriteriaOperator.Parse("OID=?",int.Parse(TemplateID)));

                        foreach (SystemUsers sysUser in View.ObjectSpace.CreateCollection(typeof(SystemUsers), CriteriaOperator.Parse("AllowAmend=?", "Y")))
                        {
                            mailMsg.To.Add(sysUser.Email);
                            receipient += sysUser.Email + ";";
                        }


                        mailMsg.Subject = emailContent.EmailTitle;

                        mailMsg.Body = emailContent.Content.Replace("[@DocNum]", selectedSO.DocNum).Replace("[@Oid]", selectedSO.Oid.ToString()) + Environment.NewLine +
                            HttpContext.Current.Request.Url.AbsoluteUri.Replace("Dialog=true&", "") + emailContent.Link.Replace("[@DocNum]", selectedSO.DocNum).Replace("[@Oid]", selectedSO.Oid.ToString());

                        try
                        {
                            SmtpClient smtpClient = new SmtpClient
                            {
                                Host = company.SMTPHost,
                                Port = int.Parse(company.Port),
                                EnableSsl = true,
                            };
                            smtpClient.Credentials = new NetworkCredential(company.EmailUser, company.EmailPass);

                            smtpClient.Send(mailMsg);

                            mailMsg.Dispose();
                            smtpClient.Dispose();
                            emailHistory.Receipient = receipient;
                            emailHistory.MailStatus = "Success";
                            errmsg = "";
                        }
                        catch (Exception ex)
                        {
                            errmsg = ex.Message.Substring(0, 100);
                            emailHistory.MailStatus = "Failed";
                        }
                        finally
                        {
                            emailHistory.SendDate = DateTime.Now;
                            emailHistory.ErrorMsg = errmsg;
                        }
                    }
                    //foreach (var selectedObject in e.SelectedObjects)
                    //{
                    //    MaterialRequest selectedMR = (MaterialRequest)ObjectSpace.GetObject(selectedObject);
                    //}
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();
                }
            }
            else
            {
                showMsg("Failed", "No document was selected - " + View.Id, InformationType.Error);
            }
        }

        private void Duplicate_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                SalesQuotation salesQuotation = (SalesQuotation)e.CurrentObject;
                IObjectSpace os = Application.CreateObjectSpace();
                SalesQuotation newSalesQuotation = os.CreateObject<SalesQuotation>();

                newSalesQuotation.DocNum = salesQuotation.DocNum;
                newSalesQuotation.DocDate = salesQuotation.DocDate;
                newSalesQuotation.DocDueDate = salesQuotation.DocDueDate;
                newSalesQuotation.Header = salesQuotation.Header;
                newSalesQuotation.Footer = salesQuotation.Footer;
                newSalesQuotation.Remark = salesQuotation.Remark;
                newSalesQuotation.Delivery = salesQuotation.Delivery;
                newSalesQuotation.Validity = salesQuotation.Validity;
                newSalesQuotation.Warranty = salesQuotation.Warranty;
                newSalesQuotation.Price = salesQuotation.Price;
                newSalesQuotation.Reference = salesQuotation.Reference;
                newSalesQuotation.RevisionNo = "1";
                //if(salesQuotation.ContactPerson != null)newSalesQuotation.ContactPerson = newSalesQuotation.Session.GetObjectByKey<vw_ContactPerson>(salesQuotation.ContactPerson.CntctCode);
                newSalesQuotation.Name = salesQuotation.Name;
                newSalesQuotation.Tel1 = salesQuotation.Tel1;
                newSalesQuotation.Email = salesQuotation.Email;
                newSalesQuotation.PaymentTermName = salesQuotation.PaymentTermName;
                // newSalesQuotation.PaymentTerm = newSalesQuotation.Session.GetObjectByKey<vw_PaymentTerm>(salesQuotation.PaymentTerm.GroupNum);
                newSalesQuotation.DocEntry = newSalesQuotation.Session.GetObjectByKey<vw_CustomerList>(salesQuotation.DocEntry.DocEntry);
                newSalesQuotation.SlpCode = newSalesQuotation.Session.GetObjectByKey<vw_SalesPerson>(salesQuotation.SlpCode.SlpCode);
                newSalesQuotation.CardCode = salesQuotation.CardCode;
                newSalesQuotation.CardName = salesQuotation.CardName;
                newSalesQuotation.BillToAdress1 = salesQuotation.BillToAdress1;
                newSalesQuotation.BillToAdress2 = salesQuotation.BillToAdress2;
                newSalesQuotation.BillToAdress3 = salesQuotation.BillToAdress3;
                newSalesQuotation.BillToAdress4 = salesQuotation.BillToAdress4;
                newSalesQuotation.ShipToAdress1 = salesQuotation.ShipToAdress1;
                newSalesQuotation.ShipToAdress2 = salesQuotation.ShipToAdress2;
                newSalesQuotation.ShipToAdress3 = salesQuotation.ShipToAdress3;
                newSalesQuotation.ShipToAdress4 = salesQuotation.ShipToAdress4;
                //if (salesQuotation.Discount == null)
                //    newSalesQuotation.Discount = 0;
                //else
                newSalesQuotation.Discount = salesQuotation.Discount;
                newSalesQuotation.DiscountAmt = salesQuotation.DiscountAmt;
                newSalesQuotation.Currency = salesQuotation.Currency;
                newSalesQuotation.CurrRate = salesQuotation.CurrRate;
                newSalesQuotation.CompanyPhone = salesQuotation.CompanyPhone;
                newSalesQuotation.CompanyFax = salesQuotation.CompanyFax;
                newSalesQuotation.Company = newSalesQuotation.Session.GetObjectByKey<Company>(salesQuotation.Company.Oid);

                foreach (SalesQuotationDetail dtl in salesQuotation.SalesQuotationDetail)
                {
                    SalesQuotationDetail newSalesQuotationdetails = os.CreateObject<SalesQuotationDetail>();

                    newSalesQuotationdetails.ItemCode = newSalesQuotationdetails.Session.GetObjectByKey<vw_ItemList>(dtl.ItemCode.ItemCode);
                    newSalesQuotationdetails.ItemName = dtl.ItemName;
                    newSalesQuotationdetails.Quantity = dtl.Quantity;
                    newSalesQuotationdetails.UOM = dtl.UOM;
                    newSalesQuotationdetails.SuppCatNum = dtl.SuppCatNum;
                    newSalesQuotationdetails.Warehouse = newSalesQuotationdetails.Session.GetObjectByKey<vw_WarehouseList>(dtl.Warehouse.WhsCode);
                    newSalesQuotationdetails.OriUnitPrice = dtl.OriUnitPrice;
                    newSalesQuotationdetails.UnitPrice = dtl.UnitPrice;
                    newSalesQuotationdetails.UnitPriceAfDisc = dtl.UnitPriceAfDisc;
                    newSalesQuotationdetails.TaxCode = newSalesQuotation.Session.GetObjectByKey<vw_OutputTax>(dtl.TaxCode.Code);
                    newSalesQuotationdetails.DiscountAmt = dtl.DiscountAmt;
                    newSalesQuotationdetails.LineTotal = dtl.LineTotal;
                    newSalesQuotationdetails.ItemDetail = dtl.ItemDetail;
                    newSalesQuotationdetails.Discount = dtl.Discount;

                    newSalesQuotationdetails.Currency = dtl.Currency;
                    newSalesQuotationdetails.CurrRate = dtl.CurrRate;

                    newSalesQuotation.SalesQuotationDetail.Add(newSalesQuotationdetails);
                }

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(os, newSalesQuotation);
                dv.ViewEditMode = ViewEditMode.Edit;
                dv.IsRoot = true;
                svp.CreatedView = dv;

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
                showMsg("Success", "Duplicate Successful", InformationType.Success);
            }
            catch (Exception ex)
            {
                showMsg("Error", "Duplicate Error - " + ex.Message, InformationType.Error);
            }
        }

        private void History_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

        }

        void action_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(PriceHistory));
            IObjectSpace persistentObjectSpace = Application.CreateObjectSpace(typeof(SalesQuotation));
            objectSpace.AdditionalObjectSpaces.Add(persistentObjectSpace);
            objectSpace.Disposed += ObjectSpace_Disposed;
            objectSpace.ObjectsGetting += objectSpace_ObjectsGetting;
            e.View = Application.CreateListView(objectSpace, typeof(PriceHistory), true);
        }


        private void ObjectSpace_Disposed(object sender, EventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)sender;
            foreach (IObjectSpace item in objectSpace.AdditionalObjectSpaces)
            {
                item.Dispose();
            }
        }

        void objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)sender;
            XPObjectSpace persistentObjectSpace = (XPObjectSpace)objectSpace.AdditionalObjectSpaces[0];
            Session session = ((XPObjectSpace)persistentObjectSpace).Session;
            SalesQuotationDetail SQDetail = (SalesQuotationDetail)View.CurrentObject;
            //SalesQuotationDetail SQDetail = SalesQuotationDetail.cur
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            SelectedData results = persistentObjectSpace.Session.ExecuteSproc("sp_GetPriceHistory", new OperandValue(user.Company.B1CompanyDB), new OperandValue(SQDetail.ItemCode.ItemCode));
            BindingList<PriceHistory> objects = new BindingList<PriceHistory>();
            foreach (SelectStatementResultRow row in results.ResultSet[0].Rows)
            {
                PriceHistory obj = new PriceHistory();
                obj.DocType = row.Values[0].ToString();
                obj.DocNum = row.Values[1].ToString();
                obj.DocDate = DateTime.Parse(row.Values[2].ToString());
                obj.CardCode = row.Values[3].ToString();
                obj.CardName = row.Values[4].ToString();
                obj.ItemCode = row.Values[5].ToString();
                obj.ItemName = row.Values[6].ToString();
                obj.PriceAfVat = double.Parse(row.Values[7].ToString());
                obj.Quantity = double.Parse(row.Values[8].ToString());
                objects.Add(obj);
            }
            e.Objects = objects;
        }

        private void PostRev_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            string testtest = "";

            testtest = View.Id;
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Document cannot be amend after post, proceed?";
            e.View = dv;
        }

        private void PostRev_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            bool Connect;
            //showMsg("Failed", "No document was selected - " + View.Id, InformationType.Error);
            if (e.SelectedObjects.Count >= 1)
            {
                Connect = ConnectSAP();
                if (Connect)
                {
                    if (View.Id == "SalesQuotation_SalesQuotationRev_ListView")
                    {
                        SalesQuotationRev selectedSO = (SalesQuotationRev)View.CurrentObject;
                        SalesQuotation SQ = ObjectSpace.GetObjectByKey<SalesQuotation>(selectedSO.SalesQuotation.Oid);
                        if (SQ.Status == StatusEnum.Posted)
                        {
                            showMsg("Posting Error", "Sales Quotation already posted.", InformationType.Error);
                            return;
                        }
                        postSalesQuotationRev(selectedSO);
                    }
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();
                }
            }
            else
            {
                showMsg("Failed", "No document was selected - " + View.Id, InformationType.Error);
            }
        }

        private void History_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void Approve_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            string TemplateID = "-1";
            string TemplateName = "";
            string TemplateLevel = "-1";
            string receipient = "";
            string errmsg = "";
            SalesQuotation salesQuotation = (SalesQuotation)e.CurrentObject;
            XPObjectSpace persistentObjectSpace = (XPObjectSpace)Application.CreateObjectSpace();
            IObjectSpace os = Application.CreateObjectSpace();

            DocStatus docStatus = os.CreateObject<DocStatus>();
            docStatus = os.CreateObject<DocStatus>();
            docStatus.Status = "Approved";
            docStatus.TemplateName = salesQuotation.CurrentAppTmpName;
            docStatus.SalesQuotation = os.GetObjectByKey<SalesQuotation>(int.Parse(salesQuotation.Oid.ToString()));
            docStatus.CreateUser = os.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            docStatus.CreateDate = DateTime.Now;

            Approval approvalEx = os.FindObject<Approval>(CriteriaOperator.Parse("SalesQuotation = ? AND Approver = ? AND TemplateName = ? AND Status = 'Pending'",
                salesQuotation.Oid, os.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId), salesQuotation.CurrentAppTmp));

            approvalEx.Status = "Approved";
            approvalEx.CreateUser = os.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            approvalEx.CreateDate = DateTime.Now;

            SelectedData results = persistentObjectSpace.Session.ExecuteSproc("sp_CheckApproval", new OperandValue(salesQuotation.Oid), new OperandValue(int.Parse(salesQuotation.CurrentAppLevel) + 1), new OperandValue(salesQuotation.CurrentAppTmpName));
            if (results.ResultSet.Count() > 0)
            {
                if (results.ResultSet[0].Rows.Count() > 0)
                {
                    foreach (SelectStatementResultRow row in results.ResultSet[0].Rows)
                    {
                        Approval approval = os.CreateObject<Approval>();
                        approval.TemplateName = os.GetObjectByKey<ApprovalTemplate>(int.Parse(row.Values[0].ToString()));
                        approval.SalesQuotation = os.GetObjectByKey<SalesQuotation>(int.Parse(salesQuotation.Oid.ToString()));
                        approval.Approver = os.GetObjectByKey<SystemUsers>(Guid.Parse(row.Values[2].ToString()));
                        approval.Status = "Pending";
                        approval.CreateDate = DateTime.Now;
                        approval.CreateUser = os.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                        approval.Level = (int.Parse(salesQuotation.CurrentAppLevel) + 1).ToString();
                        TemplateID = row.Values[0].ToString();
                        TemplateName = row.Values[1].ToString();
                        TemplateLevel = row.Values[5].ToString();
                    }
                    salesQuotation.Status = StatusEnum.Approval;
                    salesQuotation.CurrentAppLevel = TemplateLevel;
                    salesQuotation.CurrentAppTmpName = TemplateName;
                    salesQuotation.CurrentAppTmp = TemplateID;

                    EmailHistory emailHistory = ObjectSpace.CreateObject<EmailHistory>();

                    EmailContent emailContent = ObjectSpace.FindObject<EmailContent>(CriteriaOperator.Parse("EmailType=?", "Approval"));

                    Company company = ObjectSpace.FindObject<Company>(1);
                    //EmailHistory emailHistory = new 
                    //SystemUsers systemUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate));

                    MailMessage mailMsg = new MailMessage();

                    mailMsg.From = new MailAddress(company.EmailUser, company.Email);
                    emailHistory.Sender = company.Email;
                    emailHistory.DocNum = salesQuotation.DocNum;
                    emailHistory.DocType = "SQ";
                    emailHistory.EmailContent = emailContent;


                    //ApprovalTemplate appTemplate = ObjectSpace.FindObject<ApprovalTemplate>(CriteriaOperator.Parse("OID=?",int.Parse(TemplateID)));

                    foreach (Approver approver in View.ObjectSpace.CreateCollection(typeof(Approver), CriteriaOperator.Parse("ApprovalTemplate=?", int.Parse(TemplateID))))
                    {
                        SystemUsers sysUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("Oid=?", approver.User.Oid));
                        mailMsg.To.Add(sysUser.Email);
                        receipient += sysUser.Email + ";";
                    }


                    mailMsg.Subject = emailContent.EmailTitle;

                    mailMsg.Body = emailContent.Content.Replace("[@DocNum]", salesQuotation.DocNum).Replace("[@Oid]", salesQuotation.Oid.ToString()) + Environment.NewLine +
                        HttpContext.Current.Request.Url.AbsoluteUri.Replace("Dialog=true&","") + emailContent.Link.Replace("[@DocNum]", salesQuotation.DocNum).Replace("[@Oid]", salesQuotation.Oid.ToString());

                    try
                    {
                        SmtpClient smtpClient = new SmtpClient
                        {
                            Host = company.SMTPHost,
                            Port = int.Parse(company.Port),
                            EnableSsl = true,
                        };
                        smtpClient.Credentials = new NetworkCredential(company.EmailUser, company.EmailPass);

                        smtpClient.Send(mailMsg);

                        mailMsg.Dispose();
                        smtpClient.Dispose();
                        emailHistory.Receipient = receipient;
                        emailHistory.MailStatus = "Success";
                        errmsg = "";
                    }
                    catch (Exception ex)
                    {
                        errmsg = ex.Message.Substring(0, 100);
                        emailHistory.MailStatus = "Failed";
                    }
                    finally
                    {
                        emailHistory.SendDate = DateTime.Now;
                        emailHistory.ErrorMsg = errmsg;
                    }
                }
                else
                {
                    salesQuotation.Status = StatusEnum.Approved;
                    salesQuotation.IsApproved = "Y";
                    //salesQuotation.CurrentAppLevel = TemplateLevel;
                    //salesQuotation.CurrentAppTmpName = TemplateName;
                    //salesQuotation.CurrentAppTmp = TemplateID;

                    EmailHistory emailHistory = ObjectSpace.CreateObject<EmailHistory>();

                    EmailContent emailContent = ObjectSpace.FindObject<EmailContent>(CriteriaOperator.Parse("EmailType=?", "Approved"));

                    Company company = ObjectSpace.FindObject<Company>(1);
                    //EmailHistory emailHistory = new 
                    //SystemUsers systemUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate));

                    MailMessage mailMsg = new MailMessage();

                    mailMsg.From = new MailAddress(company.EmailUser, company.Email);
                    emailHistory.Sender = company.Email;
                    emailHistory.DocNum = salesQuotation.DocNum;
                    emailHistory.DocType = "SQ";
                    emailHistory.EmailContent = emailContent;


                    //ApprovalTemplate appTemplate = ObjectSpace.FindObject<ApprovalTemplate>(int.Parse(TemplateID));

                    foreach (SystemUsers sysUser in View.ObjectSpace.CreateCollection(typeof(SystemUsers), CriteriaOperator.Parse("SalesPersonCode=?", int.Parse(salesQuotation.SlpCode.SlpCode))))
                    {
                        //SystemUsers sysUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("Oid=?", approver.User.Oid));
                        mailMsg.To.Add(sysUser.Email);
                        receipient += sysUser.Email + ";";
                    }


                    mailMsg.Subject = emailContent.EmailTitle;

                    mailMsg.Body = emailContent.Content.Replace("[@DocNum]", salesQuotation.DocNum).Replace("[@Oid]", salesQuotation.Oid.ToString()) + Environment.NewLine +
                        HttpContext.Current.Request.Url.AbsoluteUri + emailContent.Link.Replace("[@DocNum]", salesQuotation.DocNum).Replace("[@Oid]", salesQuotation.Oid.ToString());

                    try
                    {
                        SmtpClient smtpClient = new SmtpClient
                        {
                            Host = company.SMTPHost,
                            Port = int.Parse(company.Port),
                            EnableSsl = true,
                        };
                        smtpClient.Credentials = new NetworkCredential(company.EmailUser, company.EmailPass);

                        smtpClient.Send(mailMsg);

                        mailMsg.Dispose();
                        smtpClient.Dispose();
                        emailHistory.Receipient = receipient;
                        emailHistory.MailStatus = "Success";
                        errmsg = "";
                    }
                    catch (Exception ex)
                    {
                        errmsg = ex.Message.Substring(0, 100);
                        emailHistory.MailStatus = "Failed";
                    }
                    finally
                    {
                        emailHistory.SendDate = DateTime.Now;
                        emailHistory.ErrorMsg = errmsg;
                    }
                }

                os.CommitChanges();
                os.Refresh();
            }
            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
        }

        private void Approve_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Approve Selected Sales Quotation, proceed?";
            e.View = dv;
        }

        private void Reject_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            string receipient = "";
            string errmsg = "";
            SalesQuotation salesQuotation = (SalesQuotation)e.CurrentObject;
            IObjectSpace os = Application.CreateObjectSpace();
            ConfirmationMsg p = (ConfirmationMsg)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            Approval approvalEx = os.FindObject<Approval>(CriteriaOperator.Parse("SalesQuotation = ? AND Approver = ? AND TemplateName = ? AND Status = 'Pending'",
                salesQuotation.Oid, os.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId), salesQuotation.CurrentAppTmp));
            approvalEx.Status = "Rejected";
            approvalEx.ApproveDate = DateTime.Now;
            approvalEx.Remark = p.ParamString;
            os.CommitChanges();

            DocStatus docStatus = ObjectSpace.CreateObject<DocStatus>();
            salesQuotation.Status = StatusEnum.Rejected;
            salesQuotation.IsRejected = "Y";
            docStatus.Status = "Rejected";
            //docStatus.SalesQuotation = os.GetObjectByKey<SalesQuotation>(int.Parse(salesQuotation.Oid.ToString()));
            docStatus.CreateUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            docStatus.CreateDate = DateTime.Now;
            docStatus.Remark = p.ParamString;
            docStatus.TemplateName = salesQuotation.CurrentAppTmpName;
            salesQuotation.DocStatus.Add(docStatus);

            EmailHistory emailHistory = ObjectSpace.CreateObject<EmailHistory>();

            EmailContent emailContent = ObjectSpace.FindObject<EmailContent>(CriteriaOperator.Parse("EmailType=?", "Rejected"));

            Company company = ObjectSpace.FindObject<Company>(1);
            //EmailHistory emailHistory = new 
            //SystemUsers systemUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate));

            MailMessage mailMsg = new MailMessage();

            mailMsg.From = new MailAddress(company.EmailUser, company.Email);
            emailHistory.Sender = company.Email;
            emailHistory.DocNum = salesQuotation.DocNum;
            emailHistory.DocType = "SQ";
            emailHistory.EmailContent = emailContent;


            //ApprovalTemplate appTemplate = ObjectSpace.FindObject<ApprovalTemplate>(int.Parse(TemplateID));
            //SystemUsers sysUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=?", salesQuotation.SlpCode));
            foreach (SystemUsers sysUser in View.ObjectSpace.CreateCollection(typeof(SystemUsers), CriteriaOperator.Parse("SalesPersonCode=?", int.Parse(salesQuotation.SlpCode.SlpCode))))
            {
                //SystemUsers sysUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("Oid=?", approver.User.Oid));
                mailMsg.To.Add(sysUser.Email);
                receipient += sysUser.Email + ";";
            }


            mailMsg.Subject = emailContent.EmailTitle;

            mailMsg.Body = emailContent.Content.Replace("[@DocNum]", salesQuotation.DocNum).Replace("[@Oid]", salesQuotation.Oid.ToString()) + Environment.NewLine +
                HttpContext.Current.Request.Url.AbsoluteUri.Replace("Dialog=true&", "") + emailContent.Link.Replace("[@DocNum]", salesQuotation.DocNum).Replace("[@Oid]", salesQuotation.Oid.ToString());

            try
            {
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = company.SMTPHost,
                    Port = int.Parse(company.Port),
                    EnableSsl = true,
                };
                smtpClient.Credentials = new NetworkCredential(company.EmailUser, company.EmailPass);

                smtpClient.Send(mailMsg);

                mailMsg.Dispose();
                smtpClient.Dispose();
                emailHistory.Receipient = receipient;
                emailHistory.MailStatus = "Success";
                errmsg = "";
            }
            catch (Exception ex)
            {
                errmsg = ex.Message.Substring(0, 100);
                emailHistory.MailStatus = "Failed";
            }
            finally
            {
                emailHistory.SendDate = DateTime.Now;
                emailHistory.ErrorMsg = errmsg;
            }

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
        }

        private void Reject_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Reject Selected Sales Quotation, proceed?";
            e.View = dv;
        }
    }

    public class PriceHistory
    {

        public string DocType { get; set; }
        [DevExpress.ExpressApp.Data.Key]
        public string DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double PriceAfVat { get; set; }
        public double Quantity { get; set; }
    }
}
