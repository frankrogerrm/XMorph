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
    }
}
