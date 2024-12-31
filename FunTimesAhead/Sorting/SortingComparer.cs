using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace FunTimesAhead.Sorting;

public class SortingComparer
{
  private static readonly ISorter LINQSorter = new LINQSorter();
  private static readonly ISorter BubbleSorter = new BubbleSorter(new Swapper());
  private readonly Consumer _consumer = new();

  private readonly List< int> _itemsToSort = Enumerable.Range(1, 100).Select(_ => Random.Shared.Next(int.MinValue, int.MaxValue)).ToList(); 
  
  [Benchmark(Baseline = true)] public void LINQSort() => LINQSorter.Sort(_itemsToSort).Consume(_consumer);
  [Benchmark] public void BubbleSort() => BubbleSorter.Sort(_itemsToSort).Consume(_consumer);
}