using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageController : MonoBehaviour
{
    #region Attributes
    [Header("UI Elements")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text requirement;
    [SerializeField] private GameObject locked;
    [SerializeField] private bool isLocked;

    #region Private Attributes
    private GameObject levelWarning;
    private GameObject panelDetails;
    // private DetailsPanelController detailsPanel;
    #endregion

    #endregion

    private void Start()
    {
        levelWarning = GameObject.FindGameObjectWithTag("LevelWarning");
        panelDetails = GameObject.FindGameObjectWithTag("PanelDetails");
        // detailsPanel = panelDetails.GetComponent<DetailsPanelController>();

        if (isLocked)
        {
            locked.SetActive(true);
            requirement.gameObject.SetActive(true);
        } else
        {
            locked.SetActive(false);
            requirement.gameObject.SetActive(false);
        }
    }

    public void OnSelectLevelClick()
    {
        if (isLocked)
        {
            levelWarning.SetActive(true);
        } else
        {
            panelDetails.SetActive(true);   
        }
    }
}
