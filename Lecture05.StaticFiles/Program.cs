var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseDirectoryBrowser();

app.MapFallbackToFile("index.html");

app.Run();
