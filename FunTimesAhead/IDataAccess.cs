namespace FunTimesAhead;

public interface IDataAccess
{
  ValueTask<string> GetImportantDataAsync(CancellationToken cancellationToken);
}
