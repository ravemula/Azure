using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DeamonApp.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;


namespace DeamonApp.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            string emailAddress = id;
            string result;
            try
            {
                ConfidentialClientApplication cl = new ConfidentialClientApplication(
                AppConstants.ClientId,
                String.Format(AppConstants.GraphV2Uri, AppConstants.TenentName),
                AppConstants.RedirectUrl,
                new Microsoft.Identity.Client.ClientCredential(AppConstants.ClientSecret),
                new TokenCache(),
                new TokenCache());
                Microsoft.Identity.Client.AuthenticationResult authResult = cl.AcquireTokenForClientAsync(new string[] { AppConstants.ApplicationDefaultScope }).Result;
                string bearerToken = AppConstants.Bearer + " " + authResult.AccessToken.ToString();
                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                var userUpn = emailAddress.Replace('@', '_') + AppConstants.ExtensionAttachedForAd + AppConstants.TenentName;
                var uri = string.Format(AppConstants.GraphApiV1Url, HttpUtility.UrlEncode(userUpn));
                client.DefaultRequestHeaders.Add(AppConstants.AuthorizatioHeader, bearerToken);
                var response = client.GetAsync(uri).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
                {
                    var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseString)["value"];
                    return this.Json(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                return this.Json(new object());
            }
            return this.Json(new object());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
