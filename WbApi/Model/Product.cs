using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace WbApi.Model
{
    internal class Product
    {
        public Product(ulong cod_1s, ulong chrt_id, int quantit)
        {
            this.cod_1s = cod_1s;
            this.chrt_id = chrt_id;
            this.quantity = quantity; 
            this.op_type = op_type;
            this.inListIndex = inListIndex;
        }
        public ulong cod_1s { get; }
        public ulong chrt_id { get; }
        public int quantity { get; }
        public int op_type { get; }
        public ulong client_ts { get; }
        public int inListIndex { get; set; }

        public async Task<HttpResponseMessage> AddAsync(Config config)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();


                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.All, CookieContainer = config.cookie, UseCookies = true }))
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
                    client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br, zstd");
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                    client.DefaultRequestHeaders.Host = "cart-storage-api.wildberries.ru:443";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.token);

                    var builder = new UriBuilder(new Uri(config.addUrl));

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
                    string jsonContent = $"[{JsonConvert.SerializeObject(this, Formatting.Indented)}]";
                    request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    request.Content.Headers.Add("Content-Length", jsonContent.Length.ToString());

                    return await client.SendAsync(request);
                };


            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }
    }
}
