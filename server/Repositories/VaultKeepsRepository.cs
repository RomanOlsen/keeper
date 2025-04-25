


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