namespace FunTimesAhead.Tests.BattleCalculatorTests;

public class CalculateDamage
{
  [Fact]
  public void Is42GivenNoStrength()
  {
    const int expectedDamage = 42;
    const int characterStrength = 0;
    const int weaponStrength = 0;

    int damage = new BattleCalculator().CalculateDamage(characterStrength, weaponStrength);

    Assert.Equal(expectedDamage, damage);
  }
}
