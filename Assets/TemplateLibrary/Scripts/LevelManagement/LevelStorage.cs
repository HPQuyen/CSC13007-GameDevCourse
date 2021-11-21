using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelStorage", menuName = GameData.GameName + "/LevelStorage")]
public class LevelStorage : ScriptableObject
{
    [SerializeField]
    private List<LevelAsset> levelAssets = new List<LevelAsset>();
    
    public enum EndOfLevelBehaviour
    {
        Stay,
        LoopBack
    }

    public LevelAsset GetLevel(int levelIndex, EndOfLevelBehaviour endOfLevelBehaviour = EndOfLevelBehaviour.Stay)
    {
        switch (endOfLevelBehaviour)
        {
            case EndOfLevelBehaviour.Stay:
                levelIndex = Mathf.Clamp(levelIndex, 0, levelAssets.Count - 1);
                break;
            case EndOfLevelBehaviour.LoopBack:
                levelIndex = levelIndex % levelAssets.Count;
                break;
            default:
                return null;
        }
        return levelAssets[levelIndex];
    }

    public int GetLevelIndex(LevelAsset levelAsset)
    {
        return levelAssets.IndexOf(levelAsset);
    }
}
