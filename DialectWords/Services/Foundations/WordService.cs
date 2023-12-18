//=============
// For Client
//=============

using DialectWords.Brokers.Storages;
using DialectWords.Models.Foundations.words;

namespace DialectWords.Services.Foundations
{
    public class WordService : IWordService
    {
        private readonly IStorageBroker storageBroker;

        public WordService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Word> AddWordAsync(Word word)
        {
            word.id = Guid.NewGuid();

            return await this.storageBroker.InsertWordAsync(word);
        }

        public IQueryable<Word> RetrieveAllWords() =>
            this.storageBroker.SelectAllWords();

        public async ValueTask<Word> RetrieveWordByIdAsync(Guid Id) =>
            await this.storageBroker.SelectWordByIdAsync(Id);

        public async ValueTask<Word> ModifyWordAsync(Word word) =>
            await this.storageBroker.UpdateWordAsync(word);

        public async ValueTask<Word> RemoveWordByIdAsync(Guid id)
        {
            Word mayBeWord = 
                await this.storageBroker.SelectWordByIdAsync(id);

            return await this.storageBroker.DeleteWordAsync(mayBeWord);
        }
    }
}
