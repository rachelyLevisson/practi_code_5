using Microsoft.AspNetCore.Mvc;
using Service;
using WebAPI.Entity;

[Route("api/[controller]")]
[ApiController]
public class RepositoryController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    public RepositoryController(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }
    [HttpGet("repositories")]
    public async Task<ActionResult<List<RepositoryEntity>>> GetRepositories()
    {
        var repositories = await _gitHubService.GetRepositoriesAsync("RachelyLevisson");

        if (repositories == null || !repositories.Any())
        {
            return NotFound("No repositories found for this user.");
        }

        return Ok(repositories);
    }



    [HttpGet("search/{term}")]
    public async Task<ActionResult<List<RepositoryEntity>>> SearchRepositories(string term)
    {
        var repositories = await _gitHubService.SearchRepositoriesAsync(term);
        return Ok(repositories);
    }
}
