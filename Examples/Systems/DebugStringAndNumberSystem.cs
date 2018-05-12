using ChocolateECS;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateExamples
{
    /*
    ** This system will print a string from a debug string component and a number from a debug number component each frame
    */
    public class DebugStringAndNumberSystem : ECSSystem
    {
        public override void OnAwake(List<ISystem> systems)
        {
            base.OnAwake(systems);
        }

        // Same with Update
        public override void OnUpdate(float deltaTime)
        {
            // We specify that we also want the collection of  
            var debugStringComponents = ComponentManager.GetDualComponents(typeof(DebugNumberComponent), typeof(DebugStringComponent));
            var debugNumberComponents = ComponentManager.GetDualComponents(typeof(DebugStringComponent), typeof(DebugNumberComponent));

            for (int i = 0; i < debugStringComponents.Count; ++i)
            {
                var debugStringComponent = debugStringComponents[i] as DebugStringComponent;
                var debugNumberComponent = debugNumberComponents[i] as DebugNumberComponent;

                Debug.Log(debugStringComponent.data + " - " + debugNumberComponent.number);
            }
        }
    }
}