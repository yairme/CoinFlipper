using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Buff
{
    public ScriptableBuff buffs;
    public Button but;
}

public class BuffsManager : MonoBehaviour
{
    [SerializeField] private Spinner Spin;
    [SerializeField] private Buff[] Buffs;
    [SerializeField] private Reset Reseter;
    void Start()
    {
        for (int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i].buffs.SetIndex(Spin, Reseter, Buffs[i].but);
        }
        Spin.SetReset(Reseter);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F1)) { PlayerPrefs.DeleteAll(); print("yeet"); }
    }
}



