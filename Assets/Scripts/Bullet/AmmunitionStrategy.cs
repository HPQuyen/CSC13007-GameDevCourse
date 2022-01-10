using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmunitionStrategy
{
    void Perform(BulletStat bulletStat, Collider2D collider, Action callback);
}
