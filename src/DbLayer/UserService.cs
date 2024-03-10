using appDefinitions;
using appDefinitions.Models;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace DbLayer
{
    internal class UserService : IDbUsers
    {
        private AppDbContext ctx;

        public UserService(AppDbContext ctx)
        {
            this.ctx = ctx;
            this.ctx.SaveChangesFailed += Ctx_SaveChangesFailed;
            this.ctx.SavingChanges += Ctx_SavingChanges;
        }

        private void Ctx_SavingChanges(object? sender, Microsoft.EntityFrameworkCore.SavingChangesEventArgs e)
        {
            
        }

        private void Ctx_SaveChangesFailed(object? sender, Microsoft.EntityFrameworkCore.SaveChangesFailedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public async Task<UserInfo> GetUser(string Id)
        {
            var user = await ctx.Users.FindAsync(Id);
            return new UserInfo(
                id: user.Id,
                FirstName : user.FirstName,
                LastName : user.LastName,
                Mobile : user.Mobile,
                Email : user.Email,
                AddressLine1 : user.AddressLine1,
                AddressLine2 : user.AddressLine2,
                City : user.City,
                Postcode : user.Postcode ??"",
                State : user.State,
                Country : user.Country,
                DateOfBirth : user.DateOfBirth,
                emailVerified : user.emailVerified,
                mobileNumberVerified : user.mobileNumberVerified
            );
        }

        public async Task AddNewUser(UserInfo user) 
        {
            // Takes the returned Weavr user and adds it to the db.



            DbUserInfo userInfo = new DbUserInfo
            {
                Id= user.id,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email= user.Email,
                Mobile= user.Mobile,
                emailVerified=user.emailVerified,
                mobileNumberVerified = user.mobileNumberVerified,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                Postcode= user.Postcode,    
                City = user.City,   
                State = user.State,
                DateOfBirth = user.DateOfBirth.ToUniversalTime(),
                Country = user.Country,
            };

            ctx.Users.Add(userInfo);
            await ctx.SaveChangesAsync();

        }


    }
}