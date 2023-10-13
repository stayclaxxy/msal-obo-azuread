using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    
    [HttpGet(Name = "GetToken")]
    public async Task<AuthResponse> Get([FromServices] ITokenAcquisition _tokenAcquisition)
    {
        try
        {
            string[] scopes = { "User.Read" };
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);

            return new AuthResponse 
            {
                AccessToken = accessToken   
            };
        }
        catch (Exception e)
        {
            return null;
        }
    }
}