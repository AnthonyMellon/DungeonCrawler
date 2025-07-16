using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.Code.Utils
{
    internal static class ObjectBin
    {
        private static Dictionary<string, object> _objects = new Dictionary<string, object>();

        public static void RegisterObject(string name, object obj)
        {            
            _objects.Add(name, obj);
        }

        public static void DeregisterObject(string name)
        {
            _objects.Remove(name);
        }

        public static object GetObject(string name)
        {
            if (_objects.ContainsKey(name)) return _objects[name];

            return null;
        }

        public static List<T> GetObjects<T>() where T : class
        {
            List<T> returnObjects = new List<T>();

            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects.ElementAt(i).GetType() == typeof(T)) returnObjects.Add(_objects.ElementAt(i).Value as T);
            }
            return returnObjects;
        }
    }
}
