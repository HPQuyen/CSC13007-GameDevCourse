using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public static int MIN_HEART = 0;

    [SerializeField]
    private int maxHeart = 20;
    [SerializeField]
    private Text heartText;

    private int currentHeart;
    public int CurrentHeart => currentHeart;

    private void Start()
    {
        currentHeart = maxHeart;
        heartText.text = $"{currentHeart}/{maxHeart}";
        heartText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }

    public void OnChangedValueHeart(int value)
    {
        if (value <= 0)
            LevelEventHandler.Invoke(LevelEventCode.OnLoseLevel);
        currentHeart = value;
        heartText.text = $"{currentHeart}/{maxHeart}";
        heartText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
}
