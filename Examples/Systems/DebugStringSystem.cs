using ChocolateECS;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateExamples
{
    public class DebugStringSystem : ECSSystem
    {
        // We override Awake so that awake is called on that system    
        public override void OnAwake(List<ISystem> systems)
        {
            base.OnAwake(systems);
        }

        // Same with Update
        public override void OnUpdate(float deltaTime)
        {
            var debugStringComponents = ComponentManager.GetComponents(typeof(DebugStringComponent));

            for (int i = 0; i < debugStringComponents.Count; ++i)
            {
                DebugStringComponent debugStringComponent = debugStringComponents[i] as DebugStringComponent;

                Debug.Log(debugStringComponent.data);
            }
        }
    }
}