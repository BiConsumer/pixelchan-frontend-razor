namespace Pixelchan.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

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
	[Route("category/list/{categoryId}")]
	[Route("/{categoryId}", Order = 2)]
	public async Task<ActionResult> ListTopics(string categoryId) {
		var list = await categoryService.List();

		string? foundCategory = list
			.Where(category => category.Id.ToLower() == categoryId.ToLower())
			.Select(category => category.Id)
			.FirstOrDefault();

		if (foundCategory == null) {
			return View("NotFound", translationService.Translate("CATEGORY.NOT-FOUND", new {
				name = categoryId
			}));
		}

		return View(await topicService.DisplaysOfCategory(foundCategory));
	}

	[Route("category/{categoryIndex:int}")]
	[Route("category/list/{categoryIndex:int}")]
	[Route("/{categoryIndex:int}")]
	public async Task<ActionResult> ListTopics(int categoryIndex) {
		var list = await categoryService.List();

		Category? category = list.ElementAtOrDefault(categoryIndex);

		if (category == null) {
			return View("NotFound", translationService.Translate("CATEGORY.NOT-FOUND", new {
				name = categoryIndex
			}));
		}

		return View(await topicService.DisplaysOfCategory(category.Id));
	}
}