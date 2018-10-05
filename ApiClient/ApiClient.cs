using ApiViewModelMapper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ApiClient
    {
        private const string _url = "http://localhost:5555/Api/";

        public async Task<List<TestViewModel>> GetDataFromApi()
        {
            var client = new RestClient(_url);
            var request = new RestRequest("Policy", Method.GET);

            var response = await client.ExecuteTaskAsync<List<TestViewModel>>(request);

            return response.Data;
        }

        public async Task<TestViewModel> GetDataByIdFromApi(int id)
        {
            var client = new RestClient(_url);
            var request = new RestRequest("Policy/{id}", Method.GET);
            request.AddUrlSegment("id", id);
            var response = await client.ExecuteTaskAsync<TestViewModel>(request);

            return response.Data;
        }

        public async Task<TestViewModel> PostDataToApi(TestViewModel data)
        {
            var client = new RestClient(_url);
            var request = new RestRequest("Policy", Method.POST);
            var json = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            var response = await client.ExecuteTaskAsync<TestViewModel>(request);

            return response.Data;
        }
    }
}
