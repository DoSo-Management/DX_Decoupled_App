using System;
using ClassLibrary3;
using DAL.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using RepoServices;

namespace DXApplication1.Module.Controllers
{

    // ReSharper disable once InconsistentNaming
    public class DI<TFrom, TTo>
    {
        public static void SetResolver(Func<TFrom, TTo> action) => Resolve += action;
        public static Func<TFrom, TTo> Resolve;// = t => {};
    }

    public class SampleController : ObjectViewController<ObjectView, EntityClasses2>
    {
        readonly Func<Type, PersistentClasses2Bl> _persistentClasses2Bl;
        public SampleController()
        {
            //_persistentClasses2Bl = objectType => DI.Resolve<PersistentClasses2Bl>(objectType);// => new PersistentClasses2Bl(new PCRepository());

            var action = new SimpleAction(this, "hi", PredefinedCategory.View);
            action.Execute += ActionOnExecute;
        }

        void ActionOnExecute(object sender, SimpleActionExecuteEventArgs simpleActionExecuteEventArgs)
        {
            var bl = _persistentClasses2Bl(ViewCurrentObject.GetType());

            bl.CalculatePremium(ViewCurrentObject);

            bl.SaveObject(ViewCurrentObject);
        }
    }


}
