using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
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
        public ActionResult<IQueryable<Word>> GetAllWords(int pageSize = 1, int pageNumber = 1)
        {
            IQueryable<Word> words = this.wordService.RetrieveAllWords();

            var paginatedData = words
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable();

            return View(paginatedData);
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
    }
}
