using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WbApi.Model
{
    internal class Product
    {
        public Product(string productLink, int quantity)
        {
            if (String.IsNullOrEmpty(productLink))
                throw new Exception("Ошибка, ссылка пуста или невалидна!");

            var produrctUrl = new Uri(productLink);


            ulong card;
            ulong option;
            if (productLink != null && ulong.TryParse(HttpUtility.ParseQueryString(produrctUrl.Query).Get("card"), out card) && ulong.TryParse(HttpUtility.ParseQueryString(produrctUrl.Query).Get("option"), out option))
            {
                this.cod_1s = card;
                this.chrt_id = option;
            }
            else
            {

                try
                {
                    if(produrctUrl.Segments.Count() >= 3)
                    {
                        this.cod_1s = ulong.Parse(produrctUrl.Segments[2].Replace("/", ""));
                        this.chrt_id = ParseWbRu(produrctUrl).Result;
                    }
                    if(ulong.TryParse(HttpUtility.ParseQueryString(produrctUrl.Query).Get("card"), out card))
                    {
                        this.cod_1s = card;
                    }
                    if (cod_1s == 0)
                    {
                        throw new Exception("Ошибка чтения ссылки. формат неверен");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка: {ex.Message}");
                }
            }
            this.quantity = quantity > 0 ? quantity : 1;
        }
        public ulong cod_1s { get; }
        public ulong chrt_id { get; }
        public int quantity { get; }
        public int op_type { get; } = 1;
        public ulong client_ts { get; private set; }
        public int inListIndex { get; set; } = 1;

        public  HttpResponseMessage Add(Config config)
        {
            this.client_ts = config.client_ts;
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

                    return client.Send(request);
                };


            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }
        public async Task<ulong> ParseWbRu(Uri url)
        {
            try
            {
                var options = new ChromeOptions()
                {
                    BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
                };
                options.AddArguments(new List<string>() { "headless", "disable-gpu" });
                var browser = new ChromeDriver(options);
                browser.Navigate().GoToUrl(url);
                var resultHtml = "";
                while (true)
                {
                    if (browser.PageSource.Replace("/*", "").Contains("product-line__param"))
                    {
                        resultHtml = browser.PageSource.Replace("/*", "");
                        break;
                    }
                    Thread.Sleep(100);
                }


                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(resultHtml);
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("product-line__param"));

                if (nodes is null || nodes.Count() == 0)
                    throw new Exception("Ошибка чтения карточки.");
                string href = nodes.First().Attributes["href"].Value.Split("?size=")[1];

                return ulong.Parse(href);

            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения ссылки формата wb.ru");
            }
        }
    }
}
