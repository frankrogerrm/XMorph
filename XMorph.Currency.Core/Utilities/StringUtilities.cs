using System.Text.Json;

namespace XMorph.Currency.Core.Utilities {
    public static class StringUtilities {

        public static string CleanString(this string par) {
            return par
                .Replace("\"", "'")
                .Replace(Environment.NewLine, string.Empty)
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("&nbsp;", string.Empty)
                .Replace(" ", string.Empty);
        }

        public static string Format<T>(this T t)
        {
            return JsonSerializer.Serialize(t, new JsonSerializerOptions() {WriteIndented = true});
        }
    }
}
