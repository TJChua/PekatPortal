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
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using System.ComponentModel;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace PekatPortal.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilterController : ViewController
    {
        public FilterController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {

            base.OnActivated();
            Validator.RuleSet.ValidationCompleted += RuleSet_ValidationCompleted;


            //if (View.ObjectTypeInfo.Type == typeof(vw_ItemList))
            //{
            //    if (View.Id == "vw_ItemList_DetailView")
            //    {
            //        //vw_ItemList masterobject = (vw_ItemList)collectionSource.MasterObject;
            //        //if (PRRole != null)
            //        //{
            //        //    ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[CreateUser] = ?", user.Oid);
            //        //}
            //    }
            //}
            // Perform various tasks depending on the target View.
            //if (View.ObjectTypeInfo.Type == typeof(vw_ItemList))
            //{
            //if (View is ListView)
            //{
            //Application.ListViewCreating += Application_ListViewCreating;
            //}
            //}
        }
        private void RuleSet_ValidationCompleted(object sender, ValidationCompletedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.Exception.ObjectHeaderFormat = "";
            }
        }
        //private void Application_ListViewCreating(Object sender, ListViewCreatingEventArgs e)
        //{
        //    if (e.CollectionSource.ObjectSpace is NonPersistentObjectSpace)
        //    {
        //        ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
        //    }
        //}

        //private void ObjectSpace_ObjectsGetting(Object sender, ObjectsGettingEventArgs e)
        //{
        //    if (e.ObjectType == typeof(vw_ItemList))
        //    {
        //        if (View is ListView)
        //        {
        //            XPObjectSpace persistentObjectSpace = (XPObjectSpace)Application.CreateObjectSpace();
        //            SelectedData sprocData = persistentObjectSpace.Session.ExecuteSproc("sp_GetItemList", new OperandValue("TWE_LIVE"));
        //            IList<vw_ItemList> lists = new List<vw_ItemList>();
        //            foreach (SelectStatementResultRow row in sprocData.ResultSet[0].Rows)
        //            {
        //                vw_ItemList itemList = new vw_ItemList();
        //                itemList.ItemCode = row.Values[0].ToString();
        //                itemList.ItemName = row.Values[1] == null? "": row.Values[1].ToString();
        //                lists.Add(itemList);
        //            }
        //            BindingList<vw_ItemList> objects = new BindingList<vw_ItemList>(lists);
        //            e.Objects = objects;
        //            persistentObjectSpace.Dispose();
        //            sprocData = null;
        //        }
        //    }
        //}
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            this.Revision.Active.SetItemValue("Enabled", false);
            this.Confirm.Active.SetItemValue("Enabled", false);

            if (View.Id == "SalesQuotation_DetailView")
            {
                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                //SystemUsers salesperson = ObjectSpace.Session.GetObjectByKey<vw_BillToAddress>(salesQuotation.BillToDef.Key);
                BusinessObjects.SalesQuotation SO = View.CurrentObject as BusinessObjects.SalesQuotation;
                SystemUsers salesperson = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=?", SO.SlpCode));
                if ((SO.SlpCode.SlpCode == user.SalesPersonCode.SlpCode || (salesperson.Escalate == null || user.Oid == salesperson.Escalate.Oid)) && (SO.Status == StatusEnum.Draft || SO.Status == StatusEnum.Open))
                {
                    this.Confirm.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                }

                if ((SO.Status == StatusEnum.Open || SO.Status == StatusEnum.Approved) && (SO.SlpCode.SlpCode == user.SalesPersonCode.SlpCode || user.AllowAmend == "Y"))
                {
                    this.Revision.Active.SetItemValue("Enabled", ((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.View);
                }
            }
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            //if (View is ListView)
            //{
            //Application.ListViewCreating -= Application_ListViewCreating;
            //}
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            Validator.RuleSet.ValidationCompleted -= RuleSet_ValidationCompleted;
        }

        private void Revision_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            try
            {
                SalesQuotation salesQuotation = (SalesQuotation)e.CurrentObject;
                IObjectSpace os = Application.CreateObjectSpace();
                SalesQuotationRev newSalesQuotation = os.CreateObject<SalesQuotationRev>();

                newSalesQuotation.DocNum = salesQuotation.DocNum;
                newSalesQuotation.Status = salesQuotation.Status;
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
                //newSalesQuotation.PaymentTerm = salesQuotation.PaymentTerm.GroupNum;
                newSalesQuotation.PaymentTermName = salesQuotation.PaymentTermName;
                newSalesQuotation.DocEntry = salesQuotation.DocEntry.DocEntry;
                newSalesQuotation.SlpCode = salesQuotation.SlpCode.SlpCode;
                newSalesQuotation.SlpName = salesQuotation.SlpCode.SlpName;
                newSalesQuotation.CardCode = salesQuotation.CardCode;
                newSalesQuotation.CardName = salesQuotation.CardName;
                //if (salesQuotation.ContactPerson != null) newSalesQuotation.ContactPerson = newSalesQuotation.Session.GetObjectByKey<vw_ContactPerson>(salesQuotation.ContactPerson.CntctCode);
                newSalesQuotation.Name = salesQuotation.Name;
                newSalesQuotation.Tel1 = salesQuotation.Tel1;
                newSalesQuotation.Email = salesQuotation.Email;
                //if (salesQuotation.BillToDef != null) newSalesQuotation.BillToDef = newSalesQuotation.Session.GetObjectByKey<vw_BillToAddress>(salesQuotation.BillToDef.Key);
                newSalesQuotation.BillToAdress1 = salesQuotation.BillToAdress1;
                newSalesQuotation.BillToAdress2 = salesQuotation.BillToAdress2;
                newSalesQuotation.BillToAdress3 = salesQuotation.BillToAdress3;
                newSalesQuotation.BillToAdress4 = salesQuotation.BillToAdress4;
                //if (salesQuotation.ShipToDef != null) newSalesQuotation.ShipToDef = newSalesQuotation.Session.GetObjectByKey<vw_ShipToAddress>(salesQuotation.ShipToDef.Key);
                newSalesQuotation.ShipToAdress1 = salesQuotation.ShipToAdress1;
                newSalesQuotation.ShipToAdress2 = salesQuotation.ShipToAdress2;
                newSalesQuotation.ShipToAdress3 = salesQuotation.ShipToAdress3;
                newSalesQuotation.ShipToAdress4 = salesQuotation.ShipToAdress4;
                newSalesQuotation.Discount = salesQuotation.Discount;
                newSalesQuotation.DiscountAmt = salesQuotation.DiscountAmt;
                newSalesQuotation.TotalBeforeDiscount = salesQuotation.TotalBeforeDiscount;
                newSalesQuotation.GrandTotal = salesQuotation.GrandTotal;
                newSalesQuotation.TaxAmount = salesQuotation.TaxAmount;
                newSalesQuotation.Currency = salesQuotation.Currency;
                newSalesQuotation.CurrRate = salesQuotation.CurrRate;
                newSalesQuotation.Company = salesQuotation.Company.Oid;
                newSalesQuotation.SalesQuotation = newSalesQuotation.Session.GetObjectByKey<SalesQuotation>(salesQuotation.Oid);

                object count = newSalesQuotation.Session.Evaluate(typeof(SalesQuotationRev), CriteriaOperator.Parse("Count()"),
    CriteriaOperator.Parse("SalesQuotation = ?", salesQuotation.Oid));

                if (count.ToString() == "0")
                {
                    newSalesQuotation.RevisionNo = salesQuotation.RevisionNo;
                    salesQuotation.RevisionNo = "2";
                }
                else
                {
                    newSalesQuotation.RevisionNo = salesQuotation.RevisionNo;
                    salesQuotation.RevisionNo = (int.Parse(count.ToString()) + 2).ToString();
                }

                //newSalesQuotation.RevisionNo = "R" + double.TryParse(count,out 1).ToString();
                foreach (SalesQuotationDetail dtl in salesQuotation.SalesQuotationDetail)
                {
                    SalesQuotationRevDetail newSalesQuotationdetails = os.CreateObject<SalesQuotationRevDetail>();

                    newSalesQuotationdetails.ItemCode = dtl.ItemCode.ItemCode;
                    newSalesQuotationdetails.ItemName = dtl.ItemName;
                    newSalesQuotationdetails.Quantity = dtl.Quantity;
                    newSalesQuotationdetails.UOM = dtl.UOM;
                    newSalesQuotationdetails.SuppCatNum = dtl.SuppCatNum;
                    newSalesQuotationdetails.Warehouse = dtl.Warehouse.WhsCode;
                    newSalesQuotationdetails.UnitPrice = dtl.UnitPrice;
                    newSalesQuotationdetails.UnitPriceAfDisc = dtl.UnitPriceAfDisc;
                    newSalesQuotationdetails.TaxCode = dtl.TaxCode.Code;
                    newSalesQuotationdetails.Discount = dtl.Discount;
                    newSalesQuotationdetails.DiscountAmt = dtl.DiscountAmt;
                    newSalesQuotationdetails.LineTotal = dtl.LineTotal;
                    newSalesQuotationdetails.ItemDetail = dtl.ItemDetail;

                    newSalesQuotation.SalesQuotationRevDetail.Add(newSalesQuotationdetails);
                }
                os.CommitChanges();
                os.Refresh();
                ObjectSpace.CommitChanges(); //This line persists created object(s).
                ObjectSpace.Refresh();

                showMsg("Success", "Quotation Revision No. " + newSalesQuotation.RevisionNo + " Captured", InformationType.Success);
            }
            catch (Exception ex)
            {
                showMsg("Error", "Quotation Revision Error - " + ex.Message, InformationType.Error);
            }
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

        private void Revision_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Revision will take a snapshot of current Sales Quotation, proceed?";
            e.View = dv;
        }

        private void Confirm_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            string TemplateID = "1";
            string TemplateName = "";
            string TemplateLevel = "1";
            string receipient = "";
            string errmsg = "";
            SalesQuotation salesQuotation = (SalesQuotation)e.CurrentObject;
            XPObjectSpace persistentObjectSpace = (XPObjectSpace)Application.CreateObjectSpace();
            IObjectSpace os = Application.CreateObjectSpace();
            DocStatus docStatus = os.CreateObject<DocStatus>();
            SelectedData results = persistentObjectSpace.Session.ExecuteSproc("sp_CheckApproval", new OperandValue(salesQuotation.Oid), new OperandValue(1), new OperandValue(salesQuotation.CurrentAppTmpName));
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
                    docStatus.Status = "Sent to Approval";
                    docStatus.TemplateName = TemplateName;

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


                    //ApprovalTemplate appTemplate = ObjectSpace.FindObject<ApprovalTemplate>(int.Parse(TemplateID));

                    foreach (Approver approver in View.ObjectSpace.CreateCollection(typeof(Approver), CriteriaOperator.Parse("ApprovalTemplate=?", int.Parse(TemplateID))))
                    {
                        SystemUsers sysUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("Oid=?", approver.User.Oid));
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
                    docStatus.SalesQuotation = os.GetObjectByKey<SalesQuotation>(int.Parse(salesQuotation.Oid.ToString()));
                    docStatus.CreateUser = os.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    docStatus.CreateDate = DateTime.Now;
                    os.CommitChanges();
                }
                else
                {
                    salesQuotation.Status = StatusEnum.Approved;
                    salesQuotation.CurrentAppLevel = "";
                    salesQuotation.CurrentAppTmpName = "";
                    salesQuotation.CurrentAppTmp = "";
                    salesQuotation.IsApproved = "Y";
                }
                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
            }
        }

        private void Confirm_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ConfirmationMsg>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
            ((ConfirmationMsg)dv.CurrentObject).IsErr = false;
            ((ConfirmationMsg)dv.CurrentObject).ActionMessage = "Sales Quotation will flow to Approval if necessary, proceed?";
            e.View = dv;
        }
    }
}
