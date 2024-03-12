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
    private void SetUp(int[] _Skin)
    {
        for (int i = 0; i < CoinSkins.Length; i++)
        {
            _Skin[i] = PlayerPrefs.GetInt(name);
            switch (_Skin[i])
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
