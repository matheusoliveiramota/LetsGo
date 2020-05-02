using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace LetsGo.Web.UI.Services
{
    public class ServiceUI
    {
        private readonly IConfiguration _configuration;
        public ServiceUI(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected string UrlApi { get { return _configuration.GetSection("UrlApi").Value; } }
        protected dynamic ChamadaApiPost(string objetoSerializado, string strApiAddress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlApi);

                    var content = new StringContent(objetoSerializado, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = client.PostAsync(strApiAddress, content).GetAwaiter().GetResult();

                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent respostahttp = result.Content)
                        {
                            return respostahttp.ReadAsStringAsync().GetAwaiter().GetResult();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected dynamic ChamadaApiPut(string objetoSerializado, string strApiAddress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlApi);

                    var content = new StringContent(objetoSerializado, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = client.PutAsync(strApiAddress, content).GetAwaiter().GetResult();

                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent respostahttp = result.Content)
                        {
                            return JsonConvert.DeserializeObject(respostahttp.ReadAsStringAsync().GetAwaiter().GetResult());
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected dynamic ChamadaApiGET(string strApiAddress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlApi);

                    HttpResponseMessage result = client.GetAsync(strApiAddress).GetAwaiter().GetResult();

                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent respostahttp = result.Content)
                        {
                            return respostahttp.ReadAsStringAsync().GetAwaiter().GetResult();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
