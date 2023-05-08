namespace Pixelchan.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;
using System.Text.RegularExpressions;

public class JsonTranslationService : TranslationService {

	public object this[string propertyName] {
		get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
	}

	public const string LANG_COOKIE = "lang";

	public static readonly string[] LANGS = {
		"fr",
		"en",
		"es"
	};

	private readonly IWebHostEnvironment hostEnvironment;
	private readonly IHttpContextAccessor contextAccessor;

	private HttpRequest request => contextAccessor.HttpContext.Request;
	private HttpResponse response => contextAccessor.HttpContext.Response;

	private IDictionary<string, string> translations;

	public string CurrentLang => request.Cookies[LANG_COOKIE] ?? "fr";

	public JsonTranslationService(IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor) {
		this.hostEnvironment = hostEnvironment;
		this.contextAccessor = contextAccessor;
		this.translations = new Dictionary<string, string>();

		Load(CurrentLang);
	}

	public void SetLang(string lang) {
		if (!LANGS.Contains(lang)) {
			return;
		}

		response.Cookies.Append(LANG_COOKIE, lang);
		Load(lang);
	}

	public string Translate(string path) {
		return translations.TryGetValue(path, out var value) ? value : path;
	}

	public string Translate(string path, object placeholders) {
		string message = translations.TryGetValue(path, out var value) ? value : path;

		MatchCollection matches = Regex.Matches(message, @"\{\{([^{{}}]+)\}\}*");
		foreach (Match match in matches) {
			string name = match.Value.Replace("{", "").Replace("}", "");

			message = message
				.Replace(
					match.Value,
					GetPropValue(placeholders, name, match.Value)
				);
		}

		return message;
	}

	private void Load(string lang) {
		var translationFilePath = Path.Combine(hostEnvironment.WebRootPath, $"i18n/{lang}.json");
		translations = DeserializeToDictionary(File.ReadAllText(translationFilePath), null);
	}

	private Dictionary<string, string> DeserializeToDictionary(string json, string? key) {
		var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
		var dictionary = new Dictionary<string, string>();

		foreach (var value in values) {
			string currentKey = key == null ? value.Key : $"{key}.{value.Key}";

			if (value.Value is JObject) {
				dictionary.AddRange(DeserializeToDictionary(value.Value.ToString(), currentKey));
			} else {
				dictionary.Add(currentKey, value.Value.ToString());
			}
		}

		return dictionary;
	}

	private string GetPropValue(object src, string name, string def) {
		object value = src.GetType().GetProperty(name).GetValue(src, null);
		return value.ToString() ?? def;
	}
}