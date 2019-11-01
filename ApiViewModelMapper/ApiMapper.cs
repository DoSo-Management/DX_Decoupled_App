using System.Collections.Generic;
using System.Linq;
using BLL;
using DAL.BusinessObjects;

namespace ApiViewModelMapper
{
    public class ApiService : IApiService
    {
        readonly PolicyBl _iec2Bl;
        readonly IRepositoryID<Policy> _eC2Repository;

        public ApiService(PolicyBl iec2Bl, IRepositoryID<Policy> _eC2Repository)
        {
            _iec2Bl = iec2Bl;
            this._eC2Repository = _eC2Repository;
        }

        public void GetObjectFromDatabase(TestViewModel viewModel)
        {
            var obj = _eC2Repository.Get(viewModel.ID);
            obj.SetPremiumAndSumInsured(viewModel.ID, 0);

            _iec2Bl.CalculatePremium(obj);

            _eC2Repository.Save(obj);
        }

        public TestViewModel GetObjectFromDatabase(int id)
        {
            var x = _eC2Repository.Get(id);
            return new TestViewModel { ID = x.Oid, SumInsured = x.SumInsured, OID = x.Oid };
        }

        public IEnumerable<TestViewModel> GetAllObjectFromDatabase()
            => _eC2Repository.GetAll().Select(s => new TestViewModel { ID = s.Oid, SumInsured = s.SumInsured, OID = s.Oid });

        public TestViewModel AddObject(TestViewModel viewModel)
        {
            var obj = _eC2Repository.Create();
            obj.SetPremiumAndSumInsured(viewModel.ID, viewModel.SumInsured);

            _eC2Repository.Save(obj);

            viewModel.OID = obj.Oid;
            return viewModel;
        }
    }
}