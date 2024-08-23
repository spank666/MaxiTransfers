using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfVista.Models;
using WpfVista.Url;
using WpfVista.TokenHandler;
using System.Net.Http.Headers;

namespace WpfVista.RequestService
{
    public class RequestServices : IRequestServices
    {
        public async Task RequestMethod(string Uri, object s, HttpMethod verb, Action<ResponseModel> success, Action error)
        {
            StringBuilder url = new StringBuilder();
            url.Append(UrlResource.ApiBaseAddress);
            url.Append(UrlResource.ResourceManager.GetString(Uri));

            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(TokenJWT.token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenJWT.token);
                }

                switch (verb.Method)
                {
                    case "GET":
                        url.Append(s.ToString());
                        break;
                    case "DELETE":
                        url.Append(s.ToString());
                        break;
                }
                using (var request = new HttpRequestMessage(verb, url.ToString()))
                {
                    switch (verb.Method)
                    {
                        case "POST":
                            request.Content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
                            break;
                        case "PUT":
                            request.Content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
                            break;
                    }
                    //request.Content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
                    try
                    {
                        client.Timeout = UrlResource.TimeOut == "0" ? TimeSpan.FromMilliseconds(Timeout.Infinite) : TimeSpan.FromSeconds(Convert.ToDouble(UrlResource.TimeOut));

                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                        {
                            var stream = await response.Content.ReadAsStreamAsync();
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.OK:
                                    success(DeserializeJsonFromStream<ResponseModel>(stream));
                                    break;
                                case HttpStatusCode.Unauthorized:
                                    error();
                                    break;
                                case HttpStatusCode.Forbidden:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (SocketException ex)
                    {
                        error();
                    }
                    catch (TaskCanceledException ex)
                    {
                        var esto = ex.Message;
                        esto = ex.HelpLink;
                        esto = ex.Source;
                        var esto2 = ex.HResult;
                        var esto3 = ex.TargetSite;
                        var esto4 = ex.Data;
                        var esto5 = ex.Task;
                        error();
                        //-2146233029 ex.HResult timeout
                    }
                    catch (Exception ex)
                    {
                        var esto = ex.Message;
                        esto = ex.HelpLink;
                        esto = ex.Source;
                        var esto2 = ex.HResult;
                        var esto3 = ex.TargetSite;
                        var esto4 = ex.Data;

                        error();
                    }
                }
            }
        }

        public async Task RequestMethod(string Uri, HttpMethod verb, Action<ResponseModel> success, Action error)
        {
            StringBuilder url = new StringBuilder();
            url.Append(UrlResource.ApiBaseAddress);
            url.Append(UrlResource.ResourceManager.GetString(Uri));

            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(TokenJWT.token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenJWT.token);
                }

                using (var request = new HttpRequestMessage(verb, url.ToString()))
                {
                    //request.Content = new StringContent("", Encoding.UTF8, "application/json");
                    //request.Content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
                    switch (verb.Method)
                    {
                        case "Get":
                            break;
                        case "Post":
                            break;
                        case "Put":
                            break;
                        case "Delete":
                            break;
                    }
                    try
                    {
                        client.Timeout = UrlResource.TimeOut == "0" ? TimeSpan.FromMilliseconds(Timeout.Infinite) : TimeSpan.FromSeconds(Convert.ToDouble(UrlResource.TimeOut));

                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                        {
                            var stream = await response.Content.ReadAsStreamAsync();
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.OK:
                                    success(DeserializeJsonFromStream<ResponseModel>(stream));
                                    break;
                                case HttpStatusCode.Unauthorized:
                                    error();
                                    break;
                                case HttpStatusCode.Forbidden:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (SocketException ex)
                    {
                        error();
                    }
                    catch (TaskCanceledException ex)
                    {
                        var esto = ex.Message;
                        esto = ex.HelpLink;
                        esto = ex.Source;
                        var esto2 = ex.HResult;
                        var esto3 = ex.TargetSite;
                        var esto4 = ex.Data;
                        var esto5 = ex.Task;
                        error();
                        //-2146233029 ex.HResult timeout
                    }
                    catch (Exception ex)
                    {
                        var esto = ex.Message;
                        esto = ex.HelpLink;
                        esto = ex.Source;
                        var esto2 = ex.HResult;
                        var esto3 = ex.TargetSite;
                        var esto4 = ex.Data;

                        error();
                    }
                }
            }
        }

        private T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new Newtonsoft.Json.JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }
    }
}
