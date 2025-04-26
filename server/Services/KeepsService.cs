


namespace keeper.Services;

public class KeepsService
{
  private readonly KeepsRepository _repository;
  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  internal Keep CreateKeep(Keep keepData, Account userInfo) // LATER ON add a check to see if the creator id matches the person logged in
  {
    Keep keep = _repository.CreateKeep(keepData);
    return keep;
  }
  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetKeepById(keepId);
    if (keep is null)
    {
      throw new Exception("Couldn't find a Keep with that id, sorry bud!");
    }
    return keep;
  }
  internal Keep EditKeep(Keep keepData, Account userInfo, int keepId)
  {
    Keep foundKeep = GetKeepById(keepId);

    if (foundKeep.CreatorId != userInfo.Id)
    {
      throw new Exception("Not your Keep that you own");
    }

    foundKeep.Name = keepData.Name ?? foundKeep.Name;
    foundKeep.Description = keepData.Description ?? foundKeep.Description;
    foundKeep.Img = keepData.Img ?? foundKeep.Img;

    Keep keep = _repository.EditKeep(foundKeep);
    return keep;
  }

  internal string DeleteKeep(int keepId, Account userInfo)
  {
    Keep foundKeep = GetKeepById(keepId);
    if (foundKeep.CreatorId != userInfo.Id)
    {
      throw new Exception("Not your Keep that you own, so you cant delete it");
    }

    _repository.DeleteKeep(keepId);

    return "Keep was deleted!";


  }

  internal List<Keep> GetAUsersKeeps(string profileId)
  {
    List<Keep> keep = _repository.GetAUsersKeeps(profileId);
    return keep;
  }

  internal Keep UpdateViewCount(int keepId)
  {
    return _repository.UpdateViewCount(keepId);
  }
}