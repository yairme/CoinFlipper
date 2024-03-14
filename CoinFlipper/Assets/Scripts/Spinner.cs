using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]private CoinBalance Coin;
    private long BaseSwipeAmount = 1; // StartValues of 1 to have a start position
    private long Mult = 1;
    private long SwipeAmount;


    private void Start()
    {
        SetBaseStats();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) { Coin.Swipe(SwipeAmount);}
    }

    private void SetBaseStats()
    {
        if (PlayerPrefs.HasKey("SwipeAmount"))
        {
            BaseSwipeAmount = Convert.ToInt64(PlayerPrefs.GetString("SwipeAmount"));
            if (PlayerPrefs.HasKey("Mult"))
            {
                Mult = Convert.ToInt64(PlayerPrefs.GetString("Mult"));
            }
        }
        SaveSwipe();
    }

    private void SaveSwipe()
    {
        SwipeAmount = BaseSwipeAmount * Mult;
        PlayerPrefs.SetString("SwipeAmount", BaseSwipeAmount.ToString());
        PlayerPrefs.SetString("Mult", Mult.ToString());
        print(SwipeAmount);
    }

    public bool AddSwipeAmount(long _Buff, long _Price)
    {
        if (Coin.Buy(_Price))
        {
            BaseSwipeAmount += _Buff;
            SaveSwipe();
            return true;
        }
        return false;
    }
    public bool AddMult(long _Buff, long _Price)
    {
        if (Coin.Buy(_Price))
        {
            Mult += _Buff;
            SaveSwipe();
            return true;
        }
        return false;
    }
    private void ResetCoins()
    {
        BaseSwipeAmount = 1; // StartValues of 1
        Mult = 1;
        SaveSwipe();
    }
    public void SetReset(Reset _Reset)
    {
        _Reset.resetGame += ResetCoins;
        Coin.SetReseter(_Reset);
    }
}
