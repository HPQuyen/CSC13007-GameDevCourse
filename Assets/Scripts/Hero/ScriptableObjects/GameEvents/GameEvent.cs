using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevCourse.Hero
{
    [CreateAssetMenu(
        fileName = "GameEvent", menuName = "ScriptableObjects/Event", order = 1
    )]
    public class GameEvent : ScriptableObject
    {
        #region Private Fields

        private Dictionary<int, GameEventListener> listeners = new Dictionary<int, GameEventListener>();

        #endregion


        #region Public Callbacks

        public void RegisterListener(GameEventListener listener) 
            => listeners.Add(listener.transform.root.gameObject.GetInstanceID(), listener);

        public void UnregisterListener(GameEventListener listener) 
            => listeners.Remove(listener.transform.root.gameObject.GetInstanceID());

        public void RaiseAllEvents()
        {
            foreach (var listener in listeners.Values)
            {
                listener.Raise();
            }
        }

        public void RaiseEventByID(int id)
        {
            foreach (var listener in listeners)
            {
                if (listener.Key == id)
                {
                    listener.Value.Raise();
                    break;
                }
            }
        }

        #endregion
    }
}