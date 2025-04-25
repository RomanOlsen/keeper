

namespace keeper.Services;

public class VaultsService
{
  private readonly VaultsRepository _repository;

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }


  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetVaultById(vaultId);
    if (vault is null)
    {
      throw new Exception("Couldn't find a Vault with that id, sorry bud!");
    }
    return vault;
  }
  internal Vault EditVault(Vault vaultData, Account userInfo, int vaultId)
  {
    Vault foundVault = GetVaultById(vaultId);
    if (foundVault.CreatorId != userInfo.Id)
    {
      throw new Exception("You are forbidden. Thats not your vault. How dare you!");
    }
    
    foundVault.Name = vaultData.Name ?? foundVault.Name;
    foundVault.IsPrivate = vaultData.IsPrivate ?? foundVault.IsPrivate;


    Vault vault = _repository.EditVault(foundVault);
    return vault;
  }
}