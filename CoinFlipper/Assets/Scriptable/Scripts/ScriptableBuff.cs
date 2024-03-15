using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;   
public class ScriptableBuff : ScriptableObject
{
    [SerializeField] protected int BuffAmount;
    [SerializeField] protected long BasePrice;
    [SerializeField] protected float Mult = 1.15f;
    protected TMP_Text Text;
    protected Button But;
    protected Spinner Spin;
    protected int Quantenty;
    protected long Price;    

    public void SetIndex(Spinner _Spin, Reset _Reset, Button _Button)
    {
        Spin = _Spin;
        _Button.onClick.AddListener(() => { OnClick(); });
        _Button.GetComponentInChildren<TMP_Text>().text = name;
        Text = _Button.GetComponentsInChildren<TMP_Text>().ToList().Find(x => x.name.Contains("CounterText"));
        Quantenty = PlayerPrefs.GetInt(name);
        Text.text = Quantenty.ToString();
        _Reset.resetGame += ResetBuff;
        Price = Convert.ToInt64(Quantenty * Mult * BasePrice);
    }   
    public void OnClick() 
    {
        if (!Buff()) { /* klein special effect om te laten zien dat het niet kan; */}
        else { /* klein effect om te laten zien dat ie gekocht is */}
    } 
    protected virtual bool Buff() 
    {
        Quantenty++;
        Price = Convert.ToInt64(Quantenty * Mult * BasePrice);
        PlayerPrefs.SetInt(name, Quantenty); 
        Text.text = Quantenty.ToString();
        return true; 
    }
    public void ResetBuff()
    {
        Quantenty = 0;
        Price = BasePrice;
        PlayerPrefs.SetInt(name, Quantenty);
    }
}
