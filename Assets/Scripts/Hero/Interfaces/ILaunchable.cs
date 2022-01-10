using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevCourse.Hero
{
    public interface ILaunchable
    {
        void Launch(GameObject hero, GameObject target);
    }
}