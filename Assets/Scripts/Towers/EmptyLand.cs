using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TowerInteractionUI))]
public class EmptyLand : MonoBehaviour
{
    [SerializeField]
    private TowerInteractionUI mTowerInteractionUI;

    private static TowerType[] arrayTowerTypeUpgradeable = new TowerType[] { TowerType.Sanitizer, TowerType.Soap, TowerType.Antibody, TowerType.Mask };

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if(mTowerInteractionUI == null)
            mTowerInteractionUI = GetComponent<TowerInteractionUI>();
    }
    #endif

    private void Awake()
    {
        mTowerInteractionUI.SetInteractions(CreateInteractions());
    }
    private IInteraction CreateInteractions()
    {
        var listInteractionData = new List<InteractionData>();

        for (int i = 0; i < 4; i++)
        {
            var towerStat = GameReference.ScriptableObjectReference.instance.GetTowerStat(arrayTowerTypeUpgradeable[i]);
            var data = new InteractionData(() => Debug.Log("Click " + towerStat.name), towerStat.towerInfo);
            listInteractionData.Add(data);
        }
        return new SimpleInteraction(listInteractionData);
    }
}
