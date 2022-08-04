namespace Application.Interfaces.Providers;

public interface IUserProvider : IProvider
{
    string UserName { get; }
}

