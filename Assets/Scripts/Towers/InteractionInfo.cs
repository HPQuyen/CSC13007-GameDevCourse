using System;
using UnityEngine;

[Serializable]
public class InteractionInfo
{
    public Sprite optionThumbnail;
    public string description;

    public InteractionInfo(Sprite optionThumbnail, string description)
    {
        this.optionThumbnail = optionThumbnail;
        this.description = description;
    }
}
