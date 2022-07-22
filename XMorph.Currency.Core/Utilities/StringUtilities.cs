using AgileObjects.AgileMapper;
using System.Text.Json;
using XMorph.Currency.Core.Models;

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

        public static string BeautyJson<T>(this T t) {
            return JsonSerializer.Serialize(t, new JsonSerializerOptions() { WriteIndented = true });
        }

        public static string BeautyUserJson(this UserModel par) {
            var model = Mapper.Map(par).ToANew<UserNoPasswordModel>();
            return JsonSerializer.Serialize(model, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
