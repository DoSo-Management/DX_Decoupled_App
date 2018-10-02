using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Updating;

namespace DXApplication1.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }


        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            //var users = ObjectSpace.GetObjects<SecuritySystemUser>();


            //var adminEmployeeRole = ObjectSpace.FindObject<DoSoRole>(
            //    new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            //if (adminEmployeeRole == null) {
            //    adminEmployeeRole = ObjectSpace.CreateObject<DoSoRole>();
            //    adminEmployeeRole.Name = SecurityStrategy.AdministratorRoleName;
            //    adminEmployeeRole.IsAdministrative = true;
            //}
            //var adminEmployee = ObjectSpace.FindObject<DoSoUser>(
            //    new BinaryOperator("UserName", "DOSO\\pchitashvili"));
            //if (adminEmployee == null) {
            //    adminEmployee = ObjectSpace.CreateObject<DoSoUser>();
            //    adminEmployee.UserName = "DOSO\\pchitashvili";
            //    //adminEmployee.FirstName = "Andrew";
            //    //adminEmployee.LastName = "Fuller";
            //    adminEmployee.SetPassword("");
            //    adminEmployee.EmployeeRoles.Add(adminEmployeeRole);
            //}
           
            ObjectSpace.CommitChanges();
        }
    }
}