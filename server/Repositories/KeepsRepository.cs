
namespace keeper.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData, Account userInfo)
  {
    string sql = @"
INSERT INTO keeps
(name, description, img, views, creator_id)
VALUES
(@Name, @Description, @Img, @Views, @CreatorId)

SELECT * FROM keeps

    ;";

    Keep keep = _db.Query<Keep>(sql, keepData).SingleOrDefault();
    return keep;
  }
}