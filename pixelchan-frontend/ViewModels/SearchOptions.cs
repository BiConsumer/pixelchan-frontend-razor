namespace Pixelchan.ViewModels;

public class SearchOptions {

	public string? Keywords { get; set; }
	public int? MinFavorites { get; set; }
	public int? MaxFavorites { get; set; }
	public string? Popular { get; set; }
	public string[]? Categories { get; set; }

}