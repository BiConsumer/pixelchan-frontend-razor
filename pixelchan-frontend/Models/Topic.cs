using Newtonsoft.Json;
using Pixelchan.Converters;

namespace Pixelchan.Models {

	public class Topic : Model.Dated {

		public DateTime CreatedAt => createdAt;
		public string Id => id;

		public string Category => category;
		public string Name => name;
		public int Favorites => favorites;

		private readonly string id;
        private readonly DateTime createdAt;

		private readonly string category;
		private readonly string name;
		private readonly int favorites;

		public Topic(string id, [JsonConverter(typeof(UnixDateTimeConverter))] DateTime createdAt, string category, string name, int favorites) {
			this.id = id;
			this.createdAt = createdAt;
			this.category = category;
			this.name = name;
			this.favorites = favorites;
		}
	}
}
