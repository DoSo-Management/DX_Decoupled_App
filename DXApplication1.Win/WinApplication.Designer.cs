using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;

namespace DXApplication1.Win
{
    partial class DXApplication1WindowsFormsApplication
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new DXApplication1.Module.DXApplication1Module();
            this.module4 = new DXApplication1.Module.Win.DXApplication1WindowsFormsModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DXApplication1WindowsFormsApplication
            // 
            this.ApplicationName = "DXApplication1";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);


            var ad = new AuthenticationActiveDirectory<SecuritySystemUser>();
            ad.CreateUserAutomatically = true;

            var securityStrategyComplex1 = new SecurityStrategyComplex();
            securityStrategyComplex1.Authentication = ad;
            //securityStrategyComplex1.Authentication = new SecurityDemoAuthentication(this, RequestedCurentUserName);

            securityStrategyComplex1.RoleType = typeof(SecuritySystemRole);
            securityStrategyComplex1.UserType = typeof(SecuritySystemUser);

            Security = securityStrategyComplex1;

            this.Modules.Add(new SecurityModule());

            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.DXApplication1WindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.DXApplication1WindowsFormsApplication_CustomizeLanguagesList);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private DXApplication1.Module.DXApplication1Module module3;
        private DXApplication1.Module.Win.DXApplication1WindowsFormsModule module4;
    }
}
