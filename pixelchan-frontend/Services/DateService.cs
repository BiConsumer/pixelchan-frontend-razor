namespace Pixelchan.Services;

public class DateService {

    private static readonly IDictionary<string, decimal> INTERVALS = new Dictionary<string, decimal>() {
        {"YEAR", 31536000},
        {"MONTH", 2592000},
        {"WEEK", 604800},
        {"DAY", 86400},
        {"HOUR", 3600},
        {"MINUTE", 60},
        {"SECOND", 1}
    };

    private readonly TranslationService translationService;

    public DateService(TranslationService translationService) {
        this.translationService = translationService;
    }

    public string DateAgo(DateTime date) {
        decimal seconds = (decimal) (DateTime.Now - date).TotalSeconds;

        if (seconds < 29) {
            return translationService.Translate("TIME.JUST-NOW");
        }

        foreach (var interval in INTERVALS) {
            decimal counter = Math.Floor(seconds / interval.Value);

            if (counter > 0) {
                string tense = counter == 1 ? "SINGULAR" : "PLURAL";
                string unit = translationService.Translate($"TIME.{interval.Key}.{tense}");

                return translationService.Translate("TIME.AGO", new {
                    value = counter.ToString(),
                    unit
                });
            }
        }

        return "";
    }
}