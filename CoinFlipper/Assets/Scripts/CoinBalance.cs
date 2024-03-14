using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinBalance : MonoBehaviour
{
    private long CoinAmount;
    [SerializeField] private TMP_Text Text;

    void Start()
    {
        if (PlayerPrefs.HasKey("Coins")) { CoinAmount = Convert.ToInt64(PlayerPrefs.GetString("Coins")); }
    }
    private void FixedUpdate()
    {
        SaveValue();
    }
    private void ResetGame() 
    {
        CoinAmount = 0; // 0 stands for nothing
    }
    public void SetReseter(Reset _Reset)
    {
        _Reset.resetGame += ResetGame;
    }
    public void Swipe(long _SwipeAmount)
    {
        CoinAmount += _SwipeAmount;
        print(CoinAmount);
    }
    public bool Buy(long _Price)
    {
        if(_Price <= CoinAmount)
        {
            CoinAmount -= _Price;
            return true;
        }
        return false;
    }
    private void SaveValue()
    {
        Text.text = CoinAmount.ToString();
        PlayerPrefs.SetString("Coins", CoinAmount.ToString());
    }
}
