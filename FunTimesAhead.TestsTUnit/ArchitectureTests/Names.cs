using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.TUnit;
using FunTimesAhead.Sorting;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace FunTimesAhead.TestsTUnit.ArchitectureTests;

public class Names
{
  private static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(System.Reflection.Assembly.GetAssembly(typeof(IBattleCalculator)))
    .Build();
  
  // These could potentially be defined elsewhere, if used across multiple test classes. 
  private readonly IObjectProvider<Class> _sorterClasses = Classes()
    .That().ImplementInterface(typeof(ISorter).FullName).As("Sorter Implementations");
  private readonly IObjectProvider<Class> _exceptionClasses = Classes()
    .That().ResideInNamespace("FunTimesAhead.Exceptions").As("Exception Classes");

  [Test]
  public void SorterImplementationsAreCalledSorter()
  {
    // Rules that are more generic could be defined elsewhere for sharing.
    IArchRule sorterNameRule = Classes().That().Are(_sorterClasses)
      .Should().HaveNameEndingWith("Sorter");
    
    sorterNameRule.Check(Architecture);
  }
  
  [Test]
  public void ExceptionClassesAreCalledException()
  {
    IArchRule exceptionNameRule = Classes().That().Are(_exceptionClasses)
      .Should().HaveNameEndingWith("Exception");
    exceptionNameRule.Check(Architecture);
  }
}