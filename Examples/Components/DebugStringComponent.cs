using ChocolateECS;
using System.Collections.Generic;
using UnityEngine;

namespace ChocolateExamples
{
    // This component also inherits from a MonoBehaviour so that we can see it in the editor
    public class DebugStringComponent : MonoBehaviour, IComponent
    {
        public string data;
    }
}