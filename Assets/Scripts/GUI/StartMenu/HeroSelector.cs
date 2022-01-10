using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroSelector : MonoBehaviour
{
    #region Attributes
    [SerializeField] private List<GameObject> heroPrefabs;
    [SerializeField] private Transform heroParent;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadHeroList();
    }

    private void LoadHeroList()
    {
        
        foreach (GameObject heroRow in heroPrefabs)
        {
            Instantiate(heroRow, heroParent);
        }
        
    }
}
