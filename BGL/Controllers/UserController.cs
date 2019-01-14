using BGL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BGL
{
    public class UserController : Controller
    {
        HttpClient client = new HttpClient();
        string uri = "http://localhost:55409/api/v1/getUser/";

        // GET: User
        public ActionResult Index(string name = "")
        {
            using (var client = new HttpClient())
            {
                if (string.IsNullOrEmpty(name))
                {
                    return View();
                }

                client.BaseAddress = new Uri(uri);                 
                
                var responseTask = client.GetAsync($"{name}");
                responseTask.Wait();

                var result = responseTask.Result;
                var user = new User();

                //Checking the response is successful or not which is sent using HttpClient  
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(readTask);
                }
                else
                {
                    user = null;
                    ModelState.AddModelError(string.Empty, "Server error. Unable to retrieve user details.");

                } 
                return View(user);
            }
        }        
    }
}