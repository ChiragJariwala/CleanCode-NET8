using CleanCode.Core.Communication;
using CleanCode.Core.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace CleanCode.Infrastructure.Services
{
    public class OrderServiceProxy : IOrderServiceProxy
    {
        private readonly IRestClient _client;
        private readonly IConfiguration _configuration;
        private readonly IRestRequest _request;

        public OrderServiceProxy(IRestClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _request = new RestRequest();
        }

        public async Task<List<long>> GetOrders(string orderedBy)
        {
            var orderServiceUrl = _configuration["OrderServiceEndpoint"];
            var orderInput = new OrderInput { OrderBy = orderedBy };
            var response = await Execute(JsonConvert.SerializeObject(orderInput), orderServiceUrl);
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(response);
            return orderResponse.Orders;
        }

        private async Task<string> Execute(string input, string url)
        {
            _request.Parameters.Clear();
            _request.Resource = url;
            _request.Method = Method.POST;
            _request.AddHeader("Content-type", "application/json");

            if (!string.IsNullOrEmpty(input))
            {
                _request.AddJsonBody(input);
            }

            // Note: Uncomment and call the service to get the data
            var response = await _client.ExecuteAsync(_request);

            // Note: Check the status code and take appropriate action if invalid
            // TODO: should use response.Content
            return JsonConvert.SerializeObject( new OrderResponse() { Orders = new List<long>() { 101,102 } });
        }
    }
}
