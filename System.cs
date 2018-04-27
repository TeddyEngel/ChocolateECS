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

        public virtual void OnAwake(List<ISystem> systems)
        {
            // Find primary components
            mainComponents = GameObject.FindObjectsOfType(typeof(MainComponentType)) as MonoBehaviour[];
            countMainComponents = mainComponents.Length;
        }

        public abstract void OnStart();

        public abstract void OnUpdate();

        public abstract void OnFixedUpdate();

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