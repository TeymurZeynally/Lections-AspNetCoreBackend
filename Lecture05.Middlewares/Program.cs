var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.MapGet("/", () => "Hello");

app.Run();