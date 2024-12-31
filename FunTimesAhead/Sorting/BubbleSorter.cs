namespace FunTimesAhead.Sorting;

public class BubbleSorter(ISwapper swapper) : ISorter
{
  public IEnumerable<int> Sort(IEnumerable<int> values)
  {
    List<int> valuesList = [.. values];
    int size = valuesList.Count;
    if (size < 2) return valuesList;
    for (int i = 0; i < size; i++)
    {
      for (int j = 0; j < size - i - 1; j++)
      {
        if (valuesList[j] > valuesList[j + 1])
        {
          swapper.Swap(valuesList, j, j + 1);
        }
      }
    }

    return valuesList;
  }
}
