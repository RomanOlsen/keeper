namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly KeepsService _keepsService;
  public ProfilesController(AccountService accountService, KeepsService keepsService)
  {
    _accountService = accountService;
    _keepsService = keepsService;
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
      Keep keeps = _keepsService.GetAUsersKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}