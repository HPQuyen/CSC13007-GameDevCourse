using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
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
    private void OnEnable()
    {
        LevelEventHandler.AddNewActionEvent(GameplayEventCode.OnChangedValueHeart, OnChangedValueHeart);
    }

    private void OnChangedValueHeart(object[] param)
    {
        if (param == null && param.Length <= 0)
            return;
        var data = (int)param[0];
        heartText.text = data.ToString();
        heartText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
}
