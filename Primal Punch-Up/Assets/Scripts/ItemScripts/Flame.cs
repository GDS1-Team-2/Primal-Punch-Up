using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Flame : MonoBehaviour
{

    private List<string> playerTags = new List<string> { "Lizard", "Bear", "Rabbit", "Fox" };

    void OnTriggerEnter(Collider other)
    {
        if (playerTags.Contains(other.gameObject.tag))
        {
            PlayerBase player = other.GetComponent<PlayerBase>();
            if (player != null)
            {
                player.TakeDamage(5); 
            }
        }
    }
}
