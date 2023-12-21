//=============
// For Client
//=============

using DialectWords.Brokers.Storages;
using DialectWords.Models;
using DialectWords.Models.Foundations.Users;
using DialectWords.Models.Foundations.words;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ValidateWord(word);

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

        public List<SelectListItem> SelectItems()
        {
            var words = this.storageBroker.SelectAllWords();
            var itemsString = new List<string>();

            foreach (Word word in words)
            {

                if (!itemsString.Contains(word.Turkum.ToLower()) && !string.IsNullOrEmpty(word.Turkum))
                    itemsString.Add(word.Turkum.ToLower());
            }

            return itemsString.Select(i => new SelectListItem { Text = i}).ToList();
        }

        private Word ValidateWord(Word word)
        {
            word.id = Guid.NewGuid();

            if (word.AdabiyTil == null)
                word.AdabiyTil = "";

            if (word.Transliteratsiya == null)
                word.Transliteratsiya = "";

            if (word.Transkripsiya == null)
                word.Transkripsiya = "";

            if (word.Turkum == null)
                word.Turkum = "";

            if (word.ShevaIzohi == null)
                word.ShevaIzohi = "";

            if (word.Misollar == null)
                word.Misollar = "";

            if (word.Sinonim == null)
                word.Sinonim = "";

            if (word.Antonim == null)
                word.Antonim = "";

            if (word.Omonim == null)
                word.Omonim = "";

            if (word.OzlashganQatlam == null)
                word.OzlashganQatlam = "";

            if (word.RusTilida == null)
                word.RusTilida = "";

            if (word.IngilizTilida == null)
                word.IngilizTilida = "";

            return word;
        }

        public async ValueTask<string> CreateUserAsync(UserViewModel userViewModel)
        {
            if (userViewModel.Verify != "xexezev")
            {
                return "Tasdiqlash kodi xato!";
            }

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = userViewModel.Name,
                Email = userViewModel.Email,
                Comment = userViewModel.Comment,
            };

            await this.storageBroker.InsertUserAsync(user);

            return "Xabaringiz yuborildi!";
        }

        public async ValueTask<User> ModifyUserAsync(User user) =>
            await this.storageBroker.UpdateUserAsync(user);

        public async ValueTask<User> RemoveUserByIdAsync(Guid id)
        {
            User mayBeUser =
                await this.storageBroker.SelectUserByIdAsync(id);

            return await this.storageBroker.DeleteUserAsync(mayBeUser);
        }

        public IQueryable<User> RetrieveAllUsers() =>
             this.storageBroker.SelectAllUsers();

        public async ValueTask<User> RetrieveUserByIdAsync(Guid Id) =>
            await this.storageBroker.SelectUserByIdAsync(Id);
    }
}
