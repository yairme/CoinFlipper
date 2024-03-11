using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    //private Rigidbody rb;
    //private Quaternion zAxis;
    //private Vector3 force;
    //private float forceTotal;
    //private float factor = 100.0f;
    //private float startTime;
    //private Vector3 startPos;
    [SerializeField]private CoinBalance coin;
    private long SwipeAmount;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("swipeAmount")) { SwipeAmount = Convert.ToInt64(PlayerPrefs.GetString("swipeAmount")); } else { SwipeAmount = 1; }
    }
    public bool AddSwipeAmount(long buff, long price)
    {
        if (coin.Buy(price))
        {
            SwipeAmount += buff;
            print(SwipeAmount);
            SaveSwipe();
            return true;
        }
        return false;
    }
    //private void Update()
    //{
    //    zAxis = Quaternion.Euler(force * factor);
    //    rb.rotation = zAxis;
    //}

    private void SaveSwipe()
    {
        PlayerPrefs.SetString("swipeAmount", SwipeAmount.ToString());
    }

    //private void OnMouseDown() // phone uses the same principles;
    //{
    //    startTime = Time.time;
    //    startPos = Input.mousePosition;
    //    startPos.z = transform.position.z - Camera.main.transform.position.z;
    //    startPos = Camera.main.ScreenToWorldPoint(startPos);
    //}

    //private void OnMouseUp()
    //{
    //    Vector3 endPos = Input.mousePosition;
    //    endPos.z = transform.position.z - Camera.main.transform.position.z;
    //    endPos = Camera.main.ScreenToWorldPoint(endPos);

    //    force = endPos - startPos;
    //    force.z = force.magnitude;
    //    force /= (Time.time - startTime);
    //    forceTotal += force.magnitude; // Use force magnitude to check how many points we get. And just make the visuals clean.
    //    if (forceTotal > factor)
    //    {
    //        forceTotal -= factor;
    //        print(forceTotal);
    //        coin.Swipe(SwipeAmount);
    //    }   
    //}   
}
