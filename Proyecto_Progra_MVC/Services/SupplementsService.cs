using Newtonsoft.Json;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Services.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Services
{
    public class SupplementsService : ISupplementsServices
    {
        public async Task<IEnumerable<Supplements>> getSupplementsAsync()
        {
            List<Supplements> supplements = new List<Supplements>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/Supplements/getSupplements"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        supplements = JsonConvert.DeserializeObject<List<Supplements>>(Response);
                    }
                }
            }
            return supplements;
        }

        public async Task<Supplements> getSupplementById(int id)
        {
            Supplements supplement = new Supplements();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44328/api/Supplements/getSupplement?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        supplement = JsonConvert.DeserializeObject<Supplements>(Response);
                    }
                }
            }
            return supplement;
        }

        public async Task<Supplements> addSupplementAsync(Supplements supplement)
        {
            Supplements supplementAdded = new Supplements();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(supplement), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44328/api/Supplements/addSupplement", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    supplementAdded = JsonConvert.DeserializeObject<Supplements>(apiResponse);
                }
            }
            return supplementAdded;
        }

        public async Task<Supplements> updateSupplementById(Supplements supplement)
        {
            Supplements supplementUpdated = new Supplements();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(supplement), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44328/api/Supplements/updateSupplement", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        supplementUpdated = JsonConvert.DeserializeObject<Supplements>(Response);
                    }
                }
            }
            return supplementUpdated;
        }

        public async Task<string> deleteSupplementById(int id)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:44328/api/Supplements/deleteSupplement?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return apiResponse;
        }
    }
}
