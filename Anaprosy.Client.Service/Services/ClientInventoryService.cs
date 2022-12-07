using Anaprosy.Client.Models;
using Anaprosy.Service.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Anaprosy.Service.Client
{
    public class ClientInventoryService : IService<InventoryVM>
    {
        private readonly HttpClient HttpClient;
        private readonly string ServerUrl;
        public ClientInventoryService(IConfiguration Configuration)
        {
            this.ServerUrl = Configuration.GetSection("AppSettings").GetSection("ServerUrl").Value;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(this.ServerUrl);
        }

       public async Task<IEnumerable<InventoryVM>> Gets(string include = null)
       {
            try
            {
                //If there is include add QueryBuilder to add the include
                QueryBuilder Query = new QueryBuilder();
                if (!string.IsNullOrWhiteSpace(include))
                    Query.Add("include", include);

                HttpResponseMessage HttpResponseMessage = await HttpClient.GetAsync($"api/inventory{Query.ToString()}");
                string JsonResult = await HttpResponseMessage.Content.ReadAsStringAsync();

                if (HttpResponseMessage.IsSuccessStatusCode)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<InventoryVM>>(JsonResult);
                else
                    throw new Exception(HttpResponseMessage.ReasonPhrase + " " + JsonResult);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
       }

        public async Task<InventoryVM> Get(Guid ID,string include = null)
        {
            try
            {
                QueryBuilder Query = new QueryBuilder();
                if (!string.IsNullOrWhiteSpace(include))
                    Query.Add("include", include);

                HttpResponseMessage HttpResponseMessage = await HttpClient.GetAsync($"api/inventory/{ID}{Query.ToString()}");
                string JsonResult = await HttpResponseMessage.Content.ReadAsStringAsync();

                if (HttpResponseMessage.IsSuccessStatusCode)
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<InventoryVM>(JsonResult);
                else
                    throw new Exception(HttpResponseMessage.ReasonPhrase + " " + JsonResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<InventoryVM> Add(InventoryVM Inventory)
        {
            try
            {
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Inventory), Encoding.UTF8, "application/json");

                HttpResponseMessage HttpResponseMessage = await HttpClient.PostAsync("api/inventory", stringContent);
                string JsonResult = await HttpResponseMessage.Content.ReadAsStringAsync();

                InventoryVM SavedInventory = Newtonsoft.Json.JsonConvert.DeserializeObject<InventoryVM>(JsonResult);

                if (HttpResponseMessage.IsSuccessStatusCode)
                    return SavedInventory;
                else
                    throw new Exception(HttpResponseMessage.ReasonPhrase + " " + JsonResult);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<InventoryVM> Put(InventoryVM Inventory)
        {
            try
            {
                StringContent stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Inventory), Encoding.UTF8, "application/json");

                HttpResponseMessage HttpResponseMessage = await HttpClient.PutAsync($"api/inventory", stringContent);
                string JsonResult = await HttpResponseMessage.Content.ReadAsStringAsync();

                InventoryVM SavedInventory = Newtonsoft.Json.JsonConvert.DeserializeObject<InventoryVM>(JsonResult);

                if (HttpResponseMessage.IsSuccessStatusCode)
                    return SavedInventory;
                else
                    throw new Exception(HttpResponseMessage.ReasonPhrase + " " + JsonResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(Guid ID)
        {
            try
            {

                HttpResponseMessage HttpResponseMessage = await HttpClient.DeleteAsync($"api/inventory/{ID}");
                string JsonResult = await HttpResponseMessage.Content.ReadAsStringAsync();

                bool IsDeleted = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(JsonResult);

                if (HttpResponseMessage.IsSuccessStatusCode)
                    return true;
                else
                    throw new Exception(HttpResponseMessage.ReasonPhrase + " " + JsonResult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
