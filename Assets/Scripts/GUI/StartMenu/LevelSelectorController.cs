using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorController : MonoBehaviour
{
    #region Attributes
    [SerializeField] private List<GameObject> stagePrefabs;
    [SerializeField] private Transform stageParent;
    [SerializeField] private GameObject panelDetails;
    [SerializeField] private GameObject dungeonLockedMsg;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadStageList();
    }

    private void LoadStageList()
    {
        foreach (GameObject stage in stagePrefabs)
        {
            Instantiate(stage, stageParent);
        }
    }

    public void DisplayPanelDetails()
    {
        panelDetails.SetActive(true);
    }
   
    public void DungeonLockedMsg()
    {
        dungeonLockedMsg.SetActive(true);
    }
}
