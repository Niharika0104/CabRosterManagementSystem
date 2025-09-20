namespace ZelisCabPlatform.Services
{
    public class EmployeeService
    {
        public HttpClient _httpClient { get; set; }

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
       
    }
}
