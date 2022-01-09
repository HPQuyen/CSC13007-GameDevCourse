using System;
using UnityEngine;

[Serializable]
public class InteractionData
{
    public Action action;
    public TowerInfo info;
    public Sprite cancelSprite;

    public InteractionData(Action callback, TowerInfo info)
    {
        action = callback;
        this.info = info;
    }
    public InteractionData(Action callback, Sprite cancelSprite)
    {
        action = callback;
        this.cancelSprite = cancelSprite;
    }
}
