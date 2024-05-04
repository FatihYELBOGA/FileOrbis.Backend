using FileOrbis.IntermediateLayer.Backend.Requests.File_System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace FileOrbis.IntermediateLayer.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileSystemController : ControllerBase
    {
        [HttpPost("/file-system/navigate")]
        public async Task<IActionResult> GetDetails([FromBody] NavigateRequest navigateRequest)
        {
            using var httpClient = new HttpClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dev5.fileorbis.com/api/v2/filesystem/navigate");
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("x-fo-client-key", "1044d5e7-3b77-40fa-9129-d55ee6d4b7c2");
                request.Headers.Add("Accept-Language", "en");

                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(navigateRequest), Encoding.UTF8, "application/json");

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

        [HttpPost("/file-system/create/folder")]
        public async Task<IActionResult> CreateFolder([FromBody] CreateFolderRequest folderRequest)
        {
            using var httpClient = new HttpClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dev5.fileorbis.com/api/v2/filesystem/create/folder");
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("x-fo-client-key", "1044d5e7-3b77-40fa-9129-d55ee6d4b7c2");
                request.Headers.Add("Accept-Language", "en");

                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(folderRequest), Encoding.UTF8, "application/json");

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

        [HttpPost("/file-system/download/token/create")]
        public async Task<IActionResult> CreateToken([FromBody] CreateTokenRequest tokenRequest)
        {
            using var httpClient = new HttpClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dev5.fileorbis.com/api/v2/filesystem/download/token/create");
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("x-fo-client-key", "1044d5e7-3b77-40fa-9129-d55ee6d4b7c2");
                request.Headers.Add("Accept-Language", "en");

                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(tokenRequest), Encoding.UTF8, "application/json");

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

        [HttpGet("/file-system/download/token")]
        public async Task<Stream> GetFileStream([FromQuery] string token)
        {
            try
            {
                // Construct the URL with the token as a query parameter
                var url = $"https://dev5.fileorbis.com/api/v2/filesystem/download/token?token={token}";

                using var httpClient = new HttpClient();

                var authToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                httpClient.DefaultRequestHeaders.Add("x-fo-client-key", "1044d5e7-3b77-40fa-9129-d55ee6d4b7c2");
                httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));

                using HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var stream = await response.Content.ReadAsStreamAsync();

                        long contentLength = response.Content.Headers.ContentLength ?? 0;

                        Console.WriteLine(contentLength);

                        return stream;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        HttpContext.Response.StatusCode = (int)response.StatusCode;
                    }

                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }


    }

}
