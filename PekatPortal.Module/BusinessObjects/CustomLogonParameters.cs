using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using PekatPortal.Module.BusinessObjects;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace PekatPortal.Module.BusinessObjects
{
    //public interface IDatabaseNameParameter
    //{
    //    string DatabaseName { get; set; }
    //}

    //[DomainComponent, Serializable]
    //[System.ComponentModel.DisplayName("Log In")]
    //public class CustomLogonParameters : INotifyPropertyChanged, ISerializable
    //{
    //    private Company company;
    //    //private string employee;
    //    private string password;

    //    //[ImmediatePostData]
    //    public Company Company
    //    {
    //        get { return company; }
    //        set
    //        {
    //            if (value == company) return;
    //            company = value;
    //            //Employee = null;
    //            OnPropertyChanged("CompanyName");
    //        }
    //    }

    //    ////[DataSourceProperty()]
    //    ////[DataSourceProperty("SystemUsers.Company"), ImmediatePostData]
    //    //public string Employee
    //    //{
    //    //    get { return employee; }
    //    //    set
    //    //    {
    //    //        if (employee == value) return;
    //    //        employee = value;
    //    //    }
    //    //    //get { return employee; }
    //    //    //set
    //    //    //{
    //    //    //    if (Company == null) return;
    //    //    //    employee = value;
    //    //    //    if (employee != null)
    //    //    //    {
    //    //    //        UserName = employee.UserName;
    //    //    //    }
    //    //    //    OnPropertyChanged("UserName");
    //    //    //}
    //    //}

    //    //[Browsable(false)]
    //    public String UserName {
    //        get;
    //        set;
    //    }

    //    [PasswordPropertyText(true)]
    //    public string Password
    //    {
    //        get { return password; }
    //        set
    //        {
    //            if (password == value) return;
    //            password = value;
    //        }
    //    }

    //    public CustomLogonParameters() { }
    //    // ISerializable 
    //    public CustomLogonParameters(SerializationInfo info, StreamingContext context)
    //    {
    //        if (info.MemberCount > 0)
    //        {
    //            UserName = info.GetString("UserName");
    //            Password = info.GetString("StoredPassword");
    //        }
    //    }
    //    private void OnPropertyChanged(string propertyName)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    [System.Security.SecurityCritical]
    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        info.AddValue("UserName", UserName);
    //        info.AddValue("StoredPassword", Password);
    //    }
    //}
}