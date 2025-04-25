namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0Provider;
  public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
  {
    _keepsService = keepsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.CreateKeep(keepData, userInfo);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public async Task<ActionResult <List<Keep>>> GetAllKeeps(){
    try 
    {
    List<Keep> keep = _keepsService.GetAllKeeps();
    return keep;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}