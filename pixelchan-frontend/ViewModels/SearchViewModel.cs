namespace Pixelchan.ViewModels {

	public class SearchViewModel {

		private readonly SearchOptions options;
		private readonly TopicDisplay[] results;

		public SearchOptions Options => options;
		public TopicDisplay[] Results => results;

		public SearchViewModel(SearchOptions options, TopicDisplay[] results) {
			this.options = options;
			this.results = results;
		}
	}
}