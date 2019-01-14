using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace BGLGITApi
{
    public class BglGitUserController : ApiController
    {        
        private IGitHubUserRepo _repo;  //= new GitHubUserRepo();
        private ILog _log;
                
        public BglGitUserController(IGitHubUserRepo repo, ILog log)
        {
            _repo = repo;
            _log = log;
        }

        /// <summary>
        /// Get Git User by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Github User Details/Information</returns>
        /// <response code="200">Returns Github user information</response>
        /// <response code="404">Github User not found</response>
        [HttpGet]
        [Route("api/v1/GetUser/{name}")]
        [ResponseType(typeof(GitUser))]
        public IHttpActionResult GetUserByName(string name)
        {
            GitUser user = null;
            IEnumerable<GitUserRepoList> userRepo = null;
            try
            {
                user = _repo.GetGitUserAsync(name);

                if (user == null)
                {
                    return NotFound();
                }

                userRepo = _repo.GetUserRepoListAsync(name);

                user.UserRepoList = userRepo
                                    .OrderByDescending(s => s.stargazers_count)
                                    .Take(5)
                                    .ToList();
            }
            catch(Exception ex)
            {
                _log.ExceptionLogger(ex.Message.ToString());
                return BadRequest();
            }
            return Ok(user);

        }
    }
}

