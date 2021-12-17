using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondUI : MonoBehaviour
{
    [SerializeField]
    private PPrefInt playerPrefDiamond;
    [SerializeField]
    private Text diamondText;

    private void Start()
    {
        diamondText.text = playerPrefDiamond.Get().ToString();
        diamondText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
    private void OnEnable()
    {
        playerPrefDiamond.onValueChanged += OnValueChanged;
    }
    private void OnDisable()
    {
        playerPrefDiamond.onValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(PPrefInt.ValueChangedData data)
    {
        diamondText.text = data.newValue.ToString();
        diamondText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }
}
