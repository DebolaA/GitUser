using Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace BGLGITApi
{
    public class GitHubUserRepo : IGitHubUserRepo
    {
        private static string url = "https://api.github.com/users/";
        private HttpResponseMessage Res = null;
        private ILog log;

        public GitHubUserRepo(ILog log)
        {
            this.log = log;
        }

        public GitUser GetGitUserAsync(string name)
        {
            var gituser = new GitUser();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage Res = null;
                try
                {
                    if (!String.IsNullOrEmpty(name))
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                        Res = client.GetAsync($"{name}").Result;
                    }
                }
                catch (Exception ex)
                {
                    log.ExceptionLogger(ex.Message.ToString());
                }

                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;
                    gituser = JsonConvert.DeserializeObject<GitUser>(UserResponse);
                }
                else
                {
                    return null;
                }
            }
            return gituser;
        }

        public IEnumerable<GitUserRepoList> GetUserRepoListAsync(string name)
        {
            IList<GitUserRepoList> repoList = null;
            using (var client = new HttpClient())
            {
                Res = null;
                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");

                Res = client.GetAsync($"{name}/repos").Result;

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    repoList = JsonConvert.DeserializeObject<IList<GitUserRepoList>>(response);
                }
            }
            return repoList;
        }
    }
}