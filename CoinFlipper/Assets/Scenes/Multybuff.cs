using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Multybuff")]
public class Multybuff : ScriptableBuff
{
    protected override bool Buff()
    {
        return spin.AddSwipeAmount(BuffAmount, price);
    }
}
