using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PekatPortal.Module
{
    public static class GeneralSettings
    {
        public static SAPbobsCOM.Company oCompany;
        public const string PRsuperuserrole = "PRSuperUserRole";
        public const string PRuserrole = "PRUserRole";
        public const string verifyrole = "VerifyUserRole";
        public const string Acceptancerole = "AcceptanceUserRole";
        public const string postrole = "PostUserRole";
        public const string ApprovalRole = "ApprovalUserRole";
        public const string PDRole = "PDUserRole";

        public const string hq = "HQ";

        public static string appurl = "";

        public static bool EmailSend;
        public static string EmailHost = "";
        public static string EmailHostDomain = "";
        public static string EmailPort = "";
        public static string Email = "";
        public static string EmailPassword = "";
        public static string EmailName = "";
        public static bool EmailSSL;
        public static bool EmailUseDefaultCredential;
        public static string DeliveryMethod = "";

        //public static bool B1Post;
        public static string B1UserName = "";
        public static string B1Password = "";
        public static string B1Server = "";
        public static string B1CompanyDB = "";
        public static string B1License = "";
        public static string B1DbServerType = "";
        public static string B1DbUserName = "";
        public static string B1DbPassword = "";
        public static string B1AttachmentPath = "";

        public static int B1APPRseries = 0;

        public static bool IsBeingLookup = false;
        public static string ObjType = ""; // to temporary save current selected ObjType
        public static string WhsCode = ""; // to temporary save current selected WhsCode
        public static string ItemCode = ""; // to temporary save current selected ItemCode
        public static string CustomerOid = ""; // to temporary save current selected customer's Oid
    }

}
