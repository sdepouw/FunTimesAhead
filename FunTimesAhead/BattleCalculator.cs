namespace FunTimesAhead;

public class BattleCalculator : IBattleCalculator
{
  public int CalculateDamage(int characterStrength, int weaponStrength) => characterStrength + weaponStrength + 42;
}
