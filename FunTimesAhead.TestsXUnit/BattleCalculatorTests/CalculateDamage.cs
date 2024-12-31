using FluentAssertions;

namespace FunTimesAhead.TestsXUnit.BattleCalculatorTests;

public class CalculateDamage
{
  private readonly BattleCalculator _calculator = new();

  [Fact]
  public void Is42GivenNoStrength()
  {
    const int expectedDamage = 42;
    const int characterStrength = 0;
    const int weaponStrength = 0;

    int damage = _calculator.CalculateDamage(characterStrength, weaponStrength);

    damage.Should().Be(expectedDamage);
  }

  [Theory]
  [InlineData(5, 10)]
  [InlineData(20, 55)]
  [InlineData(1, 95000)]
  public void AddsCharacterAndWeaponStrengths(int characterStrength, int weaponStrength)
  {
    int expectedDamage = characterStrength + weaponStrength + 42;

    int damage = _calculator.CalculateDamage(characterStrength, weaponStrength);

    damage.Should().Be(expectedDamage);
  }

  [Theory]
  [InlineData(1, -9)]
  [InlineData(5, 10)]
  [InlineData(20, 55)]
  [InlineData(1, 95000)]
  public void AddsStrengthsWithMatrix(int characterStrength, int weaponStrength)
  {
    int expectedDamage = characterStrength + weaponStrength + 42;

    int damage = _calculator.CalculateDamage(characterStrength, weaponStrength);

    damage.Should().Be(expectedDamage);
  }

  [Fact]
  public async Task ThrowsGivenNegativeCharacterStrength()
  {
    const int characterStrength = -5;

    Action calculateCall = () => _calculator.CalculateDamage(characterStrength, 52);
    Func<Task> asyncCalculateCall = () => _calculator.CalculateAsync(characterStrength, CancellationToken.None);

    calculateCall.Should().ThrowExactly<InvalidCharacterStrengthException>();
    await asyncCalculateCall.Should().ThrowExactlyAsync<InvalidCharacterStrengthException>();
  }
}
