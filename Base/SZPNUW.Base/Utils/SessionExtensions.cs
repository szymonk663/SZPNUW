using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SZPNUW.Base
{
    public static class SessionExtensions
    {
        public static void AddItem<T>(this ISession session, T item) where T : class
        {
            session.SetString(GetKey(item.GetType()), JsonConvert.SerializeObject(item));
        }

        public static T GetItem<T>(this ISession session) where T : class
        {
            if(session != null)
            {
                string data = session.GetString(GetKey(typeof(T)));
                if (data.HasValue())
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
            }
            return null;
        }

        public static T GetItemLazy<T>(this ISession session) where T : class
        {
            if (session != null)
            {
                Type type = typeof(T);
                if (!session.Keys.Any(x => x == GetKey(type)))
                {
                    T newType = Activator.CreateInstance<T>();
                    session.AddItem(newType);
                }
                return session.GetItem<T>();
            }
            return null;
        }

        public static void DeleteItem<T>(this ISession session) where T : class
        {
            session.Remove(GetKey(typeof(T)));
        }

        public static void RefreshItem<T>(this ISession session, T item) where T : class
        {
            if(session != null)
            {
                session.DeleteItem<T>();
                session.AddItem<T>(item);
            }
        }

        public static void ChangeItem<T>(this ISession session, Action<T> action) where T : class
        {
            T item = session.GetItem<T>();
            if(item != null)
            {
                T clonedItem = ((object)item).Clone() as T;
                if(clonedItem != null)
                {
                    action(clonedItem);
                    session.RefreshItem(clonedItem);
                }
            }
        }

        private static string GetKey(Type itemType)
        {
            return itemType.FullName;
        }
    }
}
