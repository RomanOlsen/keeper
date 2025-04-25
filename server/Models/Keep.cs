namespace keeper.Models;

public class Keep : DatabaseDefault
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Views { get; set; }
  public string CreatorId { get; set; }

}