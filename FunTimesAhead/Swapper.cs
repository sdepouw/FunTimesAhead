namespace FunTimesAhead;

public class Swapper : ISwapper
{
  public void Swap<T>(List<T> valuesList, int firstIndex, int secondIndex)
  {
    (valuesList[firstIndex], valuesList[secondIndex]) = (valuesList[secondIndex], valuesList[firstIndex]);
  }
}
