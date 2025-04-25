namespace keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;
  public VaultKeepsService(VaultKeepsRepository repository)
  {
    _repository = repository;
  }
}