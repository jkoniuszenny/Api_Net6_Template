using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Providers.Api;

public interface ITetaWebProvider : IProvider
{
    Task<int> GetTetaEmpId(string employeeNr);
}
