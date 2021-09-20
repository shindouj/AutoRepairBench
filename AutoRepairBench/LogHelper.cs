using System.Runtime.CompilerServices;
using MelonLoader;

namespace AutoRepairBench
{
    public class LogHelper
    {
        public static void Debug(string msg, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0)
        {
            MelonLogger.Msg($"[DEBUG] [{callerName}():{lineNumber}] {msg}");
        }
    }
}