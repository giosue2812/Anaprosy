using Anaprosy.Client.Models;
using Anaprosy.Service.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Anaprosy.Service.Client
{
    public class ClientProductService : IService<ProductVM>
    {
        private readonly HttpClient HttpClient;
        private readonly string ServerUrl;
        public ClientProductService(IConfiguration Configuration)
        {
            this.ServerUrl = Configuration.GetSection("AppSettings").GetSection("ServerUrl").Value;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(this.ServerUrl);
        }

        public async Task<IEnumerable<ProductVM>> Gets(string include = null)
        {
            try
            {
                QueryBuilder Query = new QueryBuilder();
                if (!string.IsNullOrWhiteSpace(include))
                    Query.Add("include", include);

                HttpResponseMessage response = await HttpClient.GetAsync($"api/product{Query.ToString()}");
                string JsonResult = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ProductVM>>(JsonResult);
                else
                    throw new Exception(response.ReasonPhrase + " " + JsonResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<ProductVM>> GetsByValue(string value)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(value))
                    return await Gets();
                else
                {
                    HttpResponseMessage response = await HttpClient.GetAsync($"api/product/GetsByValue/{value}");
                    string JsonResult = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ProductVM>>(JsonResult);
                    else
                        throw new Exception(response.ReasonPhrase + " " + JsonResult);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProductVM> Get(Guid ID, string include = null)
        {
            try
            {
                QueryBuilder Query = new QueryBuilder();
                if (!string.IsNullOrWhiteSpace(include))
                    Query.Add("include", include);

                var response = await HttpClient.GetAsync($"api/product/{ID}{Query.ToString()}");
                var JsonResult = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ProductVM>(JsonResult);
                else
                    throw new Exception(response.ReasonPhrase + " " + JsonResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<ProductVM> Add(ProductVM TItem)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVM> Put(ProductVM TItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid ID)
        {
            throw new NotImplementedException();
        }
    }
}
