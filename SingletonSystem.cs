using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public abstract class SingletonSystem : ISystem
    {
        public void OnGameObjectInstantiated(GameObject gameObject)
        {

        }

        public void OnGameObjectPreDestroyed(GameObject gameObject)
        {

        }

        public void OnGameObjectPostDestroyed(GameObject gameObject)
        {

        }

        public void RefreshComponents()
        {
            
        }

        public virtual void OnAwake(List<ISystem> systems)
        {
        }

        public virtual void OnStart()
        {
            
        }

        public virtual void OnEnable()
        {
            
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }

        public virtual void OnLateUpdate(float deltaTime)
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnDestroy()
        {

        }        
    }
}