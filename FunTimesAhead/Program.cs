// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FunTimesAhead;
using FunTimesAhead.Sorting;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

if (args.Length == 1 && args.Single() == "bench")
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
    Flags = HybridCacheEntryFlags.DisableDistributedCache // Probably not needed explicitly.
  };
});
#pragma warning restore EXTEXP0018

using IHost host = builder.Build();
await host.RunAsync();
return 0;
