using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public event Action<LevelController> onLevelStarted = delegate { };
    public event Action<LevelController> onLevelEnded = delegate { };
    public event Action<LevelController> onLevelPaused = delegate { };
    public event Action<LevelController> onLevelResumed = delegate { };

    public virtual void StartLevel()
    {
        onLevelStarted?.Invoke(this);
    }

    public virtual void EndLevel()
    {
        onLevelEnded?.Invoke(this);
    }

    public virtual void PauseLevel()
    {
        onLevelPaused?.Invoke(this);
    }

    public virtual void ResumeLevel()
    {
        onLevelResumed?.Invoke(this);
    }

    /// <summary>
    /// Override this method to determine Player win or lose
    /// </summary>
    /// <returns></returns>
    public virtual bool IsVictory()
    {
        return true;
    }
}
