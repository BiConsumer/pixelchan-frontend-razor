namespace Pixelchan.Models;

public interface Model {

	string Id { get; }

	interface Dated : Model {

		DateTime CreatedAt { get; }	

	}

}