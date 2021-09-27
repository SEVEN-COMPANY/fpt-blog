using StackExchange.Redis;

namespace FPTBlog.Utils.Interface {
    public interface IRedisService {
        public bool SetByKey(string key, string value);
        public string GetByKey(string key);
        public bool DeleteByKey(string key);
        public void SetObjectByKey(string key, object obj);
        public HashEntry[] ToHashEntries(object obj);
    }
}
