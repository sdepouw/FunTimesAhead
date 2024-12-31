# Fun Times Ahead!

Wanting to explore some things, including but not limited to:

- TUnit
- Mutator Testing with Stryker
- HybridCache
- Distributed Caching
  - Redis
  - SQL Server
- Architecture Testing

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

## Mutator Testing (Stryker)

Had to use XUnit, as [Stryker lacks Testing Platform support](https://github.com/stryker-mutator/stryker-net/issues/3094)
(which is what TUnit uses).

### Install and Run Stryker

Stryker is installed within the solution as a dotnet tool.

1. Run `dotnet tool restore`
2. Run `dotnet stryker` to 

That should run and generate an HTML file containing the full report
