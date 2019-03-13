using Framework.Context;
using Framework.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Repositories.Utils
{
    public interface ILoggerRepository : IRepository<Logger>
    {
        void AddInfomationLogger(String content);
        void AddWariningLogger(String content);
        void AddDanggerLogger(String content);
        List<Logger> GetLoggers();
    }
    public class LoggerRepository : BaseRepository<Logger>, ILoggerRepository
    {
        public LoggerRepository(FrameworkDbContext dbContext) :
            base(dbContext)
        {
        }
        public override Logger Add(Logger entity)
        {
            entity.Id = GenerateUniqueId();
            entity.CreationUserName = GetLoginedUserName();
            entity.CreationTime = DateTime.Now;
            return base.Add(entity);
        }
        public override void Update(Logger entity)
        {
            entity.ModifiedUserName = GetLoginedUserName();
            entity.ModifiedTime = DateTime.Now;
            base.Update(entity);
        }

        private void AddLogger(string content, string logType)
        {
            Logger logger = new Logger();
            logger.Content = content;
            logger.LogType = logType;
            this.Add(logger);
            dataContext.SaveChanges();
        }

        public void AddInfomationLogger(string content)
        {
            AddLogger(content, "infomation");
        }

        public void AddWariningLogger(string content)
        {
            AddLogger(content, "warning");
        }

        public void AddDanggerLogger(string content)
        {
            AddLogger(content, "danger");
        }

        public List<Logger> GetLoggers()
        {
            return GetAll().ToList();
        }
    }
}