using Newtonsoft.Json;
using Pixelchan.Converters;

namespace Pixelchan.Models {

	public class Category : Model.Dated {

		public DateTime CreatedAt => createdAt;
		public string Id => id;

		private readonly string id;
        private readonly DateTime createdAt;

		public Category(string id, [JsonConverter(typeof(UnixDateTimeConverter))] DateTime createdAt) {
			this.id = id;
			this.createdAt = createdAt;
		}
	}
}