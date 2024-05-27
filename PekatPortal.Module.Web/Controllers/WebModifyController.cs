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
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.SystemModule;
using PekatPortal.Module.BusinessObjects;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.ExpressApp.FileAttachments.Web;
using DevExpress.Web;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace PekatPortal.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class WebModifyController : WebModificationsController
    {
        FileDataPropertyEditor propertyEditor;
        public WebModifyController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            propertyEditor = View.FindItem("File") as FileDataPropertyEditor;
            if (propertyEditor != null)
                propertyEditor.ControlCreated += propertyEditor_ControlCreated;
            // Perform various tasks depending on the target View.
        }

        private void propertyEditor_ControlCreated(object sender, EventArgs e)
        {
            FileDataEdit control = ((FileDataPropertyEditor)sender).Editor;
            if (control != null)
                control.UploadControlCreated += control_UploadControlCreated;
        }
        private void control_UploadControlCreated(object sender, EventArgs e)
        {
            ASPxUploadControl uploadControl = ((FileDataEdit)sender).UploadControl;
            uploadControl.ValidationSettings.AllowedFileExtensions = new String[] { ".pdf" };
        }

        protected override void Save(DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs args)
        {
            DetailView view = DetailView;
            if (view.ObjectTypeInfo.Type != typeof(ConfirmationMsg)) base.Save(args);
           //IObjectSpace os = Application.CreateObjectSpace();
            if (view.ObjectTypeInfo.Type == typeof(SalesQuotation))
            {
                SalesQuotation SQ = (SalesQuotation)View.CurrentObject;
                if (SQ.Status == StatusEnum.Draft)
                {
                    sendEmail("SQ", SQ, "Draft");
                }
            }

            if (view.ObjectTypeInfo.Type != typeof(Reports) && view.ObjectTypeInfo.Type != typeof(SOReport) && view.ObjectTypeInfo.Type != typeof(Statement) && view.ObjectTypeInfo.Type != typeof(ConfirmationMsg))
            {
                view.ObjectSpace.CommitChanges();
                view.ViewEditMode = ViewEditMode.View;
                view.BreakLinksToControls();
                view.CreateControls();
            }
            if (view.ObjectTypeInfo.Type == typeof(ConfirmationMsg))
            {
                view.ViewEditMode = ViewEditMode.View;
                view.BreakLinksToControls();
                view.CreateControls();
            }

        }

        private void sendEmail(string transtype, SalesQuotation SQ, string emailType)
        {
            EmailHistory emailHistory = ObjectSpace.CreateObject<EmailHistory>();
            string errmsg = "";
            string receipient = "";
            try
            {
                if (SQ.Status == StatusEnum.Draft)
                {
                    EmailContent emailContent = ObjectSpace.FindObject<EmailContent>(CriteriaOperator.Parse("EmailType=?", emailType));

                    Company company = ObjectSpace.FindObject<Company>(1);
                    //EmailHistory emailHistory = new 
                    //SystemUsers systemUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate));

                    MailMessage mailMsg = new MailMessage();

                    mailMsg.From = new MailAddress(company.EmailUser, company.Email);
                    emailHistory.Sender = company.Email;
                    emailHistory.DocNum = SQ.DocNum;
                    emailHistory.DocType = transtype;
                    emailHistory.EmailContent = emailContent;

                    foreach (SystemUsers sysUser in View.ObjectSpace.CreateCollection(typeof(SystemUsers), CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate)))
                    {
                        if (sysUser.Email != "")
                        {
                            mailMsg.To.Add(sysUser.Email);
                            receipient += sysUser.Email + ";";
                        }
                    }

                    if (receipient == "")
                    {
                        errmsg = "No Email Address Found !";
                        return;
                    }

                    mailMsg.Subject = emailContent.EmailTitle;

                    mailMsg.Body = emailContent.Content.Replace("[@DocNum]", SQ.DocNum).Replace("[@Oid]", SQ.Oid.ToString()) + Environment.NewLine +
                        HttpContext.Current.Request.Url.AbsoluteUri + emailContent.Link.Replace("[@DocNum]", SQ.DocNum).Replace("[@Oid]", SQ.Oid.ToString());

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
                //else if (SQ.Status == StatusEnum.Posted)
                //{
                //    EmailContent emailContent = ObjectSpace.FindObject<EmailContent>(CriteriaOperator.Parse("EmailType=?", emailType));

                //    Company company = ObjectSpace.FindObject<Company>(1);
                //    //EmailHistory emailHistory = new 
                //    //SystemUsers systemUser = ObjectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("SalesPersonCode=? OR Oid = ?", SQ.SlpCode, SQ.Escalate));

                //    MailMessage mailMsg = new MailMessage();

                //    mailMsg.From = new MailAddress(company.EmailUser, company.Email);
                //    emailHistory.Sender = company.Email;
                //    emailHistory.DocNum = SQ.DocNum;
                //    emailHistory.DocType = transtype;
                //    emailHistory.EmailContent = emailContent;

                //    foreach (SystemUsers sysUser in View.ObjectSpace.CreateCollection(typeof(SystemUsers), CriteriaOperator.Parse("AllowAmend=?", "Y"))) 
                //    {
                //        if(sysUser.Email != "")
                //        {
                //            mailMsg.To.Add(sysUser.Email);
                //            receipient += sysUser.Email + ";";
                //        }
                //    }

                //    if (receipient == "")
                //    {
                //        errmsg = "No Email Address Found !";
                //        return;
                //    }

                //    mailMsg.Subject = emailContent.EmailTitle;

                //    mailMsg.Body = emailContent.Content.Replace("[@DocNum]", SQ.DocNum).Replace("[@Oid]", SQ.Oid.ToString()) + Environment.NewLine +
                //        HttpContext.Current.Request.Url.AbsoluteUri + emailContent.Link.Replace("[@DocNum]", SQ.DocNum).Replace("[@Oid]", SQ.Oid.ToString());

                //    SmtpClient smtpClient = new SmtpClient
                //    {
                //        Host = company.SMTPHost,
                //        Port = int.Parse(company.Port),
                //        EnableSsl = true,
                //    };
                //    smtpClient.Credentials = new NetworkCredential(company.EmailUser, company.EmailPass);

                //    smtpClient.Send(mailMsg);

                //    mailMsg.Dispose();
                //    smtpClient.Dispose();
                //    emailHistory.Receipient = receipient;
                //    emailHistory.MailStatus = "Success";
                //    errmsg = "";
                //}
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
                ObjectSpace.CommitChanges();
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            if (propertyEditor != null)
                propertyEditor.ControlCreated -= propertyEditor_ControlCreated;
        }
    }
}
