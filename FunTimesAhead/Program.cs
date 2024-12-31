// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FunTimesAhead.Sorting;

Console.WriteLine("Hello, World! Let's have some fun.");

if (args.Length == 1 && args.Single() == "bench")
{
  BenchmarkRunner.Run<SortingComparer>();
}
