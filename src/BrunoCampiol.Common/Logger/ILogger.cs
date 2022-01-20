using System;
using System.Threading.Tasks;

namespace BrunoCampiol.CrossCutting.Common.Logger
{
    public interface ILogger
    {
        void LogError(string errorMessage);
        Task LogErrorAsync(string errorMessage);
        void LogException(Exception ex);
        void LogException(Exception ex, string errorMessage);
        Task LogExceptionAsync(Exception ex);
        Task LogExceptionAsync(Exception ex, string errorMessage);
        void LogInfo(string infoMessage);
        Task LogInfoAsync(string infoMessage);
        void LogWarning(string warningMessage);
        Task LogWarningAsync(string warningMessage);
    }
}