using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePunchCollision : MonoBehaviour
{

    public PlayerBase thisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int firePunchDamage = 20;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage(firePunchDamage));
        }
    }
}
