namespace Pixelchan.ViewModels;

using Models;

public class CategoryDisplay {
	
	public Category Category { get; }
	public List<TopicDisplay> TopicDisplays { get; }

	public CategoryDisplay(Category category, List<TopicDisplay> topicDisplays) {
		Category = category;
		TopicDisplays = topicDisplays;
	}
}