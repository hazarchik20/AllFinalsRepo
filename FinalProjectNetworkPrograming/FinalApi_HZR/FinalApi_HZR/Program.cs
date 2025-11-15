using DLL;
using DLL.Models;
using DLL.Reposytory;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

var options = new DbContextOptionsBuilder<DataContext>()
           .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CryptoMonetDB;Integrated Security=True;")
           .Options;
var CryproContext = new DataContext(options);
var _repository = new CryptoRepository(CryproContext);

var url = "http://localhost:12000/";

var listener = new HttpListener();
listener.Prefixes.Add(url);
listener.Start();

Console.WriteLine("Server started...");

while (true)
{
    var ListenerContext = await listener.GetContextAsync();
    Task.Run(() => HandleAsync(ListenerContext));
}

async Task HandleAsync(HttpListenerContext context)
{
    var endpoint = context.Request.Url?.AbsolutePath;
    var response = string.Empty;
    var statusCode = 0;

    if (endpoint.StartsWith("/cryptoToken"))
    {
        (response, statusCode) = await HandleUsersEndpoint(context.Request);
    }

    var bytes = Encoding.UTF8.GetBytes(response);
    context.Response.ContentLength64 = bytes.Length;
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/json";
    await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
    context.Response.OutputStream.Close();
}
async Task<(string, int)> HandleUsersEndpoint(HttpListenerRequest request)
{
    var url = request.Url?.AbsolutePath;
    var response = string.Empty;
    var statusCode = 0;

    if (request.HttpMethod == "GET")
    {   //тут бд + АПІ
        var regexIDMatch = Regex.Match(url, @"^cryptoToken/(\d+)$");
        var regexSYMBLEMatch = Regex.Match(url, @"^cryptoToken\?symbol=([A-Za-z]+)$");
        var regexSYMBLE_CURRENCYMatch = Regex.Match(url, @"^cryptoToken\?symbol=([A-Za-z]+)&convert=([A-Za-z]+)$");

        if (regexIDMatch.Success)
        {
            var id = int.Parse(regexIDMatch.Groups[1].Value);
            var CryptoMonet = await _repository.GetCryptoByIdAsync(id);
            response = JsonSerializer.Serialize(CryptoMonet);
            statusCode = 200;
        }
        else if (regexSYMBLEMatch.Success)
        {
            var Symble = regexIDMatch.Groups[1].Value;
            var CryptoMonet = await _repository.GetCryptosBySymbolAsync(Symble);
            response = JsonSerializer.Serialize(CryptoMonet);
            statusCode = 200;
        }
        else if (url.StartsWith("cryptoToken/latest"))
        {
            var CryptoMonets = await _repository.Get200CryptosAsync();
            response = JsonSerializer.Serialize(CryptoMonets);
            statusCode = 200;
        }
        else if (url.StartsWith("cryptoToken/sort"))
        {
            var CryptoMonets = await _repository.SortCryptosAsync();
            response = JsonSerializer.Serialize(CryptoMonets);
            statusCode = 200;
        }
        else if (regexSYMBLE_CURRENCYMatch.Success)
        {
            string symbol = regexSYMBLE_CURRENCYMatch.Groups[1].Value;
            string convert = regexSYMBLE_CURRENCYMatch.Groups[2].Value;
            var CryptoMonets = await _repository.СhangeСurrencyCryptosAsync(symbol, convert);
            response = JsonSerializer.Serialize(CryptoMonets);
            statusCode = 200;
        }
        else
        {
            response = "Not Found";
            statusCode = 404;
        }
    }
    else if (request.HttpMethod == "POST")
    {
        var inputCryptoMonet = JsonSerializer.Deserialize<CryptoMonet>(request.InputStream); // Body

        var CryptoMonet = await _repository.AddCryptoAsync(inputCryptoMonet);
        response = JsonSerializer.Serialize(CryptoMonet);
        statusCode = 201;
    }
    else if (request.HttpMethod == "PUT")
    {
        var inputCryptoMonet = JsonSerializer.Deserialize<CryptoMonet>(request.InputStream); // Body

        var CryptoMonet = await _repository.UpdateCryptoAsync(inputCryptoMonet);
        if (CryptoMonet == null)
        {
            response = "DEVELOPER INVALID";
            statusCode = 400;
            return (response, statusCode);
        }
        response = JsonSerializer.Serialize(CryptoMonet);
        statusCode = 200;
    }
    else if (request.HttpMethod == "DELETE")
    {
        var regexIDMatch = Regex.Match(url, @"^cryptoToken/(\d+)$");

        if (regexIDMatch.Success)
        {
            var id = int.Parse(regexIDMatch.Groups[1].Value);
            await _repository.DeleteCryptoAsync(id);
            response = JsonSerializer.Serialize("Deleted Successful");
            statusCode = 200;
        }
    }
    else
    {
        response = "Method not allowed";
        statusCode = 405;
    }
    return (response, statusCode);
}
static void ShowCryptos(CryptoMonet[] cryptos)
{
    foreach (var c in cryptos)
    {
        ShowCrypto(c);
    }
}

static void ShowCrypto(CryptoMonet c)
{
    if (c != null)
        Console.WriteLine($"ID: {c.id}, Name: {c.name}, Symbol: {c.symbol}, Price USD: {c.quote?.USD?.price}");
    else
        Console.WriteLine("Crypto not found or null");
}
