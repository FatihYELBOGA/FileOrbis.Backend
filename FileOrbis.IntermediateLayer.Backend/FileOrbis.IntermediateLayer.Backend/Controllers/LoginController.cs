using Microsoft.AspNetCore.Mvc;
using System.Text;
using FileOrbis.IntermediateLayer.Backend.Requests.Auth;

namespace FileOrbis.IntermediateLayer.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            using var httpClient = new HttpClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dev5.fileorbis.com/api/v2/account/login");
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("x-fo-client-key", "1044d5e7-3b77-40fa-9129-d55ee6d4b7c2");
                request.Headers.Add("Accept-Language", "en");

                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500);
            }
        }

    }
}
