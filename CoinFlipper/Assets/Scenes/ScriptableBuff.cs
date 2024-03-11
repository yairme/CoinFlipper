using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ScriptableBuff : ScriptableObject
{
    [SerializeField] protected long price;
    protected int Quantenty;  
    protected int ArrayPosition;
    protected Button but;

    [SerializeField]protected int BuffAmount;
    protected Spinner spin; // kan veranderen in de gyro bal als die bijhoud hoeveel die swiped

    public void SetIndex(int index, Spinner temp, Reset ye, Button buttemp)
    {
        spin = temp;
        ArrayPosition = index;
        but = buttemp;
        but.onClick.AddListener(() => { OnClick(); });
        Quantenty = PlayerPrefs.GetInt("Buff" + ArrayPosition);
        ye.ResetGame += ResetBuff;
    }
    public void OnClick() 
    {
        if (!Buff())
        {
            // klein special effect om te laten zien dat het niet kan;
            Debug.Log("oof");
        }
        else Debug.Log("lit");
    } 
    protected virtual bool Buff() { return true; }
    public void ResetBuff()
    {
        Quantenty = 0;
        PlayerPrefs.SetInt(name, Quantenty);
    }
}
