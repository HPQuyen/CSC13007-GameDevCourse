using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionCellUI : MonoBehaviour
{
    private InteractionData data;
    private Image image;
    private Image mImage
    {
        get
        {
            if (image == null)
                image = gameObject.AddComponent<Image>();
            return image;
        }
    }
    private Button button;
    private Button mButton
    {
        get
        {
            if (button == null)
                button = gameObject.AddComponent<Button>();
            return button;
        }
    }

    public void Init(InteractionData interactionData)
    {
        data = interactionData;
        mButton.onClick.AddListener(() => data.action?.Invoke());
        mImage.sprite = interactionData.info == null ? interactionData.cancelSprite : interactionData.info.towerThumbnail;
    }
}
