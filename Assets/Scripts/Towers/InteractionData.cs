using System;

[Serializable]
public class InteractionData
{
    public Action action;
    public TowerInfo info;

    public InteractionData(Action callback, TowerInfo info)
    {
        action = callback;
        this.info = info;
    }
}
