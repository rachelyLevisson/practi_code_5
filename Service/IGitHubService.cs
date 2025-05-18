using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entity;

namespace Service
{
    public interface IGitHubService
    {
        Task<List<RepositoryEntity>> GetRepositoriesAsync(string username);
        Task<List<RepositoryEntity>> SearchRepositoriesAsync(string searchTerm);
    }

}
