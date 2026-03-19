using FinalProjectASP_Net.Extensions;
using FinalProjectASP_Net.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services
    .AddRepositories(builder.Configuration)
    .AddServices()
    .AddRedisCache(builder.Configuration);



var app = builder.Build();


app.UseHttpsRedirection();


app.UseAuthentication(); // хто ти
app.UseAuthorization();  // що ти можеш


app.UseMiddleware();


app.MapControllers();
app.Run();
