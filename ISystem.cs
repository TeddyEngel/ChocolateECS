using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public interface ISystem
    {
        void OnGameObjectInstantiated(GameObject gameObject);
        void OnGameObjectPreDestroyed(GameObject gameObject);
        void OnGameObjectPostDestroyed(GameObject gameObject);
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