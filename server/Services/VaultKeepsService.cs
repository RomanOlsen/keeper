
namespace keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;
  public VaultKeepsService(VaultKeepsRepository repository)
  {
    _repository = repository;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo)
  {
    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }
}