namespace Pixelchan.Models;

public class PostCreateRequest {

    public readonly string topic;
    public readonly string content;

    private PostCreateRequest(string topic, string content) {
        this.topic = topic;
        this.content = content;
    }

    public static PostCreateRequest Create(string topicId, string content) {
        return new PostCreateRequest(topicId, content);
    }
}