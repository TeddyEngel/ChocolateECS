using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public abstract class System<MainComponentType> : ISystem
    {
        MonoBehaviour[] mainComponents;
        protected int countMainComponents;
        Dictionary<Type, System.Object[]> secondaryComponents = new Dictionary<Type, System.Object[]>();

        public void OnGameObjectPreDestroyed(GameObject gameObject)
        {
            // TODO: Remove gameObject from mainComponents / secondaryComponents only where the type matches
        }

        public void OnGameObjectPostDestroyed(GameObject gameObject)
        {
            // TODO: Eventually, only refresh components with the same type as gameObject
            RefreshComponents();
        }

        public void RefreshComponents()
        {
            UpdateMainComponents();
            RefreshMainComponents();
            secondaryComponents.Clear();
            RefreshSecondaryComponents();
        }

        void UpdateMainComponents()
        {
            mainComponents = GameObject.FindObjectsOfType(typeof(MainComponentType)) as MonoBehaviour[];
            countMainComponents = mainComponents.Length;
        }

        protected abstract void RefreshMainComponents();
        protected abstract void RefreshSecondaryComponents();

        public virtual void OnAwake(List<ISystem> systems)
        {
            RefreshComponents();
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

        protected SecondaryComponentType[] RequireSecondaryComponent<SecondaryComponentType>()
        {
            Type classType = typeof(SecondaryComponentType);

            // Creates a list with the type of the secondary component
            secondaryComponents.Add(classType, new SecondaryComponentType[countMainComponents] as System.Object[]);
            
            // Check that main component also have this component
            for (int i = 0; i < countMainComponents; ++i)
            {
                MonoBehaviour mainComponent = mainComponents[i];

                SecondaryComponentType secondaryComponent = mainComponent.gameObject.GetComponent<SecondaryComponentType>();

                if (secondaryComponent == null)
                    Debug.LogError(classType.ToString() + " component missing!");
                else
                    secondaryComponents[classType][i] = secondaryComponent as System.Object;
            }
            return secondaryComponents[classType] as SecondaryComponentType[];
        }

        protected MainComponentType[] GetMainComponents()
        {
            return mainComponents as MainComponentType[];
        }

        
        protected SecondaryComponentType[] GetSecondaryComponents<SecondaryComponentType>()
        {
            Type classType = typeof(SecondaryComponentType);
            if (!secondaryComponents.ContainsKey(classType))
                return null;
            return secondaryComponents[classType] as SecondaryComponentType[];
        }        
    }
}