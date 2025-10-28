using MinIOSharpSharp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MinIOSharpSharp.Core.Services
{

    public class MinIOSharpService : IMinIOSharpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _secretKey;
        private readonly string _siteKey;
        private readonly string _verificationUrl;

        public MinIOSharpService(string secretKey, string siteKey, string verificationUrl)
        {
            _httpClientFactory = new DefaultHttpClientFactory();
            _secretKey = secretKey;
            _siteKey = siteKey;
            _verificationUrl = verificationUrl;
        }

        public MinIOSharpVerificationResult VerifyMinIOSharpResponse(string response, CancellationToken cancellationToken)
        {
            var data = new
            {
                secret_key = _secretKey,
                challenge_id = response,
                site_key = _siteKey
            };

            var requestJson = JsonSerializer.Serialize(data);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


            var httpReqest = _httpClientFactory.CreateClient();

            var result = httpReqest.PostAsync(_verificationUrl, content).Result;

            var resString = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<MinIOSharpVerificationResult>(resString);
        }

        public async Task<MinIOSharpVerificationResult> VerifyMinIOSharpResponseAsync(string response, CancellationToken cancellationToken)
        {
            var data = new
            {
                secret_key = _secretKey,
                challenge_id = response,
                site_key = _siteKey
            };

            var requestJson = JsonSerializer.Serialize(data);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


            var httpReqest = _httpClientFactory.CreateClient();

            var result = await httpReqest.PostAsync(_verificationUrl, content, cancellationToken);

            var resString = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<MinIOSharpVerificationResult>(resString);
        }
    }
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name = "")
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            return httpClient;
        }
    }
}
