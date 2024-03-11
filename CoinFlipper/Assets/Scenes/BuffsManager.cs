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
    [SerializeField]private Spinner spin;
    [SerializeField] private Buff[] Buffs;
    [SerializeField] private Reset Reseter;
    void Start()
    {
        for (int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i].buffs.SetIndex(i, spin, Reseter, Buffs[i].but);
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F1)) { PlayerPrefs.DeleteAll(); print("yeet"); }
    }
}



