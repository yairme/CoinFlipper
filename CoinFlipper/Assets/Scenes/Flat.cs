using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/flat")]
public class Flat : ScriptableBuff
{
    protected override bool Buff()
    {
        return spin.AddSwipeAmount(BuffAmount, price);
    }
}
