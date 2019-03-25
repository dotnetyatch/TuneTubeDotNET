using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuneTube.Models;
using TuneTube.ViewModels;
using Newtonsoft.Json;

namespace TuneTube.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "App Description";

            return View();
        }

        public async Task<ActionResult> Search(string title)
        {
            if (ModelState.IsValid)
               {
                string BaseUrl = @"https://api.unsplash.com";
                List<QueryResult> searchres = new List<QueryResult>();
           // QueryResult rs = new QueryResult();
            //rs.url = "Apple";
            //searchres.Add(rs);
            using (var client = new HttpClient())
                    {
                        //Passing service base url  
                        client.BaseAddress = new Uri(BaseUrl);

                        client.DefaultRequestHeaders.Clear();
                        // Define headers
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", "3da40752254289e5f74de63a235f24322a8a0b92a58a9c4e96b0348faa56a14d");

                    //Initiate request  
                    HttpResponseMessage Res = await client.GetAsync("/search/photos" +"?query="+title);
                       ///Checking the response is successful or not
                       if (title !=null && Res.IsSuccessStatusCode)
                       {
                           //Storing the response details recieved from web api   
                           var qResult = Res.Content.ReadAsStringAsync().Result;

                ////////Deserializing the response recieved from web api using C# dynamic object 
                    dynamic dm = JsonConvert.DeserializeObject(qResult);
                        //string url = dm["results"][0].urls.small;
                        try
                        {
                            for (int i = 0; i < 10; i++)
                            {

                                QueryResult imgobj = new QueryResult
                                {
                                    url = dm["results"][i].urls.small
                                };
                                searchres.Add(imgobj);
                            }
                            //rs.url = url;
                            //searchres.Add(rs);
                            return View(searchres);
                        }
                        catch (Exception)
                        {
                            ModelState.Clear();
                            //ViewData["Message"] = "No result for search item:" +" " + title;
                            return View();
                        }
                }

                //  
                ModelState.Clear();
                    return View();
                
                        
                    }
               
            }

            else {
                ModelState.Clear();
                return View();
            }
        }
            
 

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
