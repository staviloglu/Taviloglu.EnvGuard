
using NuGet.Frameworks;
using Taviloglu.EnvGuard.Exceptions;

namespace Taviloglu.EnvGuard.Tests
{
    public class EnvGuardLoaderTests
    {
        [Fact]
        public void Load_ShouldThrow_WhenRequiredVariableIsMissing()
        {            
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_REQUIRED_VAR", null);
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_BOOLEAN_VAR", "false");

            var ex = Assert.Throws<EnvGuardMissingVariableException>(() =>
            {
                MyEnvironmentVariables.Load();
            });

            Assert.Contains("ENVGUARD_TEST_MY_REQUIRED_VAR", ex.Message);
        }

        [Fact]
        public void Load_ShouldSetStaticProperties_WhenVariablesExist()
        {            
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_REQUIRED_VAR", "TestValue");
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_BOOLEAN_VAR", "false");
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_OPTIONAL_VAR", "OptionalValue");
                        
            MyEnvironmentVariables.Load();
                        
            Assert.Equal("TestValue", MyEnvironmentVariables.MyRequiredVar);
            Assert.Equal("OptionalValue", MyEnvironmentVariables.MyOptionalVar);
            Assert.False(MyEnvironmentVariables.MyBooleanVar);
        }

        [Fact]
        public void Load_ShouldNotThrow_WhenOptionalVariableMissing()
        {            
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_REQUIRED_VAR", "TestValue");
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_BOOLEAN_VAR", "false");
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_OPTIONAL_VAR", null);
            
            var exception = Record.Exception(() => MyEnvironmentVariables.Load());
            
            Assert.Null(exception);
            Assert.Equal("TestValue", MyEnvironmentVariables.MyRequiredVar);
            Assert.False(MyEnvironmentVariables.MyBooleanVar);
            Assert.Null(MyEnvironmentVariables.MyOptionalVar);
        }
        
        [Fact]
        public void Load_ShouldThrow_WhenParsingFails()
        {
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_REQUIRED_VAR", "TestValue");            
            Environment.SetEnvironmentVariable("ENVGUARD_TEST_MY_BOOLEAN_VAR", "NotABoolean");

            var ex = Assert.Throws<EnvGuardVariableParsingException>(() =>
            {
                MyEnvironmentVariables.Load();
            });

            Assert.Contains("ENVGUARD_TEST_MY_BOOLEAN_VAR", ex.Message);
        }
    }
}