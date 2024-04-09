
using System.Net;

namespace WbApi.Model
{
    internal class Config
    {
        public Config()
        {
            
        }
        public Config(string token, CookieContainer cookie)
        {
            this.token = token;
            this.cookie = cookie;
            client_ts = (ulong)new Random().Next(1, int.MaxValue);
            addUrl = $"https://cart-storage-api.wildberries.ru/api/basket/sync?ts={client_ts}&device_id=global-site_686d13f4df684ac2855953af15df3f12";
            this.cookie = cookie;
        }
        public string addUrl { get; }
        public ulong client_ts { get; }
        public string _wbauid { get; }
        public string token { get; }
        public CookieContainer cookie { get; }
        public Product product { get; set; }

    }
}
