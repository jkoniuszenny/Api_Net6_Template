using Application.Interfaces.Repositories;
using Infrastructure.Database;
using Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HealthyRepository : IHealthyRepository
    {
        private readonly DatabaseContext _databaseContext;

        public HealthyRepository(
            DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<string> GetHealthyAsync()
        {
            return
                await _databaseContext.Database.CanConnectAsync()
                ? "Healthy"
                : "Unhealthy";
        }
    }
}
