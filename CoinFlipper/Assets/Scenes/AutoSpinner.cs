using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/AutoSpinner")]
public class AutoSpinner : ScriptableBuff
{
    protected override bool Buff()
    {
        return spin.AddSwipeAmount(BuffAmount, price);
    }
}
