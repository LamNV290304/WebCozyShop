namespace WebCozyShop.Helper
{
    public class GenerateHelper
    {
        public static string GenerateSKU(string productName, string color, string size)
        {
            string namePart = RemoveDiacritics(productName).ToUpper().Replace(" ", "").Substring(0, Math.Min(5, productName.Length));
            string colorPart = RemoveDiacritics(color).ToUpper().Replace(" ", "").Substring(0, Math.Min(3, color.Length));
            string sizePart = size.ToUpper();

            return $"{namePart}-{colorPart}-{sizePart}";
        }

        #region Private methods
        private static string RemoveDiacritics(string input)
        {
            var normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            var chars = normalized.Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark);
            return new string(chars.ToArray()).Normalize(System.Text.NormalizationForm.FormC);
        }
        #endregion
    }
}
