using FPTBlog.Utils.Interface;
using StackExchange.Redis;
using System;
using System.Reflection;

namespace FPTBlog.Utils
{
    public class RedisService: IRedisService
    {
        private readonly ConnectionMultiplexer Redis;
        private readonly IDatabase RedisDB;

        public RedisService(IConfig config)
        {
            Redis = ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    EndPoints = { config.GetEnvByKey("REDIS_URL") }
                });

            RedisDB = Redis.GetDatabase();
        }

        public bool SetByKey(string key, string value)
        {
            RedisDB.StringSet(key, value);
            string result = RedisDB.StringGet(key);
            if (!result.Equals(value)) return false;
            return true;
        }

        public string GetByKey(string key)
        {
            return RedisDB.StringGet(key);
        }

        public bool DeleteByKey(string key)
        {
            return RedisDB.KeyDelete(key);
        }

        public void SetObjectByKey(string key, object obj)
        {
            RedisDB.HashSet(key, this.ToHashEntries(obj));
            Console.WriteLine(RedisDB.HashGetAll(key));
        }

        public HashEntry[] ToHashEntries(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            var entries = new HashEntry[properties.Length];
            int i = 0;
            foreach (var property in properties)
            {
                object propertyValue = property.GetValue(obj);
                entries[i++] = new HashEntry(property.Name, property.GetValue(obj).ToString());
            }
            return entries;
        }
    }
}
