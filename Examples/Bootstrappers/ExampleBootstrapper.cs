using ChocolateECS;

namespace ChocolateExamples
{
    public class ExampleBootstrapper : Bootstrapper
    {
        public override void Awake()
        {
            #if UNITY_EDITOR
                RegisterSystem(new DebugStringSystem());
            #endif

            // Warning: Do not move this
            base.Awake();
        }
    }
}