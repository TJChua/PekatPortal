using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using PekatPortal.Module.BusinessObjects;

namespace PekatPortal.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            SystemUsers sampleUser = ObjectSpace.FindObject<SystemUsers>(new BinaryOperator("UserName", "User"));
            if(sampleUser == null) {
                sampleUser = ObjectSpace.CreateObject<SystemUsers>();
                sampleUser.UserName = "User";
                sampleUser.SetPassword("");
            }
            PermissionPolicyRole defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

            SystemUsers userAdmin = ObjectSpace.FindObject<SystemUsers>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<SystemUsers>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
			// If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
            if(adminRole == null) {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);
            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private PermissionPolicyRole CreateDefaultRole() {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Salesperson"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Salesperson";

                //Navigation Permission
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/DraftSalesQuotation", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/SalesQuotation_ListView", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/SalesQuotation_ListView_Approval", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/SalesQuotation_ListView_Approved", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/SalesQuotation_ListView_Rejected", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/SalesQuotation_ListView_Posted", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Sales Order/Items/SalesQuotation_ListView_Canceled", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Master Data/Items/Customer Master", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Master Data/Items/Inventory Master", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Listing/Items/SalesOrder", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Listing/Items/Delivery", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Listing/Items/Invoice", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Report/Items/CSA", SecurityPermissionState.Allow);

                //SystemUsers
                defaultRole.AddTypePermission<SystemUsers>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddObjectPermission<SystemUsers>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<SystemUsers>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<SystemUsers>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                //ModelDifference
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //ModelDifferenceAspect
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);


               // #region Permission Type

                //Sales Order
                defaultRole.AddTypePermission<SalesQuotation>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotation>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotation>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Sales Order Details
                defaultRole.AddTypePermission<SalesQuotationDetail>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationDetail>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationDetail>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationDetail>(SecurityOperations.Delete, SecurityPermissionState.Allow);

                //Approval Status
                defaultRole.AddTypePermission<DocStatus>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DocStatus>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DocStatus>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Approval Status
                defaultRole.AddTypePermission<Approval>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Approval>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Approval>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Approval Status
                defaultRole.AddTypePermission<EmailHistory>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EmailHistory>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EmailHistory>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Approval Status
                defaultRole.AddTypePermission<EmailContent>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EmailContent>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EmailContent>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Sales Order
                defaultRole.AddTypePermission<SalesQuotationRev>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationRev>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationRev>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Sales Order Details
                defaultRole.AddTypePermission<SalesQuotationRevDetail>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationRevDetail>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SalesQuotationRevDetail>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Attachment
                defaultRole.AddTypePermission<Attachment>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Attachment>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Attachment>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Sales Order
                defaultRole.AddTypePermission<ReportList>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<ReportList>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<ReportList>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Sales Order
                defaultRole.AddTypePermission<Reports>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Reports>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Reports>(SecurityOperations.Create, SecurityPermissionState.Allow);

                //Sales Order
                defaultRole.AddTypePermission<SOReport>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SOReport>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SOReport>(SecurityOperations.Create, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<ItemCategory>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DiscountStructure>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Originator>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<QuotationLimit>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Approver>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Approval>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<ApprovalTemplate>(SecurityOperations.Read, SecurityPermissionState.Allow);

                //Master Data
                defaultRole.AddTypePermission<vw_BillToAddress>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_Committed>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_CustomerList>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_DLN1>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_INV1>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ItemList>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ItemWarehouse>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ODLN>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_OINV>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_OnOrdered>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ORDR>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_OutputTax>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_PaymentTerm>(SecurityOperations.Read, SecurityPermissionState.Allow);
                //defaultRole.AddTypePermission<vw_PostedMR>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_PredefinedText>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ProjectList>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_RDR1>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_SalesPerson>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ShipToAddress>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_WarehouseList>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ExchangeRate>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<vw_ContactPerson>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Company>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DocumentNumber>(SecurityOperations.Write, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DocumentNumber>(SecurityOperations.Read, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }
    }
}
