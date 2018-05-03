﻿using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ReflectionUtilities
{   
   public static bool IsOverride(MethodInfo method)
   {
      return !method.Equals(method.GetBaseDefinition());
   }
}

namespace ChocolateECS
{
    public abstract class Bootstrapper : MonoBehaviour
    {
        List<ISystem> allSystems = new List<ISystem>();
        int countAllSystems;
        List<ISystem> awakeSystems = new List<ISystem>();
        int countAwakeSystems;
        List<ISystem> startSystems = new List<ISystem>();
        int countStartSystems;
        List<ISystem> enableSystems = new List<ISystem>();
        int countEnableSystems;
        List<ISystem> updateSystems = new List<ISystem>();
        int countUpdateSystems;
        List<ISystem> lateUpdateSystems = new List<ISystem>();
        int countLateUpdateSystems;
        List<ISystem> fixedUpdateSystems = new List<ISystem>();
        int countFixedUpdateSystems;
        List<ISystem> disableSystems = new List<ISystem>();
        int countDisableSystems;
        List<ISystem> destroySystems = new List<ISystem>();
        int countDestroySystems;

        public virtual void Awake()
        {
            for (int i = 0; i < countAwakeSystems; ++i)
                awakeSystems[i].OnAwake(awakeSystems); // TODO: Is awake systems enough? Might want to pass all systems instead
    	}

        public virtual void Start()
        {
            for (int i = 0; i < countStartSystems; ++i)
                startSystems[i].OnStart();
        }

        public virtual void OnEnable()
        {
            for (int i = 0; i < countEnableSystems; ++i)
                enableSystems[i].OnEnable();
        }
    	
    	public virtual void Update()
        {
            for (int i = 0; i < countUpdateSystems; ++i)
                updateSystems[i].OnUpdate(Time.deltaTime);
    	}

        public virtual void LateUpdate()
        {
            for (int i = 0; i < countLateUpdateSystems; ++i)
                lateUpdateSystems[i].OnLateUpdate(Time.deltaTime);
        }

        public virtual void FixedUpdate()
        {
            for (int i = 0; i < countFixedUpdateSystems; ++i)
                fixedUpdateSystems[i].OnFixedUpdate();
        }

        public virtual void OnDisable()
        {
            for (int i = 0; i < countDisableSystems; ++i)
                disableSystems[i].OnDisable();
        }

        public virtual void OnDestroy()
        {
            for (int i = 0; i < countDestroySystems; ++i)
                destroySystems[i].OnDestroy();
            // NOTE: Might need to move that before OnDestroy if systems start destroying game objects when they get destroyed
            UnregisterSystems();
        }

        protected void RegisterSystem(ISystem system)
        {
            Type systemType = system.GetType();

            allSystems.Add(system);
            ++countAllSystems;

            MethodInfo awakeInfo = systemType.GetMethod("OnAwake", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(List<ISystem> )}, null);
            if (awakeInfo != null && ReflectionUtilities.IsOverride(awakeInfo))
            {
                awakeSystems.Add(system);
                ++countAwakeSystems;
            }
            MethodInfo startInfo = systemType.GetMethod("OnStart", BindingFlags.Instance | BindingFlags.Public);
            if (startInfo != null && ReflectionUtilities.IsOverride(startInfo))
            {
                startSystems.Add(system);
                ++countStartSystems;
            }
            MethodInfo enableInfo = systemType.GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.Public);
            if (enableInfo != null && ReflectionUtilities.IsOverride(enableInfo))
            {
                enableSystems.Add(system);
                ++countEnableSystems;
            }
            MethodInfo updateInfo = systemType.GetMethod("OnUpdate", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(float) }, null);
            if (updateInfo != null && ReflectionUtilities.IsOverride(updateInfo))
            {
                updateSystems.Add(system);
                ++countUpdateSystems;
            }
            MethodInfo lateUpdateInfo = systemType.GetMethod("OnLateUpdate", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(float) }, null);
            if (lateUpdateInfo != null && ReflectionUtilities.IsOverride(lateUpdateInfo))
            {
                lateUpdateSystems.Add(system);
                ++countLateUpdateSystems;
            }
            MethodInfo fixedUpdateInfo = systemType.GetMethod("OnFixedUpdate", BindingFlags.Instance | BindingFlags.Public);
            if (fixedUpdateInfo != null && ReflectionUtilities.IsOverride(fixedUpdateInfo))
            {
                fixedUpdateSystems.Add(system);
                ++countFixedUpdateSystems;
            }
            MethodInfo disableInfo = systemType.GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.Public);
            if (disableInfo != null && ReflectionUtilities.IsOverride(disableInfo))
            {
                disableSystems.Add(system);
                ++countDisableSystems;
            }
            MethodInfo destroyInfo = systemType.GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.Public);
            if (destroyInfo != null && ReflectionUtilities.IsOverride(destroyInfo))
            {
                destroySystems.Add(system);
                ++countDestroySystems;
            }

            RegisterSystemHandlers(system);
        }

        void UnregisterSystems()
        {
            for (int i = 0; i < countAllSystems; ++i)
            {
                ISystem system = allSystems[i];

                UnregisterSystemHandlers(system);
            }
        }

        void RegisterSystemHandlers(ISystem system)
        {
            system.OnComponentDestroyed += OnComponentDestroyed;
        }

        void UnregisterSystemHandlers(ISystem system)
        {
            system.OnComponentDestroyed -= OnComponentDestroyed;
        }

        // If an entity is destroyed OR an entity loses / adds a new component, the lists should update in all relevant lists
        void RefreshSystemComponents()
        {
            // Ideally this should only refresh systems using the component given
            for (int i = 0; i < countAllSystems; ++i)
                allSystems[i].RefreshComponents();
        }

        void OnComponentDestroyed()
        {
            RefreshSystemComponents();
        }
    }
}