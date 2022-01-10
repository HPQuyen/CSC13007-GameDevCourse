using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevCourse.Hero
{
    public class GenericSingletonClass<T> : MonoBehaviour where T : Component
    {
        #region Private Static Instance

        protected static T instance;

        #endregion


        #region Public Instance Getter

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject instanceGameObject = new GameObject
                        {
                            name = typeof(T).Name
                        };

                        instance = instanceGameObject.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        #endregion


        #region Inheritable MonoBehaviour Callbacks

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}