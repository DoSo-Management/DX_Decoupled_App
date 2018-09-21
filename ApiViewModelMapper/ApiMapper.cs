using System.Collections.Generic;
using ApiViewModelMapper;

namespace BLL
{
    public class ApiMapper : IApiMapper
    {
        readonly IPersistentClasses2Bl _persistentClasses2Bl;

        public ApiMapper(IPersistentClasses2Bl persistentClasses2Bl)
        {
            _persistentClasses2Bl = persistentClasses2Bl;
        }

        public void GetObjectFromDatabase(TestViewModel viewModel)
        {
            var obj = _persistentClasses2Bl.GetFromDb(viewModel.ID);
            obj.IntProperty = viewModel.ID;

            _persistentClasses2Bl.CalculatePremium(obj);

            _persistentClasses2Bl.SaveObject(obj);
        }

        public TestViewModel GetObjectFromDatabase(int id)
        {
            var x = _persistentClasses2Bl.GetFromDb(id);
            return new TestViewModel { ID = x.IntProperty, StringProperty = x.StringProperty, OID = x.Oid };
        }

        public IEnumerable<TestViewModel> GetAllObjectFromDatabase()
        {
            return new List<TestViewModel> { GetObjectFromDatabase(4) };
        }

        public TestViewModel AddObject(TestViewModel viewModel)
        {
            var obj = _persistentClasses2Bl.CreateNewObject();
            obj.IntProperty = viewModel.ID;
            obj.StringProperty = viewModel.StringProperty;

            _persistentClasses2Bl.SaveObject(obj);

            viewModel.OID = obj.Oid;
            return viewModel;
        }
    }
}