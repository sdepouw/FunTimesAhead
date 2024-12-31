namespace FunTimesAhead;

public interface ISwapper
{
  void Swap<T>(List<T> valuesList, int firstIndex, int secondIndex);
}
