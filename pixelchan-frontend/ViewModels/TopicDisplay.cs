using Pixelchan.Models;

namespace Pixelchan.ViewModels {

	public class TopicDisplay {

		private readonly Topic topic;
		private readonly Post[] posts;

		public Topic Topic => topic;
		public Post[] Posts => posts;

		public TopicDisplay(Topic topic, Post[] posts) {
			this.topic = topic;
			this.posts = posts;
		}
	}
}
