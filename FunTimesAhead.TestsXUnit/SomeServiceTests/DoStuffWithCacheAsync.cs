using FluentAssertions;
using Microsoft.Extensions.Caching.Hybrid;
using NSubstitute;

namespace FunTimesAhead.TestsXUnit.SomeServiceTests;

public class DoStuffWithCacheAsync
{
  private readonly SomeService _service;
  private readonly HybridCache _mockCache = Substitute.For<HybridCache>();
  public DoStuffWithCacheAsync() => _service = new SomeService(_mockCache);

  [Fact]
  public async Task GetsFromCache()
  {
    const string expectedValue = "abc";
    
    // Mocking a return value
    _mockCache.SetupGetOrCreateAsync("some-key", expectedValue);
    
    string result = await _service.DoStuffWithCacheAsync(CancellationToken.None);

    // Asserting the cache was called
    await _mockCache.AssertGetOrCreateAsyncCalledAsync("some-key", 1);
    result.Should().Be(expectedValue);
  }
}