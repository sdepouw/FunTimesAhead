using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Extensions;
using TUnit.Core.Exceptions;

// ReSharper disable once CheckNamespace
namespace ArchUnitNET.TUnit;

/// <summary>
///     Assertion failure exception for TUnit
/// </summary>
/// <remarks>
///     Extracted from TngTech.ArchUnitNET.xUnit NuGet package
/// </remarks>
public class FailedArchRuleException : TUnitException
{
  /// <summary>
  ///     Creates a new instance of the <see href="FailedArchRuleException" /> class.
  /// </summary>
  /// <param name="architecture">The architecture which was tested</param>
  /// <param name="archRule">The archrule that failed</param>
  public FailedArchRuleException(Architecture architecture, IArchRule archRule)
    : this(archRule.Evaluate(architecture)) { }

  /// <summary>
  ///     Creates a new instance of the <see href="FailedArchRuleException" /> class.
  /// </summary>
  /// <param name="evaluationResults">The results of the evaluation of the archrule</param>
  public FailedArchRuleException(IEnumerable<EvaluationResult> evaluationResults)
    : base(evaluationResults.ToErrorMessage()) { }
}
