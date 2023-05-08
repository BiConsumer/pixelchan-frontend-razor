namespace Pixelchan.ViewModels;

using Models;

public class TopicDisplay {
	
	public Topic Topic { get; }

	public Post[] Posts { get; }

	public TopicDisplay(Topic topic, Post[] posts) {
		Topic = topic;
		Posts = posts;
	}
}