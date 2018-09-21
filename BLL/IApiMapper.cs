using System.Collections.Generic;

namespace ClassLibrary3
{
    public interface IApiMapper
    {
        TestViewModel GetObjectFromDatabase(int id);
        IEnumerable<TestViewModel> GetAllObjectFromDatabase();
        TestViewModel AddObject(TestViewModel viewModel);
    }
}