using System;

namespace Taviloglu.EnvGuard.Exceptions
{
    public class EnvGuardVariableParsingException : Exception
    {
        public EnvGuardVariableParsingException(string variableName, Type targetType, Exception innerException)
            : base($"Failed to parse environment variable '{variableName}' as {targetType.Name}.", innerException)
        {
        }
    }
}
