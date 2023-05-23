using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTaskParcer;
using TestTaskParcer.Data;
using TestTaskParcer.Repositories;
using TestTaskParcer.Repositories.Interfaces;
using TestTaskParcer.Service;
using TestTaskParcer.Service.Interfaces;

var config = GetCongif();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<Parcer>();
        services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(config["ConnectionString"]));

        services.AddTransient<ICarComplectationRepsoitory, CarComplectationRepository>();
        services.AddTransient<ICarModelRepository, CarModelRepository>();
        services.AddTransient<IDetailRepository, DetailRepository>();
        services.AddTransient<IPartGroupRepository, PartGroupRepository>();
        services.AddTransient<ISubGroupRepository, SubGroupRepository>();

        services.AddScoped<IGuideService, GuideService>();
    })
    .Build();
host.Start();

//using (var scope = host.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<AppDbContext>();
//        context.Database.EnsureDeletedAsync().Wait();
//        context.Database.EnsureCreatedAsync().Wait();
//    }
//    catch
//    {
//    }
//}

var ccc = host.Services.GetRequiredService<Parcer>();
// ccc.GetImage("https://www.ilcats.ru/toyota/?function=getParts&market=EU&model=162510&modification=ADE150L-AEFNXW&complectation=001&option=359W&group=4&subgroup=8106");
var url = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";
ccc.Start(url);


Console.ReadLine();

IConfiguration GetCongif()
{
    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("config.json")
        .AddEnvironmentVariables()
        .Build();
}