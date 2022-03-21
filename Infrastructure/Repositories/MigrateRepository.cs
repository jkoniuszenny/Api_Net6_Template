using Application.Interfaces.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class MigrateRepository : IMigrateRepository
{
    private readonly DatabaseContext _context;

    public MigrateRepository(
        DatabaseContext context)
    {
        _context = context;
    }

    public async Task MigrateExecute()
    {
        await _context.Database.MigrateAsync();
    }
}

