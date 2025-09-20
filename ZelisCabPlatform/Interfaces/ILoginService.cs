using ZelisCabPlatform.Models;

namespace ZelisCabPlatform.Interfaces
{
    public interface ILoginService
    {
        public Task<bool> Login(LoginRequest request);
        public  HttpClient _httpClient { get; set; }

        public Employee employee { get;  set; }
    }
}
