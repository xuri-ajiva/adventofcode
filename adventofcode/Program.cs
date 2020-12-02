using System;
using System.IO;
using System.Linq;
using System.Reflection;
using adventofcode;
using Microsoft.Extensions.Logging;

ILogger<ISolver> log = new ConLogger();
foreach (var type in Assembly.GetAssembly(typeof(ISolver))?.GetTypes()!)
{
    if (type.GetInterfaces().Any(x => x == typeof(ISolver)))
    {
        var solver = (ISolver) Activator.CreateInstance(type);

        if (solver != null)
        {
            log.Log(LogLevel.Information, $" ## --- # {solver.GetType()} # --- ## ");

            solver.Logger = log;
            try
            {
                await solver.Init((await File.ReadAllLinesAsync($"Input/{solver.InFile}")).Where(x=> !string.IsNullOrEmpty(x)).ToArray());
            }
            catch (Exception e)
            {
                log.Log(LogLevel.Error, e, e.Message);
            }

            try
            {
                await solver.Run();
            }
            catch (Exception e)
            {
                log.Log(LogLevel.Error, e, e.Message);
            }
        }
    }
}


namespace adventofcode
{
    internal class ConLogger : ILogger<ISolver>
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"[{logLevel.ToString(),11}]: {state.ToString()}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
