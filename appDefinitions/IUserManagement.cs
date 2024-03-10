using appDefinitions.Models;

namespace appDefinitions
{
  public interface IUserManagement
    {
        Task<UserInfo> CreateNewUser(BasicNewUserInfo userInfo);
        Task<IEnumerable<UserInfo>> AllUsers();
    }
}
