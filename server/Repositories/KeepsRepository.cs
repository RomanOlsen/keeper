namespace keeper.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
INSERT INTO keeps
(name, description, img, views, creator_id)
VALUES
(@Name, @Description, @Img, @Views, @CreatorId);

SELECT keeps.*, accounts.*
FROM keeps
INNER JOIN accounts ON accounts.id = keeps.creator_id
WHERE keeps.id = LAST_INSERT_ID()
    ;";

    Keep keep = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).SingleOrDefault();
    return keep;
  }
  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT keeps.*, accounts.* FROM keeps
    INNER JOIN accounts ON accounts.id = keeps.creator_id
    ;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();

    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"SELECT keeps.*, accounts.* FROM keeps
        INNER JOIN accounts ON accounts.id = keeps.creator_id
        WHERE keeps.id = @keepId

    ;";
    Keep keep = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { keepId }).SingleOrDefault();
    return keep;
  }
  internal Keep EditKeep(Keep foundKeep) // UPDATE
  {
    string sql = @"
UPDATE keeps
SET
name = @Name,
description = @Description,
img = @Img
WHERE id = @Id LIMIT 1;

SELECT keeps.*, accounts.* FROM keeps
INNER JOIN accounts ON accounts.id = keeps.creator_id
WHERE keeps.id = @Id

;";
    Keep keep = _db.Query(sql, (Keep keep, Account account) => { keep.Creator = account; return keep; }, foundKeep).SingleOrDefault();
    return keep;
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = @"
DELETE FROM keeps WHERE id = @keepId LIMIT 1
;";
    int rowsGotten = _db.Execute(sql, new { keepId });
    if (rowsGotten != 1)
    {
      throw new Exception("Error. There were either no rows affected or multiple");
    }
  }

  internal List<Keep> GetAUsersKeeps(string profileId)
  {
    string sql = @"
    SELECT keeps.*, accounts.* FROM keeps 
    INNER JOIN accounts ON accounts.id = keeps.creator_id
    WHERE keeps.creator_id = @profileId
    
    ;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Account account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { profileId }).ToList();
    return keeps;
  }

  internal Keep UpdateViewCount(int keepId)
  {
    string sql = @"
    UPDATE keeps
    SET
    views = views + 1
    WHERE id = @keepId
    LIMIT 1;

    SELECT keeps.*, accounts.* FROM keeps
        INNER JOIN accounts ON accounts.id = keeps.creator_id
        WHERE keeps.id = @keepId
    ;";

    // return _db.Query(sql, new { keepId }).SingleOrDefault();

    Keep keep = _db.Query(sql, (Keep keep, Account account) => { keep.Creator = account; return keep; }, new { keepId }).SingleOrDefault();
    return keep;
  }

  // internal void UpdateKeptCount(int keepId)
  // {
  //   string sql = @"
  //   UPDATE keeps
  //   SET kept = kept + 1
  //   WHERE id = @keepId
  //   LIMIT 1
  //   ;";

  //   _db.Query(sql, new { keepId });
  // }
}