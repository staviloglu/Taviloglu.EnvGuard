using System;

namespace Taviloglu.EnvGuard.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EnvGuardAttribute : Attribute
    {
        public string Name { get; }
        public bool IsRequired { get; }
        public string Description { get; }

        public EnvGuardAttribute(string name, bool isRequired = true, string description = "")
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsRequired = isRequired;
            Description = description;
        }
    }
}