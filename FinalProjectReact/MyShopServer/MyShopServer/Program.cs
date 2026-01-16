using BLL.Services;
using DLL;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Threading.Tasks;

var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FurnitureShopDB;Integrated Security=True;")
        .Options;


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
    var statusCode = 200;
    using var _context = new DataContext(options);

    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

    
    if (context.Request.HttpMethod == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        context.Response.Close();
        return;
    }

    if (
        endpoint.StartsWith("/user") ||
        endpoint.StartsWith("/product") ||
        endpoint.StartsWith("/order") ||
        endpoint.StartsWith("/cartItem") ||
         endpoint.StartsWith("/category")
    )
    {
        (response, statusCode) = await HandleUsersEndpoint(context.Request, _context);
    }
    else
    {
        response = "Endpoint not found";
        statusCode = 404;
    }

    var bytes = Encoding.UTF8.GetBytes(response);
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/json";
    context.Response.ContentLength64 = bytes.Length;

    await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
    context.Response.Close();
}
async Task<(string, int)> HandleUsersEndpoint(HttpListenerRequest request, DataContext dbcontext)
{
        var url = request.Url?.AbsolutePath;
        var response = string.Empty;
        var statusCode = 0;
    try
    {

        CatcheEndpoints catcheEndpoints = new CatcheEndpoints(dbcontext);

        switch(request.HttpMethod)
        {
            case "GET": { (response, statusCode) = await catcheEndpoints.CatcheGET(request); break; }
            case "POST": { (response, statusCode) = await catcheEndpoints.CatchePOST(request); break; }
            case "PUT": { (response, statusCode) = await catcheEndpoints.CatchePUT(request); break; }
            case "DELETE": { (response, statusCode) = await catcheEndpoints.CatcheDELETE(request); break; }
        
            default: { (response, statusCode) = ("Method not allowed", 405); break;}
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine("_________________\n"+ex+ "\n_________________");
        (response, statusCode) = ("developer invalid", 500);
    }
    
        return (response, statusCode);
    
}