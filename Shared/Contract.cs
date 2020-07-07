using System;

namespace Shared
{
    public class Contract
    {
        public static void Requires(bool requirement, string message)
        {
            if (!requirement)
                throw new Exception(message);
        }

        public static void Requires<T>(Func<bool> requirement, T arg, string message)
        {
            if (!requirement.Invoke())
                throw new Exception(string.Format(message, arg));
        }
    }
}