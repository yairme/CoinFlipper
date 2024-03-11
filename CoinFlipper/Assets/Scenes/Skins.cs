using NUnit.Framework.Internal.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] int[] CoinSkins; // -1 is the active skin in the game
    [SerializeField] int[] MapSkins; // 0 means you didnt buy it and 1 means bought
    void Start()
    {
        SetUp(CoinSkins);
        SetUp(MapSkins);
    }
    private void SetUp(int[] Skin)
    {
        for (int i = 0; i < CoinSkins.Length; i++)
        {
            Skin[i] = PlayerPrefs.GetInt(Skin.ToString() + i);
            switch (Skin[i])
            {
                case 0:
                    break;
                case 1:
                    //SetBougth()
                    break;
                case 2:
                    //SetActive()
                    break;
            }
        }
    }
}
