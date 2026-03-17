using FinalProjectASP_Net.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddRepositories();
builder.Services.AddServices();


var app = builder.Build();


app.UseHttpsRedirection();


app.UseAuthentication(); // хто ти
app.UseAuthorization();  // що ти можеш


app.UseMiddleware();


app.MapControllers();
app.Run();
