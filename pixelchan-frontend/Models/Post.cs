using Newtonsoft.Json;
using Pixelchan.Converters;

namespace Pixelchan.Models {

	public class Post : Model.Dated {

		public DateTime CreatedAt => createdAt;
		public string Id => id;

		public string Topic => topic;
		public string Content => content;

		private readonly string id;
        private readonly DateTime createdAt;

		private readonly string topic;
		private readonly string content;

		public Post(string id, [JsonConverter(typeof(UnixDateTimeConverter))] DateTime createdAt, string topic, string content) {
			this.id = id;
			this.createdAt = createdAt;
			this.topic = topic;
			this.content = content;
		}
	}
}
