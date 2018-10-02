using System;
using System.Linq;
using BLL;
using DAL.BusinessObjects;
using DAL.CriteriaOperator;
using DAL.ValueObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace DXApplication1.Module.Controllers
{
    public class SampleController : ObjectViewController<ObjectView, Client>
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

        void SingleChoiceAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var bl = ViewCurrentObject.Bl<PolicyBl>();

            var policyPremium = PolicyPremium.Create(15m, new Currency(ViewCurrentObject.Session));
            throw new Exception(policyPremium.ToString());
        }

        void ActionOnExecute(object sender, SimpleActionExecuteEventArgs simpleActionExecuteEventArgs)
        {
            try
            {
                PolicyEqualsCriteriaOperator.Register();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

            //var filtered = ObjectSpace.FindObject<Client>(ObjectSpace.ParseCriteria("Policy.Number == '36'"));

            var f = ViewCurrentObject.Session.Query<Client>().Where(c => c.PolicyNumberIsEqualTo("DOSO\\pchitashvili")).ToList();

            //var bl = ViewCurrentObject.Bl<PolicyBl>();

            //bl.CalculatePremium(ViewCurrentObject);
        }
    }
}
