using Domain.External.EmployeesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Providers.Api;

public interface IEmployeesProvider : IProvider
{
    Task<LoginDepartments> GetEmployeePermissionAsync(int employeenr, DateTime monthId, int typeId);
}
