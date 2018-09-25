using System.Collections.Generic;

namespace ApiViewModelMapper
{
    public interface IApiService
    {
        TestViewModel GetObjectFromDatabase(int id);
        IEnumerable<TestViewModel> GetAllObjectFromDatabase();
        TestViewModel AddObject(TestViewModel viewModel);
    }
}