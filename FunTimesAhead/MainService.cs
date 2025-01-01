using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Hosting;

namespace FunTimesAhead;

#pragma warning disable CS9113 // Parameter is unread.
public class MainService(HybridCache cache) : IHostedService
#pragma warning restore CS9113 // Parameter is unread.
{
  public Task StartAsync(CancellationToken cancellationToken)
  {
    Console.WriteLine("Hello, World! Let's have some fun.");
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    Console.WriteLine("Goodbye, Mr. Bond");
    return Task.CompletedTask;
  }
}
