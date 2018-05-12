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
        List<Type> _types = new List<Type>();

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
            var gameObjectComponents = gameObject.GetComponents(typeof(Component));

            for (int i = 0; i < gameObjectComponents.Length; ++i)
            {
                for (int j = 0; j < _types.Count; ++j)
                    RemoveSecondaryComponent(_types[j], gameObjectComponents[i]);
                RemoveMainComponent(gameObjectComponents[i]);
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
            _types.Clear();

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
            var gameObjectComponents = gameObject.GetComponents(typeof(Component));

            for (int i = 0; i < gameObjectComponents.Length; ++i)
                AddMainComponent(gameObjectComponents[i]);
        }

        void AddMainComponent(Component mainComponent)
        {
            Type mainComponentType = mainComponent.GetType();

            if (!_components.ContainsKey(mainComponentType))
            {
                _components.Add(mainComponentType, new List<Component>());
                if (!_types.Contains(mainComponentType))
                    _types.Add(mainComponentType);
            }
            _components[mainComponentType].Add(mainComponent);
        }

        void RemoveMainComponent(Component mainComponent)
        {
            Type mainComponentType = mainComponent.GetType();

            if (!_components.ContainsKey(mainComponentType))
                return;
            _components[mainComponentType].Remove(mainComponent);
            if (_components[mainComponentType].Count == 0)
            {
                _components.Remove(mainComponentType);
                _types.Remove(mainComponentType);
            }
        }

        void AddGameObjectSecondaryComponents(GameObject gameObject)
        {
            var gameObjectComponents = gameObject.GetComponents(typeof(Component));

            for (int i = 0; i < gameObjectComponents.Length; ++i)
            {
                for (int j = 0; j < gameObjectComponents.Length; ++j)
                {
                    AddSecondaryComponent(gameObjectComponents[i].GetType(), gameObjectComponents[j]);
                }
            }
        }

        void AddSecondaryComponent(Type mainComponentType, Component secondaryComponent)
        {
            Type secondaryComponentType = secondaryComponent.GetType();

            if (mainComponentType == secondaryComponentType)
                return;
            if (!_secondaryComponents.ContainsKey(mainComponentType))
                _secondaryComponents.Add(mainComponentType, new Dictionary<Type, List<Component>>());
            if (!_secondaryComponents[mainComponentType].ContainsKey(secondaryComponentType))
                _secondaryComponents[mainComponentType].Add(secondaryComponentType, new List<Component>());
            if (!_secondaryComponents[mainComponentType][secondaryComponentType].Contains(secondaryComponent))
                _secondaryComponents[mainComponentType][secondaryComponentType].Add(secondaryComponent);
        }

        void RemoveSecondaryComponent(Type mainComponentType, Component secondaryComponent)
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

        public List<Component> GetComponents(Type type)
        {
            if (type == null)
                throw new ArgumentException();
            if (!_components.ContainsKey(type))
                return _emptyList;
            return _components[type];
        }

        public List<Component> GetDualComponents(Type mainType, Type secondaryType)
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