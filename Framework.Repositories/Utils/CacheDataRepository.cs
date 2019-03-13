using Framework.Context;
using Framework.Models.Utils;
using Framework.Utils;
using Newtonsoft.Json;
using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace Framework.Repositories.Utils
{
    public interface ICacheDataRepository : IRepository<CacheData>
    {
        void AddCache(String key, String value, double daysAlive);
        void AddCache<T>(String key, T value, double daysAlive) where T : class;
        void AddCacheList<T>(String key, List<T> list, double daysAlive) where T : class;
        List<T> GetCacheList<T>(String key) where T : class;
        void InvalidateCache(String key);
        String GetValue(String key);
        T GetValue<T>(String key) where T : class;
    }
    public class CacheDataRepository : BaseRepository<CacheData>, ICacheDataRepository
    {
        public CacheDataRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
        public override CacheData Add(CacheData entity)
        {
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            return base.Add(entity);
        }
        public override void Update(CacheData entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }

        public void AddCache(string key, string value, double daysAlive)
        {
            CacheData cacheData = new CacheData();
            DateTime expiredDate = DateTime.Now.AddDays(daysAlive);
            cacheData.Key = key;
            cacheData.Value = value;
            cacheData.Expired = false;
            Add(cacheData);
            DbContext.SaveChanges();
        }

        public void AddCache<T>(string key, T value, double daysAlive) where T : class
        {
            String json = JsonConvert.SerializeObject(value);
            AddCache(key, json, daysAlive);
        }

        public void AddCacheList<T>(string key, List<T> list, double daysAlive) where T : class
        {
            //String content = CsvExtension.GetContentFromList(list);
            String content = CsvSerializer.SerializeToString(list);
            AddCache(key, content, daysAlive);
        }

        public T GetValue<T>(string key) where T : class
        {
            var cache = GetSingleByCondition(x => x.Key == key);
            if (cache == null || cache.Expired || cache.ExpiredDate > DateTime.Now)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(cache.Value);
        }

        public string GetValue(string key)
        {
            var cache = GetSingleByCondition(x => x.Key == key);
            if (cache == null || cache.Expired || cache.ExpiredDate > DateTime.Now)
            {
                return null;
            }
            return cache.Value;
        }

        public void InvalidateCache(string key)
        {
            var cache = GetSingleByCondition(x => x.Key == key);
            cache.Expired = true;
            Update(cache);
            DbContext.SaveChanges();
        }

        public List<T> GetCacheList<T>(string key) where T : class
        {
            string content = GetValue(key);
            if (content == null)
                return null;
            return CsvSerializer.DeserializeFromString<List<T>>(content);
        }
    }
}