using System;

[Serializable]
public class InteractionData
{
    public Action action;
    public InteractionInfo info;

    public InteractionData(Action callback, InteractionInfo info)
    {
        action = callback;
        this.info = info;
    }
}
