using Hackaton.Boilerplate.Abstraction.Internals;
using Hackaton.Boilerplate.Constraint;
using log4net;
using log4net.Appender;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Hackaton.Boilerplate.Shared.Internals
{
    public class Log4NetAdapter : ILogger
    {
        private readonly ILog _log = LogManager.GetLogger(
            MethodBase.GetCurrentMethod().DeclaringType
        );

        private string GetExceptionData(object message, Exception ex)
        {
            StringBuilder dataMessage = new StringBuilder();

            if (ex != null && ex.Data != null && ex.Data.Count > 0)
            {
                foreach (DictionaryEntry data in ex.Data)
                {
                    dataMessage.AppendFormat(
                        "{0}: {1}{2}", 
                        data.Key, 
                        data.Value, 
                        Environment.NewLine
                    );
                }
            }

            dataMessage.Append(message);
            return dataMessage.ToString();
        }

        public Log4NetAdapter()
        {
            Check.IsNull(() => _log);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Debug(object message, Exception ex)
        {
            var dataMessage = GetExceptionData(message, ex);
            _log.Debug(dataMessage, ex);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Error(object message, Exception ex)
        {
            var dataMessage = GetExceptionData(message, ex);
            _log.Error(dataMessage, ex);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Fatal(object message, Exception ex)
        {
            var dataMessage = GetExceptionData(message, ex);
            _log.Fatal(dataMessage, ex);
        }

        public string GetFileLogger()
        {
            var filename = string.Empty;

            IAppender[] appenders = _log.Logger.Repository.GetAppenders();

            for (int i = 0; i < appenders.Length && string.IsNullOrEmpty(filename); i++)
            {
                var appender = appenders[i];
                Type t = appender.GetType();

                if (t.Equals(typeof(FileAppender)) || t.Equals(typeof(RollingFileAppender)))
                {
                    filename = ((FileAppender)appender).File;
                }
            }

            return filename;
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Info(object message, Exception ex)
        {
            var dataMessage = GetExceptionData(message, ex);
            _log.Info(dataMessage, ex);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Warn(object message, Exception ex)
        {
            var dataMessage = GetExceptionData(message, ex);
            _log.Warn(dataMessage, ex);
        }
    }
}
