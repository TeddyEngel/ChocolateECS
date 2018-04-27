using System;

namespace ChocolateECS
{
    [Flags] public enum BootstrapperPermission
    {
        Awake = 1, // 0001
        Start = 2, // 0010
        Update = 4, // 0100
        FixedUpdate = 8 // 1000
    }
}