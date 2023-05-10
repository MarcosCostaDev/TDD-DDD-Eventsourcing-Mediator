using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace TheProject.Core.Loggers
{
    public class QueryLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new QueryLogger(categoryName);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    internal class QueryLogger : ILogger
    {
        private readonly string categoryName;

        public QueryLogger(string categoryName) => this.categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            Trace.WriteLine("---------------------------------- DBCONTEXT LOGGER BEGIN ---------------------------------------------------------------------");
            Trace.WriteLine($"{DateTime.Now.ToString("o")} {logLevel} {eventId.Id} {this.categoryName}");
            Trace.WriteLine(formatter(state, exception));
            Trace.WriteLine("---------------------------------- DBCONTEXT LOGGER ENDS ---------------------------------------------------------------------");
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
