using System.Collections.Generic;
using System.Web.Http;
using ApiViewModelMapper;

namespace MySolution.Api.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IApiMapper _employeeRepository;

        public EmployeesController(IApiMapper employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET api/<controller>
        public IEnumerable<TestViewModel> Get()
        {
            var data = _employeeRepository.GetAllObjectFromDatabase();
            return data;
        }

        //GET api/<controller>/5
        public TestViewModel Get(int id)
        {
            return _employeeRepository.GetObjectFromDatabase(id);
        }

        // POST api/<controller>
        //public Employee Post([FromBody]Employee value)
        //{
        //    return _employeeRepository.AddEmployee(value);
        //}

        // PUT api/<controller>/5
        public TestViewModel Put([FromBody]TestViewModel value)
    {
            return _employeeRepository.AddObject(value);
        }

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //    _employeeRepository.DeleteEmployee(id);
        //}
    }
}
