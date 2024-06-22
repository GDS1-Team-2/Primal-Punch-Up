using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplosion : MonoBehaviour
{

    private float damageCurrent = 0.0f;
    SphereCollider sphereCol;

    // Start is called before the first frame update
    void Start()
    {
        sphereCol = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        damageCurrent += Time.deltaTime;
        if (damageCurrent >= 0.3)
        {
            sphereCol.enabled = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Lizard" || collision.tag == "Bear" || collision.tag == "Rabbit" || collision.tag == "Fox")
        {
            int BADamage = 50;
            PlayerBase otherPlayer = collision.gameObject.GetComponent<PlayerBase>();

            if (otherPlayer != null && !collision.isTrigger)
            {
                StartCoroutine(otherPlayer.TakeDamage(BADamage));
            }
        }
    }
}
