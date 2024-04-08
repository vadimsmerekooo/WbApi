using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WbApi;
using WbApi.Model;


//new test().Test();


bool showMenu = true;
while (showMenu)
{
    MainMenu();
}


static bool MainMenu()
{
    Console.Clear();
    Console.WriteLine("Console program to search for wildberries product and add it to cart.");


    Console.WriteLine("Введите ссылку на карточку товара: ");
    string productLink = Console.ReadLine();

    Product productMain = new Product(productLink);



    Console.Write("Введите id номер товара: ");
    ulong cod_1s = ulong.Parse(Console.ReadLine());
    Console.Write("Введите option номер товара: ");
    ulong chrt_id = ulong.Parse(Console.ReadLine());
    Console.Write("Введите кол-во товара: ");
    int quantity = int.Parse(Console.ReadLine());

    Product product = new Product(cod_1s, chrt_id, quantity);


    Console.Write("Введите токен авторизации Bearer: Bearer ");
    string token = Console.ReadLine();
    Console.Write("Введите значение параметра куки -> _wbauid: ");
    string _wbauid = Console.ReadLine();

    CookieContainer cookie = new CookieContainer();
    cookie.Add(new Cookie(nameof(_wbauid), _wbauid, "/", "www.wildberries.by"));

    Config config = new Config(token, cookie, product);

    try
    {
        var response = product.AddAsync(config);
        Console.WriteLine(response.Result);
        Console.WriteLine(response.Exception);
        Console.ReadKey();
    }
    catch(Exception ex)
    {
        Console.WriteLine("Exception: " + ex);
        Console.ReadKey();
    }

    return true;
}
