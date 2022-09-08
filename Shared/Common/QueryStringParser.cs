using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Shared.Common
{
    public class QueryStringParser<T> where T : class, new()
    {
        public static ValueTask<T?> BindAsync(HttpContext context)
        {
            var result = new T();

            var tmp = result.GetType();

            foreach (var item in tmp.GetProperties())
            {
                Type tProp = item.PropertyType;
                string tName = item.Name;

                var propertyValue = context.Request.Query[tName].Count == 0 ? null : context.Request.Query[tName].ToString();

                if (tProp.IsGenericType && tProp.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (propertyValue == null || propertyValue == "null")
                    {
                        item.SetValue(result, null, null);
                        continue;
                    }

                    tProp = new NullableConverter(tProp).UnderlyingType;
                }
                if (tProp.IsEnum)
                {
                    Enum.TryParse(tProp, propertyValue, out var enumVal);
                    item.SetValue(result, enumVal, null);

                }
                else
                {
                    var type = Convert.ChangeType(propertyValue, tProp);
                    item.SetValue(result, type, null);
                }
            }

            return ValueTask.FromResult<T?>(result);
        }

        public static bool TryParse(string? value, out T? output)
        {
            output = new T();

            value?.TrimStart('(').TrimEnd(')');

            return false;
        }
    }
}
