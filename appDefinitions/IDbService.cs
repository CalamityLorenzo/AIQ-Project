using appDefinitions.Models;

namespace appDefinitions
{
    /// <summary>
    ///  The layou for the database service.
    /// </summary>

    public interface IDbService
    {
        IDbUsers Users { get; }
    }


    /// <summary>
    /// User management tasks
    /// </summary>
    public interface IDbUsers
    {
        Task AddNewUser(UserInfo user);
        Task <UserInfo> GetUser(string Id);
        Task<IEnumerable<UserInfo>> AllUsers();

    }


}
