using Microsoft.AspNetCore.Mvc;
using Pixelchan.Models;
using Pixelchan.Services;
using Pixelchan.ViewModels;

namespace Pixelchan.Controllers {

	public class TopicController : Controller {

		private readonly TranslationService translationService;
		private readonly TopicService topicService;
		private readonly PostService postService;

		public TopicController(
			TranslationService translationService, 
			TopicService topicService,
			PostService postService
		) {
			this.translationService = translationService;
			this.topicService = topicService;
			this.postService = postService;
		}

		[Route("topic/{topicId}/{postId?}")]
		public ActionResult Index(string topicId, string? postId) {
			TopicDisplay display = topicService.Displays().Where(display => display.Topic.Id == topicId).FirstOrDefault();

			if (display == null) {
                return View("NotFound", translationService.Translate("TOPIC.NOT-FOUND", new {
                    name = topicId
                }));
            }

            Array.Reverse(display.Posts);

			ViewBag.Post = postId ?? "";
            return View(display);
		}

		[Route("topic/{topicId}/comment")]
		public ActionResult Comment(string topicId, string content) {
			Post post = postService.Create(PostCreateRequest.Create(topicId, content));

			return RedirectToAction("Index", "Topic", new {
				topicId,
                postId = post.Id
			});
		}

		public ActionResult Favorite(string topicId) {
			return RedirectToAction("Index", "Topic", new {
                topicId
            });
		}
	}
}
