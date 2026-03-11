using Lecture06.Configuration.Api.Cats.Options;
using Winton.Extensions.Configuration.Consul;
using Winton.Extensions.Configuration.Consul.Parsers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Configuration.Sources.Clear();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddYamlFile("appsettings.yaml", optional: false, reloadOnChange: true)
    .AddYamlFile($"appsettings.{builder.Environment.EnvironmentName}.yaml", optional: true, reloadOnChange: true)
    .AddConsul("applications/Lecture06.Configuration", options =>
    {
        options.Parser = new SimpleConfigurationParser();
        options.ConsulConfigurationOptions = o => o.Address = new Uri("http://localhost:8500");
    })
    .AddEnvironmentVariables();

builder.Services.Configure<CatApiOptions>(builder.Configuration.GetRequiredSection("CatApiSettings"));


var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.MapControllers();

app.Run();

