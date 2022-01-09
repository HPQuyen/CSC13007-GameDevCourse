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

    public void OnChangedValueCoin(int value)
    {
        currentCoin = value;
        cointText.text = value.ToString();
        cointText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
}
