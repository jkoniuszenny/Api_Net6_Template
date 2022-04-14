using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Settings;

public class TokenSettings
{
    public string SecretKey { get; set; }
    public string RefreshKey { get; set; }
    public string AudienceKey { get; set; }
    public string IssuerKey { get; set; }
}
