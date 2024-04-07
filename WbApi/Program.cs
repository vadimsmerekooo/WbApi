
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
    request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTI0NTMwMTEsInZlcnNpb24iOjIsInVzZXIiOiIzNzgyMDMwMiIsInNoYXJkX2tleSI6IjIzIiwiY2xpZW50X2lkIjoid2IiLCJzZXNzaW9uX2lkIjoiM2Y5MDE5NjQzNTk4NGE2ZDk1Zjk0MGEyMjZmZWI0NzgiLCJ1c2VyX3JlZ2lzdHJhdGlvbl9kdCI6MTU5NjM0ODY5NCwidmFsaWRhdGlvbl9rZXkiOiJjMTg4NTAyNmYxOTU5YTVmOWQwMjM3YThlOTQ1MjkyNzhjOWVlZmUyMWMyMGYwMzY2ZDRiODgwYjQ2NzUyOTNiIiwicGhvbmUiOiJKMGR4SU02UFFLaENFNkcwMEY4ZGd3PT0ifQ.Y3H10cs9d_yoiljjN9VaasN62ors5V_raCVlhnm3E-w_u0al9MtlFYPhboVdk2ZhGAGpgpOnzcMwOhCnLcr0zKnn_wSXz3N-jGQv66IdOc5kO11DZKWyS8g0AnXHawvDViPTZKmUTpRhpmgX-Ubw_tXY89yEOEg1QRumSuf4h-P4__MUhUTSqi8SrsHjPejiARjF4gwxDVkLq7t0V7BAxTx8ClcYxLqKdySDPFP6lmSGxSMqbnOQPiKt8HADMrPvJUmD6G6KSBXczgN4yX_hqPIEs9kcKqU-PLOjj6xLrnixx31dwJBtKz2oApAF6vH4wQI5St-IqARzaC7ies7o8A");
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
    request.AddCookie("_wbauid", "4276668881712453012", "/", ".wildberries.by");
    request.AddCookie("WILDAUTHNEW_V3", "C4DBE4CA169FC6340987CCDCCE71334FD245B4054FBF5CEDB0FB1BD5A0B0D73BA5DA786F8EEE57B92AE9A65530CC2CB310D7C57CC280E8023E802E931EE8C11DFEB12B1624B629B4818BDFDAB325E912229866F88651E7219F641FC561983E89711B288DB1BDB0DAF8588507E0C832492A4E3537AC216A25E526EFA33033229205FDAE56BD241A9D0FF658A61F5EE5A0B08BAFE0A6591E0BDAB71F2F612A9E3300D9194378ABC9C87B6AFE46E9312DACA98836667CE2A00FC6096B5CD0A5EDAE477DC172D1243C826CB389147CEBD22C34F0477E97B616E975E5315AFF0A90A7AD9821F9E9DCD185007B996DD06F14C33CC30708109D9A8E82116CFDA1E686986025EC63A059C5CDAC6A90CE37FC89DE3F5BA50AA3FE70673DAFD14D4A3AF489B381002B74DCE7BCA7A8E105A06A0D8BF0884E5513304BEC75EDA00BEC41EE121BD63445", "/", "wildberries.ru");
    request.AddCookie("wbx-validation-key", "a2055098-1055-4078-a45e-400da541beb6", "/", ".wildberries.by");




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