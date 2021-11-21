using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public event Action onReplay = delegate { };
    public event Action onNextLevel = delegate { };

    [SerializeField]
    private Button replayBtn;
    [SerializeField]
    private Button nextLevelBtn;
    [SerializeField]
    private TMP_Text titleText;

    private void Start()
    {
        replayBtn.onClick.AddListener(() => onReplay?.Invoke());
        nextLevelBtn.onClick.AddListener(() => onNextLevel?.Invoke());
    }

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void SetButtonGroup(bool enableReplay, bool enableNextLevel)
    {
        replayBtn.gameObject.SetActive(enableReplay);
        nextLevelBtn.gameObject.SetActive(enableNextLevel);
    }
}
