using System;
using System.Collections.Generic;
using PekatPortal.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace PekatPortal.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CustomLogonParameter : ViewController //AuthenticationBase, IAuthenticationStandard
    {
        //private CustomLogonParameters customLogonParameters;
        //public CustomLogonParameter()
        //{
        //    customLogonParameters = new CustomLogonParameters();
        //}
        //public override void Logoff()
        //{
        //    base.Logoff();
        //    customLogonParameters = new CustomLogonParameters();
        //}
        //public override void ClearSecuredLogonParameters()
        //{
        //    customLogonParameters.Password = "";
        //    base.ClearSecuredLogonParameters();
        //}
        //public override object Authenticate(IObjectSpace objectSpace)
        //{

        //    SystemUsers employee = objectSpace.FindObject<SystemUsers>(CriteriaOperator.Parse("UserName=? AND Company=?", customLogonParameters.UserName,customLogonParameters.Company.Oid));

        //    if (employee == null)
        //        throw new AuthenticationException("","Invalid User Name");

        //    if (!employee.ComparePassword(customLogonParameters.Password))
        //        throw new AuthenticationException(
        //            employee.UserName, "Password mismatch.");

        //    return employee;
        //}

        //public override void SetLogonParameters(object logonParameters)
        //{
        //    this.customLogonParameters = (CustomLogonParameters)logonParameters;
        //}

        //public override IList<Type> GetBusinessClasses()
        //{
        //    return new Type[] { typeof(CustomLogonParameter) };
        //}
        //public override bool AskLogonParametersViaUI
        //{
        //    get { return true; }
        //}
        //public override object LogonParameters
        //{
        //    get { return customLogonParameters; }
        //}
        //public override bool IsLogoffEnabled
        //{
        //    get { return true; }
        //}
    }
}
