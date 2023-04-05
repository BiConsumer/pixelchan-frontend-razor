using Pixelchan.Models;

namespace Pixelchan.Services {

	public interface RestService<M> where M : Model {

		M Find(string id);

		IEnumerable<M> List();

		M Create<P>(P partial);

	}
}
