using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGLGITApi
{
    public class GitUser
    {
        public string login { get; set; }
        public string id { get; set; }
        public string location { get; set; }
        public string avatar_url { get; set; }
        public string repos_url { get; set; }
        public IEnumerable<GitUserRepoList> UserRepoList { get; set; }
    }
}