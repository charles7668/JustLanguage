using JustLanguage.Extensions;
using JustLanguage.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.InitServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scopeService = app.Services.CreateScope().ServiceProvider;

// init app
var initApp = scopeService.GetRequiredService<IInitApp>();
initApp.Init();

// use static files in wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();