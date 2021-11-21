using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateGameController))]
public class StateBasedGameLoopManagement : MonoBehaviour
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

    private void Start()
    {
        stateGameController.StartNextLevel();
    }
}
