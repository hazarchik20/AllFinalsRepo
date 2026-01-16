using BLL.Models;
using BLL.Services;
using DLL;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
var options = new DbContextOptionsBuilder<CarApiContext>()
           .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarApiDB;Integrated Security=True;")
           .Options;
var CarContext = new CarApiContext(options);
var _repository = new CarApiRepository(CarContext);
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
    // Дозволяємо CORS
    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
    context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

    // Якщо це OPTIONS — повертаємо пусту відповідь (preflight)
    if (context.Request.HttpMethod == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        context.Response.OutputStream.Close();
        return;
    }

    var endpoint = context.Request.Url?.AbsolutePath;
    var response = string.Empty;
    var statusCode = 0;

    if (endpoint.StartsWith("/Car"))
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

async Task<(string response, int statusCode)> HandleUsersEndpoint(HttpListenerRequest request)
{
    var url = request.Url?.AbsolutePath;
    var response = string.Empty;
    var statusCode = 0;
    try
    {
        if (request.HttpMethod == "GET")
        {
            //var regexTypeMatch = Regex.Match(url, @" ^ Car\?carType=([A-Za-z]+)$&carSort=([A-Za-z]+)$"); /*пошук по Type*/

            var regexIDMatch = Regex.Match(url, @"^/Car/([0-9]+)$");

            if (regexIDMatch.Success)
            {
                var idString = regexIDMatch.Groups[1].Value;
                if (int.TryParse(idString, out int id))
                {
                    var car = await _repository.GetCarByIdAsync(id);

                    response = JsonSerializer.Serialize(car);
                    statusCode = 200;
                    return (response, statusCode);

                }
                else
                {
                    response = "Invalid ID format";
                    statusCode = 400;
                    return (response, statusCode);

                }
            }
            else if (url.StartsWith("/Car/all"))
            {
                var Car = await _repository.GetAllCarAsync();
                response = JsonSerializer.Serialize(Car);
                statusCode = 200;
                return (response, statusCode);

            }
            else
            {
                response = "Not found";
                statusCode = 404;
                return (response, statusCode);

            }

        }
        else 
        if (request.HttpMethod == "POST")
        {
            using var reader = new StreamReader(request.InputStream);
            var body = await reader.ReadToEndAsync();
            var inputCar = JsonSerializer.Deserialize<Car>(body);

            if (!CarServices.isCarValid(inputCar))
            {
                response = "DEVELOPER INVALID";
                statusCode = 400;
                return (response, statusCode);
            }
                var Car = await _repository.AddCarAsync(inputCar);
                response = JsonSerializer.Serialize(Car);
                statusCode = 201;
            return (response, statusCode);

        }
        else if (request.HttpMethod == "PUT")
        {
            using var reader = new StreamReader(request.InputStream);
            var body = await reader.ReadToEndAsync();
            var inputCar = JsonSerializer.Deserialize<Car>(body);

            if (!CarServices.isCarValid(inputCar))
            {
                response = "DEVELOPER INVALID";
                statusCode = 400;
                return (response, statusCode);
            }
            var Car = await _repository.UpdateCarAsync(inputCar);
            response = JsonSerializer.Serialize(Car);
            statusCode = 200;
            return (response, statusCode);

        }
        else if (request.HttpMethod == "DELETE")
        {
            var regexIDMatch = Regex.Match(url, @"^/Car/([0-9]+)$");

            if (regexIDMatch.Success)
            {
                var idString = regexIDMatch.Groups[1].Value;
                if (int.TryParse(idString, out int id))
                {
                    await _repository.DeleteCarAsync(id);
                    response = JsonSerializer.Serialize("Deleted Successful");
                    statusCode = 200;
                    return (response, statusCode);

                }
                else
                {
                    response = "Invalid ID format";
                    statusCode = 400;
                    return (response, statusCode);

                }
            }
            else {             
                response = "Car not found";
                statusCode = 404;
                return (response, statusCode);

            }
        }
        else
        {
            response = "Method not allowed";
            statusCode = 405;
        }
        return (response, statusCode);
        }
    catch (Exception ex)
    {
        response = $"Internal Server Error: {ex.Message}";
        statusCode = 500;
        return (response, statusCode);
    }
}

