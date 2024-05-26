using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Flame : MonoBehaviour
{

    public int playerNo;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lizard") ||
            other.gameObject.CompareTag("Bear") ||
            other.gameObject.CompareTag("Fox") ||
            other.gameObject.CompareTag("Rabbit"))
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo != playerNo)
            {
                other.gameObject.GetComponent<PlayerBase>().DamagePlayer(0.2f);
                Debug.Log("fire");
            }
        }
    }
}
