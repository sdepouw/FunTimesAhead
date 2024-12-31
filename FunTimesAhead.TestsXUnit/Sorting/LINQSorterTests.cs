using FunTimesAhead.Sorting;

namespace FunTimesAhead.TestsXUnit.Sorting;

public class LINQSorterTests : SorterTestBase<LINQSorter>
{
  protected override LINQSorter Sorter { get; } = new();
}
