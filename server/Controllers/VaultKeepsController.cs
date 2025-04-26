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
      Vault foundVault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userInfo); // ANCHOR this is the only thing i dont feel very proud of in my code. I just had to figure out a way to get around the circular dependency error. But all I would have done is moved this line to the service.
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo, foundVault);
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