namespace FunTimesAhead.Sorting;

public class BubbleSorter : ISorter
{
  public IEnumerable<int> Sort(IEnumerable<int> values)
  {
    List<int> valuesList = [.. values];
    int size = valuesList.Count;
    if (size < 2) return valuesList;

    for (int i = 0; i < size - 1; i++)
    {
      bool swapped = false;
      for (int j = 0; j < size - i - 1; j++)
      {
        if (valuesList[j] <= valuesList[j + 1]) continue;
        (valuesList[j], valuesList[j + 1]) = (valuesList[j + 1], valuesList[j]);
        swapped = true;
      }
      if (!swapped) break;
    }

    return valuesList;
  }
}