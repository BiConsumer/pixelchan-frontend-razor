using Pixelchan.Models;

namespace Pixelchan.ViewModels {

	public class CategoryDisplay {

		private readonly Category category;
		private readonly TopicDisplay[] topicDisplays;

		public Category Category => category;
		public TopicDisplay[] TopicDisplays => topicDisplays;

		public CategoryDisplay(Category category, TopicDisplay[] topicDisplays) {
			this.category = category;
			this.topicDisplays = topicDisplays;
		}
	}
}
