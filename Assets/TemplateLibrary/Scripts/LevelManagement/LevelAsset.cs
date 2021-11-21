using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelAsset : ScriptableObject
{
    public abstract bool CanBeLoadedImmediately();
    public abstract bool CanBeUnloadedImmediately();
    public abstract IAsyncLevelLoad LoadLevelAsync();
    public abstract void LoadLevelImmediately();
    public abstract IAsyncLevelUnload UnloadLevelAsync(LevelController level);
    public abstract void UnloadLevelImmediately(LevelController level);

    public interface IAsyncLevelLoad
    {
        bool IsFinished();
        float GetProgress();
        LevelController GetLevelController();
    }
    public interface IAsyncLevelUnload
    {
        bool IsFinished();
        float GetProgress();
    }
}
