using appDefinitions;
using appDefinitions.Models;
using DbLayer.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DbLayer
{
    internal class UserService : IDbUsers
    {
        private readonly ILogger<DbService> logger;
        private AppDbContext ctx;

        public UserService(ILogger<DbService> logger, AppDbContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.ctx.SaveChangesFailed += Ctx_SaveChangesFailed;
            this.ctx.SavingChanges += Ctx_SavingChanges;
        }

        private void Ctx_SavingChanges(object? sender, Microsoft.EntityFrameworkCore.SavingChangesEventArgs e)
        {
            logger.LogInformation("Saving changes");
        }

        private void Ctx_SaveChangesFailed(object? sender, Microsoft.EntityFrameworkCore.SaveChangesFailedEventArgs e)
        {
            logger.LogCritical($"Save failed: {e}");
        }

        public async Task<UserInfo> GetUser(string Id)
        {
            var dbUser = await ctx.Users.FindAsync(Id);
            if (dbUser == null) throw new ArgumentNullException($"Cannot find user with id :{Id}");
            return DbToUser(dbUser);
        }

        public async Task AddNewUser(UserInfo user)
        {
            // Takes the returned Weavr user and adds it to the db.
            DbUserInfo userInfo = new DbUserInfo
            {
                Id = user.id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                emailVerified = user.emailVerified,
                mobileNumberVerified = user.mobileNumberVerified,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                Postcode = user.Postcode,
                City = user.City,
                State = user.State,
                DateOfBirth = user.DateOfBirth.ToUniversalTime(),
                Country = user.Country,
            };

            ctx.Users.Add(userInfo);
            await ctx.SaveChangesAsync();

        }

        public async Task<IEnumerable<UserInfo>> AllUsers()
        {
            var users = await ctx.Users.ToListAsync();
            return users.Select(dbUser => DbToUser(dbUser));
        }
        // Helper
        private UserInfo DbToUser(DbUserInfo dbUser)
        {
            return new UserInfo(
                     id: dbUser.Id,
                     FirstName: dbUser.FirstName,
                     LastName: dbUser.LastName,
                     Mobile: dbUser.Mobile,
                     Email: dbUser.Email,
                     AddressLine1: dbUser.AddressLine1,
                     AddressLine2: dbUser.AddressLine2,
                     City: dbUser.City,
                     Postcode: dbUser.Postcode ?? "",
                     State: dbUser.State,
                     Country: dbUser.Country,
                     DateOfBirth: dbUser.DateOfBirth,
                     emailVerified: dbUser.emailVerified,
                     mobileNumberVerified: dbUser.mobileNumberVerified);
        }
    }
};