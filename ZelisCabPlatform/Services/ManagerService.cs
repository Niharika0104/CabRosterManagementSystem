using System.Net.Http.Json;
using ZelisCabPlatform.Models;
using ZelisCabPortalCoreLayer.Models;

namespace ZelisCabPlatform.Services
{
    public class ManagerService
    {
        public HttpClient _httpClient { get; set; }

        public ManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Employee>> GetAllEmloyees(int id)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<int>("Manager/GetAllEmloyees", id);
                List<Employee> list = await response.Content.ReadFromJsonAsync<List<Employee>>();
                return list;
            }
            catch (Exception) { throw; }
           
        }
        public async Task<bool> ProcessRequest(ProcessDTO process)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<ProcessDTO>("Manager/process", process);
                bool result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            catch (Exception) { throw; }

        }
        public async Task<List<Employee>> PendingRequests(int id)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<int>("Manager/getPendingApprovals", id);
                List<Employee> result = await response.Content.ReadFromJsonAsync<List<Employee>>();
                return result;
            }
            catch (Exception) { throw; }

        }
    }
}
