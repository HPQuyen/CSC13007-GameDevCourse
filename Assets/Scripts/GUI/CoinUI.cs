using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    private int initialCoin = 1000;
    [SerializeField]
    private Text cointText;

    private int currentCoin;
    public int CurrentCoin => currentCoin;

    private void Start()
    {
        currentCoin = initialCoin;
        cointText.text = initialCoin.ToString();
        cointText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
    private void OnEnable()
    {
        LevelEventHandler.AddNewActionEvent(GameplayEventCode.OnChangedValueCoin, OnChangedValueCoin);
    }

    private void OnChangedValueCoin(object[] param)
    {
        if (param == null && param.Length <= 0)
            return;
        var data = (int) param[0];
        cointText.text = data.ToString();
        cointText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
}
