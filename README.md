# Fun Times Ahead!

Wanting to explore some things, including but not limited to:

- [TUnit](#TUnit)
- [Mutator Testing with Stryker](#mutator-testing-with-stryker)
- [Benchmark.NET](#benchmarknet)
- [Architecture Testing with ArchUnitNET](#architecture-testing-with-archunitnet)
- HybridCache
- Distributed Caching
  - Redis
  - SQL Server

## TUnit

Feature parity with XUnit/NUnit, with some extra features.

- `[Test]` is used regardless of whether it's a single test, or a test with inputs
- Inputs are passed in via `[Arguments]`
- Alternatively, a parameter can have `[Matrix(1, 2, 3)]` next to it instead
  - Will generate a test run for every combination with other `[Matrix]` parameters
  - Example: `[Matrix(1, 2)]`, `[Matrix(3, 4)]` for a test with two parameters will run 4 tests
    `(1, 3)`, `(1, 4)`, `(2, 3)`, `(2, 4)`
- Every test is `async Task` as built-in assertions are `await Assert.That(...)`
  - Exception assertions are supported (both sync and async)
    - Can use `ThrowsNothing()` to assert an exception was *not* thrown 
  - Can still use [Fluent Assertions](https://www.nuget.org/packages/fluentassertions/) as an alternative

## Mutator Testing with Stryker

Had to use XUnit, as [Stryker lacks Testing Platform support](https://github.com/stryker-mutator/stryker-net/issues/3094)
(which is what TUnit uses).

### Install and Run Stryker

Stryker is installed within the solution as a dotnet tool.

1. Run `dotnet tool restore`
2. Run `dotnet stryker` to 

That should run and generate an HTML file containing the full report

### Mutants We Let Live for the Bubble Sort Implementation

There's one mutant in Bubble Sort that we're letting live. Technically, `<` can be changed to `<=` without changing
behavior, which lets the mutant survive. However, this is an optimization that removes an unnecessary iteration of the
Bubble Sort. We could have a real optimization like this in commercial code. We could ignore the mutation via a
comment, but if `Equality` mutations are ignored it'll take out other ones that we want to have checked. Currently
I've not found a way to ignore one specific mutation in [Stryker's documentation](https://stryker-mutator.io/docs/stryker-net/ignore-mutations/)
so we're settling for letting it live.

I destroyed another niggling mutation by abstracting `ISwapper`, using `NSubstitute` to count invocations, but honestly
it felt like overkill. Though, it could represent Stryker forcing us to correctly abstract some complex behavior buried
within another class. I could go either way on it!

### Continuous Integration

There exists a [GitHub Action for Stryker.NET](https://github.com/stryker-mutator/github-action), where you can give a
threshold (which would allow forgiving mutations that we let survive, like the one mentioned previously). If the
threshold isn't met, the build fails.

Similarly, Stryker can be [configured to fail](https://stryker-mutator.io/blog/azure-pipelines-integration/#-forcing-test-quality)
(by returning a non-zero exit code) via configuration, which is how you would set up Azure DevOps pipelines to fail.

## Benchmark.NET

A super basic benchmark has been set up among all the `ISorter` implementations (with `LINQSorter` being the baseline).

Run via `dotnet run -c Release --project FunTimesAhead\FunTimesAhead.csproj bench` (release mode required and the
argument `"bench"` is explicitly looked for to kick off the benchmark runner). In addition to the console output,
HTML, CSV, and Markdown files are also generated and placed in the directory `BenchmarkDotNet.Artifacts`.

Here's an output after running on my machine:

```
// * Summary *

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2605)
AMD Ryzen 9 7950X3D, 1 CPU, 32 logical and 16 physical cores
.NET SDK 9.0.101
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```

| Method     | Mean        | Error     | StdDev    | Ratio | RatioSD |
|----------- |------------:|----------:|----------:|------:|--------:|
| LINQSort   |    869.1 ns |  12.80 ns |  11.34 ns |  1.00 |    0.02 |
| BubbleSort | 13,010.5 ns | 152.89 ns | 143.01 ns | 14.97 |    0.25 |

```
// * Hints *
Outliers
  SortingComparer.LINQSort: Default -> 2 outliers were removed (913.47 ns, 974.10 ns)

// * Legends *
  Mean    : Arithmetic mean of all measurements
  Error   : Half of 99.9% confidence interval
  StdDev  : Standard deviation of all measurements
  Ratio   : Mean of the ratio distribution ([Current]/[Baseline])
  RatioSD : Standard deviation of the ratio distribution ([Current]/[Baseline])
  1 ns    : 1 Nanosecond (0.000000001 sec)

// ***** BenchmarkRunner: End *****
Run time: 00:00:47 (47.28 sec), executed benchmarks: 2

Global total time: 00:00:51 (51.08 sec), executed benchmarks: 2
// * Artifacts cleanup *
Artifacts cleanup is finished
```

## Architecture Testing with ArchUnitNET

TODO: Experiment with [ArchUnitNET](https://github.com/TNG/ArchUnitNET)
