using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Taviloglu.EnvGuard.Base;
using Taviloglu.EnvGuard.Attributes;

namespace Taviloglu.EnvGuard.Benchmarks
{
    public class EnvironmentVariableBenchmarks
    {
        [GlobalSetup]
        public void Setup()
        {
            // Set environment variable before starting
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_REQUIRED_VAR", "BenchmarkValue");
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_BOOLEAN_VAR", "false");
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_OPTIONAL_VAR", "OptionalValue");

            // Load into static properties
            MyEnvironmentVariables.Load();
        }

        [Benchmark]
        public string StaticPropertyAccess()
        {
            return MyEnvironmentVariables.MyRequiredVar;
        }

        [Benchmark]
        public string GetEnvironmentVariableAccess()
        {
            return Environment.GetEnvironmentVariable("ENVGUARD_TEST_MY_REQUIRED_VAR")!;
        }
    }
    public class MyEnvironmentVariables : EnvGuardBase
    {
        [EnvGuardAttribute("ENVGUARD_TEST_MY_REQUIRED_VAR")]
        public static string MyRequiredVar { get; private set; }

        [EnvGuardAttribute("ENVGUARD_TEST_MY_BOOLEAN_VAR")]
        public static bool MyBooleanVar { get; private set; }

        [EnvGuardAttribute("ENVGUARD_TEST_MY_OPTIONAL_VAR", isRequired: false)]
        public static string? MyOptionalVar { get; private set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<EnvironmentVariableBenchmarks>();
        }
    }
}
