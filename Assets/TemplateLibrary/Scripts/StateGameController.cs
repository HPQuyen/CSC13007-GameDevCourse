using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGameController : MonoBehaviour
{
    public event Action<StateChangedData> onStateChanged = delegate { };

    public struct StateChangedData
    {
        public State oldState;
        public State newState;

        public StateChangedData(State oldState, State newState)
        {
            this.oldState = oldState;
            this.newState = newState;
        }
    }

    public enum State
    {
        Prepare,
        Playing,
        Pause,
        Ending
    }

    private State currentState;
    public State CurrentState
    {
        get => currentState;
        set
        {
            var oldState = currentState;
            currentState = value;
            if (currentState != oldState)
                onStateChanged?.Invoke(new StateChangedData(oldState, currentState));
        }
    }

    [SerializeField] 
    private LevelStorage levelStorage = null;
    public LevelStorage LevelStorage => levelStorage;

    [SerializeField]
    private PPrefInt playerAchievedLevel;

    private GameSession currentSession = null;
    public GameSession CurrentSession => currentSession;

    public void StartNextLevel()
    {
        if (levelStorage == null)
        {
            Debug.LogWarning("Level Storage is null");
            return;
        }
        StartLevel(levelStorage.GetLevel(playerAchievedLevel.Get() + 1, LevelStorage.EndOfLevelBehaviour.LoopBack));
    }
    public void StartLevel(LevelAsset levelAsset)
    {
        if (levelStorage == null)
        {
            Debug.LogWarning("Level Storage is null");
            return;
        }
        StartCoroutine(StartGameLoop(levelAsset));
    }
    IEnumerator StartGameLoop(LevelAsset levelAsset)
    {
        if(currentSession != null)
        {
            var unloading = currentSession.LevelAsset.UnloadLevelAsync(currentSession.LevelController);
            yield return new WaitUntil(unloading.IsFinished);
        }
        var loading = levelAsset.LoadLevelAsync();
        yield return new WaitUntil(loading.IsFinished);
        var newSession = new GameSession(levelAsset, loading.GetLevelController());
        StartCoroutine(GameLoopCR(newSession));
    }
    IEnumerator GameLoopCR(GameSession session)
    {
        currentSession = session;
        CurrentState = State.Playing;
        bool gameEnded = false;
        session.LevelController.StartLevel();
        Action<LevelController> gameEndListener = _ => gameEnded = true;
        session.LevelController.onLevelEnded += gameEndListener;
        yield return new WaitUntil(() => gameEnded);
        session.LevelController.onLevelEnded -= gameEndListener;
        CurrentState = State.Ending;
        if (session.LevelController.IsVictory())
            playerAchievedLevel.Set(levelStorage.GetLevelIndex(session.LevelAsset));
    }
    public void Replay()
    {
        StartLevel(currentSession.LevelAsset);
    }
    public void Pause()
    {
        if (CurrentState != State.Playing || currentSession == null)
            return;
        currentSession.LevelController.PauseLevel();
        CurrentState = State.Pause;
    }
    public void Resume()
    {
        if (CurrentState != State.Pause || currentSession == null)
            return;
        currentSession.LevelController.ResumeLevel();
        CurrentState = State.Playing;
    }
}
