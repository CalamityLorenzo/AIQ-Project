using appDefinitions.Models;

namespace appDefinitions
{

    // Interface for the main user service,
    // Effectively interacting with Weavr.
  public interface IUserManagement
    {
        Task<UserInfo> CreateNewUser(BasicNewUserInfo userInfo);
        Task<IEnumerable<UserInfo>> AllUsers();
    }
}
