



namespace keeper.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO vault_keeps
    (keep_id, vault_id, creator_id)
    VALUES (@KeepId, @VaultId, @CreatorId);
    
    SELECT * FROM vault_keeps
  
WHERE vault_keeps.id = LAST_INSERT_ID()

    ;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault(); // no need to have the extra function to populate a creator
    return vaultKeep;
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = @"
DELETE FROM vault_keeps WHERE id = @vaultKeepId LIMIT 1
;";
    int rowsGotten = _db.Execute(sql, new { vaultKeepId });
    if (rowsGotten != 1)
    {
      throw new Exception("Error. There were either no rows affected or multiple");
    }
  }

  internal List<KeepsInVault> GetKeepsInVault(int vaultId)
  {
    string sql = @"
    SELECT vault_keeps.*, keeps.*, accounts.* FROM vault_keeps
    INNER JOIN keeps ON keeps.id = vault_keeps.keep_id
    INNER JOIN accounts ON accounts.id = vault_keeps.creator_id
    WHERE vault_keeps.vault_id = @vaultId;
    ;";

    List<KeepsInVault> vaultKeeps = _db.Query(sql, (VaultKeep vk, KeepsInVault keep, Account account) =>
    {
      keep.VaultKeepId = vk.Id;
      keep.Creator = account;
      return keep;
    }, new { vaultId }).ToList();
    return vaultKeeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
        SELECT * FROM vault_keeps
        WHERE id = @vaultKeepId
    ;";

    VaultKeep vk = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
    return vk;
  }
}