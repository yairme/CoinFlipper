    
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

/// <summary>
/// Represents the main logic for handling touch input and controlling the rotation and scoring of a game object.
/// </summary>
public class MainTouchLogic : MonoBehaviour
{
    // Score Settings
    [Header("Stefan")]
    [SerializeField] private CoinBalance CoinBalance;
    private long BaseSwipeAmount = 1; // StartValues of 1 to have a start position
    private long Mult = 1;
    private long SwipeAmount;

    // Rotated Amount Settings
    [Header("Rotated Amount Settings")]
    [SerializeField] private float rotatedAmount;
    [SerializeField] private float rotatedAmountToReset;
    [SerializeField] private float resetRotatedAmount;

    // Rotation Speed Settings
    [Header("Rotation Speed Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float returnRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float addRotationSpeed;

    // Max Rotation Settings
    [Header("Max Rotation Settings")]
    [SerializeField] private float rotatedMaxCheck;
    [SerializeField] private float addMaxRotation;


    private float _coinGainCheck = 1f; // This is only checking if it has swiped or not, so it doesn't need to be heigher than 1

    // Private fields
    private bool _began;
    private bool _moved;
    private bool _end;
    private bool _hasRotated;

    /// <summary>
    /// Initializes the touch input simulation.
    /// </summary>
    void Start()
    {
        Input.simulateMouseWithTouches = true;
        SetBaseStats();
    }

    /// <summary>
    /// Handles touch input and updates the rotation and scoring of the game object.
    /// </summary>
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _began = true;
                _moved = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                _began = false;
                _moved = true;
                _end = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _began = false;
                _moved = false;
                _end = true;
            }
        }

        if (_moved)
        {
            _coinGainCheck++;
             
            if (moveSpeed <= maxRotationSpeed)
            {
                moveSpeed += addRotationSpeed;
            }

            if (rotatedAmount >= -rotatedMaxCheck)
            {
                rotatedAmount -= addMaxRotation;
            }
        }

        if (_end)
        {
            _hasRotated = false;
            
            rotatedAmount++;
            resetRotatedAmount = 0;
        }

        if (_end && !_moved && _coinGainCheck > 1)
        {
            CoinBalance.Swipe(SwipeAmount);  
            _coinGainCheck = 0;
        }

        if (_end && !_hasRotated)
        {
            Rotate();

            if (rotatedAmount >= rotatedMaxCheck)
            {
                rotatedAmount = 0;
                _hasRotated = true;
                _end = false;
            }
        }

        if (_hasRotated)
        {
            resetRotatedAmount++;
        }

        if (resetRotatedAmount >= rotatedAmountToReset)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * returnRotationSpeed);
            if (transform.rotation.eulerAngles.y == 0)
            {
                resetRotatedAmount = 0;
            }
        }
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
        print(SwipeAmount);
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
        if (CoinBalance.Buy(_Price))
        {
            BaseSwipeAmount += _Buff;
            SaveSwipe();
            return true;
        }
        return false;
    }

    public bool AddMult(long _Buff, long _Price)
    {
        if (CoinBalance.Buy(_Price))
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
        CoinBalance.SetReseter(_Reset);
    }

    /// <summary>
    /// Rotates the game object based on the current move speed.
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(Vector3.down, moveSpeed * Time.deltaTime);
    }
}
 