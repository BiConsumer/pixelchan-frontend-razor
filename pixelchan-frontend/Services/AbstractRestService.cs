using Newtonsoft.Json;
using Pixelchan.Models;
using System.Text;

namespace Pixelchan.Services {

    public abstract class AbstractRestService<M> : RestService<M> where M : Model {

        public const string URL = "https://backend.pixelchan.org";

        protected readonly HttpClient client;
        protected readonly string route;

        protected AbstractRestService(HttpClient client, string modelRoute) {
            this.client = client;
            route = URL + "/" + modelRoute;
        }

        public M Find(string id) {
            var result = client.GetAsync(route + "/" + id).Result;
            return JsonConvert.DeserializeObject<M>(result.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<M> List() {
            var result = client.GetAsync(route).Result;
            return JsonConvert.DeserializeObject<List<M>>(result.Content.ReadAsStringAsync().Result);
        }

        public M Create<P>(P partial) {
            var content = new StringContent(JsonConvert.SerializeObject(partial), Encoding.UTF8, "application/json");

            return JsonConvert.DeserializeObject<M>(
                client.PostAsync(route, content).Result.Content.ReadAsStringAsync().Result
            );
        }
    }
}
