using FunTimesAhead.Sorting;
using NSubstitute;
using Shouldly;

namespace FunTimesAhead.TestsXUnit.Sorting;

public class BubbleSorterTests : SorterTestBase<BubbleSorter>
{
  private readonly ISwapper _mockSwapper = Substitute.For<ISwapper>();
  
  protected override BubbleSorter Sorter { get; }

  public BubbleSorterTests()
  {
    Sorter = new(_mockSwapper);
    Swapper realSwapper = new();
    _mockSwapper
      .When(x => x.Swap(Arg.Any<List<int>>(), Arg.Any<int>(), Arg.Any<int>()))
      // We want to actually swap values, and just count the number of invocations via NSubstitute
      .Do(x => realSwapper.Swap(x.Arg<List<int>>(), x.ArgAt<int>(1), x.ArgAt<int>(2)));
  }
  
  [Fact]
  public void DoesNotChangeEqualValues()
  {
    const int expectedNumberOfSwaps = 0;
    List<int> values = [1, 1, 1];
    List<int> expectedSortedValues = [1, 1, 1];
    
    IEnumerable<int> sortedValues = Sorter.Sort(values);
    
    sortedValues.ShouldBe(expectedSortedValues);
    _mockSwapper.Received(expectedNumberOfSwaps).Swap(Arg.Any<List<int>>(), Arg.Any<int>(), Arg.Any<int>());
  }
}
