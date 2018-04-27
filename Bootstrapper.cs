using System.Collections.Generic;
using UnityEngine;

namespace ChocolateECS
{
    public abstract class Bootstrapper : MonoBehaviour
    {
        List<ISystem> awakeSystems = new List<ISystem>();
        int countAwakeSystems;
        List<ISystem> startSystems = new List<ISystem>();
        int countStartSystems;
        List<ISystem> updateSystems = new List<ISystem>();
        int countUpdateSystems;
        List<ISystem> fixedUpdateSystems = new List<ISystem>();
        int countFixedUpdateSystems;

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
    	
    	public virtual void Update()
        {
            for (int i = 0; i < countUpdateSystems; ++i)
                updateSystems[i].OnUpdate();
    	}

        public virtual void FixedUpdate()
        {
            for (int i = 0; i < countFixedUpdateSystems; ++i)
                fixedUpdateSystems[i].OnFixedUpdate();
        }

        protected void RegisterSystem(ISystem system, BootstrapperPermission permission)
        {
            if ((permission & BootstrapperPermission.Awake) == BootstrapperPermission.Awake)
            {
                awakeSystems.Add(system);
                ++countAwakeSystems;
            }
            if ((permission & BootstrapperPermission.Start) == BootstrapperPermission.Start)
            {
                startSystems.Add(system);
                ++countStartSystems;
            }
            if ((permission & BootstrapperPermission.Update) == BootstrapperPermission.Update)
            {
                updateSystems.Add(system);
                ++countUpdateSystems;
            }
            if ((permission & BootstrapperPermission.FixedUpdate) == BootstrapperPermission.FixedUpdate)
            {
                fixedUpdateSystems.Add(system);
                ++countFixedUpdateSystems;
            }
        }
    }
}