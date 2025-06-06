namespace keeper.Services;

public class VaultsService
{
  private readonly VaultsRepository _repository;
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0Provider;




  public VaultsService(VaultsRepository repository, VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
  {
    _repository = repository;
    _vaultKeepsService = vaultKeepsService;
    _auth0Provider = auth0Provider;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }


  internal Vault GetVaultById(int vaultId, Account userInfo)
  {
    Vault vault = _repository.GetVaultById(vaultId);
    if (vault is null)
    {
      throw new Exception("Couldn't find a Vault with that id, sorry bud!");
    }
    if (userInfo is null)
    {
      if (vault.IsPrivate is true)
      {
        throw new Exception("forbidden. log in buddy");
      }
      return vault;
    }
    if (userInfo.Id != vault.CreatorId && vault.IsPrivate is true)
    {
      throw new Exception("forbidden. not your vault and this ones private");
    }
    // if (userInfo.Id != null && userInfo.Id != vault.CreatorId && vault.IsPrivate is true)
    // {
    //   throw new Exception("you are forbidden to view another persons vault that they have privated");
    // }
    return vault;
  }
  internal Vault EditVault(Vault vaultData, Account userInfo, int vaultId)
  {
    Vault foundVault = GetVaultById(vaultId, userInfo);
    if (foundVault.CreatorId != userInfo.Id)
    {
      throw new Exception("You are forbidden. Thats not your vault. How dare you!");
    }

    foundVault.Name = vaultData.Name ?? foundVault.Name;
    foundVault.IsPrivate = vaultData.IsPrivate ?? foundVault.IsPrivate;


    Vault vault = _repository.EditVault(foundVault);
    return vault;
  }

  internal string DeleteVault(int vaultId, Account userInfo)
  {
    Vault foundVault = GetVaultById(vaultId, userInfo);
    if (foundVault.CreatorId != userInfo.Id)
    {
      throw new Exception("Not your Vault that you own, so you cant delete it. Good try I guess");
    }

    _repository.DeleteVault(vaultId);

    return "Vault was deleted!";
  }

  internal List<KeepsInVault> GetKeepsInVault(int vaultId, Account userInfo)
  {
    Vault foundVault = GetVaultById(vaultId, userInfo);
    if (foundVault.IsPrivate is true)
    {
      if (foundVault.CreatorId == userInfo.Id || userInfo.Id != null) // null check prob not needed but cant hurt
      {
        List<KeepsInVault> keepsInVault = _vaultKeepsService.GetKeepsInVault(vaultId);
        return keepsInVault;
      }
      else
      {
        throw new Exception("Error! This vault is private, and you do not own it. So you cant access.");
      }
    }
    List<KeepsInVault> keepsInPublicVault = _vaultKeepsService.GetKeepsInVault(vaultId);
    return keepsInPublicVault;
  }

  internal List<Vault> GetAUsersVaults(string profileId, Account userInfo)
  {
    List<Vault> vaults = _repository.GetAUsersVaults(profileId);
    return vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userInfo?.Id);  // lecture reference
  }

  internal List<Vault> GetMyVaults(string UserInfoId) // basically get vaults for an account id
  {
    List<Vault> vaults = _repository.GetMyVaults(UserInfoId);
    return vaults;
  }
}