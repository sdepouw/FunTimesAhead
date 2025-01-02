// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FunTimesAhead;
using FunTimesAhead.Sorting;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

if (args.Any(arg => arg == "bench"))
{
  BenchmarkRunner.Run<SortingComparer>();
  return 0;
}



HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MainService>();

builder.Services.AddTransient<IDataAccess, SomeExpensiveDataAccess>();
#pragma warning disable EXTEXP0018 // Warns that this is an experimental feature
builder.Services.AddHybridCache(options =>
{
  options.DefaultEntryOptions = new HybridCacheEntryOptions
  {
    LocalCacheExpiration = TimeSpan.FromSeconds(5),
    Expiration = TimeSpan.FromMinutes(20)
  };
});
#pragma warning restore EXTEXP0018
if (args.Any(arg => arg == "redis"))
{
  builder.Services.AddStackExchangeRedisCache(options => options
    .Configuration = builder.Configuration.GetConnectionString("Redis"));  
}
else if (args.Any(arg => arg == "sql"))
{
  builder.Services.AddDistributedSqlServerCache(options => options
    .ConnectionString = builder.Configuration.GetConnectionString("SQL"));  
}

using IHost host = builder.Build();
await host.RunAsync();
return 0;
