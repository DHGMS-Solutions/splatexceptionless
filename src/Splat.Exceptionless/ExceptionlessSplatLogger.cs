using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Exceptionless;
using Exceptionless.Logging;
using LogLevel = Splat.LogLevel;

namespace Splat.Exceptionless
{

    /// <summary>
    /// Exceptionless Logger based upon code taken from ReactiveUI 5.
    /// </summary>
    [DebuggerDisplay("Name={SourceType} Level={Level}")]
    internal sealed class ExceptionlessSplatLogger : IFullLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionlessSplatLogger"/> class.
        /// </summary>
        /// <param name="sourceType">The type being tracked</param>
        /// <exception cref="ArgumentNullException">Type was not passed</exception>
        public ExceptionlessSplatLogger(Type sourceType)
        {
            this.SourceType = sourceType.FullName;
        }

        /// <summary>
        /// Gets or sets the logging level.
        /// </summary>
        public LogLevel Level { get; set; }

        public string SourceType { get; }

        /// <summary>
        /// Writes a message at the specified log level
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="logLevel">The log level to write the message at.</param>
        public void Write(String message, LogLevel logLevel)
        {
            this.CreateLog(message, RxUitoExceptionlessLevel(logLevel));
        }

        /// <summary>
        /// Writes a debug message using a generic argument
        /// </summary>
        /// <typeparam name="T">The type of the argument</typeparam>
        /// <param name="value">The argument to log</param>
        public void Debug<T>(T value)
        {
            this.CreateLog(value.ToString(), global::Exceptionless.Logging.LogLevel.Debug);
        }

