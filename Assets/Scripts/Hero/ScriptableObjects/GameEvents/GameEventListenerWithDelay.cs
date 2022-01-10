using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameDevCourse.Hero
{
    public class GameEventListenerWithDelay : GameEventListener
    {
        #region Serialized Fields

        [SerializeField]
        private float delay;
        [SerializeField]
        private UnityEvent delayResponses;

        #endregion


        #region Public Callbacks

        public override void Raise()
        {
            responses?.Invoke();
            StartCoroutine(RaiseDelay());
        }

        #endregion


        #region Coroutines

        private IEnumerator RaiseDelay()
        {
            yield return new WaitForSeconds(delay);
            delayResponses?.Invoke();
        }

        #endregion
    }
}