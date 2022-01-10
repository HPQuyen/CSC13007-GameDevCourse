using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameDevCourse.Hero
{
    public class GameEventListener : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        protected GameEvent _event;
        [SerializeField]
        protected UnityEvent responses;

        #endregion


        #region MonoBehaviour Callbacks

        private void OnEnable() => _event.RegisterListener(this);

        private void OnDisable() => _event.UnregisterListener(this);

        #endregion


        #region Public Callbacks

        public virtual void Raise() => responses?.Invoke();

        #endregion
    }
}