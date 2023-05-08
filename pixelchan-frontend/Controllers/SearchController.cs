namespace Pixelchan.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services;
using ViewModels;

public class SearchController : Controller {

	private const int POPULAR_COUNT = 20;

	private readonly TopicService topicService;
	private readonly CategoryService categoryService;

	public SearchController(TopicService topicService, CategoryService categoryService) {
		this.topicService = topicService;
		this.categoryService = categoryService;
	}

	public async Task<IActionResult> Index(SearchOptions options) {
		var categoryList = await categoryService.List();
		var topicDisplays = await topicService.Displays();

		options.Keywords = options.Keywords ?? "";
		options.Popular = options.Popular ?? "none";
		options.Categories = options.Categories ?? categoryList.Select(category => category.Id).ToArray();

		TopicDisplay[] results = topicDisplays
			.Where(display => options.Keywords == "" || display.Topic.Name.ToLower().Contains(options.Keywords.ToLower()))
			.Where(display => 
				options.Popular == "none" 
				|| (
					options.Popular == "yes" && display.Topic.Favorites >= POPULAR_COUNT
				) || (
					options.Popular == "no" && display.Topic.Favorites < POPULAR_COUNT
				)
			)
			.Where(display => options.MinFavorites == null || display.Topic.Favorites >= options.MinFavorites)
			.Where(display => options.MaxFavorites == null || display.Topic.Favorites <= options.MaxFavorites)
			.Where(display => options.Categories.Contains(display.Topic.Category))
			.ToArray();

		return View(new SearchViewModel(
			options,
			results
		));
	}

}