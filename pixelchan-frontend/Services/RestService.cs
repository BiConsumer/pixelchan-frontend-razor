using Pixelchan.Models;

namespace Pixelchan.Services;

public interface RestService<M> where M : Model {

	Task<M> Find(string id);

	Task<IEnumerable<M>> List();

	Task<M> Create<P>(P partial);

}