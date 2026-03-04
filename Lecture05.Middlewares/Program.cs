using System.Diagnostics;
using System.Net.Mime;
using Lecture05.Middlewares.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<SomeDependency>();
builder.Services.AddSingleton<RickRollingMiddleware>();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI();

app.UseHttpsRedirection();

app.Use(async (httpContext, next) =>
{
    Console.WriteLine($"[1] --> {httpContext.Request.Method} {httpContext.Request.Path}");

    await next();

    Console.WriteLine($"[1] <-- {httpContext.Request.Method} {httpContext.Request.Path}");
});


app.UseMiddleware<RickRollingMiddleware>();


app.Use(async (httpContext, next) =>
{
    var sw = Stopwatch.StartNew();

    httpContext.Response.Headers["TrackStarted"] = DateTime.UtcNow.ToString("o");

    await next();

    sw.Stop();

    Console.WriteLine($"{httpContext.Request.Method} {httpContext.Request.Path} {sw.Elapsed.TotalMilliseconds}");
});


app.UseRouting();

/*
app.UseEndpoints(endpoints => endpoints
    .MapGet("/hello", () =>
    {
        return Results.Content("Hello!", MediaTypeNames.Text.Plain);
    }));
*/

app.Use(async (httpContext, next) =>
{
    Console.WriteLine($"[2] --> {httpContext.Request.Method} {httpContext.Request.Path}");

    await next();

    Console.WriteLine($"[2] <-- {httpContext.Request.Method} {httpContext.Request.Path}");
});

app.Use(async (httpContext, next) =>
{
    Console.WriteLine($"[3] --> {httpContext.Request.Method} {httpContext.Request.Path}");

    await next();

    Console.WriteLine($"[3] <-- {httpContext.Request.Method} {httpContext.Request.Path}");
});

// app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Content("Hello!", MediaTypeNames.Text.Plain);
});

app.Run();
