
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
}