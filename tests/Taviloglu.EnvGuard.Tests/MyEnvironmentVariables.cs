using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taviloglu.EnvGuard.Attributes;
using Taviloglu.EnvGuard.Base;

namespace Taviloglu.EnvGuard.Tests
{
    public class MyEnvironmentVariables : EnvGuardBase
    {
        [EnvGuardAttribute("ENVGUARD_TEST_MY_REQUIRED_VAR")]
        public static string MyRequiredVar { get; private set; }

        [EnvGuardAttribute("ENVGUARD_TEST_MY_BOOLEAN_VAR")]
        public static bool MyBooleanVar { get; private set; }

        [EnvGuardAttribute("ENVGUARD_TEST_MY_OPTIONAL_VAR", isRequired: false)]
        public static string? MyOptionalVar { get; private set; }
    }
}
