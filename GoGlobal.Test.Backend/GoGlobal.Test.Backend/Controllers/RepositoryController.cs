using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using GoGlobal.Test.Backend.Services;
using GoGlobal.Test.Data;
using GoGlobal.Test.Data.Entities;
using GoGlobal.Test.Domain;
using GoGlobal.Test.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Core.ExcelPackage;

namespace GoGlobal.Test.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepositoryController : ControllerBase
{
    private readonly RepositoryProvider<IRepositoryInfo> _provider;
    private readonly TestDbContext _dbContext;
    private readonly IMailSender _mailSender;
    public RepositoryController(RepositoryProvider<IRepositoryInfo> provider, TestDbContext dbContext,IMailSender mailSender)
    {
        _provider = provider;
        _dbContext = dbContext;
        _mailSender = mailSender;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public async Task<IEnumerable<IRepositoryInfo>> GetRepositories(string repositoryName)
    {
        var repositories = await _provider.GetRepositories(repositoryName);
        return repositories;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public async Task<IActionResult> SaveGithubRepositories(GithubRepositoryInfo repositoryInfos)
    {
        _dbContext.Repositories.Add(new Repository()
        {
            RepositoryId = Guid.NewGuid(),
            RepositoryDescription = repositoryInfos.RepositoryDescription,
            Avatar = repositoryInfos.Avatar,
            RepositoryName = repositoryInfos.RepositoryName
        });
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpGet("/export")]
    [Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public ActionResult<FileInfo> ExportGithubRepositoriesCsv()
    {
        var bookmarks = _dbContext.Repositories.AsEnumerable();
        var stream = new MemoryStream();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using (var writer = new StreamWriter(stream, leaveOpen: true))
        {
            var csv = new CsvWriter(writer, config);
            csv.WriteRecords(bookmarks);
        }

        stream.Position = 0;
        
        return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "repositories.csv");
    }

    /// <summary>
    /// Get all saved repositories from DB
    /// </summary>
    /// <returns></returns>
    [HttpGet("/bookmark")]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public  ActionResult<IEnumerable<Repository>> GetGithubSavedRepositories()
    {
        var repos = _dbContext.Repositories.AsEnumerable();
        return Ok(repos);
    }

    [HttpDelete("/bookmark")]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public async Task<ActionResult<bool>> DeleteSavedRepositories(Guid repositoryId)
    {
        var repository = _dbContext.Repositories.FirstOrDefault(m => m.RepositoryId == repositoryId);
        if (repository == null)
        {
            return BadRequest();
        }
        else
        {
            _dbContext.Repositories.Remove(repository);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
    [HttpPost("/email")]
    [Authorize(AuthenticationSchemes = "CustomAuth")]
    public async Task<ActionResult<bool>> SendEmail(string to, string emailSubject, string emailBody)
    {
        var status = await _mailSender.SendMailAsync(to, emailSubject, emailBody);
        return status;
    }
}