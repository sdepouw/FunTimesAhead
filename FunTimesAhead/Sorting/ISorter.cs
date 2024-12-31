namespace FunTimesAhead.Sorting;

/// <summary>
/// Defining a basic sort interface with multiple implementations, to test Benchmark.NET
/// </summary>
public interface ISorter
{
  /// <summary>
  /// Sorts the given values in numerical order
  /// </summary>
  /// <param name="values">The values to sort</param>
  /// <returns>The sorted values</returns>
  IEnumerable<int> Sort(IEnumerable<int> values);
}
