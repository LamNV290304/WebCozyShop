namespace WebCozyShop.Helper
{
    public class ValidationHelper
    {
        

        #region Private Methods
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
        #endregion
    }
}
