//=============
// For Client
//=============

using DialectWords.Models;
using DialectWords.Models.Foundations.Users;
using DialectWords.Models.Foundations.words;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DialectWords.Services.Foundations
{
    public interface IWordService
    {
        ValueTask<Word> AddWordAsync(Word word);
        IQueryable<Word> RetrieveAllWords();
        ValueTask<Word> RetrieveWordByIdAsync(Guid Id);
        ValueTask<Word> ModifyWordAsync(Word word);
        ValueTask<Word> RemoveWordByIdAsync(Guid id);
        List<SelectListItem> SelectItems();
        //=================================
        ValueTask<string> CreateUserAsync(UserViewModel userViewModel);
        IQueryable<User> RetrieveAllUsers();
        ValueTask<User> RetrieveUserByIdAsync(Guid Id);
        ValueTask<User> ModifyUserAsync(User user);
        ValueTask<User> RemoveUserByIdAsync(Guid id);
    }
}
