using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiViewModelMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IApiService _policyService;

        public PolicyController(IApiService policyService)
        {
            _policyService = policyService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TestViewModel>> Get()
        {
            var data = _policyService.GetAllObjectFromDatabase();
            return Ok(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<TestViewModel> Get(int id)
        {
            return _policyService.GetObjectFromDatabase(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TestViewModel> Post([FromBody] TestViewModel value)
        {
            return _policyService.AddObject(value);
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
