using appDefinitions;
using appDefinitions.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace WeavrAccounts
{
    public class UserAccountManagement : IUserManagement
    {
        private HttpClient client;
        private WeavrDetails options;

        public UserAccountManagement(IHttpClientFactory clientFactory, IOptions<WeavrDetails> opts)
        {
            this.client = clientFactory.CreateClient();
            this.options = opts.Value;
            client.BaseAddress = new Uri(options.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("api-key", new string[] { this.options.ApiKey });
        }

        public async Task<IEnumerable<UserInfo>> AllUsers()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "/multi/users?offset=0&limit=1&active=true&email=user%40example.com&tag=string");
            request.Headers.Add("api-key", new string[] { this.options.ApiKey });

            var allUsers = await client.SendAsync(request);

            return null;
        }

        // Sends the new user request to Weavr
        public async Task<UserInfo> CreateNewUser(BasicNewUserInfo userInfo)
        {
            var dateOfBirth = DateTime.Parse(userInfo.DateOfBirth);

            var response = await PostRequest(HttpMethod.Post, "/multi/consumers", new
            {
                profileId = options.ProfileId,
                ipAddress = userInfo.UserIp,
                acceptedTerms = true,
                baseCurrency = "GBP",
                rootUser = new
                {
                    name = userInfo.FirstName,
                    surname = userInfo.LastName,
                    email = userInfo.Email,
                    mobile = new
                    {
                        countryCode = "+44",
                        number = userInfo.Mobile
                    },
                    dateOfBirth = new {
                        year= dateOfBirth.Year,
                       month= dateOfBirth.Month,
                         day= dateOfBirth.Day
                    },
                    address = new
                    {
                        addressLine1 = userInfo.AddressLine1,
                        addressLine2 = userInfo.AddressLine2,
                        city = userInfo.City,
                        postcode = userInfo.Postcode,
                        state = userInfo.State,
                        country = userInfo.Country,
                    }
                }
            });

            var responseBody = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(response.Content.ReadAsStringAsync().Result);

            if (!response.IsSuccessStatusCode)
            {
                if(response.StatusCode == (HttpStatusCode)409)
                {
                    throw new Exception("User already exists");
                }
               ;
            }


            var rootUser = responseBody["rootUser"];
            return new UserInfo(rootUser["id"]["id"].ToString(),
                    FirstName: rootUser["name"].ToString(),
                    LastName: rootUser["surname"].ToString(),
                    Mobile: $"{rootUser["mobile"]!["countryCode"]!.ToString()} {rootUser["mobile"]!["number"]!.ToString()}",
                    Email: rootUser["email"].ToString(),
                    AddressLine1: rootUser["address"]["addressLine1"].ToString(),
                    AddressLine2: rootUser["address"]["addressLine2"].ToString(),
                    City: rootUser["address"]["city"].ToString(),
                    Postcode: rootUser["address"]["postcode"]?.ToString(),
                    State: rootUser["address"]["state"].ToString(),
                    Country: rootUser["address"]["country"].ToString(),
                    // The universal time forces UTC. This is equired by the PostGreSql provider
                    DateOfBirth: DateTime.Parse($"{rootUser["dateOfBirth"]["day"]}/{rootUser["dateOfBirth"]["month"]}/{rootUser["dateOfBirth"]["year"]}").ToUniversalTime(),
                    emailVerified: (rootUser["emailVerified"].ToString() == "true"),
                    mobileNumberVerified: (rootUser["mobileNumberVerified"].ToString() == "true")

                );
        }


        private Task<HttpResponseMessage> PostRequest(HttpMethod method, string relativeUrl, object bodyObject)
        {
            var bodyText = System.Text.Json.JsonSerializer.Serialize(bodyObject);

            var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl);
            request.Headers.Add("api-key", new string[] { this.options.ApiKey });
            request.Content = new StringContent(bodyText, encoding: Encoding.UTF8,
            "application/json");

            return client.SendAsync(request);
        }

    }
}
