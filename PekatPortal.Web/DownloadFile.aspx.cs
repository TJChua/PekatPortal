using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PekatPortal.Web
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = HttpContext.Current.Server.MapPath("~\\Report\\") + Request.QueryString["filename"];
            if (System.IO.File.Exists(filename))
            {
                FileInfo fileInfo = new FileInfo(filename);
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                //To Download PDF
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);

                //To View PDF
                //Response.AddHeader("Content-Disposition", "inline; filename=" + fileInfo.Name);

                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.WriteFile(fileInfo.FullName);
                Response.Flush();
                Response.End();
            }
        }
    }
}