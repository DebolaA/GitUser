using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGL.Models
{
    public class GitUserRepoDetails
    {
        public string id { get; set; }
        public string name { get; set; }
        public int stargazers_count { get; set; }
    }
}