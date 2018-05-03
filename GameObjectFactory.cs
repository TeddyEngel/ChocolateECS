using System;
using UnityEngine;

namespace ChocolateECS
{
    public class GameObjectFactory
    {
        public Action<GameObject> OnGameObjectInstantiated;
        public Action<GameObject> OnGameObjectDestroyed;

        public GameObject Instantiate(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentException();
            GameObject go = GameObject.Instantiate(gameObject);
            if (OnGameObjectInstantiated != null)
                OnGameObjectInstantiated(go);
            return go;
        }

        public void DestroyImmediate(GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentException();
            if (OnGameObjectDestroyed != null)
                OnGameObjectDestroyed(gameObject);
            GameObject.DestroyImmediate(gameObject);
        }
    }
}