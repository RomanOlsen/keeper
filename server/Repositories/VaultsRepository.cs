



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
}