using Framework.Models;
using Framework.Models.Utils;
using Framework.Repositories;
using Framework.Repositories.Utils;
using Framework.Repositories.Infrastructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.Utils
{
    public interface ILoggerService
    {
        void AddInfomationLogger(String content);
        void AddWariningLogger(String content);
        void AddDanggerLogger(String content);
        List<Logger> GetLoggers();
    }
    public class LoggerService : ILoggerService
    {
        IUnitOfWork unitOfWork;
        ILoggerRepository loggerRepository;

        public LoggerService(IUnitOfWork unitOfWork, ILoggerRepository loggerRepository)
        {
            this.unitOfWork = unitOfWork;
            this.loggerRepository = loggerRepository;
        }

        public void AddInfomationLogger(string content)
        {
            this.loggerRepository.AddInfomationLogger(content);
        }

        public void AddWariningLogger(string content)
        {
            this.loggerRepository.AddWariningLogger(content);
        }

        public void AddDanggerLogger(string content)
        {
            this.loggerRepository.AddDanggerLogger(content);
        }

        public List<Logger> GetLoggers()
        {
            return this.loggerRepository.GetLoggers();
        }
    }
}
