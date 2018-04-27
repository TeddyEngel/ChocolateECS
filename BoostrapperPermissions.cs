using System;

namespace ChocolateECS
{
    [Flags] public enum BootstrapperPermission
    {
        Awake = 1,
        Start = 2,
        Enable = 4,
        Update = 8,
        FixedUpdate = 16,
        Disable = 32,
        Destroy = 64
    }
}