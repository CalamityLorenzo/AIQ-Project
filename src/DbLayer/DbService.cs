using appDefinitions;
using appDefinitions.Models;
using Microsoft.Extensions.Logging;

namespace DbLayer
{
    // IMplementation of db Service.
    public class DbService : IDbService
    {
        private readonly ILogger<DbService> logger;
        private AppDbContext ctx;

        public DbService(ILogger<DbService> logger, AppDbContext ctx) { this.logger = logger; this.ctx = ctx; }
        public IDbUsers Users => new UserService(logger, ctx);

  
    }
}
