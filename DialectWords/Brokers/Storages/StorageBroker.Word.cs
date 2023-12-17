//=============
// For Client
//=============

using DialectWords.Models.Foundations.words;
using Microsoft.EntityFrameworkCore;

namespace DialectWords.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Word> Words { get; set; }

        public async ValueTask<Word> InsertWordAsync(Word word) =>
            await InsertAsync(word);


        public IQueryable<Word> SelectAllWords() =>
            SelectAll<Word>();

        public async ValueTask<Word> SelectWordByIdAsync(Guid id) =>
            await SelectWordByIdAsync(id);


        public async ValueTask<Word> UpdateWordAsync(Word word) =>
            await UpdateWordAsync(word);

        public async ValueTask<Word> DeleteWordAsync(Word word) =>
            await DeleteWordAsync(word);

    }
}
