using System.Net;
using CleanCode.Core.Models;
using CleanCode.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace CleanCode.Infrastructure.Services
{
    public class SsoService : ISsoService
    {
        private readonly IRestClient _client;
        private readonly IRestRequest _request;
        private readonly IOptions<SsoApiModel> _ssoApiModel;
        private readonly ILogger<SsoService> _logger;

        public SsoService(IRestClient client, IOptions<SsoApiModel> ssoApiModel, ILogger<SsoService> logger)
        {
            _ssoApiModel = ssoApiModel ?? throw new ArgumentNullException(nameof(ssoApiModel));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _request = new RestRequest();
        }

        public async Task<bool> ValidateToken(string token)
        {
            var ssoApiUrl = _ssoApiModel.Value.Url;
            var endPoint = _ssoApiModel.Value.Endpoint.ValidateToken;
            _request.Parameters.Clear();
            _request.Resource = ssoApiUrl + endPoint;
            _request.Method = Method.POST;
            _request.AddHeader("Content-type", "application/json");
            _request.AddHeader("Authorization", token);
            var response = await _client.ExecuteAsync(_request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> Logout(string token)
        {
            var ssoApiUrl = _ssoApiModel.Value.Url;
            var endPoint = _ssoApiModel.Value.Endpoint.Logout;
            _request.Parameters.Clear();
            _request.Resource = ssoApiUrl + endPoint;
            _request.Method = Method.POST;
            _request.AddHeader("Content-type", "application/json");
            var response = await _client.ExecuteAsync(_request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        private async Task<T> Execute<T>(string url)
        {
            _request.Parameters.Clear();
            _request.Resource = url;
            _request.Method = Method.GET;
            _request.AddHeader("Content-type", "application/json");
            var response = await _client.ExecuteAsync(_request);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(response.Content);

            throw new ApplicationException(response.Content);
        }
    }
}