using System;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using PekatPortal.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using System.Web;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.Web;

public partial class Default : BaseXafPage
{
    protected override ContextActionsMenu CreateContextActionsMenu()
    {
        return new ContextActionsMenu(this, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports");
    }
    public override Control InnerContentPlaceHolder
    {
        get
        {
            return Content;
        }
    }

    protected void Page_Init()
    {
        CustomizeTemplateContent += (s, e) =>
        {
            IHeaderImageControlContainer content = TemplateContent as IHeaderImageControlContainer;
            if (content == null) return;
            content.HeaderImageControl.DefaultThemeImageLocation = "Images";
            content.HeaderImageControl.ImageName = "Pekat_Engineering.png";
            content.HeaderImageControl.Width = Unit.Pixel(150);
            content.HeaderImageControl.Height = Unit.Pixel(50);
        };
    }

    //            if (popupHeight > currentHeight + 8) { }
    //protected void Page_Init()
    //{
    //    if (WebApplication.Instance != null)
    //    {

    //        //using (IObjectSpace os = WebApplication.Instance.CreateObjectSpace())
    //        //{
    //        //    SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
    //        //    Company obj = os.FindObject<Company>(new BinaryOperator("Oid", user.Company.Oid));
    //        //    if (obj != null)
    //        //    {
    //        //        try
    //        //        {
    //        //            IHeaderImageControlContainer content = TemplateContent as IHeaderImageControlContainer;
    //        //            content.HeaderImageControl.DefaultThemeImageLocation = "Images";
    //        //            content.HeaderImageControl.ImageName = "Pekat_Engineering.png";
    //        //            //content.HeaderImageControl.ImageUrl = DevExpress.ExpressApp.Web.Editors.ASPx.ImageEditHelper.GetImageUrl(os, obj, "ImageProperty");
    //        //            content.HeaderImageControl.Width = Unit.Pixel(150);
    //        //            content.HeaderImageControl.Height = Unit.Pixel(50);
    //        //            //                    this.LogoImageName..ImageUrl = DevExpress.ExpressApp.Web.Editors.ASPx.ImageEditHelper.GetImageUrl(os, obj, "ImageProperty");
    //        //        }
    //        //        catch (Exception)
    //        //        {

    //        //        }
    //        //    }
    //        //}
    //    }
    //}
}
