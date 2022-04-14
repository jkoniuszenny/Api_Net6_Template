using Domain.External.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Providers;

public interface ILdapProvider: IProvider
{
    Task<UserAuthenticate> LoginToLdap(string userName, string password, bool checkDomain);
}
