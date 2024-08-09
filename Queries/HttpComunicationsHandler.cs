using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IcingaBot.Models
{
    public static class HttpComunicationsHandler
    {
        public static HttpClient GetHttpClient() {
            HttpClientHandler clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<string> HttpResonseMessage(string url, HttpMethod method, BodyRequestModels body = null) {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(body);
            var request = new HttpRequestMessage {
                Method = method,
                RequestUri = new Uri(url),
                Content = new StringContent(json),
            };
            request.Headers.Add("X-HTTP-Method-Override", "GET");
            var response = await client.SendAsync(request).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
