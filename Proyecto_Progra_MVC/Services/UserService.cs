using Newtonsoft.Json;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Services.IServices;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Services
{
    public class UserService : IUserServices
    {
        public async Task<IEnumerable<User>> getUsersAsync()
        {
            List<User> users = new List<User>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/User/getUsers"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<User>>(Response);
                    }

                }
            }
            return users;
        }

        public async Task<User> getUserById(string id)
        {
            User users = new User();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44328/api/User/getUser?id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<User>(Response);
                    }
                }
            }
            return users;
        }

        public async Task<User> updateUserById(User user)
        {
            User users = new User();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44328/api/User/updateUser", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<User>(Response);
                    }
                }
            }
            return users;
        }

        public async Task<string> deleteUserById(string id)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:44328/api/User/deleteUser?id=" + id))
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
