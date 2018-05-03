using System;
using UnityEngine;

namespace ChocolateECS
{
    public class GameObjectFactory
    {
        public static Action<GameObject> OnGameObjectInstantiated;
        public static Action<GameObject> OnGameObjectPreDestroyed;
        public static Action<GameObject> OnGameObjectPostDestroyed;

        public static GameObject Instantiate(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentException();
            GameObject go = GameObject.Instantiate(gameObject);
            if (OnGameObjectInstantiated != null)
                OnGameObjectInstantiated(go);
            return go;
        }

        public static void DestroyImmediate(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentException();
            if (OnGameObjectPreDestroyed != null)
                OnGameObjectPreDestroyed(gameObject);
            GameObject.DestroyImmediate(gameObject);
            if (OnGameObjectPostDestroyed != null)
                OnGameObjectPostDestroyed(gameObject);
        }
    }
}