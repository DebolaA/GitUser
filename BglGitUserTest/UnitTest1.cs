using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BGLGITApi;
using Logger;
using Newtonsoft.Json;
using System.Web.Mvc;
using Moq;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Net;

namespace BglGitUserTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow("robconery")]
        public void TestMethod1(string name)
        {
            //            
            List<GitUserRepoList> repoList = new List<GitUserRepoList>()
            {
                new GitUserRepoList
                    {
                        id = "1",
                        name = "repo1",
                        stargazers_count = 20
                    },
                    new GitUserRepoList
                    {
                        id = "2",
                        name = "repo2",
                        stargazers_count = 22
                    },
                    new GitUserRepoList
                    {
                        id = "3",
                        name = "repo3",
                        stargazers_count = 24
                    },
                    new GitUserRepoList
                    {
                        id = "4",
                        name = "repo4",
                        stargazers_count = 26
                    },
                    new GitUserRepoList
                    {
                        id = "5",
                        name = "repo5",
                        stargazers_count = 28
                    },
                    new GitUserRepoList
                    {
                        id = "6",
                        name = "repo6",
                        stargazers_count = 30
                    }   
            };
            GitUser user = new GitUser
            {
                id = "78586",
                login = "robconery",
                location = "Honolulu, HI",
                avatar_url = "https://avatars0.githubusercontent.com/u/78586?v=4",
                repos_url = "https://api.github.com/users/robconery/repos",
                UserRepoList = null
            };
            
            Mock<IGitHubUserRepo> _mockGitUserRepo = new Mock<IGitHubUserRepo>();
            _mockGitUserRepo
                 .Setup(x => x.GetGitUserAsync(It.IsAny<string>()))
                 .Returns(user);

            _mockGitUserRepo
                 .Setup(x => x.GetUserRepoListAsync(It.IsAny<string>()))
                 .Returns(repoList);

            Mock<ILog> _mocklog = new Mock<ILog>();
            var controller = new BGLGITApi.BglGitUserController(_mockGitUserRepo.Object, _mocklog.Object);
            
            //
            var result = controller.GetUserByName(name);
            var type = result.GetType();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, type);
        }


        [TestMethod]
        [DataRow("robconery")]
        public void TestMethod2(string name)
        {
            //            
            List<GitUserRepoList> repoList = null;

            GitUser user = null;
            

            Mock<IGitHubUserRepo> _mockGitUserRepo = new Mock<IGitHubUserRepo>();
            _mockGitUserRepo
                 .Setup(x => x.GetGitUserAsync(It.IsAny<string>()))
                 .Returns(user);

            _mockGitUserRepo
                 .Setup(x => x.GetUserRepoListAsync(It.IsAny<string>()))
                 .Returns(repoList);

            Mock<ILog> _mocklog = new Mock<ILog>();
            var controller = new BGLGITApi.BglGitUserController(_mockGitUserRepo.Object, _mocklog.Object);

            //
            var badresult = controller.GetUserByName(name);
            var type = badresult.GetType();
            Assert.IsInstanceOfType(badresult, typeof(NotFoundResult));
        }
    }

    
}