        /// <summary>
        /// Writes a debug message using a generic argument
        /// </summary>
        /// <typeparam name="T">The type of the argument</typeparam>
        /// <param name="formatProvider">The format provider</param>
        /// <param name="value">The argument to log</param>
        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            var message = string.Format(formatProvider, "{0}", new[] {value});
            this.Debug(message);
        }

        /// <summary>
        /// Logs a debug message and exception.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception that has occurred</param>
        public void DebugException(String message, Exception exception)
        {
            this.CreateLogWithException(message, exception, global::Exceptionless.Logging.LogLevel.Debug);
        }

        /// <summary>
        /// Logs a debug message and array of arguments.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Debug(IFormatProvider formatProvider, String message, params Object[] args)
        {
            var value = string.Format(formatProvider, message, args);
            Debug(value);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Debug(String message)
        {
            this.CreateLog(message, global::Exceptionless.Logging.LogLevel.Debug);
        }

        /// <summary>
        /// Logs a debug message and array of arguments.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Debug(String message, params Object[] args)
        {
            var value = string.Format(message, args);
            Debug(value);
        }

        /// <summary>
        /// Logs a debug message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Debug<TArgument>(IFormatProvider formatProvider, String message, TArgument argument)
        {
            var value = string.Format(message, new[] { argument });
            Debug(value);
        }

        /// <summary>
        /// Logs a debug message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Debug<TArgument>(String message, TArgument argument)
        {
            this.CreateLog(message, argument, global::Exceptionless.Logging.LogLevel.Debug);
        }

        /// <summary>
        /// Logs a debug message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2 });
            Debug(value);
        }

        /// <summary>
        /// Logs a debug message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Debug<TArgument1, TArgument2>(String message, TArgument1 argument1, TArgument2 argument2)
        {
            this.CreateLog(message, argument1, argument2, global::Exceptionless.Logging.LogLevel.Debug);
        }

        /// <summary>
        /// Logs a debug message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Debug<TArgument1, TArgument2, TArgument3>(
            IFormatProvider formatProvider,
            String message,
            TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2, argument3 });
            Debug(value);
        }

        /// <summary>
        /// Logs a debug message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Debug<TArgument1, TArgument2, TArgument3>(String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            this.CreateLog(message, argument1, argument2, argument3, global::Exceptionless.Logging.LogLevel.Debug);
        }

        public void Info<T>(T value)
        {
            this.CreateLog(value.ToString(), global::Exceptionless.Logging.LogLevel.Info);
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            var message = string.Format(formatProvider, "{0}", new[] { value });
            this.Info(message);
        }

        /// <summary>
        /// Logs an information message and exception.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception that has occurred</param>
        public void InfoException(String message, Exception exception)
        {
            this.CreateLogWithException(message, exception, global::Exceptionless.Logging.LogLevel.Info);
        }

        /// <summary>
        /// Logs an information message and array of arguments.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Info(IFormatProvider formatProvider, String message, params Object[] args)
        {
            var value = string.Format(formatProvider, message, args);
            Info(value);
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Info(String message)
        {
            this.CreateLog(message, global::Exceptionless.Logging.LogLevel.Info);
        }

        /// <summary>
        /// Logs an information message and array of arguments.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Info(String message, params Object[] args)
        {
            var value = string.Format(message, args);
            Info(value);
        }

        public void Info<TArgument>(IFormatProvider formatProvider, String message, TArgument argument)
        {
            var value = string.Format(formatProvider, message, new[] { argument });
            Debug(value);
        }

        /// <summary>
        /// Logs an information message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Info<TArgument>(String message, TArgument argument)
        {
            this.CreateLog(message, argument, global::Exceptionless.Logging.LogLevel.Info);
        }

        /// <summary>
        /// Logs an information message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2 });
            Info(value);
        }

        /// <summary>
        /// Logs an information message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Info<TArgument1, TArgument2>(String message, TArgument1 argument1, TArgument2 argument2)
        {
            this.CreateLog(message, argument1, argument2, global::Exceptionless.Logging.LogLevel.Info);
        }

        /// <summary>
        /// Logs an information message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2, argument3 });
            Info(value);
        }

        /// <summary>
        /// Logs an information message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Info<TArgument1, TArgument2, TArgument3>(String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            this.CreateLog(message, argument1, argument2, argument3, global::Exceptionless.Logging.LogLevel.Info);
        }

        public void Warn<T>(T value)
        {
            this.CreateLog(value.ToString(), global::Exceptionless.Logging.LogLevel.Warn);
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            var message = string.Format(formatProvider, "{0}", new[] { value });
            this.Warn(message);
        }

        /// <summary>
        /// Logs a warning message and exception.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception that has occurred</param>
        public void WarnException(String message, Exception exception)
        {
            this.CreateLogWithException(message, exception, global::Exceptionless.Logging.LogLevel.Warn);
        }

        /// <summary>
        /// Logs a warning message and array of arguments.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Warn(IFormatProvider formatProvider, String message, params Object[] args)
        {
            var value = string.Format(formatProvider, message, args);
            Warn(value);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Warn(String message)
        {
            this.CreateLog(message, global::Exceptionless.Logging.LogLevel.Warn);
        }

        /// <summary>
        /// Logs a debug message and array of arguments.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Warn(String message, params Object[] args)
        {
            var value = string.Format(message, args);
            Warn(value);
        }

        /// <summary>
        /// Logs a warning message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Warn<TArgument>(IFormatProvider formatProvider, String message, TArgument argument)
        {
            var value = string.Format(formatProvider, message, new object[] { argument });
            Info(value);
        }

        /// <summary>
        /// Logs a warning message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Warn<TArgument>(String message, TArgument argument)
        {
            this.CreateLog(message, argument, global::Exceptionless.Logging.LogLevel.Warn);
        }

        /// <summary>
        /// Logs a warning message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2 });
            Warn(value);
        }

        /// <summary>
        /// Logs a warning message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Warn<TArgument1, TArgument2>(String message, TArgument1 argument1, TArgument2 argument2)
        {
            this.CreateLog(message, argument1, argument2, global::Exceptionless.Logging.LogLevel.Warn);
        }

        /// <summary>
        /// Logs a warning message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2, argument3 });
            Warn(value);
        }

        /// <summary>
        /// Logs a warning message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Warn<TArgument1, TArgument2, TArgument3>(String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            this.CreateLog(message, argument1, argument2, argument3, global::Exceptionless.Logging.LogLevel.Warn);
        }

        public void Error<T>(T value)
        {
            this.CreateLog(value.ToString(), global::Exceptionless.Logging.LogLevel.Error);
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            var message = string.Format(formatProvider, "{0}", new[] { value });
            this.Error(message);
        }

        /// <summary>
        /// Logs an error message and exception.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception that has occurred</param>
        public void ErrorException(String message, Exception exception)
        {
            this.CreateLogWithException(message, exception, global::Exceptionless.Logging.LogLevel.Error);
        }

        /// <summary>
        /// Logs an error message and array of arguments.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Error(IFormatProvider formatProvider, String message, params Object[] args)
        {
            var value = string.Format(formatProvider, message, args);
            Error(value);
        }

        /// <summary>
        /// Logs a error message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error(String message)
        {
            this.CreateLog(message, global::Exceptionless.Logging.LogLevel.Error);
        }

        /// <summary>
        /// Logs a debug message and array of arguments.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Error(String message, params Object[] args)
        {
            var value = string.Format(message, args);
            Error(value);
        }

        /// <summary>
        /// Logs an error message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Error<TArgument>(IFormatProvider formatProvider, String message, TArgument argument)
        {
            var value = string.Format(formatProvider, message, new object[] { argument });
            Error(value);
        }

        /// <summary>
        /// Logs an error message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Error<TArgument>(String message, TArgument argument)
        {
            this.CreateLog(message, argument, global::Exceptionless.Logging.LogLevel.Error);
        }

        /// <summary>
        /// Logs an error message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2 });
            Error(value);
        }

        /// <summary>
        /// Logs an error message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Error<TArgument1, TArgument2>(String message, TArgument1 argument1, TArgument2 argument2)
        {
            this.CreateLog(message, argument1, argument2, global::Exceptionless.Logging.LogLevel.Error);
        }

        /// <summary>
        /// Logs an error message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2, argument3 });
            Error(value);
        }

        /// <summary>
        /// Logs an error message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Error<TArgument1, TArgument2, TArgument3>(String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            this.CreateLog(message, argument1, argument2, argument3, global::Exceptionless.Logging.LogLevel.Error);
        }

        public void Fatal<T>(T value)
        {
            this.CreateLog(value.ToString(), global::Exceptionless.Logging.LogLevel.Error);
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            var message = string.Format(formatProvider, "{0}", new[] { value });
            this.Fatal(message);
        }

        /// <summary>
        /// Logs a fatal message and exception.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="exception">The exception that has occurred</param>
        public void FatalException(String message, Exception exception)
        {
            this.CreateLogWithException(message, exception, global::Exceptionless.Logging.LogLevel.Info);
        }

        /// <summary>
        /// Logs a fatal message and array of arguments.
        /// </summary>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Fatal(IFormatProvider formatProvider, String message, params Object[] args)
        {
            var value = string.Format(formatProvider, message, args);
            Fatal(value);
        }

        /// <summary>
        /// Logs a fatal message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Fatal(String message)
        {
            this.CreateLog(message, global::Exceptionless.Logging.LogLevel.Info);
        }

        /// <summary>
        /// Logs a fatal message and array of arguments.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="args">The argument array to log</param>
        public void Fatal(String message, params Object[] args)
        {
            var value = string.Format(message, args);
            Fatal(value);
        }

        /// <summary>
        /// Logs a fatal message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Fatal<TArgument>(IFormatProvider formatProvider, String message, TArgument argument)
        {
            var value = string.Format(formatProvider, message, new object[] { argument });
            Fatal(value);
        }

        /// <summary>
        /// Logs a fatal message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument">The type of argument</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument">The argument to log</param>
        public void Fatal<TArgument>(String message, TArgument argument)
        {
            this.CreateLog(message, argument, global::Exceptionless.Logging.LogLevel.Error);
        }

        /// <summary>
        /// Logs a fatal message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2 });
            Fatal(value);
        }

        /// <summary>
        /// Logs a fatal message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        public void Fatal<TArgument1, TArgument2>(String message, TArgument1 argument1, TArgument2 argument2)
        {
            this.CreateLog(message, argument1, argument2, global::Exceptionless.Logging.LogLevel.Error);
        }

        /// <summary>
        /// Logs a fatal message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="formatProvider">The format provider to use for the message</param>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            var value = string.Format(formatProvider, message, new object[] { argument1, argument2, argument3 });
            Fatal(value);
        }

        /// <summary>
        /// Logs a fatal message while passing generic arguments
        /// </summary>
        /// <typeparam name="TArgument1">The type of argument 1</typeparam>
        /// <typeparam name="TArgument2">The type of argument 2</typeparam>
        /// <typeparam name="TArgument3">The type of argument 3</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="argument1">The first argument to log</param>
        /// <param name="argument2">The second argument to log</param>
        /// <param name="argument3">The third argument to log</param>
        public void Fatal<TArgument1, TArgument2, TArgument3>(String message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            this.CreateLog(message, argument1, argument2, argument3, global::Exceptionless.Logging.LogLevel.Error);
        }

        private void CreateLog<TArgument1>(
            string message,
            TArgument1 argument1,
            global::Exceptionless.Logging.LogLevel level)
        {
            var formattedMessage = string.Format(message, new object[] { argument1 });
            CreateLog(formattedMessage, level);
        }

        private void CreateLog<TArgument1, TArgument2>(
            string message,
            TArgument1 argument1,
            TArgument2 argument2,
            global::Exceptionless.Logging.LogLevel level)
        {
            var formattedMessage = string.Format(message, new object[] { argument1, argument2, });
            CreateLog(formattedMessage, level);
        }

        private void CreateLog<TArgument1, TArgument2, TArgument3>(
            string message,
            TArgument1 argument1,
            TArgument2 argument2,
            TArgument3 argument3,
            global::Exceptionless.Logging.LogLevel level)
        {
            var formattedMessage = string.Format(message, new object[] {argument1, argument2, argument3});
            CreateLog(formattedMessage, level);
        }

        private static global::Exceptionless.Logging.LogLevel RxUitoExceptionlessLevel(LogLevel logLevel)
        {
            var mappings = new[]
                               {
                                   new Tuple<LogLevel, global::Exceptionless.Logging.LogLevel>(LogLevel.Debug, global::Exceptionless.Logging.LogLevel.Debug),
                                   new Tuple<LogLevel, global::Exceptionless.Logging.LogLevel>(LogLevel.Info, global::Exceptionless.Logging.LogLevel.Info),
                                   new Tuple<LogLevel, global::Exceptionless.Logging.LogLevel>(LogLevel.Warn, global::Exceptionless.Logging.LogLevel.Warn),
                                   new Tuple<LogLevel, global::Exceptionless.Logging.LogLevel>(LogLevel.Error, global::Exceptionless.Logging.LogLevel.Error),
                                   new Tuple<LogLevel, global::Exceptionless.Logging.LogLevel>(LogLevel.Fatal, global::Exceptionless.Logging.LogLevel.Error)
                               };

            return mappings.First(x => x.Item1 == logLevel).Item2;
        }

        private void CreateLog(string message, global::Exceptionless.Logging.LogLevel level)
        {
            ExceptionlessClient.Default.SubmitLog(this.SourceType, message, level);
        }

        private void CreateLogWithException(string message, Exception exception, global::Exceptionless.Logging.LogLevel level)
        {
            var reference = Guid.NewGuid();


            // exception is the parent
            var eventBuilder = ExceptionlessClient.Default.CreateException(exception);
            eventBuilder.SetReferenceId(reference.ToString());
            eventBuilder.Submit();

            // log event is the child
            var logger = ExceptionlessClient.Default.CreateLog(
                this.SourceType,
                message,
                global::Exceptionless.Logging.LogLevel.Debug);
            logger.SetEventReference("ReferenceId", reference.ToString());
            logger.Submit();
        }
    }
}
