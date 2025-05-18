using Octokit;
using Service;
using WebAPI.Entity;

public class GitHubService : IGitHubService
{
    private readonly GitHubClient _client;

    public GitHubService(string token)
    {
        _client = new GitHubClient(new ProductHeaderValue("YourAppName"))
        {
            Credentials = new Credentials(token)
        };
    }

    public async Task<List<RepositoryEntity>> GetRepositoriesAsync(string username)
    {
        var repositories = await _client.Repository.GetAllForUser(username);
        return repositories.Select(repo => new RepositoryEntity
        {
            Name = repo.Name,
            Language = repo.Language,
            LastUpdated = repo.UpdatedAt.DateTime,
            Stars = repo.StargazersCount,
            Url = repo.HtmlUrl
        }).ToList();
    }

    public async Task<List<RepositoryEntity>> SearchRepositoriesAsync(string searchTerm)
    {
        var searchRequest = new SearchRepositoriesRequest(searchTerm);
        var searchResult = await _client.Search.SearchRepo(searchRequest);
        return searchResult.Items.Select(repo => new RepositoryEntity
        {
            Name = repo.Name,
            Language = repo.Language,
            LastUpdated = repo.UpdatedAt.DateTime,
            Stars = repo.StargazersCount,
            Url = repo.HtmlUrl
        }).ToList();
    }
}
