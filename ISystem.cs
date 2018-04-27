using System.Collections.Generic;

namespace ChocolateECS
{
    public interface ISystem
    {
        void OnAwake(List<ISystem> systems);
        void OnStart();
        void OnUpdate();
        void OnFixedUpdate();
    }
}