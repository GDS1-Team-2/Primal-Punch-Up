using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject target;

    private float speed = 15;
    private float rotateSpeed = 500;

    private float maxDistancePredict = 100;
    private float minDistancePredict = 5;
    private float maxTimePrediction = 5;
    private Vector3 standardPrediction, deviatedPrediction;

    private float deviationAmount = 50;
    private float deviationSpeed = 2;

    public GameObject thisPlayer;
    private bool hit = false;
    private bool start = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //target = GetClosestPlayer();
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);
        start = true;
        rb.velocity = Vector3.zero;
    }

    

    private void FixedUpdate()
    {
        if (!start)
        {
            rb.velocity = thisPlayer.transform.forward * speed;
            Vector3 direction = thisPlayer.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
        }
        else
        {
            //Debug.Log(target);
            var step = speed * Time.fixedDeltaTime; // calculate distance to move
            Vector3 targetPos = new Vector3(target.transform.position.x, 2, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

            Vector3 direction = targetPos - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = 15;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer.gameObject != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(HitTarget(otherPlayer, damage));
        }
    }

    IEnumerator HitTarget(PlayerBase otherPlayer, int damage)
    {
        hit = true;
        yield return StartCoroutine(otherPlayer.TakeDamage(damage));
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.transform.position);
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(standardPrediction, deviatedPrediction);
    }

}
