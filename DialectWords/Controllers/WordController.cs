using DialectWords.Models;
using DialectWords.Models.Foundations.words;
using DialectWords.Services.Foundations;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult LoyihaHaqida()
        {
            return View("LoyihaHaqida");
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

        [HttpGet]
        public ActionResult<IQueryable<Word>> GetAllWords(int pageSize = 10, int pageNumber = 1, string searchString = "")
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();
            IQueryable<Word> foundWords;

            if (!string.IsNullOrEmpty(searchString))
            {
                foundWords = words.Where(a =>
                    a.AdabiyTil.ToLower().Contains(searchString.ToLower()) ||
                    a.Transliteratsiya.ToLower().Contains(searchString.ToLower()) ||
                    a.Transkripsiya.ToLower().Contains(searchString.ToLower()) ||
                    a.Sinonim.ToLower().Contains(searchString.ToLower()) ||
                    a.Omonim.ToLower().Contains(searchString.ToLower()) ||
                    a.Antonim.ToLower().Contains(searchString.ToLower()) ||
                    a.Turkum.ToLower().Contains(searchString.ToLower()) ||
                    a.OzlashganQatlam.ToLower().Contains(searchString.ToLower()) ||
                    a.RusTilida.ToLower().Contains(searchString.ToLower()) ||
                    a.IngilizTilida.ToLower().Contains(searchString.ToLower())).AsQueryable();
            }
            else
            {
                foundWords = words;
            }

            int totalItems = foundWords.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var paginatedData = foundWords
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            WordsViewModel wordsViewModel = new WordsViewModel
            {
                Words = paginatedData,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchString = searchString
            };

            return View(wordsViewModel);
        }


        [HttpGet]
        public async ValueTask<ActionResult<Word>> EditWord(Guid id)
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
                    a.AdabiyTil.ToLower().Contains(searchString.ToLower()) ||
                    a.Transliteratsiya.ToLower().Contains(searchString.ToLower()) ||
                    a.Transkripsiya.ToLower().Contains(searchString.ToLower()) ||
                    a.Sinonim.ToLower().Contains(searchString.ToLower()) ||
                    a.Omonim.ToLower().Contains(searchString.ToLower()) ||
                    a.Antonim.ToLower().Contains(searchString.ToLower()) ||
                    a.Turkum.ToLower().Contains(searchString.ToLower()) ||
                    a.OzlashganQatlam.ToLower().Contains(searchString.ToLower()) ||
                    a.RusTilida.ToLower().Contains(searchString.ToLower()) ||
                    a.IngilizTilida.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                foundWords = applicants;
            }

            return View("GetAllWords" ,foundWords);
        }
    }
}
