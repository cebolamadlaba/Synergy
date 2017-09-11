using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    public interface IAdminRepository
    {
        int CreateUser(User user);
        IEnumerable<User> GetUsers();
        int DeleteUser(string aNumber);
        User GetUser(int id);
        void UpdateUser(User user);
    }
}
