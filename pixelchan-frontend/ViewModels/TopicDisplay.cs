using Pixelchan.Models;

namespace Pixelchan.ViewModels;

public class TopicDisplay {
	
	public Topic Topic { get; }

	public Post[] Posts { get; }

	public TopicDisplay(Topic topic, Post[] posts) {
		Topic = topic;
		Posts = posts;
	}
}