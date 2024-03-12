using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Flat")]
public class Flat : ScriptableBuff
{
    protected override bool Buff()
    {
        if(Spin.AddSwipeAmount(BuffAmount, BasePrice)){ return base.Buff(); }
        return false;
    }
}
