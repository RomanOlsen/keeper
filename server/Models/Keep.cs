namespace keeper.Models;

public class Keep : DatabaseDefault
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Views { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
  public int Kept { get; set; } // TODO figure out Kept (should be an int) amount of vaults its saved in. look at UML too.

}

public class KeepsInVault : Keep
{
  public int VaultKeepId { get; set; }
}