using System.Runtime.CompilerServices;

namespace FunTimesAhead.TestsTUnit.BattleCalculatorTests;

// Extras Tutorial -- https://thomhurst.github.io/TUnit/docs/category/tutorial---extras

public class CalculateDamage
{
  private readonly BattleCalculator _calculator = new();

  [Test]
  //[Repeat(10)]
  public async Task Is42GivenNoStrength()
  {
    const int expectedDamage = 42;
    const int characterStrength = 0;
    const int weaponStrength = 0;

    int damage = _calculator.CalculateDamage(characterStrength, weaponStrength);

    await Assert.That(damage).IsEqualTo(expectedDamage);

    // AssertionScope
    // using (Assert.Multiple())
    // {
    //   await Assert.That(damage).IsEqualTo(expectedDamage + 2);
    //   await Assert.That(damage).IsEqualTo(expectedDamage + 3);
    // }
  }

  [Test]
  [Arguments(5, 10)]
  [Arguments(20, 55)]
  [Arguments(1, 95000)]
  public async Task AddsCharacterAndWeaponStrengths(int characterStrength, int weaponStrength)
  {
    int expectedDamage = characterStrength + weaponStrength + 42;

    int damage = _calculator.CalculateDamage(characterStrength, weaponStrength);

    await Assert.That(damage).IsEqualTo(expectedDamage);
  }

  // Can use Arguments and MatrixAttribute together! 3 * 3 + 1 = 10 tests generated
  [Test]
  [Arguments(1, -9)]
  public async Task AddsStrengthsWithMatrix(
    [Matrix(5, 20, 1)] int characterStrength,
    [Matrix(10, 55, 95000)] int weaponStrength)
  {
    int expectedDamage = characterStrength + weaponStrength + 42;

    int damage = _calculator.CalculateDamage(characterStrength, weaponStrength);

    await Assert.That(damage).IsEqualTo(expectedDamage);
  }

  [Test]
  public async Task ThrowsGivenNegativeCharacterStrength()
  {
    const int characterStrength = -5;

    void CalculateCall() => _calculator.CalculateDamage(characterStrength, 52);
    Action calculateCallAlt = () => _calculator.CalculateDamage(characterStrength, 52);
    Task AsyncCalculateCall() => _calculator.CalculateAsync(characterStrength, CancellationToken.None);
    Func<Task> asyncCalculateCallAlt = () => _calculator.CalculateAsync(characterStrength, CancellationToken.None);

    using IDisposable _ = Assert.Multiple();
    await Assert.That(CalculateCall).Throws<InvalidCharacterStrengthException>();
    await Assert.That(calculateCallAlt).Throws<InvalidCharacterStrengthException>();
    await Assert.ThrowsAsync<InvalidCharacterStrengthException>(AsyncCalculateCall);
    await Assert.ThrowsAsync<InvalidCharacterStrengthException>(asyncCalculateCallAlt);
  }
}
