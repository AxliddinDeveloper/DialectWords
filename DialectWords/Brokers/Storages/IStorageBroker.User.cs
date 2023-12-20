//=============
// For Client
//=============

using DialectWords.Models.Foundations.Users;

namespace DialectWords.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<User> InsertUserAsync(User word);
        IQueryable<User> SelectAllUsers();
        ValueTask<User> SelectUserByIdAsync(Guid id);
        ValueTask<User> UpdateUserAsync(User word);
        ValueTask<User> DeleteUserAsync(User word);
    }
}
