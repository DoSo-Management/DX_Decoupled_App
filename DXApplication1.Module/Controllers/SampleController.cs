using BLL;
using DAL.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace DXApplication1.Module.Controllers
{
    public class SampleController : ObjectViewController<ObjectView, Policy>
    {
        public SampleController()
        {
            var action = new SimpleAction(this, "SIMPLE_ACTION", PredefinedCategory.View);
            action.Execute += ActionOnExecute;
        }

        void ActionOnExecute(object sender, SimpleActionExecuteEventArgs simpleActionExecuteEventArgs)
        {
            var bl = ViewCurrentObject.Bl<PolicyBl>();

            bl.CalculatePremium(ViewCurrentObject);
        }
    }
}
