using Taviloglu.EnvGuard.Attributes;
using Taviloglu.EnvGuard.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace Taviloglu.EnvGuard.Core
{
    internal static class EnvGuardLoader
    {
        public static void LoadStatic()
        {
            var derivedType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.IsSubclassOf(typeof(Base.EnvGuardBase)));

            if (derivedType == null)
                throw new InvalidOperationException("No class inheriting from EnvironmentVariablesBase found.");

            var properties = derivedType.GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<EnvGuardAttribute>();
                if (attribute == null)
                    continue;

                var envVarValue = Environment.GetEnvironmentVariable(attribute.Name);

                if (string.IsNullOrWhiteSpace(envVarValue))
                {
                    if (attribute.IsRequired)
                        throw new EnvGuardMissingVariableException(attribute.Name);
                    else
                        continue;
                }

                object parsedValue;
                try
                {
                    parsedValue = ConvertToPropertyType(property.PropertyType, envVarValue);
                }
                catch (Exception ex)
                {
                    throw new EnvGuardVariableParsingException(attribute.Name, property.PropertyType, ex);
                }

                property.SetValue(null, parsedValue);
            }
        }

        private static object ConvertToPropertyType(Type propertyType, string value)
        {
            if (propertyType == typeof(string))
                return value;
            if (propertyType == typeof(int))
                return int.Parse(value);
            if (propertyType == typeof(long))
                return long.Parse(value);
            if (propertyType == typeof(double))
                return double.Parse(value);
            if (propertyType == typeof(decimal))
                return decimal.Parse(value);
            if (propertyType == typeof(bool))
                return bool.Parse(value);
            if (propertyType == typeof(TimeSpan))
                return TimeSpan.Parse(value);
            if (propertyType == typeof(Guid))
                return Guid.Parse(value);

            throw new NotSupportedException($"Unsupported property type: {propertyType.Name}");
        }
    }
}
