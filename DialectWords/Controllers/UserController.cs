using DialectWords.Models.Foundations.words;
using DialectWords.Models;
using DialectWords.Services.Foundations;
using Microsoft.AspNetCore.Mvc;

namespace DialectWords.Controllers
{
    public class UserController : Controller
    {
        private readonly IWordService wordService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(IWordService wordService, IWebHostEnvironment webHostEnvironment)
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
        public ActionResult<IQueryable<Word>> GetAllWordsForUser(int pageSize = 10, int pageNumber = 1, string searchString = "", string filter = "")
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();
            IQueryable<Word> foundWords;
            IQueryable<Word> foundWords2;


            if (!string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(filter))
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
            else if (!string.IsNullOrEmpty(filter) && string.IsNullOrEmpty(searchString))
            {
                foundWords = words.Where(a =>
                    a.AdabiyTil.ToLower().Contains(filter.ToLower()) ||
                    a.Transliteratsiya.ToLower().Contains(filter.ToLower()) ||
                    a.Transkripsiya.ToLower().Contains(filter.ToLower()) ||
                    a.Sinonim.ToLower().Contains(filter.ToLower()) ||
                    a.Omonim.ToLower().Contains(filter.ToLower()) ||
                    a.Antonim.ToLower().Contains(filter.ToLower()) ||
                    a.Turkum.ToLower().Contains(filter.ToLower()) ||
                    a.OzlashganQatlam.ToLower().Contains(filter.ToLower()) ||
                    a.RusTilida.ToLower().Contains(filter.ToLower()) ||
                    a.IngilizTilida.ToLower().Contains(filter.ToLower())).AsQueryable();
            }
            else if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(searchString))
            {
                foundWords = words.Where(a =>
                    a.AdabiyTil.ToLower().Contains(filter.ToLower()) ||
                    a.Transliteratsiya.ToLower().Contains(filter.ToLower()) ||
                    a.Transkripsiya.ToLower().Contains(filter.ToLower()) ||
                    a.Sinonim.ToLower().Contains(filter.ToLower()) ||
                    a.Omonim.ToLower().Contains(filter.ToLower()) ||
                    a.Antonim.ToLower().Contains(filter.ToLower()) ||
                    a.Turkum.ToLower().Contains(filter.ToLower()) ||
                    a.OzlashganQatlam.ToLower().Contains(filter.ToLower()) ||
                    a.RusTilida.ToLower().Contains(filter.ToLower()) ||
                    a.IngilizTilida.ToLower().Contains(filter.ToLower()) ||
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

            ViewBag.Turkumlar = this.wordService.SelectItems();

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

            return View("GetAllWords", foundWords);
        }
    }
}
