﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;

using Microsoft.Extensions.Logging;

#nullable enable

namespace ThePlague.Networking.Connections
{
    public static class LoggerExtensions
    {
        public static ILogger? CreateLogger<T>(this IServiceProvider serviceProvider)
        {
            ILoggerFactory? loggerFactory = (ILoggerFactory?)serviceProvider.GetService(typeof(ILoggerFactory));

            if (loggerFactory is null)
            {
                return (ILogger?)serviceProvider.GetService(typeof(ILogger));
            }
            else
            {
                return loggerFactory?.CreateLogger<T>();
                
            }
        }

        [Conditional("TRACE")]
        public static void TraceLog
        (
            this ILogger logger,
            string identifier,
            string message,
            [CallerFilePath] string? file = null,
            [CallerMemberName] string? caller = null,
            [CallerLineNumber] int lineNumber = 0
        )
            => TraceLog(logger, identifier, message, $"{System.IO.Path.GetFileName(file)}:{caller}#{lineNumber}");

        public static void TraceLog
        (
            this ILogger logger,
            string identifier,
            string message,
            string caller
        )
        {
#if TRACE
            logger.LogTrace($"[{Thread.CurrentThread.ManagedThreadId.ToString()}, {identifier}, {caller}] {message}");
#endif
        }
    }
}