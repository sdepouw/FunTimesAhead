using Microsoft.Extensions.Caching.Hybrid;
using NSubstitute;
using NSubstitute.Core;

namespace FunTimesAhead.TestsXUnit.SomeServiceTests;

/// <summary>
/// Extension methods to make testing <see cref="HybridCache" /> easier in NSubstitute
/// </summary>
public static class NSubstituteHybridCacheExtensions
{
  public static ConfiguredCall SetupGetOrCreateAsync(this HybridCache mockCache, string key, string expectedValue)
  {
    return mockCache.GetOrCreateAsync(
      key,
      Arg.Any<object>(),
      Arg.Any<Func<object, CancellationToken, ValueTask<string>>>(),
      Arg.Any<HybridCacheEntryOptions?>(), 
      Arg.Any<IEnumerable<string>?>(), 
      Arg.Any<CancellationToken>()
    ).Returns(expectedValue);
  }
  
  public static async Task AssertGetOrCreateAsyncCalledAsync(this HybridCache mockCache, string key, int requiredNumberOfCalls)
  {
    await mockCache.Received(requiredNumberOfCalls).GetOrCreateAsync(
      key,
      Arg.Any<object>(),
      Arg.Any<Func<object, CancellationToken, ValueTask<string>>>(),
      null,
      null,
      Arg.Any<CancellationToken>()
    );
  }
}
