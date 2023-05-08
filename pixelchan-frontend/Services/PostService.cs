using Pixelchan.Models;

namespace Pixelchan.Services;

public class PostService : AbstractRestService<Post> {

    public PostService(HttpClient client, IHttpContextAccessor contextAccessor) : base(client, contextAccessor, "post") { }

    public async Task<Post[]> OfTopic(string topicId) {
        var list = await List();

        return list.Where(post => post.Topic == topicId).ToArray();
    }

    public async Task<Post[]> OfTopicSortedOldest(string topicId) {
        var list = await List();

        return list.OrderBy(post => post.CreatedAt).ToArray();
    }

    public Post[] Order(Post[] posts) {
        Array.Sort(posts, (x, y) => DateTime.Compare(y.CreatedAt, x.CreatedAt));
        return posts;
    }
}