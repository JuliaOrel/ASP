using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Medicines.Extensions
{
    public static class SessionExtension
    {
        public static T GetOrAdd<T>(
           this ISession session,
string key,
           Func<T> creator) where T : class
        {
            var data = session.Get<T>(key);
            if (data == null)
            {
                data = creator();
                Set(session, key, data);
            }
            return data;
        }


        public static void Set<T>(
            this ISession session,
            string key,
            T value)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // добавляем поддержку циклических ссылок
                WriteIndented = true,
            };

            session.SetString(key, JsonSerializer.Serialize<T>(value, options));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // добавляем поддержку циклических ссылок
                WriteIndented = true,
            };

            return value == null ?
                default(T) :
                JsonSerializer.Deserialize<T>(value, options);
        }

        public static void Remove<T>(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}

