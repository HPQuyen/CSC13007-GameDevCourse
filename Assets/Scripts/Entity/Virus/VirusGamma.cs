using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusGamma : Virus
{
    protected override void OnAttackState()
    {
        if (attackTarget == null || attackTarget.IsDied())
        {
            mState = VirusState.Walk;
            return;
        }
        mState = VirusState.None;
    }
}
