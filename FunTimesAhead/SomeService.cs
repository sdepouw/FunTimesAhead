using Microsoft.Extensions.Caching.Hybrid;

namespace FunTimesAhead;

public class SomeService(HybridCache cache)
{
  public async Task<string> DoStuffWithCacheAsync(CancellationToken cancellationToken)
  {
    return await cache.GetOrCreateAsync("some-key", 
      async cancel => await Task.FromResult("Hello, World!"), 
      cancellationToken: cancellationToken);
  }
}
