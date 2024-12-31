using FluentAssertions;
using FunTimesAhead.Sorting;

namespace FunTimesAhead.TestsXUnit.Sorting;

public abstract class SorterTestBase<TSorter> where TSorter : ISorter
{
  protected abstract TSorter Sorter { get; }
  
  [Fact]
  public void SortsGivenNumbers()
  {
    List<int> values = [9, -384, 0, 38, 299, -2, 34, 8, 1, 3];
    IEnumerable<int> expectedSortedValues = values.OrderBy(x => x);

    IEnumerable<int> sortedValues = Sorter.Sort(values);

    sortedValues.Should().Equal(expectedSortedValues);
  }
  
  [Fact]
  public void ReturnsNothingGivenNothing()
  {
    IEnumerable<int> noValues = [];
    
    IEnumerable<int> sortedValues = Sorter.Sort(noValues);
    
    sortedValues.Should().BeEmpty();
  }

  [Fact]
  public void ReturnsSingleItem()
  {
    List<int> values = [42];
    List<int> expectedSortedValues = [42];
    
    IEnumerable<int> sortedValues = Sorter.Sort(values);
    
    sortedValues.Should().Equal(expectedSortedValues);
  }

  [Fact]
  public void SortsPair()
  {
    List<int> values = [30, 10];
    List<int> expectedSortedValues = [10, 30];
    
    IEnumerable<int> sortedValues = Sorter.Sort(values);
    
    sortedValues.Should().Equal(expectedSortedValues);
  }
  
  [Fact]
  public void DoesNotChangeAlreadySortedValues()
  {
    List<int> values = [1, 2, 3, 4, 5];
    List<int> expectedSortedValues = [1, 2, 3, 4, 5];
    
    IEnumerable<int> sortedValues = Sorter.Sort(values);
    
    sortedValues.Should().Equal(expectedSortedValues);
  }
}