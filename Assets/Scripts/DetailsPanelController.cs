using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetailsPanelController : Singleton<DetailsPanelController>
{
    public Button fightBtn;

    protected override void Awake()
    {
        base.Awake();
        fightBtn.onClick.AddListener(OnPressFightButton);
        gameObject.SetActive(false);
    }

    public void OnPressFightButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
