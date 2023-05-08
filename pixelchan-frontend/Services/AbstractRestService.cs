using Newtonsoft.Json;
using Pixelchan.Models;
using System.Text;

namespace Pixelchan.Services;

public abstract class AbstractRestService<M> : RestService<M> where M : Model {

    public const string URL = "https://backend.pixelchan.org";

    protected readonly HttpClient client;
    protected readonly IHttpContextAccessor contextAccessor;

    protected readonly string route;

    protected HttpContext Context => contextAccessor.HttpContext;

    protected AbstractRestService(HttpClient client, IHttpContextAccessor contextAccessor, string modelRoute) {
        this.client = client;
        this.contextAccessor = contextAccessor;
        route = URL + "/" + modelRoute;
    }

    public virtual async Task<M> Find(string id) {
        var result = await client.GetAsync(route + "/" + id);
        var text = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<M>(text);
    }

    public virtual async Task<IEnumerable<M>> List() {
        var result = await client.GetAsync(route);
        var text = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<M>>(text);
    }

    public virtual async Task<M> Create<P>(P partial) {
        var content = new StringContent(JsonConvert.SerializeObject(partial), Encoding.UTF8, "application/json");
        var result = await client.PostAsync(route, content);
        var text = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<M>(text);
    }
}