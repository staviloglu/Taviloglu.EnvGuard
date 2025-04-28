using Taviloglu.EnvGuard.Core;

namespace Taviloglu.EnvGuard.Base
{
    public abstract class EnvGuardBase
    {
        public static void Load()
        {
            EnvGuardLoader.LoadStatic();
        }
    }
}
