using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
using DialectWords.Models;
using DialectWords.Models.Foundations.words;
using DialectWords.Services.Foundations;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace DialectWords.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordService wordService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public WordController(IWordService wordService, IWebHostEnvironment webHostEnvironment)
        {
            this.wordService = wordService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult PostWord()
        {
            return View("PostWord");
        }

        [HttpPost]
        public async ValueTask<ActionResult<Word>> PostWord(Word word)
        {
            Word storeword = await this.wordService.AddWordAsync(word);

            return RedirectToAction("GetAllWords");
        }

        public IActionResult Forward()
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();

            int pageNumber = 1, pageSize = 1;
            pageNumber += 1;
            var paginatedData = words
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            return RedirectToAction("GetAllWords", paginatedData);
        }

        public IActionResult Back()
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();

            int pageNumber = 1, pageSize = 1;

            if ( pageNumber > 1)
                pageNumber -= 1;

            var paginatedData = words
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            return RedirectToAction("GetAllWords", paginatedData);
        }

        [HttpGet]
        public ActionResult<IQueryable<Word>> GetAllWords(int pageSize =10,int pageNumber = 1)
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();

            int pages = words.Count();
            int totalPages = pages / pageSize;
            if (pages % pageSize != 0)
                totalPages += 1;

            var paginatedData = words
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            WordsViewModel wordsViewModel = new WordsViewModel();

            wordsViewModel.Words = paginatedData;
            wordsViewModel.TotalPages = totalPages;
            wordsViewModel.PageNumber = pageNumber;

            return View(wordsViewModel);
        }
        
        [HttpGet]
        public async ValueTask<ActionResult<Word>> Edit(Guid id)
        {
            Word word = await this.wordService.RetrieveWordByIdAsync(id);

            return View(word);
        }

        [HttpPost]
        public IActionResult UpdateWord(Word word)
        {
            this.wordService.ModifyWordAsync(word);

            return RedirectToAction("GetAllWords");
        }

        [HttpGet]
        public IActionResult DeleteWord(Guid id)
        {
            this.wordService.RemoveWordByIdAsync(id);

            return RedirectToAction("GetAllWords");
        }

        [HttpGet]
        public IActionResult SearchWord(string searchString)
        {
            var applicants = this.wordService.RetrieveAllWords().ToList();

            List<Word> foundWords = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                foundWords = applicants.Where(a =>
                    a.AdabiyTil.ToLower() == searchString.ToLower() ||
                    a.Transliteratsiya.ToLower() == searchString.ToLower() ||
                    a.Transkripsiya.ToLower() == searchString.ToLower() ||
                    a.Sinonim.ToLower() == searchString.ToLower() ||
                    a.Omonim.ToLower() == searchString.ToLower() ||
                    a.Antonim.ToLower() == searchString.ToLower() ||
                    a.Turkum.ToLower() == searchString.ToLower() ||
                    a.OzlashganQatlam.ToLower() == searchString.ToLower() ||
                    a.RusTilida.ToLower() == searchString.ToLower() ||
                    a.IngilizTilida.ToLower() == searchString.ToLower()).ToList();
            }

            return View(foundWords);
        }
    }
}
