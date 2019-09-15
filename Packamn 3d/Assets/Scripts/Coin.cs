using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    public static event Action<float> SendCoinCount;

    private void OnTriggerEnter(Collider other)           //Taking Coin Count as 1 , sendingMessage and then destroying the coin
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (SendCoinCount != null)
            {
                SendCoinCount(1);
                Destroy(gameObject);
            }
        }
    }
}
