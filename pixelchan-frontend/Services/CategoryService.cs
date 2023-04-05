using Newtonsoft.Json;
using Pixelchan.Models;
using Pixelchan.ViewModels;

namespace Pixelchan.Services {

    public class CategoryService : AbstractRestService<Category> {

        private readonly TopicService topicService;

        public CategoryService(HttpClient client, TopicService topicService) : base(client, "category") {
            this.topicService = topicService;
        }

        public CategoryDisplay[] Displays() {
            var result = client.GetAsync(route + "/displays/").Result;
            return JsonConvert.DeserializeObject<CategoryDisplay[]>(result.Content.ReadAsStringAsync().Result).OrderByDescending(display => {
                if (display.TopicDisplays.Length <= 0) {
                    return display.Category.CreatedAt;
                }

                TopicDisplay[] orderedDisplays = topicService.Order(display.TopicDisplays);
                return orderedDisplays[0].Posts[0].CreatedAt;
            }).ToArray();
        }
    }
}
