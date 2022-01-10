using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevCourse.Hero
{
    public class InputProcessor : GenericSingletonClass<InputProcessor>
    {
        #region Public Properties

        public bool MouseRightClick { get; private set; }

        #endregion


        #region MonoBehaviour Callbacks

        private void Update()
        {
            MouseRightClick = Input.GetKeyDown(KeyCode.Mouse1);
        }

        #endregion
    }
}