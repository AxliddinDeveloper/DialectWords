using System.Diagnostics;
using DialectWords.Models;
using DialectWords.Models.Foundations.Users;
using DialectWords.Models.Foundations.words;
using DialectWords.Services.Foundations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DialectWords.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordService wordService;

        public HomeController(IWordService wordService)
        {
            this.wordService = wordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult salom()
        {
            return View();
        }

        public IActionResult LoyihaHaqida()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<IQueryable<Word>> GetAllWordsForUser(int pageSize = 10, int pageNumber = 1, string searchString = "", string filter = "")
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();
            IQueryable<Word> foundWords;

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

            WordsViewModel wordsViewModel = new WordsViewModel();
            
            if (foundWords != null)
            {
                int totalItems = foundWords.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                var paginatedData = foundWords
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable();

                ViewBag.Turkumlar = this.wordService.SelectItems();

                wordsViewModel.Words = paginatedData;
                wordsViewModel.TotalPages = totalPages;
                wordsViewModel.PageNumber = pageNumber;
                wordsViewModel.PageSize = pageSize;
                wordsViewModel.SearchString = searchString;
                wordsViewModel.Filter = filter;
            }
            else
            {
                wordsViewModel.Words = new List<Word>().AsQueryable();
                wordsViewModel.TotalPages = 1;
                wordsViewModel.PageNumber = pageNumber;
                wordsViewModel.PageSize = pageSize;
                wordsViewModel.SearchString = searchString;
            }


            return View(wordsViewModel);
        }

        [HttpGet]
        public IActionResult PostUser()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostUser(UserViewModel userViewModel)
        {
            string result = await this.wordService.CreateUserAsync(userViewModel);
            ViewBag.Message = result;

            return View("PostUser");
        }
    }
}
