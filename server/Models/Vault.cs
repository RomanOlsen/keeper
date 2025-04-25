namespace keeper.Models;

public class Vault : DatabaseDefault
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}