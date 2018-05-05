using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public abstract class ECSSystem : ISystem
    // public abstract class System<MainComponentType> : ISystem
    {
        // MonoBehaviour[] mainComponents;
        // protected int countMainComponents;
        // Dictionary<Type, System.Object[]> secondaryComponents = new Dictionary<Type, System.Object[]>();

        public ComponentManager ComponentManager
        {
            get; set;
        }

        // TODO: Fix this, there is a bug when I try to just remove the component from the list, not sure why
        // int CountGameObjectInMainComponents(GameObject gameObject)
        // {
        //     int countGameObject = 0;

        //     for (int i = 0; i < mainComponents.Length; ++i)
        //     {
        //         if (mainComponents[i].gameObject.GetInstanceID() == gameObject.GetInstanceID())
        //             ++countGameObject;
        //     }
        //     return countGameObject;
        // }

        // void RemoveGameObjectsFromMainComponents(GameObject gameObject)
        // {
        //     int countGameObjects = CountGameObjectInMainComponents(gameObject);

        //     if (countGameObjects == 0)
        //         return;
        //     MonoBehaviour[] newBehaviours = new MonoBehaviour[countMainComponents - countGameObjects];
        //     for (int i = 0, j = 0; i < countMainComponents; ++i)
        //     {
        //         if (mainComponents[i].gameObject.GetInstanceID() != gameObject.GetInstanceID())
        //         {
        //             Debug.Log("NewB[" + j + "] equals to mainComponents["+i+"]");
        //             newBehaviours[j] = mainComponents[i];
        //             j++;
        //         }
        //     }
        //     mainComponents = (MonoBehaviour[])newBehaviours.Clone();
        //     countMainComponents = mainComponents.Length;
        // }

        // public void OnGameObjectInstantiated(GameObject gameObject)
        // {
            // TODO: Eventually, only add the game object to the list of components that match
        //     RefreshComponents();
        // }

        // public void OnGameObjectPreDestroyed(GameObject gameObject)
        // {
            // Debug.Log("Before: " + countMainComponents);
            // for (int i = 0; i < countMainComponents; ++i)
            // {
            //     Debug.Log(mainComponents[i].gameObject.GetInstanceID());
            // }
            // RemoveGameObjectsFromMainComponents(gameObject);
            // Debug.Log("After: " + countMainComponents);
            // for (int i = 0; i < countMainComponents; ++i)
            // {
            //     Debug.Log(mainComponents[i].gameObject.GetInstanceID());
            // }
            // RefreshMainComponents();
        // }

        // public void OnGameObjectPostDestroyed(GameObject gameObject)
        // {
            // TODO: Eventually, only refresh components with the same type as gameObject
        //     RefreshComponents();
        // }

        // public void RefreshComponents()
        // {
        //     UpdateMainComponents();
        //     RefreshMainComponents();
        //     secondaryComponents.Clear();
        //     RefreshSecondaryComponents();
        // }

        // void UpdateMainComponents()
        // {
        //     mainComponents = GameObject.FindObjectsOfType(typeof(MainComponentType)) as MonoBehaviour[];
        //     countMainComponents = mainComponents.Length;
        // }

        // protected abstract void RefreshMainComponents();
        // protected abstract void RefreshSecondaryComponents();

        public virtual void OnAwake(List<ISystem> systems)
        {
            // RefreshComponents();
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

        /*
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
        */       
    }
}