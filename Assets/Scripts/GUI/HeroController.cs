using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    [Header("UI Elements")]
    [SerializeField] 
    private TMP_Text HeroName;
    [SerializeField] 
    private TMP_Text requirement;
    [SerializeField] 
    private GameObject locked;
    [SerializeField]
    private bool isLocked;

    #region Private Variables
    private GameObject character;
    private GameObject characterUI;
    [SerializeField]
    private GameObject warning;
    private GameObject warningUI;
    #endregion

    #endregion

    void Start()
    {
        if (isLocked)
        {
            locked.SetActive(true);
            requirement.gameObject.SetActive(true);
        }
        else
        {
            locked.SetActive(false);
            requirement.gameObject.SetActive(false);
        }
    }

    public void OnClickChangeCharacter()
    {
        if (isLocked)
        {
            warningUI.SetActive(true);
        } else
        {
            Vector3 pos = character.transform.position;
            character = this.gameObject;
            Instantiate(character, new Vector2(pos.x, pos.y), Quaternion.identity);
        }
    }
}
