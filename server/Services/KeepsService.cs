namespace keeper.Services;

public class KeepsService
{
  private readonly KeepsRepository _repository;
  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  internal Keep CreateKeep(Keep keepData, Account userInfo)
  {
    Keep keep = _repository.CreateKeep(keepData, userInfo);
    return keep;
  }
}