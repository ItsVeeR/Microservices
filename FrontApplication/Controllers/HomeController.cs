using FrontApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FrontApplication.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://localhost:44377/";
        string AuthenticationBaseurl = "https://localhost:44309/";

        string AuthToken = "";

        //Get All
        public async Task<ActionResult> Index()
        {
            List<Input> inputs = new List<Input>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource
                HttpResponseMessage Res = await client.GetAsync("api/StringReversal/GetAll");

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api 
                    inputs = JsonConvert.DeserializeObject<List<Input>>(response);
                }
                //returning list to view
                return View(inputs);
            }
        }

        public ActionResult Create()
        {
            var input = new Input { RequestedOn = DateTime.UtcNow };
            return View(input);
        }

        [HttpPost]
        public ActionResult Create(Input value)
        {
            var input = value;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource
                var res = client.PostAsJsonAsync<Input>("api/StringReversal", input);
                res.Wait();
                var result = res.Result;
                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = result.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Index");
                }

                else
                {
                    // Displaying error from api in case of failure
                    Response.Write(
                        @"<SCRIPT LANGUAGE=""JavaScript"">alert('" + "Error from post request - status code - " + result.StatusCode + "')</SCRIPT>");
                    return View(input);
                }

            }
        }

        // This method is to demonstrate calling api using Authentication
        public async Task<ActionResult> Edit(string Id)
        {
            if (string.IsNullOrWhiteSpace(AuthToken))
                await FetchNewTokenForAuthentication();



            return null;
        }


        // This part is used to get a Token from Authentication Service
        private async Task FetchNewTokenForAuthentication()
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(AuthenticationBaseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource
                var res = client.PostAsJsonAsync<User>("api/Authentication", new User { Username = "Admin", Password = "Pass2" });
                res.Wait();
                var result = res.Result;
                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = result.Content.ReadAsStringAsync().Result;
                    AuthToken = JsonConvert.DeserializeObject<string>(AuthToken);
                }
            }
        }
    }
}