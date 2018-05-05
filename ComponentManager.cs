using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public class ComponentManager
    {
        List<Component> _emptyList = new List<Component>(); // Just used for empty returns
        Dictionary<Type, List<Component>> _components = new Dictionary<Type, List<Component>>();
        Dictionary<Type, Dictionary<Type, List<Component>>> _secondaryComponents = new Dictionary<Type, Dictionary<Type, List<Component>>>();

        public void OnGameObjectInstantiated(GameObject gameObject)
        {
            Debug.Log("ComponentManager::OnGameObjectInstantiated");
            // TODO: Eventually, only add the game object to the list of _components that match
            // RefreshComponents();
            RefreshComponents();
        }

        public void OnGameObjectPreDestroyed(GameObject gameObject)
        {
            Debug.Log("ComponentManager::OnGameObjectPreDestroyed");
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
        }

        public void OnGameObjectPostDestroyed(GameObject gameObject)
        {
            Debug.Log("ComponentManager::OnGameObjectPostDestroyed");
            // TODO: Eventually, only refresh _components with the same type as gameObject
            // RefreshComponents();
            RefreshComponents();
        }

        void RefreshComponents()
        {
            // Clear previous data
            _components.Clear();
            _secondaryComponents.Clear();

            // Get all gameobjects
            var foundObjects = GameObject.FindObjectsOfType<GameObject>();

            // Create the main lists of _components
            for (int i = 0; i < foundObjects.Length; ++i)
            {
                var gameObjectComponents = foundObjects[i].GetComponents(typeof(Component));

                for (int j = 0; j < gameObjectComponents.Length; ++j)
                {
                    Type componentType = gameObjectComponents[j].GetType();

                    if (!_components.ContainsKey(componentType))
                    {
                        _components.Add(componentType, new List<Component>()); 
                        _secondaryComponents.Add(componentType, new Dictionary<Type, List<Component>>()); 
                    }
                    _components[componentType].Add(gameObjectComponents[j]);
                }
            }

            // Go through main component types
            foreach (KeyValuePair<Type, List<Component>> entry in _components)
            {
                Type mainType = entry.Key;

                //  For each type, go through _components
                foreach (Component mainComponent in entry.Value)
                {
                    // For each component, try and get all possible component type
                    foreach (Type secondaryType in _components.Keys)
                    {
                        if (secondaryType == mainType)
                            continue;
                        Component secondaryComponent = mainComponent.gameObject.GetComponent(secondaryType);
                        if (secondaryComponent == null)
                            continue;
                        if (!_secondaryComponents[mainType].ContainsKey(secondaryType))
                            _secondaryComponents[mainType].Add(secondaryType, new List<Component>());
                        _secondaryComponents[mainType][secondaryType].Add(secondaryComponent);
                    }
                }
            }

            /*
            foreach (KeyValuePair<Type, Dictionary<Type, List<Component>>> mainEntry in _secondaryComponents)
            {
                Debug.Log("The main type: " + mainEntry.Key);
                foreach (KeyValuePair<Type, List<Component>> secondaryEntry in mainEntry.Value)
                {
                    Debug.Log("Has " + secondaryEntry.Value.Count + " entries with secondary type " + secondaryEntry.Key);
                }
            }
            */
        }

        void RegisterFactoryHandlers()
        {
            GameObjectFactory.OnGameObjectInstantiated += OnGameObjectInstantiated;
            GameObjectFactory.OnGameObjectPreDestroyed += OnGameObjectPreDestroyed;
            GameObjectFactory.OnGameObjectPostDestroyed += OnGameObjectPostDestroyed;
        }

        void UnregisterFactoryHandlers()
        {
            GameObjectFactory.OnGameObjectInstantiated -= OnGameObjectInstantiated;
            GameObjectFactory.OnGameObjectPreDestroyed -= OnGameObjectPreDestroyed;
            GameObjectFactory.OnGameObjectPostDestroyed -= OnGameObjectPostDestroyed;
        }

        public void OnAwake()
        {
            RefreshComponents();
        }

        public void OnStart()
        {
            
        }

        public void OnEnable()
        {
            RegisterFactoryHandlers();
        }

        public void OnUpdate(float deltaTime)
        {

        }

        public void OnLateUpdate(float deltaTime)
        {

        }

        public void OnFixedUpdate()
        {

        }

        public void OnDisable()
        {
            UnregisterFactoryHandlers();
        }

        public void OnDestroy()
        {

        }

        public int CountComponents(Type type)
        {
            return GetComponents(type).Count;
        }

        public List<Component> GetComponents(Type type)
        {
            if (!_components.ContainsKey(type))
                return _emptyList;
            return _components[type];
        }

        public List<Component> GetSecondaryComponents(Type mainType, Type secondaryType)
        {
            if (!_secondaryComponents.ContainsKey(mainType))
                return _emptyList;
            if (!_secondaryComponents[mainType].ContainsKey(secondaryType))
                return _emptyList;
            return _secondaryComponents[mainType][secondaryType];
        }
    }
}