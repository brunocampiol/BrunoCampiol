using BrunoCampiol.CrossCutting.Common.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrunoCampiol.CrossCutting.Common.Logger
{
    public class Logger : ILogger
    {
        public async Task LogExceptionAsync(Exception ex, string errorMessage)
        {
            await Console.Error.WriteLineAsync($"[ERROR]: {errorMessage}, [STACK]: {ex.StackTrace}");
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            await Console.Error.WriteLineAsync($"[ERROR]: {ex.AllExceptionMessages()}, [STACK]: {ex.StackTrace}");
        }

        public async Task LogErrorAsync(string errorMessage)
        {
            await Console.Error.WriteLineAsync($"[ERROR]: {errorMessage}");
        }

        public async Task LogWarningAsync(string warningMessage)
        {
            await Console.Out.WriteLineAsync($"[WARNING]: {warningMessage}");
        }

        public async Task LogInfoAsync(string infoMessage)
        {
            await Console.Out.WriteLineAsync($"[INFO]: {infoMessage}");
        }

        public void LogException(Exception ex, string errorMessage)
        {
            Console.Error.WriteLine($"[ERROR]: {errorMessage}, [STACK]: {ex.StackTrace}");
        }

        public void LogException(Exception ex)
        {
            Console.Error.WriteLine($"[ERROR]: {ex.AllExceptionMessages()}, [STACK]: {ex.StackTrace}");
        }

        public void LogError(string errorMessage)
        {
            Console.Error.WriteLine($"[ERROR]: {errorMessage}");
        }

        public void LogWarning(string warningMessage)
        {
            Console.Out.WriteLine($"[WARNING]: {warningMessage}");
        }

        public void LogInfo(string infoMessage)
        {
            Console.Out.WriteLine($"[INFO]: {infoMessage}");
        }
    }
}
