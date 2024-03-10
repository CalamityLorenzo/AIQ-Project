using appDefinitions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer
{
    public class DbService : IDbService
    {
        private AppDbContext ctx;

        public DbService(AppDbContext ctx) { this.ctx = ctx; }
        public IDbUsers Users => new UserService(ctx);
    }
}
