using Microsoft.AspNetCore.Mvc;
using Pixelchan.Services;

namespace Pixelchan.Controllers {

	public class HomeController : Controller {

		private readonly CategoryService categoryService;
        private readonly TranslationService translationService;

        public HomeController(CategoryService categoryService, TranslationService translationService) {
            this.categoryService = categoryService;
            this.translationService = translationService;
        }

        public IActionResult Index() {
            return View(categoryService.Displays());
        }

        [Route("lang/{lang}")]
        public IActionResult ChangeLang(string lang) {
            this.translationService.SetLang(lang);
            return RedirectToAction("Index", "Home");
        }
	}
}
