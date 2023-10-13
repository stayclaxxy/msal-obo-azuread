using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

//[AuthorizeForScopes(ScopeKeySection = "WeatherForecast:Scopes")]
public class WeatherController : Controller
{
    private IDownstreamApi _weatherForecastService;
    private readonly ITokenAcquisition _tokenAcquisition;
    
    public WeatherController(IDownstreamApi weatherForecastService, ITokenAcquisition tokenAcquisition)
    {
        _weatherForecastService = weatherForecastService;
        _tokenAcquisition = tokenAcquisition;
    }
    
    
    //TODO: Authenticate and call weather API
    public async Task<ActionResult> Index()
    {
        try
        {
            var accessToken = _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "api://330c92a3-464a-4c22-9e57-2a6b50982b38/WeatherForecast.Read" });
            var result = await _weatherForecastService.GetForUserAsync<IEnumerable<WeatherForecast>>("WeatherForecast");
            return View(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        
        return View();
    }
}