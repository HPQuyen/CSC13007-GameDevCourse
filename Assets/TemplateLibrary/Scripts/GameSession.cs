using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession
{
    public GameSession(LevelAsset levelAsset, LevelController levelController)
    {
        this.levelAsset = levelAsset;
        this.levelController = levelController;
    }

    private readonly LevelAsset levelAsset;
    public LevelAsset LevelAsset => levelAsset;

    private readonly LevelController levelController;
    public LevelController LevelController => levelController;

}
