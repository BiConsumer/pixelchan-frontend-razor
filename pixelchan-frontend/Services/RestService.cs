namespace Pixelchan.Services;

using Models;

public interface RestService<M> where M : Model {

	Task<M> Find(string id);

	Task<IEnumerable<M>> List();

	Task<M> Create<P>(P partial);

}