
namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;

  public ProfilesController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetAUsersProfile(string profileId)
  {
    try
    {
      Profile profile = _accountService.GetAUsersProfile(profileId);
      return profile;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetAUsersKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAUsersKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetAUsersVaultsAsync(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext); // could be null
      List<Vault> vaults = _vaultsService.GetAUsersVaults(profileId, userInfo);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}