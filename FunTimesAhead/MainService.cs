using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Hosting;

namespace FunTimesAhead;

public class MainService(IDataAccess dataAccess, HybridCache cache) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    try
    {
      Console.WriteLine("Hello, World! Let's have some fun.");
      await DoStuffAsync(cancellationToken);
    }
    catch (TaskCanceledException) { }
  }

  private async Task DoStuffAsync(CancellationToken cancellationToken)
  {
    for (int i = 0; i < 100; i++)
    {
      string data = await cache.GetOrCreateAsync("data", 
        async cancel => await dataAccess.GetImportantDataAsync(cancel),
        cancellationToken: cancellationToken);
      Console.WriteLine(data);
      await Task.Delay(1000, cancellationToken);
    }
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Console.WriteLine("Goodbye, Mr. Bond");
    return Task.CompletedTask;
  }
}
