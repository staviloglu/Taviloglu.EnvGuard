using System;

namespace Taviloglu.EnvGuard.Exceptions
{
    public class EnvGuardMissingVariableException : Exception
    {
        public EnvGuardMissingVariableException(string variableName)
            : base($"Missing required environment variable: {variableName}")
        {
        }
    }
}
