using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    public interface IAdminRepository
    {
        int CreateUser(UserModel user);
        IEnumerable<UserModel> GetUsers();
        int DeleteUser(string aNumber);
        UserModel GetUser(int id);
        void UpdateUser(UserModel user);
    }
}
