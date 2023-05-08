namespace Pixelchan.Controllers;

using Microsoft.AspNetCore.Mvc;
using Extensions;
using Services;

public class FavoritesController : Controller {

    private readonly TopicService topicService;

    public FavoritesController(TopicService topicService) {
        this.topicService = topicService;
    }

    public async Task<IActionResult> Index() {
        var favorites = HttpContext.Session.Get<List<string>>("FAVORITES") ?? new List<string>();
        var displays = await topicService.Displays();

        return View(
            displays
                .Where(display => favorites.Contains(display.Topic.Id))
                .ToArray()
        );
    }
}