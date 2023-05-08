using Microsoft.AspNetCore.Mvc;
using Pixelchan.Extensions;
using Pixelchan.Models;
using Pixelchan.Services;
using Pixelchan.ViewModels;

namespace Pixelchan.Controllers;

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
	public async Task<ActionResult> Index(string topicId, string? postId) {
		var topicDisplays = await topicService.Displays();

		TopicDisplay display = topicDisplays.Where(display => display.Topic.Id == topicId).FirstOrDefault();

		if (display == null) {
			return View("NotFound", translationService.Translate("TOPIC.NOT-FOUND", new {
				name = topicId
			}));
		}

		Array.Reverse(display.Posts);
            
		var favorites = HttpContext.Session.Get<List<string>>("FAVORITES") ?? new List<string>();
		ViewBag.Post = postId ?? "";
		ViewBag.IsFavorite = favorites.Contains(topicId);
            
		return View(display);
	}

	[Route("topic/{topicId}/comment")]
	public async Task<ActionResult> Comment(string topicId, string content) {
		Post post = await postService.Create(PostCreateRequest.Create(topicId, content));

		return RedirectToAction("Index", "Topic", new {
			topicId,
			postId = post.Id
		});
	}

	[Route("topic/{topicId}/mask")]
	public async Task<ActionResult> Mask(string topicId) {
		Topic topic = await topicService.Find(topicId);
		var maskedIds = HttpContext.Session.Get<List<string>>("MASKED_TOPICS") ?? new List<string>();
		maskedIds.Add(topic.Id);
		HttpContext.Session.Set("MASKED_TOPICS", maskedIds);

		return RedirectToAction("ListTopics", "Category", new { categoryId = topic.Category});
	}

	[HttpPost]
	[Route("topic/{topicId}/favorite")]
	public async Task<ActionResult> Favorite(string topicId) {
		var favorites = HttpContext.Session.Get<List<string>>("FAVORITES") ?? new List<string>();
		if (favorites.Contains(topicId)) {
			await topicService.Favorite(topicId);
		} else {
			await topicService.Unfavorite(topicId);
		}

		return RedirectToAction("Index", "Topic", new {
			topicId
		});
	}
}