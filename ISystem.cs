using System.Collections.Generic;

namespace ChocolateECS
{
    public interface ISystem
    {
        void OnAwake(List<ISystem> systems);
        void OnStart();
        void OnEnable();
        void OnUpdate(float deltaTime);
        void OnFixedUpdate();
        void OnDisable();
        void OnDestroy();
    }
}