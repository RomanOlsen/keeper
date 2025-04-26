




namespace keeper.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO vaults
    (name, description, img, is_private, creator_id)
    VALUES (@Name, @Description, @Img, @IsPrivate, @CreatorId);
    
    SELECT vaults.*, accounts.* FROM vaults
INNER JOIN accounts ON accounts.id = vaults.creator_id
WHERE vaults.id = LAST_INSERT_ID()

    ;";

    Vault vault = _db.Query(sql, (Vault vault, Account account) =>
    {
      vault.Creator = account;
      return vault;
    }, vaultData).SingleOrDefault();
    return vault;
  }


  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"SELECT vaults.*, accounts.* FROM vaults
        INNER JOIN accounts ON accounts.id = vaults.creator_id
        WHERE vaults.id = @vaultId

    ;";
    Vault vault = _db.Query(sql, (Vault vault, Account account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { vaultId }).SingleOrDefault();
    return vault;
  }
  internal Vault EditVault(Vault foundVault) // only update name and IsPrivate?
  {
    string sql = @"
UPDATE vaults
SET
name = @Name,
is_private = @IsPrivate,
description = @Description,
img = @Img
WHERE id = @Id LIMIT 1;

SELECT vaults.*, accounts.* FROM vaults
INNER JOIN accounts ON accounts.id = vaults.creator_id
WHERE vaults.id = @Id


;";

    Vault vault = _db.Query(sql, (Vault vault, Account account) => { vault.Creator = account; return vault; }, foundVault).SingleOrDefault();
    return vault;
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = @"
DELETE FROM vaults WHERE id = @vaultId LIMIT 1
;";
    int rowsGotten = _db.Execute(sql, new { vaultId });
    if (rowsGotten != 1)
    {
      throw new Exception("Error. There were either no rows affected or multiple");
    }
  }

  internal List<Vault> GetAUsersVaults(string profileId)
  {
    string sql = @"
    SELECT vaults.*, accounts.* FROM vaults
    INNER JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.creator_id = @profileId

    ;";
    List<Vault> vaults = _db.Query(sql, (Vault vault, Account account) => { vault.Creator = account; return vault; }, new { profileId }).ToList();
    return vaults;
  }

  internal List<Vault> GetMyVaults(string userInfoId)
  {
    string sql = @"
        SELECT vaults.*, accounts.* FROM vaults
        INNER JOIN accounts ON accounts.id = vaults.creator_id
        WHERE vaults.creator_id = @userInfoId
    ;";
    List<Vault> vaults = _db.Query(sql, (Vault vault, Account account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { userInfoId }).ToList();
    return vaults;
  }

  // internal List<KeepsInVault> GetKeepsInPrivateVault(int vaultId)
  // {
  //   string sql = @"
  //   SELECT vault_keeps.*, keeps.*, accounts.* FROM vault_keeps
  //   INNER JOIN keeps ON keeps.id = vault_keeps.keep_id
  //   INNER JOIN accounts ON accounts.id = vault_keeps.creator_id
  //   WHERE vault_keeps.vault_id = @vaultId;
  //   ;";

  //   List<KeepsInVault> vaultKeeps = _db.Query(sql, (VaultKeep vk, KeepsInVault keep, Account account) =>
  //   {
  //     keep.VaultKeepId = vk.Id;
  //     keep.Creator = account;
  //     return keep;
  //   }, new { vaultId }).ToList();
  //   return vaultKeeps;
  // }

  // internal List<KeepsInVault> GetKeepsInPublicVault(int vaultId)
  // {
  //   throw new NotImplementedException();
  // }
}

