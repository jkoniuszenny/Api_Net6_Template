using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GlobalResponse;

public interface IResponse<T>
    where T : class
{
    bool Succeded { get; set; }
    Payload<T> Payload { get; set; }
    Error? Error { get; set; }
}

