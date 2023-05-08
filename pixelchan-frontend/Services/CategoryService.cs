using Newtonsoft.Json;
using Pixelchan.Models;
using Pixelchan.ViewModels;

namespace Pixelchan.Services;

public class CategoryService : AbstractRestService<Category> {

    private readonly TopicService topicService;

    public CategoryService(HttpClient client, IHttpContextAccessor contextAccessor, TopicService topicService) : base(client, contextAccessor, "category") {
        this.topicService = topicService;
    }

    public async Task<CategoryDisplay[]> Displays() {
        var result = await client.GetAsync(route + "/displays/");

        return JsonConvert.DeserializeObject<CategoryDisplay[]>(await result.Content.ReadAsStringAsync()).OrderByDescending(display => {
            if (display.TopicDisplays.Count <= 0) {
                return display.Category.CreatedAt;
            }

            List<TopicDisplay> orderedDisplays = topicService.Order(display.TopicDisplays);
            return orderedDisplays[0].Posts[0].CreatedAt;
        }).ToArray();
    }
}