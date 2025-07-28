using System.Text.Json;
using WebCozyShop.ViewModels;

namespace WebCozyShop.Helper
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
    public static class SessionHelper
    {
        private const string TempInvoicesKey = "TempInvoices";

        public static Dictionary<Guid, List<TempInvoiceItem>> GetAllInvoices(this ISession session)
        {
            return session.GetObject<Dictionary<Guid, List<TempInvoiceItem>>>(TempInvoicesKey) ?? new();
        }

        public static void SaveAllInvoices(this ISession session, Dictionary<Guid, List<TempInvoiceItem>> invoices)
        {
            session.SetObject(TempInvoicesKey, invoices);
        }

        public static List<TempInvoiceItem> GetInvoice(this ISession session, Guid tempUserId)
        {
            var all = session.GetAllInvoices();
            return all.ContainsKey(tempUserId) ? all[tempUserId] : new List<TempInvoiceItem>();
        }

        public static void SetInvoice(this ISession session, Guid tempUserId, List<TempInvoiceItem> items)
        {
            var all = session.GetAllInvoices();
            all[tempUserId] = items;
            session.SaveAllInvoices(all);
        }

        public static void RemoveInvoice(this ISession session, Guid tempUserId)
        {
            var all = session.GetAllInvoices();
            if (all.ContainsKey(tempUserId))
            {
                all.Remove(tempUserId);
                session.SaveAllInvoices(all);
            }
        }
    }

}
