using FunTimesAhead.Exceptions;

namespace FunTimesAhead;

public class BattleCalculator : IBattleCalculator
{
  public int CalculateDamage(int characterStrength, int weaponStrength)
  {
    if (characterStrength < 0) throw new InvalidCharacterStrengthException();
    return characterStrength + weaponStrength + 42;
  }

  public Task<int> CalculateAsync(int characterStrength, CancellationToken cancellationToken)
  {
    if (characterStrength < 0) throw new InvalidCharacterStrengthException();
    return Task.FromResult(characterStrength);
  }
}
