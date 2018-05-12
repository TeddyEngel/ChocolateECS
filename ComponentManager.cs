using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public class ComponentManager
    {
        List<IComponent> _emptyList = new List<IComponent>(); // Just used for empty returns
        Dictionary<Type, List<IComponent>> _components = new Dictionary<Type, List<IComponent>>();
        Dictionary<Type, Dictionary<Type, List<IComponent>>> _secondaryComponents = new Dictionary<Type, Dictionary<Type, List<IComponent>>>();

        public ComponentManager()
        {
            RegisterFactoryHandlers();
        }

        ~ComponentManager()
        {
            UnregisterFactoryHandlers();
        }

        public void OnGameObjectInstantiated(GameObject gameObject)
        {
            AddGameObjectMainComponents(gameObject);
            AddGameObjectSecondaryComponents(gameObject);
        }

        public void OnGameObjectPreDestroyed(GameObject gameObject)
        {
            var gameObjectComponents = gameObject.GetComponents(typeof(IComponent));

            for (int i = 0; i < gameObjectComponents.Length; ++i)
            {
                for (int j = 0; j < gameObjectComponents.Length; ++j)
                {
                    if (i == j)
                        continue;
                    RemoveSecondaryComponent((gameObjectComponents[i]).GetType(), (IComponent)gameObjectComponents[j]);
                }
                RemoveMainComponent((IComponent)gameObjectComponents[i]);
            }
        }

        public void OnGameObjectPostDestroyed(GameObject gameObject)
        {
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
                AddGameObjectMainComponents(foundObjects[i]);
                AddGameObjectSecondaryComponents(foundObjects[i]);
            }
        }

        void AddGameObjectMainComponents(GameObject gameObject)
        {
            var gameObjectComponents = gameObject.GetComponents(typeof(IComponent));

            for (int i = 0; i < gameObjectComponents.Length; ++i)
                AddMainComponent((IComponent)gameObjectComponents[i]);
        }

        void AddMainComponent(IComponent mainComponent)
        {
            Type mainComponentType = mainComponent.GetType();

            if (!_components.ContainsKey(mainComponentType))
                _components.Add(mainComponentType, new List<IComponent>());
            _components[mainComponentType].Add(mainComponent);
        }

        void RemoveMainComponent(IComponent mainComponent)
        {
            Type mainComponentType = mainComponent.GetType();

            if (!_components.ContainsKey(mainComponentType))
                return;
            _components[mainComponentType].Remove(mainComponent);
            if (_components[mainComponentType].Count == 0)
                _components.Remove(mainComponentType);
        }

        void AddGameObjectSecondaryComponents(GameObject gameObject)
        {
            var gameObjectComponents = gameObject.GetComponents(typeof(IComponent));

            for (int i = 0; i < gameObjectComponents.Length; ++i)
            {
                for (int j = 0; j < gameObjectComponents.Length; ++j)
                {
                    if (i == j)
                        continue;
                    AddSecondaryComponent(gameObjectComponents[i].GetType(), (IComponent)gameObjectComponents[j]);
                }
            }
        }

        void AddSecondaryComponent(Type mainComponentType, IComponent secondaryComponent)
        {
            Type secondaryComponentType = secondaryComponent.GetType();

            if (mainComponentType == secondaryComponentType)
                return;
            if (!_secondaryComponents.ContainsKey(mainComponentType))
                _secondaryComponents.Add(mainComponentType, new Dictionary<Type, List<IComponent>>());
            if (!_secondaryComponents[mainComponentType].ContainsKey(secondaryComponentType))
                _secondaryComponents[mainComponentType].Add(secondaryComponentType, new List<IComponent>());
            if (!_secondaryComponents[mainComponentType][secondaryComponentType].Contains(secondaryComponent))
                _secondaryComponents[mainComponentType][secondaryComponentType].Add(secondaryComponent);
        }

        void RemoveSecondaryComponent(Type mainComponentType, IComponent secondaryComponent)
        {
            if (!_secondaryComponents.ContainsKey(mainComponentType))
                return;
            Type secondaryComponentType = secondaryComponent.GetType();
            if (!_secondaryComponents[mainComponentType].ContainsKey(secondaryComponentType))
                return;
            _secondaryComponents[mainComponentType][secondaryComponentType].Remove(secondaryComponent);
            if (_secondaryComponents[mainComponentType][secondaryComponentType].Count == 0)
                _secondaryComponents[mainComponentType].Remove(secondaryComponentType);
            if (_secondaryComponents[mainComponentType].Count == 0)
                _secondaryComponents.Remove(mainComponentType);
        }

        void RegisterFactoryHandlers()
        {
            ECSFactory.OnGameObjectInstantiated += OnGameObjectInstantiated;
            ECSFactory.OnGameObjectPreDestroyed += OnGameObjectPreDestroyed;
            ECSFactory.OnGameObjectPostDestroyed += OnGameObjectPostDestroyed;
        }

        void UnregisterFactoryHandlers()
        {
            ECSFactory.OnGameObjectInstantiated -= OnGameObjectInstantiated;
            ECSFactory.OnGameObjectPreDestroyed -= OnGameObjectPreDestroyed;
            ECSFactory.OnGameObjectPostDestroyed -= OnGameObjectPostDestroyed;
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
        }

        public void OnDestroy()
        {

        }

        public int CountComponents(Type type)
        {
            return GetComponents(type).Count;
        }

        public List<IComponent> GetComponents(Type type)
        {
            if (type == null)
                throw new ArgumentException();
            if (!_components.ContainsKey(type))
                return _emptyList;
            return _components[type];
        }

        public List<IComponent> GetDualComponents(Type mainType, Type secondaryType)
        {
            if (mainType == null
                || secondaryType == null)
                throw new ArgumentException();
            if (mainType == secondaryType)
                return _emptyList;
            if (!_secondaryComponents.ContainsKey(mainType))
                return _emptyList;
            if (!_secondaryComponents[mainType].ContainsKey(secondaryType))
                return _emptyList;
            return _secondaryComponents[mainType][secondaryType];
        }
    }
}