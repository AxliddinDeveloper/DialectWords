﻿//=============
// For Client
//=============

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
    }
}
