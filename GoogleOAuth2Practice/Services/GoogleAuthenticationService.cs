using GoogleOAuth2Practice.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace GoogleOAuth2Practice.Services
{
    public interface IOAuth2Service
    {

        string GetOAuth2Url();
        Task<GoogleToken> GetAccessTokenAsync(string code);
        Task<UserInfoResponse> GetUserInfo(string accessToken);
    }


    public class GoogleAuthenticationService : IOAuth2Service
    {
        private readonly IConfiguration _configuration;

        private readonly string _client_id;
        private readonly string _client_secret;


        private const string OAuthUrl = "https://accounts.google.com/o/oauth2/auth?";

        private const string RedirectURL = "http://localhost:8080/api/user/token";

        private const string Scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";

        private const string TokenUrl = "https://accounts.google.com/o/oauth2/token";

        private const string ApiUrl = "https://www.googleapis.com/oauth2/v3/userinfo";


        public GoogleAuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;

            _client_id = configuration["Authentication:Google:ClientId"];
            _client_secret = configuration["Authentication:Google:ClientSecret"];
        }

        public string GetOAuth2Url() => new StringBuilder(OAuthUrl).Append($"scope={Scope}&")
                                                                   .Append("access_type=offline&")
                                                                   .Append("include_granted_scopes=true&")
                                                                   .Append("response_type=code&")
                                                                   .Append("state=state_parameter_passthrough_value&")
                                                                   .Append($"redirect_uri={RedirectURL}&")
                                                                   .Append($"client_id={_client_id}")
                                                                   .ToString();
                                                                           

        public async Task<GoogleToken> GetAccessTokenAsync(string code)
        {
            var postData = new { code = code, client_id = _client_id, client_secret = _client_secret, redirect_uri = RedirectURL, grant_type = "authorization_code" };

            using (HttpClient httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

                using (var res = await httpClient.PostAsync(TokenUrl, stringContent))
                {
                    return JsonConvert.DeserializeObject<GoogleToken>(await res.Content.ReadAsStringAsync()!)!;
                }
            }
        }

        public async Task<UserInfoResponse> GetUserInfo(string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var res = await httpClient.PostAsync(ApiUrl,null))
                {
                    return JsonConvert.DeserializeObject<UserInfoResponse>(await res.Content.ReadAsStringAsync()!)!;
                }
            }
        }
    }
}
