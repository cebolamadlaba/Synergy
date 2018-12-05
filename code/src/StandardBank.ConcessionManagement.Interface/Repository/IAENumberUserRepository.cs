using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// AENumberUser repository interface
    /// </summary>
    public interface IAENumberUserRepository
    {
        AENumberUser ReadById(int id);
    }
}