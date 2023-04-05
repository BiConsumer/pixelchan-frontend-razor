namespace Pixelchan.Services {

	public interface TranslationService {

		string CurrentLang { get; }

		string Translate(string path);

		string Translate(string path, object placeholders);

		void SetLang(string lang);

	}
}