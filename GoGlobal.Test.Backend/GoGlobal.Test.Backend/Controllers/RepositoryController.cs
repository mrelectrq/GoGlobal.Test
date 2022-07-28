using GoGlobal.Test.Domain;
using GoGlobal.Test.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoGlobal.Test.Backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RepositoryController :ControllerBase
{
    private readonly RepositoryProvider<IRepositoryInfo> _provider;
    public RepositoryController(RepositoryProvider<IRepositoryInfo> provider)
    {
        _provider = provider;
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public async Task<IEnumerable<IRepositoryInfo>> GetRepositories(string repositoryName)
    {
        var repositories =await _provider.GetRepositories(repositoryName);
        return repositories;
    }

}