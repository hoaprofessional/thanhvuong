using Framework.Models;
using Framework.Models.Utils;
using Framework.Repositories;
using Framework.Repositories.Utils;
using Framework.Repositories.Infrastructor;
using Framework.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.Utils
{
    public interface ICacheService
    {
        void AddCache(String key, String value, double daysAlive);
        void AddCache<T>(String key, T value, double daysAlive) where T : class;
        void AddCacheList<T>(String key, List<T> list, double daysAlive) where T : class;
        void InvalidateCache(String key);
        String GetValue(String key);
        T GetValue<T>(String key) where T : class;
    }
    public class CacheService : ICacheService
    {
        readonly ILoggerService loggerService;
        private ICacheDataRepository cacheDataRepository;
        private IUnitOfWork unitOfWork;

        public CacheService(ILoggerService loggerService,
            ICacheDataRepository cacheDataRepository,
            IUnitOfWork unitOfWork)
        {
            this.loggerService = loggerService;
            this.cacheDataRepository = cacheDataRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddCache(string key, string value, double daysAlive)
        {
            CacheData cacheData = new CacheData();
            DateTime expiredDate = DateTime.Now.AddDays(daysAlive);
            cacheData.Key = key;
            cacheData.Value = value;
            cacheData.Expired = false;
            cacheDataRepository.Add(cacheData);
            unitOfWork.Commit();
        }

        public void AddCache<T>(string key, T value, double daysAlive) where T : class
        {
            String json = JsonConvert.SerializeObject(value);
            AddCache(key, json, daysAlive);
        }

        public void AddCacheList<T>(string key, List<T> list, double daysAlive) where T : class
        {
            String content = CsvExtension.GetContentFromList(list);
            AddCache(key, content, daysAlive);
        }

        public T GetValue<T>(string key) where T : class
        {
            var cache = cacheDataRepository.GetSingleByCondition(x => x.Key == key);
            if(cache==null || cache.Expired || cache.ExpiredDate > DateTime.Now)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(cache.Value);
        }

        public string GetValue(string key)
        {
            var cache = cacheDataRepository.GetSingleByCondition(x => x.Key == key);
            if (cache == null || cache.Expired || cache.ExpiredDate > DateTime.Now)
            {
                return null;
            }
            return cache.Value;
        }

        public void InvalidateCache(string key)
        {
            var cache = cacheDataRepository.GetSingleByCondition(x => x.Key == key);
            cache.Expired = true;
            cacheDataRepository.Update(cache);
            unitOfWork.Commit();
        }
    }
}
