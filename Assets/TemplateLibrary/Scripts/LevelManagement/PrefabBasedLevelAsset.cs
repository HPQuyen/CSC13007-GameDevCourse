using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabBasedLevelAsset", menuName = GameData.GameName + "/PrefabBasedLevelAsset")]
public class PrefabBasedLevelAsset : LevelAsset
{
    [SerializeField]
    private LevelController levelPrefab;
    [SerializeField]
    private Texture levelThumbnail;

    public override bool CanBeLoadedImmediately()
    {
        return true;
    }

    public override bool CanBeUnloadedImmediately()
    {
        return true;
    }

    public override IAsyncLevelLoad LoadLevelAsync()
    {
        var newLevel = Instantiate(levelPrefab);
        return new AsyncLevelLoad(newLevel);
    }

    public override void LoadLevelImmediately()
    {
        Instantiate(levelPrefab);
    }

    public override IAsyncLevelUnload UnloadLevelAsync(LevelController level)
    {
        if (!Application.isPlaying)
            DestroyImmediate(level.gameObject);
        else
            Destroy(level.gameObject);
        return new AsyncLevelUnload();
    }

    public override void UnloadLevelImmediately(LevelController level)
    {
        if (!Application.isPlaying)
            DestroyImmediate(level.gameObject);
        else
            Destroy(level.gameObject);
    }

    public class AsyncLevelLoad : IAsyncLevelLoad
    {
        private LevelController prefab;

        public AsyncLevelLoad(LevelController prefab)
        {
            this.prefab = prefab;
        }

        public LevelController GetLevelController()
        {
            return this.prefab;
        }

        public float GetProgress()
        {
            return 1f;
        }

        public bool IsFinished()
        {
            return true;
        }
    }

    public class AsyncLevelUnload : IAsyncLevelUnload
    {
        public float GetProgress()
        {
            return 1f;
        }

        public bool IsFinished()
        {
            return true;
        }
    }
}
