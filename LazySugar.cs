using System;
using D2D.Core;
using D2D.Gameplay;
using D2D.Utilities;
using DG.Tweening;
using UnityEngine;

namespace D2D.Utilities
{
    public static class GameObjectSugar
    {
        /// <summary>
        /// Finds the lazy GameObject or creates it.
        /// </summary>
        public static T FindLazy<T>(this MonoBehaviour target) where T: Component, ILazy
        {
            var instances = GameObject.FindObjectsOfType<T>();

            // More than 1 ILazyCreating is an error
            if (instances?.Length > 1)
            {
                Debug.LogError("There are more than 1 LazyCreating objects! " +
                               "Please, find the duplicates and remove them.");
            }

            // Create automatically if object not exist
            if (instances != null && instances.Length == 0)
            {
                // if (CoreSettings.Instance.lazyRuntimeCreationEnabled)
                //{
                    string lazyName = $"{typeof(T).Name} [created at runtime]";
                    return new GameObject(lazyName, typeof(T)).GetComponent<T>();
                // }

                return null;
            }

            return instances[0];
        }
    }
}
