namespace Application.Interfaces.Services
{
    public interface ITestService : IService
    {
        Task<string> GetStringAsync();
    }
}
