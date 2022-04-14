using Domain.External.OriginApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Providers.Api;

public interface IOriginProvider : IProvider
{
    Task<EmployeeInfo> GetEmployeeInfoForEmployeeNr(int employeeNr);
    Task<HierarchyTree> GetHrSupervisorHierarchyByEmployeeNr(int employeeNr);
}
