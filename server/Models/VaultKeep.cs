namespace keeper.Models;

public class VaultKeep : DatabaseDefault
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  public string CreatorId { get; set; }
}