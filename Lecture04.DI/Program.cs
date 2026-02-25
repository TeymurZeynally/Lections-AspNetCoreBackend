using Lecture04.DI.Notifications;
using Lecture04.DI.Repositories;
using Lecture04.DI.Services;
using Lecture04.DI.Utility;
using Microsoft.Extensions.DependencyInjection;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var serviceColection = new ServiceCollection();

serviceColection.AddSingleton<IConsoleLogger, ConsoleLogger>();

serviceColection.AddTransient<INotifier, EmailNotifier>(_ => new EmailNotifier("smtp.kotikmail.com"));
serviceColection.AddTransient<IFoodRepository, FoodRepository>(_ => new FoodRepository("cats.db"));

serviceColection.AddTransient<ICatFeederService, CatFeederService>();

serviceColection.AddScoped<ICatFeederService, CatFeederService>();

/*-----------------------*/

var provider = serviceColection.BuildServiceProvider();

var feeder2 = provider.GetRequiredService<ICatFeederService>();
feeder2.FeedCat("Barsik");

/*-----------------------*/

var scope = provider.CreateScope();

var feeder1 = scope.ServiceProvider.GetRequiredService<ICatFeederService>();
feeder1.FeedCat("Barsik");