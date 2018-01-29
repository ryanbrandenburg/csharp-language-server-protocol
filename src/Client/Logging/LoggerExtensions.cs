using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace OmniSharp.Extensions.LanguageServer.Client.Logging
{
    /// <summary>
    ///     Extension methods for <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        ///     <see cref="EventId"/> representing a generic error event.
        /// </summary>
        public static EventId GenericErrorEventId = new EventId(500);

        /// <summary>
        ///     Log an error.
        /// </summary>
        /// <param name="logger">
        ///     The <see cref="ILogger"/>.
        /// </param>
        /// <param name="exception">
        ///     The exception (if any) associated with the error.
        /// </param>
        /// <param name="message">
        ///     The log message.
        /// </param>
        /// <param name="args">
        ///     The message format arguments (if any).
        /// </param>
        public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.LogError(GenericErrorEventId, exception, message, args);
        }

        class Disposable : IDisposable
        {
            private readonly IDisposable _disposable;
            private readonly Action<long> _action;
            private readonly Stopwatch _sw;

            public Disposable(IDisposable disposable, Action<long> action)
            {
                _disposable = disposable;
                _action = action;
                _sw = new Stopwatch();
                _sw.Start();
            }

            public void Dispose()
            {
                _sw.Stop();
                _action(_sw.ElapsedMilliseconds);
                _disposable.Dispose();
            }
        }

        /// <summary>
        /// Times the trace.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>IDisposable.</returns>
        internal static IDisposable TimeTrace(this ILogger logger, string message, params object[] args)
        {
            var scope = logger.BeginScope(new { });
            logger.LogTrace($"Starting: {message}", args);
            return new Disposable(scope, elapsed =>
            {
                var a = args.Concat(new object[] { elapsed }).ToArray();
                logger.LogTrace($"Finished: {message} in {{ElapsedMilliseconds}}ms", a);
            });
        }

        /// <summary>
        /// Times the debug.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>IDisposable.</returns>
        internal static IDisposable TimeDebug(this ILogger logger, string message, params object[] args)
        {
            var scope = logger.BeginScope(new { });
            logger.LogDebug($"Starting: {message}", args);
            return new Disposable(scope, elapsed =>
            {
                var a = args.Concat(new object[] { elapsed }).ToArray();
                logger.LogDebug($"Finished: {message} in {{ElapsedMilliseconds}}ms", a);
            });
        }

        /// <summary>
        /// Times the information.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>IDisposable.</returns>
        internal static IDisposable TimeInformation(this ILogger logger, string message, params object[] args)
        {
            var scope = logger.BeginScope(new { });
            logger.LogInformation($"Starting: {message}", args);
            return new Disposable(scope, elapsed =>
            {
                var a = args.Concat(new object[] { elapsed }).ToArray();
                logger.LogInformation($"Finished: {message} in {{ElapsedMilliseconds}}ms", a);
            });
        }
    }
}
