
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

try
{
    ASCIIEncoding encoding = new ASCIIEncoding();
    var baseUrl = "https://cart-storage-api.wildberries.ru"; // Базовый URL Wildberries
    var nomenclature = "148748593"; // Номенклатура товара
    var client_ts = "1712510537";

    var client = new RestClient("https://cart-storage-api.wildberries.ru/api/basket/sync");
    var request = new RestRequest("?ts=1712510537&device_id=global-site_273a8dfe91a64d4e826358a677741825");
    request.Method = Method.Post; 
    var content = $"[{{\"cod_1s\":{nomenclature},\"chrt_id\":89166911,\"quantity\":1,\"op_type\":1,\"client_ts\":{client_ts},\"inListIndex\":8,\"tailObject\":{{\"loc\":\"\",\"loc_way\":\"IG\",\"sort\":\"\",\"terms\":{{}}}}]";

    request.AddHeader("Accept", "application/json");
    request.AddHeader("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
    request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
    request.AddHeader("Content-Type", "application/json");
    request.AddParameter("Content", content, true);
    //request.AddBody(new {
    //    cod_1s = 45387309,
    //    chrt_id = 89166911,
    //    quantity = 1,
    //    op_type = 1,
    //    client_ts = 1712510537,
    //    inListIndex = 8,
    //    tailObject = new {
    //        loc = "",
    //        loc_way = "IG",
    //        sort = "",
    //        terms = new {}
    //    }
    //});
   



    // Выполнение запроса

    var response = client.Execute(request);
    Console.WriteLine(response.StatusCode + " -> " + response.Content);
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}






//bool showMenu = true;
//while(showMenu)
//{
//    MainMenu();
//}


static bool MainMenu()
{
    Console.Clear();
    Console.WriteLine("");
    return true;
}
