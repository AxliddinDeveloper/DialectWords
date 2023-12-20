using DialectWords.Models.Foundations.Users;
using Microsoft.EntityFrameworkCore;

namespace DialectWords.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User word) =>
            await this.InsertAsync(word);
        public IQueryable<User> SelectAllUsers() =>
            this.SelectAll<User>();
        public async ValueTask<User> SelectUserByIdAsync(Guid id) =>
            await this.SelectAsync<User>(id);
        public async ValueTask<User> UpdateUserAsync(User word) =>
            await this.UpdateAsync<User>(word);
        public async ValueTask<User> DeleteUserAsync(User word) =>
            await this.DeleteAsync<User>(word);
    }
}
