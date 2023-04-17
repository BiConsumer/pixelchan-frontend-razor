using Newtonsoft.Json;
using Pixelchan.Models;
using Pixelchan.ViewModels;

namespace Pixelchan.Services {

    public class TopicService : AbstractRestService<Topic> {

        private readonly PostService postService;

        public TopicService(HttpClient client, PostService postService) : base(client, "topic") {
            this.postService = postService;
        }

        public async Task<TopicDisplay[]> Displays() {
            var result = await client.GetAsync(route + "/displays/");

            return JsonConvert.DeserializeObject<TopicDisplay[]>(await result.Content.ReadAsStringAsync()).OrderByDescending(display =>
                postService.Order(display.Posts)[0].CreatedAt
            ).ToArray();
        }

        public async Task<TopicDisplay[]> DisplaysOfCategory(string categoryId) {
            var result = await client.GetAsync(route + "/displays/");

            return JsonConvert.DeserializeObject<TopicDisplay[]>(await result.Content.ReadAsStringAsync())
                .Where(display => display.Topic.Category == categoryId)
                .OrderByDescending(display =>
                    postService.Order(display.Posts)[0].CreatedAt
                ).ToArray();
        }

        public TopicDisplay[] Order(TopicDisplay[] displays) {
            Array.Sort(displays, (x, y) => {
                Post[] orderedPostsOne = postService.Order(x.Posts);
                Post[] orderedPostsTwo = postService.Order(y.Posts);

                return DateTime.Compare(orderedPostsTwo[0].CreatedAt, orderedPostsOne[0].CreatedAt);
            });

            return displays;
        }

        public void Favorite(string topicId) {
            //TODO
        }

        public void Unfavorite(string topicId) {
            //TODO
        }

    }
}
