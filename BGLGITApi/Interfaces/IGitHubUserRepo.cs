using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLGITApi
{
    public interface IGitHubUserRepo
    {
        GitUser GetGitUserAsync(string name);
        IEnumerable<GitUserRepoList> GetUserRepoListAsync(string url);
    }
}
