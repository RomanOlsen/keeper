namespace keeper.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  private Account GetAccount(string accountId)
  {
    Account account = _repo.GetById(accountId);
    if (account == null)
    {
      throw new Exception("Invalid Account Id");
    }
    return account;
  }

  internal Account GetOrCreateAccount(Account userInfo)
  {
    Account account = _repo.GetById(userInfo.Id);
    if (account == null)
    {
      return _repo.Create(userInfo);
    }
    return account;
  }

  internal Account Edit(Account editData, string accountId)
  {
    Account original = GetAccount(accountId);
    original.Name = editData.Name ?? editData.Name;
    original.Picture = editData.Picture ?? editData.Picture;
    return _repo.Edit(original);
  }

  internal Profile GetAUsersProfile(string profileId)
  {
    Profile profile = _repo.GetProfileById(profileId);
    return profile;

  }

  internal Profile EditMyAccount(Profile profileData, Account userInfo) // yes, i see how redundant i made this. i am aware.
  {


    Account original = GetAccount(userInfo.Id);
    original.Name = profileData.Name ?? profileData.Name;
    original.Picture = profileData.Picture ?? profileData.Picture;
    original.Picture = profileData.Picture ?? profileData.Picture;
    original.CoverImg = profileData.CoverImg ?? profileData.CoverImg;

    Profile profile = _repo.EditMyAccount(original);
    return profile;
  }
}
