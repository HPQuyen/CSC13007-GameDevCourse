using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class UIController : MonoBehaviour
    {
        #region Attributes
        [Header("Start Menu Panels")]
        [SerializeField]
        private GameObject startMenu;
        [SerializeField]
        private GameObject settings;
        [SerializeField]
        private GameObject levelSelector;
        [SerializeField]
        private GameObject heroSelector;
        #endregion

        #region Unity Functions
        private void Start()
        {
            startMenu.SetActive(true);
            settings.SetActive(false);
            levelSelector.SetActive(false);
            heroSelector.SetActive(false);
        }
        #endregion

        #region Other Methods / Functions
        public void ExitGame()
        {
            SceneManager.instance.ExitGame();
        }
        #endregion
    }
}

