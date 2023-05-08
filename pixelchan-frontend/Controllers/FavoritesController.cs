using Microsoft.AspNetCore.Mvc;
using Pixelchan.Extensions;
using Pixelchan.Services;

namespace Pixelchan.Controllers;

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