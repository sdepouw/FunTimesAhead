namespace FunTimesAhead;

public class SomeExpensiveDataAccess : IDataAccess
{
  public ValueTask<string> GetImportantDataAsync(CancellationToken cancellationToken)
  {
    Console.WriteLine("Real data gotten!");
    return new ValueTask<string>(Guid.NewGuid().ToString());
  }
}
