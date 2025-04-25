
namespace keeper.Services;

public class KeepsService
{
  private readonly KeepsRepository _repository;
  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  internal Keep CreateKeep(Keep keepData, Account userInfo) // LATER ON add a check to see if the creator id matches the person logged in
  {
    Keep keep = _repository.CreateKeep(keepData);
    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAllKeeps();
    return keeps;
  }
}