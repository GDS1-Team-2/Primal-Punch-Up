using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject target;

    private float speed = 15;
    private float rotateSpeed = 95;

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
    }

    

    private void FixedUpdate()
    {
        if (!start)
        {
            rb.velocity = thisPlayer.transform.forward * speed;
        }
        else
        {
            Debug.Log(target);
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
        
        
        

        //var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, target.transform.position));

        //PredictMovement(leadTimePercentage);

        //AddDeviation(leadTimePercentage);

        //RotateRocket();
    }

    private void PredictMovement(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, maxTimePrediction, leadTimePercentage);

        standardPrediction = target.GetComponent<Rigidbody>().position + target.GetComponent<Rigidbody>().velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * deviationAmount * leadTimePercentage;

        deviatedPrediction = standardPrediction + predictionOffset;
    }

    private void RotateRocket()
    {
        var heading = deviatedPrediction - transform.position;

        var rotation = Quaternion.LookRotation(heading);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        int carrotDamage = 20;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer.gameObject != thisPlayer && !other.isTrigger)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.transform.position);
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(standardPrediction, deviatedPrediction);
    }

}
