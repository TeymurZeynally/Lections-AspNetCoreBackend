using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();


app.Map("/", (HttpContext context) =>
{
    context.Response.StatusCode = 418;

    return Results.File("cat.html", "text/html");
});

app.MapGet("/methods", () => Results.Json(new { CalledMethod = "GET" }));
app.MapPost("/methods", () => Results.Json(new { CalledMethod = "POST" }));
app.MapPatch("/methods", () => Results.Json(new { CalledMethod = "PATCH" }));


app.MapGet("/params/queryparams", (int? age, string? name) => {
    return Results.Json(new { CalledMethod = "GET", Params = $"Переданы параметры age = {age}, name = {name}" });
});

app.MapGet("/params/queryparams/object", ([AsParameters] CatQueryRequestContract request) => {
    return Results.Json(new { CalledMethod = "GET", Params = $"Переданы параметры age = {request.Age}, name = {request.Name}" });
});

app.MapPost("/params/body", ([FromBody] CatQueryRequestContract request) => {
    return Results.Json(new { CalledMethod = "POST", Params = $"Переданы параметры age = {request.Age}, name = {request.Name}" });
});


app.Run();


class CatQueryRequestContract
{
    public required int Age { get; init; }
    public required string Name { get; init; }
}