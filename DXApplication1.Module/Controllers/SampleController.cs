using System;
using System.Linq;
using ApiViewModelMapper;
using BLL;
using DAL.BusinessObjects;
using DAL.CriteriaOperator;
using DAL.ValueObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Domain.NETStandard;

namespace DXApplication1.Module.Controllers
{
    public class SampleController : ObjectViewController<ObjectView, Client>
    {
        public SampleController()
        {
            var action = new SimpleAction(this, "SIMPLE_ACTION", PredefinedCategory.View);
            var actionApi = new SimpleAction(this, "API_CALL", PredefinedCategory.View);
            actionApi.Execute += ActionApi_Execute;
            action.Execute += ActionOnExecute;

            var singleChoiceAction = new SingleChoiceAction(this, "SINGLE_CHOICE_ACTION", PredefinedCategory.View);
            singleChoiceAction.Items.Add(new ChoiceActionItem("Item1", "Item1"));
            singleChoiceAction.Items.Add(new ChoiceActionItem("Item2", "Item2"));
            singleChoiceAction.Items.Add(new ChoiceActionItem("Item3", "Item3"));

            singleChoiceAction.Execute += SingleChoiceAction_Execute;
        }

        private async void ActionApi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var client = new ApiClient.ApiClient();
            
            var model = new TestViewModel
            {
                ID = 778899,
                OID = 0,
                SumInsured = 150
            };

            var res = await client.PostDataToApi(model);

            var data = await client.GetDataFromApi();

            var single = await client.GetDataByIdFromApi(4);
        }

        void SingleChoiceAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var bl = ViewCurrentObject.Bl<PolicyBl>();

            var policyPremium = new PolicyPremium(15m, new CurrencyV("empty"));
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
