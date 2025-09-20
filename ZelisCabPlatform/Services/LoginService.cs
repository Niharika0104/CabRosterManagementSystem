using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.Http.Json;
using ZelisCabPlatform.Interfaces;
using ZelisCabPlatform.Models;
using ZelisCabPlatform.Pages;
 

namespace ZelisCabPlatform.Services
{
    public class LoginService
    {
        private readonly ISessionStorageService _session;

        public  HttpClient _httpClient { get; set; }

        public Employee employee { get;  set; }
        public LoginService(HttpClient httpClient,ISessionStorageService session)
        {
            _httpClient = httpClient;
            _session = session;
        }
       
        
        public async Task< Employee> Login(LoginRequest request)
        {
            try
            {
                var res = await _httpClient.PostAsJsonAsync<LoginRequest>("Auth/login", request);
                if (res.IsSuccessStatusCode)
                {
                    Employee emp = await res.Content.ReadFromJsonAsync<Employee>();

                    if (emp != null)
                    {
                        employee = emp;
                        return employee;
                    }
                }
                else return null;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        public async  Task UpdateEmployee(int id)
        {
            string apiUrl = $"/Employee/{id}";

             employee = await _httpClient.GetFromJsonAsync<Employee>(apiUrl);
           
        }
        public async Task SetEmployee()
        {


            var emp = await _session.GetItemAsync<Employee>("employee");
            employee = emp;
           
            // employee = await response.Content.ReadFromJsonAsync<Employee>();
        }
    }
}
