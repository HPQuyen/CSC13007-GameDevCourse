using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateGameController))]
public class StateBasedLevelEndSoundFX : MonoBehaviour
{
    private StateGameController _stateGameController;
    private StateGameController stateGameController
    {
        get
        {
            if (_stateGameController == null)
                _stateGameController = GetComponent<StateGameController>();
            return _stateGameController;
        }
    }

    [SerializeField]
    private AudioSource winSound = null;
    [SerializeField]
    private AudioSource loseSound = null;

    private void OnEnable()
    {
        stateGameController.onStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        stateGameController.onStateChanged -= OnStateChanged;
    }
    private void OnStateChanged(StateGameController.StateChangedData date)
    {
        if(stateGameController.CurrentState == StateGameController.State.Ending)
        {
            if (stateGameController.CurrentSession.LevelController.IsInvoking())
                winSound?.Play();
            else
                loseSound?.Play();
        }
    }
}
