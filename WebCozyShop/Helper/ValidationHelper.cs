namespace WebCozyShop.Helper
{
    public class ValidationHelper
    {
        public static string NormalizeSearchTerm(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var words = input.Trim()
                             .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return string.Join(" ", words);
        }
        #region Private Methods

        #endregion
    }
}
