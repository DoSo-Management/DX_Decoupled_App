using System;
using BLL;
using DAL.BusinessObjects;
using DAL.ValueObjects;
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

            var singleChoiceAction = new SingleChoiceAction(this, "SINGLE_CHOICE_ACTION", PredefinedCategory.View);
            singleChoiceAction.Items.Add(new ChoiceActionItem("Item1", "Item1"));
            singleChoiceAction.Items.Add(new ChoiceActionItem("Item2", "Item2"));
            singleChoiceAction.Items.Add(new ChoiceActionItem("Item3", "Item3"));

            singleChoiceAction.Execute += SingleChoiceAction_Execute;
        }

        private void SingleChoiceAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var bl = ViewCurrentObject.Bl<PolicyBl>();

            var policyPremium = PolicyPremium.Create(15m, new Currency(ViewCurrentObject.Session));
            throw new Exception(policyPremium.ToString());
        }

        void ActionOnExecute(object sender, SimpleActionExecuteEventArgs simpleActionExecuteEventArgs)
        {
            var bl = ViewCurrentObject.Bl<PolicyBl>();

            bl.CalculatePremium(ViewCurrentObject);
        }
    }
}
