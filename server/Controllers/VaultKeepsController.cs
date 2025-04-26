namespace keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      _vaultsService.GetVaultById(vaultKeepData.VaultId, userInfo);
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo);
      return Ok(vaultKeep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // [HttpGet] MOVED TO VAULTS CONTROLLER
  // public ActionResult<List<Va>>

  [Authorize]
  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<VaultKeep>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}