namespace Pixelchan.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services;

public class HomeController : Controller {

    private readonly CategoryService categoryService;
    private readonly TranslationService translationService;

    public HomeController(CategoryService categoryService, TranslationService translationService) {
        this.categoryService = categoryService;
        this.translationService = translationService;
    }

    public async Task<IActionResult> Index() {
        return View(await categoryService.Displays());
    }

    [Route("lang/{lang}")]
    public IActionResult ChangeLang(string lang, string page) {
        this.translationService.SetLang(lang);
        return Redirect(page);
    }
}