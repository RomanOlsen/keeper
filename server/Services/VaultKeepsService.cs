



namespace keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;
  // private readonly VaultsService _vaultsService;
  public VaultKeepsService(VaultKeepsRepository repository)
  {
    _repository = repository;
    // _vaultsService = vaultsService;
  }


  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo, Vault foundVault)
  {

    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    if (foundVault.CreatorId != userInfo.Id)
    {
      throw new Exception("No. You cant make a vaultKeep on someone elses vault!");
    }
    return vaultKeep;
  }

  internal string DeleteVaultKeep(int vaultKeepId, Account userInfo)
  {
    VaultKeep foundVaultKeep = GetVaultKeepById(vaultKeepId);
    if (foundVaultKeep.CreatorId != userInfo.Id)
    {
      throw new Exception("Not your VK that you own, so you cant delete it");
    }

    _repository.DeleteVaultKeep(vaultKeepId); // that (foundVaultKeep.Id) or vaultKeepId should work fine

    return "VK was deleted!";
  }

  // internal VaultKeep GetKeepsInPublicVault()
  // {
  //   throw new NotImplementedException();
  // }

  internal List<KeepsInVault> GetKeepsInVault(int vaultId)
  {
    List<KeepsInVault> keepsInVault = _repository.GetKeepsInVault(vaultId);
    return keepsInVault;
  }

  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep is null)
    {
      throw new Exception("Couldn't find a VaultKeep with that id, sorry bud!");
    }
    return vaultKeep;
  }
}