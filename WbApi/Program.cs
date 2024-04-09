using OpenQA.Selenium.DevTools.V121.Debugger;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WbApi;
using WbApi.Model;

Config config = new Config();

bool showMenu = true;

while (showMenu)
{
    config = MainMenu(config);
}


static Config MainMenu(Config config)
{
    Console.Clear();
    Console.WriteLine("Console program to search for wildberries product and add it to cart.");


    Console.Write("Введите ссылку на карточку товара WB: ");
    string productLink = Console.ReadLine();
    Console.Write("Введите кол-во: ");
    int count = int.Parse(Console.ReadLine());
    while(count < 1)
    {
        Console.Write("Введите кол-во не менее 1: ");
        count = int.Parse(Console.ReadLine());
    }
    Product product;
    try
    {
        product = new Product(productLink, count);
    }
    catch(Exception ex)
    {
        Console.WriteLine("Ошибка. " + ex);
        Console.ReadKey();
        return config;
    }

    if (String.IsNullOrEmpty(config.token) && String.IsNullOrEmpty(config._wbauid))
    {
        Console.Write("Введите токен авторизации (его можно узнать при отправлении запроса на добавления в корзину, в headers: Authorization)! Это обязательный параметр Bearer: ");
        string token = Console.ReadLine();
        Console.Write("Введите значение параметра куки -> _wbauid: ");
        string _wbauid = Console.ReadLine();

        CookieContainer cookie = new CookieContainer();
        cookie.Add(new Cookie(nameof(_wbauid), _wbauid, "/", "www.wildberries.by"));
        config = new Config(token, cookie);
    }

    try
    {
        var response = product.Add(config);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Товар успешно добавлен в корзину!");
        }
        else
        {
            Console.WriteLine("Ошибка добавления товара в корзину!");
        }
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception: " + ex);
        Console.ReadKey();
    }

    return config;
}
