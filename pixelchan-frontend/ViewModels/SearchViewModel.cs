namespace Pixelchan.ViewModels;

public class SearchViewModel {
	public SearchOptions Options { get; }

	public TopicDisplay[] Results { get; }

	public SearchViewModel(SearchOptions options, TopicDisplay[] results) {
		Options = options;
		Results = results;
	}
}