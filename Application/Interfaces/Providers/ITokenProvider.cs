using Domain.External.EmployeesApi;
using Domain.External.Ldap;
using Domain.External.OriginApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Providers;

public interface ITokenProvider : IProvider
{
    Task<(string Token, string RefreshToken)> GetTokenAndRefreshTokenAsync(int empId, UserAuthenticate userAuthenticate, LoginDepartments loginDepartments, HierarchyTree hrSupervisorInfo);
    Task<string> GetUserNameFromTokenAsync(string refreshToken);
    Task<bool> IsExpiredRefreshToken(string refreshToken);
}
