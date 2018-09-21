using System.Collections.Generic;
using System.Linq;
using BLL;

namespace ApiViewModelMapper
{
    public class ApiMapper : IApiMapper
    {
        readonly IPersistentClasses2Bl _persistentClasses2Bl;
        readonly IPCRepository _pcRepository;

        public ApiMapper(IPersistentClasses2Bl persistentClasses2Bl, IPCRepository pcRepository)
        {
            _persistentClasses2Bl = persistentClasses2Bl;
            _pcRepository = pcRepository;
        }

        public void GetObjectFromDatabase(TestViewModel viewModel)
        {
            var obj = _pcRepository.GetFromDb(viewModel.ID);
            obj.IntProperty = viewModel.ID;

            _persistentClasses2Bl.CalculatePremium(obj);

            _pcRepository.SaveObject(obj);
        }

        public TestViewModel GetObjectFromDatabase(int id)
        {
            var x = _pcRepository.GetFromDb(id);
            return new TestViewModel { ID = x.IntProperty, StringProperty = x.StringProperty, OID = x.Oid };
        }

        public IEnumerable<TestViewModel> GetAllObjectFromDatabase()
        {
            return _pcRepository.GetAllFromDb().Select(s => new TestViewModel{ID = s.IntProperty, StringProperty = s.StringProperty, OID = s.Oid});
        }

        public TestViewModel AddObject(TestViewModel viewModel)
        {
            var obj = _pcRepository.CreateNewObject();
            obj.IntProperty = viewModel.ID;
            obj.StringProperty = viewModel.StringProperty;

            _persistentClasses2Bl.SaveObject(obj);

            viewModel.OID = obj.Oid;
            return viewModel;
        }
    }
}