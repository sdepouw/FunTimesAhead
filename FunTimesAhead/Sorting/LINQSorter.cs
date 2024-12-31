namespace FunTimesAhead.Sorting;

public class LINQSorter : ISorter
{
  public IEnumerable<int> Sort(IEnumerable<int> values) => values.OrderBy(x => x);
}
