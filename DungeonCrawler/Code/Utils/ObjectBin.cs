using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.Code.Utils
{
    internal static class ObjectBin
    {

        private static Dictionary<string, object> _objects = new Dictionary<string, object>();

        /// <summary>
        /// Register an object to the bin
        /// </summary>
        /// <param name="name">Name of object</param>
        /// <param name="obj">The object to register</param>
        public static void RegisterObject(string name, object obj)
        {
            _objects.Add(name, obj);
        }

        /// <summary>
        /// Deregister object of name
        /// </summary>
        /// <param name="name">name of object to deregister</param>
        public static void DeregisterObject(string name)
        {
            if (_objects.ContainsKey(name))
            {
                _objects.Remove(name);
            }
        }

        /// <summary>
        /// Deregister first object of type T
        /// </summary>
        /// <typeparam name="T">type of object to deregister</typeparam>
        public static void DeregisterObject<T>() where T : class
        {
            string name = GetObject<T>()?.name;

            if (name != null)
            {
                DeregisterObject(name);
            }
        }

        /// <summary>
        /// Get object by name>
        /// </summary>
        /// <param name="name">name of object to get</param>
        /// <returns>Object of name or null if none found</returns>
        public static object GetObject(string name)
        {
            object obj = null;

            if (_objects.ContainsKey(name))
            {
                obj = _objects[name];
            }

            return obj;
        }

        /// <summary>
        /// Get first object of type T
        /// </summary>
        /// <typeparam name="T">type of object to be found</typeparam>
        /// <returns>Object of type T or null if none found</returns>
        public static (string name, T obj)? GetObject<T>() where T : class
        {
            // Check all objects for the first one of type T>
            for (int i = 0; i < _objects.Count; i++)
            {
                object currentObject = _objects.ElementAt(i).Value;
                if (currentObject.GetType() == typeof(T))
                {
                    // Object found, return it
                    return (_objects.ElementAt(i).Key, currentObject as T);
                }
            }

            // no object found
            return null;
        }
    }
}
