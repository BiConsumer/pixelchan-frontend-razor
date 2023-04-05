using Pixelchan.Models;

namespace Pixelchan.Services {

    public class PostService : AbstractRestService<Post> {

        public PostService(HttpClient client) : base(client, "post") { }

        public Post[] OfTopic(string topicId) {
            return List().Where(post => post.Topic == topicId).ToArray();
        }

        public Post[] OfTopicSortedOldest(string topicId) {
            return List().OrderBy(post => post.CreatedAt).ToArray();
        }

        public Post[] Order(Post[] posts) {
            Array.Sort(posts, (x, y) => DateTime.Compare(y.CreatedAt, x.CreatedAt));
            return posts;
        }
    }
}
