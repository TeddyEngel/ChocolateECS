using System;
using System.Collections.Generic;

namespace ChocolateECS
{
    public interface ISystem
    {
        Action OnComponentDestroyed
        {
            get; set;
        }
        
        void RefreshComponents();
        void OnAwake(List<ISystem> systems);
        void OnStart();
        void OnEnable();
        void OnUpdate(float deltaTime);
        void OnLateUpdate(float deltaTime);
        void OnFixedUpdate();
        void OnDisable();
        void OnDestroy();
    }
}