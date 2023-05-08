namespace Pixelchan.Services;

using Newtonsoft.Json;
using Extensions;
using Models;
using ViewModels;

public class TopicService : AbstractRestService<Topic> {

    private readonly PostService postService;

    public TopicService(
        HttpClient client,
        IHttpContextAccessor contextAccessor,
        PostService postService
    ) : base(client, contextAccessor, "topic") 
    {
        this.postService = postService;
    }

    public async Task<TopicDisplay[]> Displays() {
        var result = await client.GetAsync(route + "/displays/");
        var maskedIds = Context.Session.Get<List<string>>("MASKED_TOPICS") ?? new List<string>();

        return JsonConvert.DeserializeObject<TopicDisplay[]>(await result.Content.ReadAsStringAsync()).OrderByDescending(display =>
            postService.Order(display.Posts)[0].CreatedAt
        ).Where(display => !maskedIds.Contains(display.Topic.Id)).ToArray();
    }

    public async Task<TopicDisplay[]> DisplaysOfCategory(string categoryId) {
        return (await Displays())
            .Where(display => display.Topic.Category == categoryId)
            .ToArray();
    }

    public List<TopicDisplay> Order(List<TopicDisplay> displays) {
        var maskedIds = Context.Session.Get<List<string>>("MASKED_TOPICS") ?? new List<string>();
        displays.RemoveAll(display => maskedIds.Contains(display.Topic.Id));

        displays.Sort((x, y) => {
            Post[] orderedPostsOne = postService.Order(x.Posts);
            Post[] orderedPostsTwo = postService.Order(y.Posts);

            return DateTime.Compare(orderedPostsTwo[0].CreatedAt, orderedPostsOne[0].CreatedAt);
        });

        return displays;
    }

    public override async Task<IEnumerable<Topic>> List() {
        var maskedIds = Context.Session.Get<List<string>>("MASKED_TOPICS") ?? new List<string>();

        return (await base.List()).Where(topic => !maskedIds.Contains(topic.Id)).ToList();
    }

    public async Task Favorite(string topicId) {
        await client.GetAsync(route + "/favorite/" + topicId);
            
        var favorites = Context.Session.Get<List<string>>("FAVORITES") ?? new List<string>();
        favorites.Add(topicId);
        
        Context.Session.Set("FAVORITES", favorites);
    }

    public async Task Unfavorite(string topicId) {
        await client.GetAsync(route + "/unfavorite/" + topicId);
            
        var favorites = Context.Session.Get<List<string>>("FAVORITES") ?? new List<string>();
        favorites.Remove(topicId);
        
        Context.Session.Set("FAVORITES", favorites);
    }
}