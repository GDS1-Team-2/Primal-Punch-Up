using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotDagger : MonoBehaviour
{

    public Rigidbody carrotBody;
    public float throwSpeed = 40.0f;
    private Vector3 startPos;
    private float distance;
    private Vector3 currentPos;
    public float throwDistance = 40.0f;
    public PlayerBase thisPlayer;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        carrotBody = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        carrotBody.velocity = transform.forward * throwSpeed;
        currentPos = transform.position;
        distance = Vector3.Distance(currentPos, startPos);

        if (!hit && distance > throwDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int carrotDamage = 20;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(HitTarget(otherPlayer, carrotDamage));
        }
    }

    IEnumerator HitTarget(PlayerBase otherPlayer, int damage)
    {
        hit = true;
        yield return StartCoroutine(otherPlayer.TakeDamage(damage));
        Destroy(gameObject);
    }
}
