using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardSmoke : MonoBehaviour
{

    public PlayerBase thisPlayer;
    public SphereCollider smokeCollider;

    // Start is called before the first frame update
    void Start()
    {
        smokeCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SmokeCollider()
    {
        GameObject[] smokeBombs = GameObject.FindGameObjectsWithTag("Smoke Bomb");

        if (smokeBombs.Length == 1)
        {
            smokeCollider.enabled = true;
        }
        yield return new WaitForSeconds(0.25f);
        smokeCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        int smokeDamage = 10;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage(smokeDamage));
            otherPlayer.lizSmokeDmg = true;
        }
    }
}
