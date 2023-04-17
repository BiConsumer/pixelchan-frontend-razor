using Microsoft.AspNetCore.Mvc;
using Pixelchan.Services;

namespace Pixelchan.Controllers {

    public class FavoritesController : Controller {

        private readonly TopicService topicService;

        public FavoritesController(TopicService topicService) {
            this.topicService = topicService;
        }

        public async Task<IActionResult> Index() {
            return View(await topicService.Displays());
        }
    }
}
