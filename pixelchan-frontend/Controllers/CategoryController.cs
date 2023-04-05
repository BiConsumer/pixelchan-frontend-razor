using Microsoft.AspNetCore.Mvc;
using Pixelchan.Services;

namespace Pixelchan.Controllers {

	public class CategoryController : Controller {

		private readonly TranslationService translationService;
		private readonly CategoryService categoryService;
		private readonly TopicService topicService;

		public CategoryController(
			TranslationService translationService,
			CategoryService categoryService,
			TopicService topicService
		) {
			this.translationService = translationService;
			this.categoryService = categoryService;
			this.topicService = topicService;
		}

		[Route("category/{categoryId}")]
		public ActionResult ListTopics(string categoryId) {
			if (!categoryService.List().Any(category => category.Id == categoryId)) {
				return View("NotFound", translationService.Translate("CATEGORY.NOT-FOUND", new {
					name = categoryId
				}));
			}

			return View(topicService.DisplaysOfCategory(categoryId));
		}
	}
}
