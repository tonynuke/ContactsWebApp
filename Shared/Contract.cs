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
    }
}