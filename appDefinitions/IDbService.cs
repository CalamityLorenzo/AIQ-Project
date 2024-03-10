using appDefinitions.Models;

namespace appDefinitions
{
    public interface IDbService
    {
        IDbUsers Users { get; }
    }

    public interface IDbUsers
    {
        Task AddNewUser(UserInfo user);
        Task <UserInfo> GetUser(string Id);
    }

    
}
