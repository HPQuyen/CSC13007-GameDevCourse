using UnityEngine;

[RequireComponent(typeof(StateGameController))]
public class StateUIController : MonoBehaviour
{
    [SerializeField]
    private GameOverUI gameOverUI;
    public GameOverUI GameOverUI => gameOverUI;

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
        gameOverUI.onNextLevel += stateGameController.StartNextLevel;
        gameOverUI.onReplay += stateGameController.Replay;
    }
    private void OnEnable()
    {
        stateGameController.onStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        stateGameController.onStateChanged -= OnStateChanged;
    }
    public void OnStateChanged(StateGameController.StateChangedData data)
    {
        switch (data.newState)
        {
            case StateGameController.State.Prepare:
                break;
            case StateGameController.State.Playing:
                gameOverUI.GetComponent<IUIVisibilityController>()?.Hide();
                break;
            case StateGameController.State.Pause:
                break;
            case StateGameController.State.Ending:
                bool isVictory = stateGameController.CurrentSession.LevelController.IsVictory();
                gameOverUI.SetTitle(isVictory ? GameData.VictoryTitle : GameData.DefeatTitle);
                gameOverUI.SetButtonGroup(true, isVictory);
                gameOverUI.GetComponent<IUIVisibilityController>()?.Show();
                break;
            default:
                break;
        }
    }
}
