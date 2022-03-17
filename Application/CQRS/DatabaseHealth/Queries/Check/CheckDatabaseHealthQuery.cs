using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.DatabaseHealth.Queries.Check;

public class CheckDatabaseHealthQuery : IRequest<string>
{
}

