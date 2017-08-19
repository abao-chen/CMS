using log4net;

namespace CmsCommon
{
    public class Log
    {
        private readonly ILog logger;

        public Log(ILog log)
        {
            logger = log;
        }

        public void Debug(object message)
        {
            logger.Debug(message);
        }

        public void Info(object message)
        {
            logger.Info(message);
        }

        public void Warn(object message)
        {
            logger.Warn(message);
        }

        public void Error(object message)
        {
            logger.Error(message);
        }
    }
}