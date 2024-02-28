using JustLanguage.Extensions;
using JustLanguage.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, service, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.InitServices();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("cache-one-hour",
        new CacheProfile
        {
            Duration = 60 * 60,
            Location = ResponseCacheLocation.Any
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod()
            .AllowAnyHeader();
    });
});

WebApplication app = builder.Build();
IServiceProvider scopeService = app.Services.CreateScope().ServiceProvider;

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

app.UseCors("AllowAll");

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();