using FluentAssertions;
using FunTimesAhead.Sorting;

namespace FunTimesAhead.TestsXUnit.Sorting;

public abstract class SorterTestBase<TSorter> where TSorter : ISorter, new()
{
  private readonly TSorter _sorter = new();
  
  [Fact]
  public void SortsGivenNumbers()
  {
    List<int> values = [9, -384, 0, 38, 299, -2, 34, 8, 1, 3];
    IEnumerable<int> expectedSortedValues = values.OrderBy(x => x);

    IEnumerable<int> sortedValues = _sorter.Sort(values);

    sortedValues.Should().BeEquivalentTo(expectedSortedValues);
  }

  [Fact]
  public void ReturnsNothingGivenNothing()
  {
    IEnumerable<int> noValues = [];
    
    IEnumerable<int> sortedValues = _sorter.Sort(noValues);
    
    sortedValues.Should().BeEmpty();
  }
}