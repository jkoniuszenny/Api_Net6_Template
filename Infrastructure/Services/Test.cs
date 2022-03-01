using Application.Interfaces.Services;

namespace Infrastructure.Services;

public class Test : ITestService
{
    public Test()
    {

    }

    public async Task<string> GetStringAsync()
    {
        return await Task.FromResult("Test");
    }
}